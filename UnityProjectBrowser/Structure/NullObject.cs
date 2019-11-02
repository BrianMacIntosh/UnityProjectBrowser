// Copyright(c) 2018 Brian MacIntosh
// MIT License

using System;

namespace ProjectBrowser
{
	public class NullObject : ProjectObject
	{
		/// <summary>
		/// Returns true if this object should not appear in the hierarchy view.
		/// </summary>
		public override bool HideFromHierarchy
		{
			get { return true; }
		}

		public NullObject(string uniqueId)
			: base(uniqueId)
		{
			
		}

		/// <summary>
		/// Gets the key of the icon to use for this object.
		/// </summary>
		public override string GetIconKey()
		{
			//TODO:
			return "";
		}

		/// <summary>
		/// Gets the absolute path of the file this object belongs to.
		/// </summary>
		public override string GetFilePath()
		{
			return "";
		}

		public override string ToString()
		{
			return "null";
		}
	}
}
