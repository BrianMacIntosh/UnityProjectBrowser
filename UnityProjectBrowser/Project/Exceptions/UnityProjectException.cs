// Copyright(c) 2018 Brian MacIntosh
// MIT License

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
