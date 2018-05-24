// Copyright(c) 2018 Brian MacIntosh
// MIT License

namespace ProjectBrowser
{
	/// <summary>
	/// Represents the contents of a single-asset file (such as a font or a texture).
	/// </summary>
	public class UnityDefaultAsset : UnityObject
	{
		/// <summary>
		/// 
		/// </summary>
		public UnityDefaultAsset(string documentId, UnityObjectKey key)
			: base(documentId, null, key)
		{
			
		}

		public override string GetIconKey()
		{
			return "DefaultAsset_Icon";
		}

		public override string ToString()
		{
			if (ObjectDatabase.TryGetObject(DocumentId, out ProjectObject documentAsset))
			{
				return documentAsset.ToString();
			}
			else
			{
				return DocumentId;
			}
		}
	}
}
