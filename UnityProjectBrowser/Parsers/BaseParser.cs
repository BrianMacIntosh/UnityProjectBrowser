// Copyright(c) 2018 Brian MacIntosh
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBrowser
{
	/// <summary>
	/// Base class for a parser that parses a certain kind of asset.
	/// </summary>
	public abstract class BaseParser
	{
		/// <summary>
		/// Parses the specified file.
		/// </summary>
		public abstract void ParseFile(string absolutePath);
	}
}
