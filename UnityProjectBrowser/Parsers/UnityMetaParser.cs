// Copyright(c) 2018 Brian MacIntosh
// MIT License

using System;
using System.IO;
using YamlDotNet.RepresentationModel;

namespace ProjectBrowser
{
	public class UnityMetaParser : BaseParser
	{
		static UnityMetaParser()
		{
			// add a dummy object for "null"
			string emptyGuid = Guid.Empty.ToString();
			ObjectDatabase.AddObject(emptyGuid, new NullObject(emptyGuid));
		}

		/// <summary>
		/// Parses the specified file.
		/// </summary>
		public override void ParseFile(string absolutePath)
		{
			using (StreamReader fileReader = new StreamReader(new FileStream(absolutePath, FileMode.Open, FileAccess.Read)))
			{
				YamlStream stream = new YamlStream();
				stream.Load(fileReader);
				YamlDocument document = stream.Documents[0];
				YamlMappingNode rootNode = (YamlMappingNode)document.RootNode;

				YamlScalarNode guidNode = (YamlScalarNode)rootNode.Children["guid"];
				Guid assetGuid = new Guid(guidNode.Value);
				string assetUniqueId = assetGuid.ToString();

				// determine the asset's default file id
				int defaultFileId = 0;
				foreach (string key in rootNode.Children.Keys)
				{
					if (key.EndsWith("Importer", StringComparison.OrdinalIgnoreCase))
					{
						int classId;
						if (UnityClassIds.TryGetClassIdByImporterName(key, out classId))
						{
							defaultFileId = classId * 100000;
							break;
						}
					}
				}
				UnityObjectKey defaultAssetKey = new UnityObjectKey(assetGuid, defaultFileId);

				bool isFolderAsset = false;
				YamlNode folderAssetNode;
				if (rootNode.Children.TryGetValue("folderAsset", out folderAssetNode))
				{
					YamlScalarNode folderAssetScalarNode = (YamlScalarNode)folderAssetNode;
					isFolderAsset = folderAssetScalarNode.Value == "yes";
				}
				else
				{
					// seems folder assets may randomly not have a folderAsset value
					// try to identify them by the absence of other data
					if (rootNode.Children.Count == 2 && Directory.Exists(UnityFolder.GetFolderPathForMeta(absolutePath)))
					{
						isFolderAsset = true;
					}
				}

				if (isFolderAsset)
				{
					string folderPath = Path.Combine(Path.GetDirectoryName(absolutePath), Path.GetFileNameWithoutExtension(absolutePath));
					ObjectDatabase.MapFolderObject(folderPath, assetUniqueId);
					ObjectDatabase.AddObject(assetUniqueId, new UnityFolder(absolutePath, assetGuid));
				}
				else
				{
					// metas shouldn't override assets we've already parsed
					ObjectDatabase.AddPlaceholderObject(assetUniqueId, new UnityAsset(absolutePath, assetUniqueId));
					if (defaultAssetKey.FileId != 0)
					{
						ObjectDatabase.AddObject(defaultAssetKey.ToString(), new UnityDefaultAsset(assetUniqueId, defaultAssetKey));
					}
				}

				// check for sprites
				YamlNode textureImporterNode;
				if (rootNode.Children.TryGetValue("TextureImporter", out textureImporterNode))
				{
					YamlMappingNode textureImporterMappingNode = (YamlMappingNode)textureImporterNode;
					YamlNode fileIDToRecycleNameNode;
					if (textureImporterMappingNode.Children.TryGetValue("fileIDToRecycleName", out fileIDToRecycleNameNode))
					{
						YamlMappingNode fileIDToRecycleNameMappingNode = (YamlMappingNode)fileIDToRecycleNameNode;
						foreach (var kv in fileIDToRecycleNameMappingNode.Children)
						{
							string fileIdStr = ((YamlScalarNode)kv.Key).Value;
							string name = ((YamlScalarNode)kv.Value).Value;
							long fileId = long.Parse(fileIdStr);
							UnityObjectKey spriteKey = new UnityObjectKey(assetGuid, fileId);
							ObjectDatabase.AddObject(spriteKey.ToString(), new UnitySprite(assetUniqueId, name, spriteKey));
						}
					}

					YamlNode spriteModeNode;
					if (textureImporterMappingNode.Children.TryGetValue("spriteMode", out spriteModeNode))
					{
						YamlScalarNode spriteModeScalarNode = (YamlScalarNode)spriteModeNode;
						if (spriteModeScalarNode.Value == "1")
						{
							// single sprite
							string name = Path.GetFileNameWithoutExtension(absolutePath);
							UnityObjectKey spriteKey = new UnityObjectKey(assetGuid, 21300000);
							ObjectDatabase.AddObject(spriteKey.ToString(), new UnitySprite(assetUniqueId, name, spriteKey));
						}
					}
				}
			}
		}

		/// <summary>
		/// Reads the asset guid from the specified meta file.
		/// </summary>
		public static Guid ReadGuidFromMeta(string metaFile)
		{
			using (StreamReader fileReader = new StreamReader(new FileStream(metaFile, FileMode.Open, FileAccess.Read)))
			{
				YamlStream stream = new YamlStream();
				stream.Load(fileReader);
				YamlDocument document = stream.Documents[0];
				YamlMappingNode rootNode = (YamlMappingNode)document.RootNode;

				YamlScalarNode guidNode = (YamlScalarNode)rootNode.Children["guid"];
				return new Guid(guidNode.Value);
			}
		}
	}
}
