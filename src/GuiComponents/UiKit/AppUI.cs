#region Copyright (c) 2002-2003, James W. Newkirk, Michael C. Two, Alexei A. Vorontsov, Charlie Poole, Philip A. Craig
/************************************************************************************
'
' Copyright  2002-2003 James W. Newkirk, Michael C. Two, Alexei A. Vorontsov, Charlie Poole
' Copyright  2000-2002 Philip A. Craig
'
' This software is provided 'as-is', without any express or implied warranty. In no 
' event will the authors be held liable for any damages arising from the use of this 
' software.
' 
' Permission is granted to anyone to use this software for any purpose, including 
' commercial applications, and to alter it and redistribute it freely, subject to the 
' following restrictions:
'
' 1. The origin of this software must not be misrepresented; you must not claim that 
' you wrote the original software. If you use this software in a product, an 
' acknowledgment (see the following) in the product documentation is required.
'
' Portions Copyright  2002-2003 James W. Newkirk, Michael C. Two, Alexei A. Vorontsov, Charlie Poole
' or Copyright  2000-2002 Philip A. Craig
'
' 2. Altered source versions must be plainly marked as such, and must not be 
' misrepresented as being the original software.
'
' 3. This notice may not be removed or altered from any source distribution.
'
'***********************************************************************************/
#endregion

using System;
using System.IO;
using System.Windows.Forms;
using NUnit.Util;

namespace NUnit.UiKit
{
	/// <summary>
	/// AppUI keeps track of the main components
	/// for an NUnit-based application so that
	/// UI elements can find them.
	/// </summary>
	public class AppUI
	{
		private static TextWriter outWriter;
		private static TextWriter errWriter;

		private static TestLoader loader;
		private static TestLoaderUI loaderUI;

		public static void Init( 
			Form ownerForm, TextWriter outWriter, TextWriter errWriter,	TestLoader loader, bool vsSupport )
		{
			AppUI.outWriter = outWriter;
			Console.SetOut( new ConsoleWriter( outWriter ) );

			AppUI.errWriter = errWriter;
			Console.SetError( new ConsoleWriter( errWriter ) );

			AppUI.loader = loader;

			AppUI.loaderUI = new TestLoaderUI( ownerForm, loader, vsSupport );
		}

		public static TextWriter Out
		{
			get { return outWriter; }
		}

		public static TextWriter Error
		{
			get { return errWriter; }
		}

		public static TestLoader TestLoader
		{
			get { return loader; }
		}

		public static TestLoaderUI TestLoaderUI
		{
			get { return loaderUI; }
		}
	}
}
