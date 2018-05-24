// Copyright(c) 2018 Brian MacIntosh
// MIT License

using System;

namespace ProjectBrowser
{
	public class UnityProjectNotTextException : UnityProjectException
	{
		public UnityProjectNotTextException(string path)
			: base(path)
		{

		}

		public override string Message
		{
			get { return "Unity project '" + Path + "' is not using Text asset serialization."; }
		}
	}
}
