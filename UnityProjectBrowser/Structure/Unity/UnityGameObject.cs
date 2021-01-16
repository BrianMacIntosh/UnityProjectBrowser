// Copyright(c) 2018 Brian MacIntosh
// MIT License

using System.Linq;
using YamlDotNet.RepresentationModel;

namespace ProjectBrowser
{
	public class UnityGameObject : UnityObject
	{
		/// <summary>
		/// The name of the <see cref="UnityGameObject"/>.
		/// </summary>
		public string Name
		{
			get; private set;
		}

		/// <summary>
		/// Reads a <see cref="UnityGameObject"/> from the specified Yaml node.
		/// </summary>
		public UnityGameObject(string documentId, YamlDocument yaml, UnityObjectKey key)
			: base(documentId, yaml, key)
		{
			
		}

		protected override void Parse(YamlDocument yaml, UnityObjectKey key)
		{
			base.Parse(yaml, key);

			YamlMappingNode rootNode = (YamlMappingNode)yaml.RootNode;
			YamlMappingNode objectNode = (YamlMappingNode)rootNode.Children.First().Value;

			// read object name
			Name = ((YamlScalarNode)objectNode.Children["m_Name"]).Value;

			// read object component links
			YamlSequenceNode components = ((YamlSequenceNode)objectNode.Children["m_Component"]);
			foreach (YamlMappingNode listNode in components.Children)
			{
				YamlMappingNode component = listNode.Children["component"] as YamlMappingNode;
				UnityObjectKey reference = ParseReference(component, key);
				AddRelationship(reference, "has-component", "");
			}
		}

		public override string GetIconKey()
		{
			return "GameObject_Icon";
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
