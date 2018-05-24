// Copyright(c) 2018 Brian MacIntosh
// MIT License

using System.Collections;
using System.Globalization;
using System.Resources;
using System.Windows.Forms;

namespace ProjectBrowser
{
	public static class IconImages
	{
		private static ImageList s_iconList;

		public static ImageList ImageList
		{
			get
			{
				if (s_iconList == null)
				{
					s_iconList = new ImageList();
					ResourceSet resourceSet = UnityProjectBrowser.Properties.Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
					foreach (DictionaryEntry entry in resourceSet)
					{
						string key = entry.Key.ToString();
						if (key.EndsWith("_Icon"))
						{
							s_iconList.Images.Add(key, (System.Drawing.Bitmap)entry.Value);
						}
					}
				}
				return s_iconList;
			}
		}

		public static int GetImageIndex(string key)
		{
			return s_iconList.Images.IndexOfKey(key);
		}
	}
}
