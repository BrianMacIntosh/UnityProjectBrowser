// Copyright(c) 2018 Brian MacIntosh
// MIT License

using System;
using System.Collections.Generic;
using System.Threading;

namespace ProjectBrowser
{
	/// <summary>
	/// Object managing a pool of <see cref="BaseParser"/>s of a specific type.
	/// </summary>
	public class ParserPool<T> : IParserPool where T : BaseParser
	{
		private List<Thread> m_threads;

		/// <summary>
		/// The number of threads currently working on parsing.
		/// </summary>
		private int m_threadsWorking = 0;

		public ParserPool()
		{
			m_threads = new List<Thread>();
		}

		/// <summary>
		/// Creates the specified number of parser threads of this type.
		/// </summary>
		/// <param name="threadCount"></param>
		public void AddThreads(int threadCount)
		{
			for (int i = 0; i < threadCount; i++)
			{
				Thread thread = new Thread(ParserThread);
				thread.Start();
				m_threads.Add(thread);
			}
		}

		/// <summary>
		/// Stops all the parsers in this pool.
		/// </summary>
		public void Stop()
		{
			foreach (Thread thread in m_threads)
			{
				thread.Abort();
			}
			m_threads.Clear();
		}

		/// <summary>
		/// Returns the number of files currently being parsed.
		/// </summary>
		public int GetOutstandingFileCount()
		{
			return m_threadsWorking;
		}

		private void ParserThread()
		{
			string absoluteFilePath;
			BaseParser parser = Activator.CreateInstance<T>();
			while (true)
			{
				Thread.Sleep(0);

				absoluteFilePath = FileUpdater.GetNextFile<T>();

				Interlocked.Increment(ref m_threadsWorking);
				try
				{
					parser.ParseFile(absoluteFilePath);
				}
				catch (Exception e)
				{
					//TODO: LOG ERROR
				}
				finally
				{
					Interlocked.Decrement(ref m_threadsWorking);
				}
			}
		}
	}
}
