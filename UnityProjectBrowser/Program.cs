// Copyright(c) 2018 Brian MacIntosh
// MIT License

using System;
using System.Windows.Forms;
using UnityProjectBrowser;

namespace ProjectBrowser
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
	}
}
