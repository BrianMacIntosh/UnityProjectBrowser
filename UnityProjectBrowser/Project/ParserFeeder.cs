// Copyright(c) 2018 Brian MacIntosh
// MIT License

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace ProjectBrowser
{
	/// <summary>
	/// Feeds new and changed files to the associated parser.
	/// </summary>
	public class ParserFeeder<T> : IDisposable, IParserFeeder where T : BaseParser
	{
		private List<FileSystemWatcher> m_fileWatchers = new List<FileSystemWatcher>();
		
		/// <summary>
		/// The absolute root directory this feeder is monitoring.
		/// </summary>
		public string AbsoluteRootDirectory { get; private set; }

		/// <summary>
		/// Array of Windows file filters this feeder collects.
		/// </summary>
		private readonly string[] m_fileFilters;

		/// <summary>
		/// Returns the type of parser this object feeds.
		/// </summary>
		public Type GetParserType()
		{
			return typeof(T);
		}

		/// <summary>
		/// Constructs a new <see cref="ParserFeeder"/>.
		/// </summary>
		public ParserFeeder(string absoluteRootDirectory, string[] fileFilters)
		{
			AbsoluteRootDirectory = absoluteRootDirectory;
			m_fileFilters = fileFilters;

			foreach (string fileFilter in m_fileFilters)
			{
				FileSystemWatcher fileWatcher = new FileSystemWatcher(AbsoluteRootDirectory);
				fileWatcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastWrite;
				fileWatcher.Filter = fileFilter;
				fileWatcher.IncludeSubdirectories = true;
				fileWatcher.Created += OnFileCreated;
				fileWatcher.Changed += OnFileChanged;
				fileWatcher.EnableRaisingEvents = true;
				m_fileWatchers.Add(fileWatcher);
			}

			Thread collectFiles = new Thread(CollectFilesInitial);
			collectFiles.Start();
		}

		/// <summary>
		/// Stops the feeder.
		/// </summary>
		public void Stop()
		{
			foreach (FileSystemWatcher watcher in m_fileWatchers)
			{
				watcher.Dispose();
			}
			m_fileWatchers.Clear();
		}

		private void CollectFilesInitial()
		{
			foreach (string fileFilter in m_fileFilters)
			{
				foreach (string absolutePath in Directory.GetFiles(AbsoluteRootDirectory, fileFilter, SearchOption.AllDirectories))
				{
					FileUpdater.AddFileToParse<T>(absolutePath);
				}
			}
		}

		private void OnFileChanged(object sender, FileSystemEventArgs e)
		{
			FileUpdater.AddFileToParse<T>(e.FullPath);
		}

		private void OnFileCreated(object sender, FileSystemEventArgs e)
		{
			FileUpdater.AddFileToParse<T>(e.FullPath);
		}

		#region IDisposable Support

		protected virtual void Dispose(bool disposing)
		{
			foreach (FileSystemWatcher watcher in m_fileWatchers)
			{
				watcher.Dispose();
			}
			m_fileWatchers.Clear();
		}

		~ParserFeeder()
		{
			Dispose(false);
		}
		
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		#endregion
	}
}
