// Copyright(c) 2018 Brian MacIntosh
// MIT License

using System;

namespace ProjectBrowser
{
	public delegate void ObjectDatabaseEventDelegate(object sender, ObjectDatabaseEventArgs args);

	public class ObjectDatabaseEventArgs : EventArgs
	{
		/// <summary>
		/// The object affected by the event.
		/// </summary>
		public readonly ProjectObject Object;

		public ObjectDatabaseEventArgs(ProjectObject affectedObject)
		{
			Object = affectedObject;
		}
	}
}
