// Copyright(c) 2018 Brian MacIntosh
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBrowser
{
	/// <summary>
	/// Represents one file that's part of the project.
	/// </summary>
	public class ProjectFile
	{
		/// <summary>
		/// The absolute path to the file.
		/// </summary>
		public string AbsolutePath { get; private set; }

		/// <summary>
		/// The last time this file was read from disk.
		/// </summary>
		public DateTime LastReadTime { get; private set; }

		public ProjectFile(string absolutePath)
		{

		}
	}
}
