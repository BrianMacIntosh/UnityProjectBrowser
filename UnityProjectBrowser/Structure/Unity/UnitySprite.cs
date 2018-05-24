using System;

namespace ProjectBrowser
{
	public class UnitySprite : UnityObject
	{
		public readonly string Name;

		/// <summary>
		/// 
		/// </summary>
		public UnitySprite(string documentId, string name, UnityObjectKey key)
			: base(documentId, null, key)
		{
			Name = name;
		}

		public override string GetIconKey()
		{
			return "Sprite_Icon";
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
