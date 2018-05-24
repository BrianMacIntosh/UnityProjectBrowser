// Copyright(c) 2018 Brian MacIntosh
// MIT License

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using YamlDotNet.RepresentationModel;

namespace ProjectBrowser
{
	/// <summary>
	/// Handles loading and updating of project files.
	/// </summary>
	public static class FileUpdater
	{
		/// <summary>
		/// List of all available feeders, by parser type.
		/// </summary>
		private static List<IParserFeeder> s_feeders = new List<IParserFeeder>();

		/// <summary>
		/// List of all active parsers by type.
		/// </summary>
		private static Dictionary<Type, IParserPool> s_parsers = new Dictionary<Type, IParserPool>();

		/// <summary>
		/// Lists of files to parse, indexed by parser type.
		/// </summary>
		private static Dictionary<Type, ConcurrentQueue<string>> s_queuedFiles = new Dictionary<Type, ConcurrentQueue<string>>();

		/// <summary>
		/// The root directory of the loaded project.
		/// </summary>
		public static string ProjectPath { get; private set; }

		/// <summary>
		/// Returns true if the specified directory is a Unity project;
		/// </summary>
		public static bool IsUnityProject(string directory)
		{
			string assetsDirectory = Path.Combine(directory, "Assets");
			string projectSettingsDirectory = Path.Combine(directory, "ProjectSettings");
			return Directory.Exists(assetsDirectory) && Directory.Exists(projectSettingsDirectory);
		}

		/// <summary>
		/// Sets the root directory of the project to load.
		/// </summary>
		public static void SetProjectPath(string projectPath)
		{
			//TODO: path compare
			if (string.Equals(ProjectPath, projectPath, StringComparison.OrdinalIgnoreCase))
			{
				return;
			}

			// check that the target path is a Unity project
			if (!IsUnityProject(projectPath))
			{
				throw new NotAUnityProjectException(projectPath);
			}

			// check that assets are text serialized
			{
				string projectSettings = Path.Combine(projectPath, "ProjectSettings", "ProjectSettings.asset");
				using (StreamReader fileReader = new StreamReader(new FileStream(projectSettings, FileMode.Open, FileAccess.Read)))
				{
					try
					{
						YamlStream stream = new YamlStream();
						stream.Load(new UnityTextReader(fileReader));
					}
					catch (YamlDotNet.Core.SyntaxErrorException)
					{
						throw new UnityProjectNotTextException(projectPath);
					}
				}
			}

			// kill feeders
			foreach (IParserFeeder feeder in s_feeders)
			{
				feeder.Dispose();
			}
			s_feeders.Clear();

			// empty queue
			s_queuedFiles.Clear();

			// stop parsers
			foreach (IParserPool parserPool in s_parsers.Values)
			{
				parserPool.Stop();
			}

			// wait for all parsers to stop
			bool isParserActive = false;
			do
			{
				if (isParserActive)
				{
					Thread.Sleep(100);
				}
				isParserActive = false;
				foreach (IParserPool parserPool in s_parsers.Values)
				{
					if (parserPool.GetOutstandingFileCount() > 0)
					{
						isParserActive = true;
						break;
					}
				}
			} while (isParserActive);

			// unload database
			ObjectDatabase.Clear();

			ProjectPath = projectPath;

			// start up new feeders
			AddFeederType<UnityAssetParser>(
				Path.Combine(ProjectPath, "Assets"),
				new string[] { "*.unity", "*.prefab", "*.asset", "*.anim", "*.controller", "*.overridecontroller", "*.mat" });
			AddFeederType<UnityAssetParser>(
				Path.Combine(ProjectPath, "ProjectSettings"),
				new string[] { "*.asset" });
			AddFeederType<UnityMetaParser>(
				Path.Combine(ProjectPath, "Assets"),
				new string[] { "*.meta" });

			// start up new parsers
			AddParserType<UnityAssetParser>(4);
			AddParserType<UnityMetaParser>(4);

			//TODO: detect if there are no parsers for added feeders
		}

		/// <summary>
		/// Returns the total number of outstanding files that need to be parsed.
		/// </summary>
		public static int GetOutstandingFileCount()
		{
			int count = 0;
			foreach (ConcurrentQueue<string> queue in s_queuedFiles.Values)
			{
				count += queue.Count;
			}
			foreach (IParserPool pool in s_parsers.Values)
			{
				count += pool.GetOutstandingFileCount();
			}
			return count;
		}

		/// <summary>
		/// Registers a new feeder that feeds project files to parsers.
		/// </summary>
		public static void AddFeederType<T>(string absoluteRootDirectory, string[] fileFilters)
			where T : BaseParser
		{
			if (!s_queuedFiles.ContainsKey(typeof(T)))
			{
				s_queuedFiles.Add(typeof(T), new ConcurrentQueue<string>());
			}

			s_feeders.Add(new ParserFeeder<T>(absoluteRootDirectory, fileFilters));
		}

		/// <summary>
		/// Adds the specified number of threads for the specified parser type.
		/// </summary>
		public static void AddParserType<T>(int threads)
			where T : BaseParser
		{
			IParserPool parserPool;
			if (!s_parsers.TryGetValue(typeof(T), out parserPool))
			{
				parserPool = Activator.CreateInstance<ParserPool<T>>();
				s_parsers.Add(typeof(T), parserPool);
			}
			parserPool.AddThreads(threads);
		}

		/// <summary>
		/// Queues a file to be parsed by a parser of the specified type.
		/// </summary>
		public static void AddFileToParse<T>(string absolutePath)
			where T : BaseParser
		{
			ConcurrentQueue<string> queue;
			if (s_queuedFiles.TryGetValue(typeof(T), out queue))
			{
				queue.Enqueue(absolutePath);
			}
			else
			{
				throw new InvalidOperationException("No queue for parser of type '" + typeof(T).Name + "'.");
			}
		}

		/// <summary>
		/// Stops all threads.
		/// </summary>
		public static void Stop()
		{
			foreach (IParserPool parser in s_parsers.Values)
			{
				parser.Stop();
			}

			foreach (IParserFeeder feeder in s_feeders)
			{
				feeder.Stop();
			}
			s_feeders.Clear();
		}

		/// <summary>
		/// Dequeues a file for a parser of the specified type.
		/// Blocks until a file is available.
		/// </summary>
		public static string GetNextFile<T>()
		{
			ConcurrentQueue<string> queue;
			if (s_queuedFiles.TryGetValue(typeof(T), out queue))
			{
				string filename;
				while (true) //TODO: do not spin
				{
					if (queue.TryDequeue(out filename))
					{
						return filename;
					}
					Thread.Sleep(2000);
				}
			}
			else
			{
				// no queue for parsers of this type
				return "";
			}
		}
	}
}
