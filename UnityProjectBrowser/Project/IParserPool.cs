namespace ProjectBrowser
{
	public interface IParserPool
	{
		/// <summary>
		/// Creates the specified number of parser threads of this type.
		/// </summary>
		/// <param name="threadCount"></param>
		void AddThreads(int threadCount);

		/// <summary>
		/// Stops all the parsers in this pool.
		/// </summary>
		void Stop();

		/// <summary>
		/// Returns the number of files currently being parsed.
		/// </summary>
		int GetOutstandingFileCount();
	}
}
