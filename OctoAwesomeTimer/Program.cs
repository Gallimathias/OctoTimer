﻿/*
 * Erstellt mit SharpDevelop.
 * Benutzer: susch
 * Datum: 18.07.2015
 * Zeit: 18:44
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
using System;
using System.Windows.Forms;
using TimerOctoAwesome;

namespace OctoAwesomeTimer
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
		
	}
}
