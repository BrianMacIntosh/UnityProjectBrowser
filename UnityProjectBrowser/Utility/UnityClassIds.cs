// Copyright(c) 2018 Brian MacIntosh
// MIT License

using System.Collections.Generic;

namespace ProjectBrowser
{
	public static class UnityClassIds
	{
		/// <summary>
		/// Dictionary mapping asset importer names to the class id of the ASSET.
		/// </summary>
		private static Dictionary<string, int> s_importerNameToClassId = new Dictionary<string, int>()
		{
			{ "TextureImporter", 28 },
			{ "AudioImporter", 83 },
			{ "MonoImporter" , 115 },
			{ "TrueTypeFontImporter", 128 },
		};

		public static bool TryGetClassIdByImporterName(string importerName, out int classId)
		{
			return s_importerNameToClassId.TryGetValue(importerName, out classId);
		}
	}
}
