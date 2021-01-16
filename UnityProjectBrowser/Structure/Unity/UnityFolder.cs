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
				return GetFolderPathForMeta(filePath);
			}
		}

		/// <summary>
		/// Returns the directory path for the specified folder meta.
		/// </summary>
		public static string GetFolderPathForMeta(string folderMeta)
		{
			return Path.Combine(Path.GetDirectoryName(folderMeta), Path.GetFileNameWithoutExtension(folderMeta));
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
