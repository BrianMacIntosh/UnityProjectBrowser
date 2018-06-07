// Copyright(c) 2018 Brian MacIntosh
// MIT License

using System;
using YamlDotNet.RepresentationModel;

namespace ProjectBrowser
{
	public abstract class UnityObject : ProjectObject
	{
		/// <summary>
		/// The unique ID of the document this object is saved in.
		/// </summary>
		public readonly string DocumentId;

		/// <summary>
		/// Reads a <see cref="UnityGameObject"/> from the specified Yaml node.
		/// </summary>
		public UnityObject(string documentId, YamlDocument yaml, UnityObjectKey key)
			: base(key)
		{
			// add to document
			DocumentId = documentId.ToString();
			AddRelationship(DocumentId, "is-in-document", "document-contains-object");

			if (yaml != null)
			{
				YamlMappingNode rootNode = (YamlMappingNode)yaml.RootNode;

				// create prefab link
				YamlNode prefabNode;
				if (rootNode.Children.TryGetValue("m_PrefabParentObject", out prefabNode))
				{
					YamlMappingNode prefabMappingNode = (YamlMappingNode)prefabNode;
					UnityObjectKey reference = ParseReference(prefabMappingNode, key);
					AddRelationship(reference, "is-instance-of-prefab", "has-instance");
				}
			}
		}

		public override string GetParentId()
		{
			return DocumentId;
		}

		/// <summary>
		/// Gets the absolute path of the file this object belongs to.
		/// </summary>
		public override string GetFilePath()
		{
			ProjectObject document;
			if (ObjectDatabase.TryGetObject(DocumentId, out document))
			{
				return document.GetFilePath();
			}
			else
			{
				return "";
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="yamlNode"></param>
		/// <param name="fileGuid">The guid of the file containing this element.</param>
		/// <param name="fileId"></param>
		/// <param name="guid"></param>
		protected UnityObjectKey ParseReference(YamlMappingNode yamlNode, UnityObjectKey parentKey)
		{
			long fileId = 0;
			Guid guid = Guid.Empty;

			YamlNode fileIdNode;
			if (yamlNode.Children.TryGetValue("fileID", out fileIdNode))
			{
				YamlScalarNode fileIdScalarNode = (YamlScalarNode)fileIdNode;
				string fileIdStr = fileIdScalarNode.Value;
				fileId = long.Parse(fileIdStr);

				if (fileId != 0)
				{
					// guid defaults to this file
					guid = parentKey.AssetGuid;
				}
			}

			YamlNode guidNode;
			if (yamlNode.Children.TryGetValue("guid", out guidNode))
			{
				YamlScalarNode guidScalarNode = (YamlScalarNode)guidNode;
				string guidStr = guidScalarNode.Value;
				guid = new Guid(guidStr);
			}

			return new UnityObjectKey(guid, fileId);
		}
	}
}
