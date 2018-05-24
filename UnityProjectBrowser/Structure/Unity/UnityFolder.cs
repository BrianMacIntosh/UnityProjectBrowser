// Copyright(c) 2018 Brian MacIntosh
// MIT License

using System;
using System.IO;

namespace ProjectBrowser
{
	public class UnityFolder : UnityAsset
	{
		/// <summary>
		/// Returns the absolute path to this folder.
		/// </summary>
		public string FolderPath
		{
			get
			{
				string filePath = GetFilePath();
				return Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath));
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public UnityFolder(string path, Guid guid)
			: base(path, guid.ToString())
		{
			
		}

		public override string GetIconKey()
		{
			return "Folder_Icon";
		}
	}
}
