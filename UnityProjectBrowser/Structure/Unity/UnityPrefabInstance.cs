using System;
using System.Linq;
using YamlDotNet.RepresentationModel;

namespace ProjectBrowser
{
	public class UnityPrefabInstance : UnityDefaultAsset
	{
		/// <summary>
		/// The name of the <see cref="UnityPrefabInstance"/>.
		/// </summary>
		public readonly string Name;
		
		/// <summary>
		/// Reads a <see cref="UnityPrefabInstance"/> from the specified Yaml node.
		/// </summary>
		public UnityPrefabInstance(string documentId, YamlDocument yaml, UnityObjectKey key)
			: base(documentId, key)
		{
			YamlMappingNode rootNode = (YamlMappingNode)yaml.RootNode;
			YamlMappingNode objectNode = (YamlMappingNode)rootNode.Children.First().Value;
			
			// read parent prefab
			YamlNode prefabNode;
			if (objectNode.Children.TryGetValue("m_ParentPrefab", out prefabNode))
			{
				YamlMappingNode prefabMappingNode = (YamlMappingNode)prefabNode;
				UnityObjectKey reference = ParseReference(prefabMappingNode, key);
				AddRelationship(reference, "is-instance-of-prefab", "has-prefab-instance");
			}

			// read instance modifications
			YamlMappingNode modificationNode = (YamlMappingNode)objectNode.Children["m_Modification"];
			YamlSequenceNode modifications = ((YamlSequenceNode)modificationNode.Children["m_Modifications"]);
			foreach (YamlMappingNode listNode in modifications.Children)
			{
				UnityObjectKey reference;

				YamlScalarNode propertyPathNode = (YamlScalarNode)listNode.Children["propertyPath"];
				YamlScalarNode valueNode = (YamlScalarNode)listNode.Children["value"];

				// save overridden GameObject name
				if (propertyPathNode.Value == "m_Name")
				{
					Name = valueNode.Value;
				}

				YamlMappingNode targetNode = (YamlMappingNode)listNode.Children["target"];
				reference = ParseReference(targetNode, key);
				AddRelationship(reference, "modifies-member", "modified-by-instance");

				YamlMappingNode objectReferenceNode = (YamlMappingNode)listNode.Children["objectReference"];
				reference = ParseReference(objectReferenceNode, key);
				if (!reference.IsEmpty)
				{
					AddRelationship(reference, "has-reference-to", "is-referenced-by");
				}
			}
		}
	}
}
