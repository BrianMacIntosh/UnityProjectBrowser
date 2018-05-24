using System;

namespace ProjectBrowser
{
	public abstract class UnityProjectException : Exception
	{
		public readonly string Path;

		public UnityProjectException(string path)
		{
			Path = path;
		}
	}
}
