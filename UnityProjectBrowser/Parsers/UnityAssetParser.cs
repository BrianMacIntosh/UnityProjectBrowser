using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using YamlDotNet.RepresentationModel;

namespace ProjectBrowser
{
	public class UnityAssetParser : BaseParser
	{
		/// <summary>
		/// Parses the specified file.
		/// </summary>
		public override void ParseFile(string absolutePath)
		{
			Guid guid = Guid.Empty;

			if (Path.GetExtension(absolutePath) != ".meta")
			{
				// find the meta file
				string metaFilePath = absolutePath + ".meta";

				if (File.Exists(metaFilePath))
				{
					guid = UnityMetaParser.ReadGuidFromMeta(metaFilePath);
				}
			}

			// create a project object for the document
			string uniqueId = guid != Guid.Empty ? guid.ToString() : absolutePath;
			ObjectDatabase.AddObject(uniqueId, new UnityAsset(absolutePath, uniqueId));

			using (StreamReader fileReader = new StreamReader(new FileStream(absolutePath, FileMode.Open, FileAccess.Read)))
			{
				YamlStream stream = new YamlStream();
				stream.Load(new UnityTextReader(fileReader));

				// each document is one UnityEngine.Object
				foreach (YamlDocument document in stream.Documents)
				{
					YamlMappingNode documentNode = (YamlMappingNode)document.RootNode;
					long fileId = long.Parse(documentNode.Anchor);
					UnityObjectKey key = new UnityObjectKey(guid, fileId);
					KeyValuePair<YamlNode, YamlNode> rootNode = documentNode.Children.First();
					YamlScalarNode rootNodeScalar = (YamlScalarNode)rootNode.Key;
					if (rootNodeScalar.Value == "GameObject")
					{
						ObjectDatabase.AddObject(key, new UnityGameObject(uniqueId, document, key));
					}
					else if (rootNodeScalar.Value == "Prefab")
					{
						ObjectDatabase.AddObject(key, new UnityPrefabInstance(uniqueId, document, key));
					}
					else
					{
						ObjectDatabase.AddObject(key, new UnityComponent(uniqueId, document, key));
					}
				}
			}
		}
	}
}
