// Copyright(c) 2018 Brian MacIntosh
// MIT License

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ProjectBrowser
{
	/// <summary>
	/// Tracks recently-opened filenames.
	/// </summary>
	public class RecentlyOpenedList
	{
		public string Key { get; private set; }

		/// <summary>
		/// The maximum number of entries that can be in the list.
		/// </summary>
		public int MaxSize { get; private set; }

		private ToolStripMenuItem m_menuParent;

		private List<string> m_recentFiles;

		/// <summary>
		/// Event called when a recent item is selected from the menu.
		/// </summary>
		public event Action<object, string> OnItemSelected;

		public RecentlyOpenedList(string key, int maxSize, ToolStripMenuItem menuParent)
		{
			Key = key;
			MaxSize = maxSize;

			m_menuParent = menuParent;
			m_recentFiles = new List<string>(MaxSize);

			Load();
		}

		/// <summary>
		/// Loads the history from the cache file.
		/// </summary>
		private void Load()
		{
			string cacheFilePath = GetCachePath();
			if (File.Exists(cacheFilePath))
			{
				foreach (string file in File.ReadAllLines(cacheFilePath))
				{
					if (string.IsNullOrEmpty(file))
					{
						continue;
					}
					if (m_recentFiles.Count >= m_recentFiles.Capacity)
					{
						break;
					}
					m_recentFiles.Add(file);
				}
			}
			RebuildChildren();
		}

		/// <summary>
		/// Saves the history to disk.
		/// </summary>
		private void Save()
		{
			string cacheFilePath = GetCachePath();
			string cacheFileDirectory = Path.GetDirectoryName(cacheFilePath);
			if (!Directory.Exists(cacheFileDirectory))
			{
				Directory.CreateDirectory(cacheFileDirectory);
			}
			using (StreamWriter writer = new StreamWriter(new FileStream(cacheFilePath, FileMode.Create, FileAccess.Write)))
			{
				foreach (string file in m_recentFiles)
				{
					writer.WriteLine(file);
				}
			}
		}

		/// <summary>
		/// Marks the specified file as acccessed.
		/// </summary>
		public void Access(string file)
		{
			for (int i = 0; i < m_recentFiles.Count; i++)
			{
				//TODO: file path equality
				if (string.Equals(m_recentFiles[i], file, StringComparison.OrdinalIgnoreCase))
				{
					m_recentFiles.RemoveAt(i);
					m_recentFiles.Insert(0, file);
					Save();
					return;
				}
			}
			
			// new file
			if (m_recentFiles.Count >= m_recentFiles.Capacity)
			{
				m_recentFiles.RemoveAt(m_recentFiles.Count - 1);
			}
			m_recentFiles.Insert(0, file);
			Save();

			RebuildChildren();
		}

		/// <summary>
		/// Clears the recent file history.
		/// </summary>
		public void Clear()
		{
			if (m_recentFiles.Count > 0)
			{
				m_recentFiles.Clear();
				Save();
			}
			RebuildChildren();
		}

		private void RebuildChildren()
		{
			// clear dropdown
			m_menuParent.DropDownItems.Clear();

			if (m_recentFiles.Count == 0)
			{
				m_menuParent.Enabled = false;
				return;
			}
			else
			{
				m_menuParent.Enabled = true;
			}

			foreach (string file in m_recentFiles)
			{
				ToolStripMenuItem recentFileMenuItem = new ToolStripMenuItem()
				{
					Text = file
				};
				recentFileMenuItem.Click += OnToolStripItemClicked;
				m_menuParent.DropDownItems.Add(recentFileMenuItem);
			}

			m_menuParent.DropDownItems.Add(new ToolStripSeparator());

			ToolStripMenuItem clearMenuItem = new ToolStripMenuItem()
			{
				Name = "clearRecentFilesMenuItem",
				Text = "Clear"
			};
			clearMenuItem.Click += OnClearClicked;
			m_menuParent.DropDownItems.Add(clearMenuItem);
		}

		private void OnToolStripItemClicked(object sender, EventArgs e)
		{
			string path = ((ToolStripMenuItem)sender).Text;
			Access(path);
			OnItemSelected(this, path);
		}

		private void OnClearClicked(object sender, EventArgs e)
		{
			Clear();
		}

		private string GetCachePath()
		{
			return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UnityProjectBrowser", "recent_" + Key + ".txt");
		}
	}
}
