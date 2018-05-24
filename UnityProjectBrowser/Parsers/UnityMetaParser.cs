// Copyright(c) 2018 Brian MacIntosh
// MIT License

using System;
using System.IO;
using YamlDotNet.RepresentationModel;

namespace ProjectBrowser
{
	public class UnityMetaParser : BaseParser
	{
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
						if (UnityClassIds.TryGetClassIdByImporterName(key, out int classId))
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
					if (textureImporterMappingNode.Children.TryGetValue("fileIDToRecycleName", out YamlNode fileIDToRecycleNameNode))
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

					if (textureImporterMappingNode.Children.TryGetValue("spriteMode", out YamlNode spriteModeNode))
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
