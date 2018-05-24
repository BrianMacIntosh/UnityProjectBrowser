using System;

namespace ProjectBrowser
{
	public class NotAUnityProjectException : UnityProjectException
	{
		public NotAUnityProjectException(string path)
			: base(path)
		{
			
		}

		public override string Message
		{
			get { return "The folder '" + Path + "' is not a Unity project."; }
		}
	}
}
