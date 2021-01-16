// Copyright(c) 2018 Brian MacIntosh
// MIT License

using System;
using System.IO;

namespace ProjectBrowser
{
	/// <summary>
	/// <see cref="TextReader"/> that removes 'stripped' tags from read files, which are invalid YAML.
	/// </summary>
	public class UnityTextReader : TextReader
	{
		private TextReader m_backing;

		/// <summary>
		/// The current buffered line read.
		/// </summary>
		private string m_currentLine = "";

		/// <summary>
		/// The index of the next character in the line that will be read.
		/// </summary>
		private int m_currentCharacter = 0;

		public UnityTextReader(TextReader backing)
		{
			m_backing = backing;
		}
		
		/// <summary>
		/// Reads the next character from the text reader and advances the character position by one character.
		/// <returns>The next character from the text reader, or -1 if no more characters are available.</returns>
		public override int Read()
		{
			if (m_currentLine == null)
			{
				return -1;
			}
			else if (m_currentCharacter == m_currentLine.Length)
			{
				m_currentCharacter++;
				return '\n';
			}
			else if (m_currentCharacter > m_currentLine.Length)
			{
				GetNextBackingLine();
			}

			if (m_currentLine == null)
			{
				return -1;
			}
			else if (m_currentCharacter == m_currentLine.Length)
			{
				// edge case: just read a new empty line
				m_currentCharacter++;
				return '\n';
			}
			else
			{
				return m_currentLine[m_currentCharacter++];
			}
		}

		/// <summary>
		/// Reads a line of characters from the text reader and returns the data as a string.
		/// </summary>
		/// <returns>The next line from the reader, or null if all characters have been read.</returns>
		public override string ReadLine()
		{
			string line = m_currentLine;
			GetNextBackingLine();
			return line.Substring(m_currentCharacter);
		}

		private void GetNextBackingLine()
		{
			m_currentLine = m_backing.ReadLine();
			m_currentCharacter = 0;

			if (m_currentLine == null)
			{
				return;
			}

			// Unity adds this 'stripped' thing that's illegal YAML and messes up the parser.
			if (m_currentLine.StartsWith("---", StringComparison.OrdinalIgnoreCase)
				&& m_currentLine.EndsWith(" stripped"))
			{
				m_currentLine = m_currentLine.Substring(0, m_currentLine.Length - " stripped".Length);
			}
		}
	}
}
