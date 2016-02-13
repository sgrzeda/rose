using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using XorNet.Rose.Forms;
using System.Windows.Forms;
using XorNet.Rose.Properties;

namespace XorNet.Rose.Resource
{
	abstract public class ScannerBase
	{
		#region Fields
		/// <summary>
		/// Holds all lines loaded from the file.
		/// </summary>
		protected List<string> lines;

		/// <summary>
		/// Current line.
		/// </summary>
		protected int line = 0;

		/// <summary>
		/// Current cursor position.
		/// </summary>
		protected int pos = 0;
		#endregion

		#region Constructors
		/// <summary>
		/// Creates a new scanner object.
		/// </summary>
		/// <param name="fileName">File to be scanned.</param>
		public ScannerBase(string fileName)
		{
			// Load file.
			LoadFile(fileName);

			// Process tokens.
			for (int i = 0; i < lines.Count; i++)
			{
				string line = lines[i];

				// Commented.
				if (line.StartsWith("//"))
				{
					lines.RemoveAt(i);
					continue;
				}

				lines[i] = line;
			}
		}

		private void LoadFile(string fileName)
		{
			try
			{
				string[] buffer = File.ReadAllText(fileName)
					.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
				lines = new List<string>(buffer.Length);
				lines.AddRange(buffer);
			}
			catch (System.Exception ex)
			{
				if (MessageBox.Show(ex.Message, "Rose GUI Editor - Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Cancel)
					Environment.Exit(-1);
				//Logging.Error(ex.Message);
				ConfigForm form = new ConfigForm();
				Application.Run(form);
				//if (form.Success == false)
					//Environment.Exit(-1);
				LoadFile(fileName);
			}
		}
		#endregion

		#region API
		/// <summary>
		/// Main token accessor abstract.
		/// Will be overridden in derived classes.
		/// </summary>
		/// <returns>Scanned token.</returns>
		abstract protected string _GetToken();

		/// <summary>
		/// Get next token as a string.
		/// </summary>
		/// <returns>The next token as a string.</returns>
		public string GetToken()
		{
			return _GetToken();
		}

		/// <summary>
		/// Get next token as an integer.
		/// </summary>
		/// <returns>The next token as an integer.</returns>
		public int GetNumber()
		{
			int nRet;
			string tok = _GetToken();
			if(tok.StartsWith("0x"))
			{
				if (Int32.TryParse(tok.Substring(2), NumberStyles.HexNumber, null, out nRet) == false)
					return 0;
			}
			else
			{
				if (Int32.TryParse(tok, out nRet) == false)
					return 0;
			}
			return nRet;
		}
		#endregion
	}

	/// <summary>
	/// Scanner class used to read various flyff script files.
	/// </summary>
	public class Scanner : ScannerBase
	{
		#region Constructors
		/// <summary>
		/// Creates a new script file scanner.
		/// </summary>
		/// <param name="Filename">File to be scanned.</param>
		public Scanner(string Filename)
			: base(Filename)
		{
			for (int i = 0; i < lines.Count; i++)
			{
				lines[i] = lines[i].Trim();
			}
		}
		#endregion

		#region API
		override protected string _GetToken()
		{
			// If out of lines, return null.
			if (line >= lines.Count)
				return null;

			string[] tokens = lines[line].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

			// If pos is out of range or if token specifies a comment, skip to next line.
			if (pos >= tokens.Length || tokens[pos].StartsWith("//"))
			{
				line++;
				pos = 0;
				return GetToken();
			}

			// Store current token and advance pos.
			string token = tokens[pos];
			pos++;

			// Is string?
			while (token.StartsWith("\"") && !token.EndsWith("\""))
				token += " " + GetToken();

			// If token has comment, skip to next line.
			if (token.Contains("//"))
			{
				line++;
				pos = 0;
			}

			return token.Replace("\"", "");
		}
		#endregion
	}

	/// <summary>
	/// Scanner class used to read flyff string define files.
	/// </summary>
	public class DefineScanner : ScannerBase
	{
		#region Constructors
		/// <summary>
		/// Creates a new define file scanner.
		/// </summary>
		/// <param name="Filename">File to be scanned.</param>
		public DefineScanner(string Filename)
			: base(Filename)
		{
			for (int i = 0; i < lines.Count; i++)
			{
				lines[i] = lines[i].Trim(' ');
			}
		}
		#endregion

		#region API
		override protected string _GetToken()
		{
			// If out of lines, return null.
			if (line >= lines.Count)
				return null;

			string[] tokens = lines[line].Split(new char[] { '\t' }, StringSplitOptions.None);

			// If pos is out of range or if token specifies a comment, skip to next line.
			if (pos >= tokens.Length)
			{
				line++;
				pos = 0;
				return GetToken();
			}

			// Store current token and advance pos.
			string token = tokens[pos];
			pos++;

			return token;
		}
		#endregion
	}

	/// <summary>
	/// Scanner class used to read flyff string define files.
	/// </summary>
	public class HeaderScanner : ScannerBase
	{
		#region Constructors
		/// <summary>
		/// Creates a new define file scanner.
		/// </summary>
		/// <param name="Filename">File to be scanned.</param>
		public HeaderScanner(string Filename)
			: base(Filename)
		{
			for (int i = 0; i < lines.Count; i++)
			{
				lines[i] = lines[i].Replace("#ifndef", "").Replace("#ifdef", "").Replace("#endif", "").Replace("#define", "").Trim(' ');
				if (lines[i].Length <= 0 || (!lines[i].Contains('\t') && !lines[i].Contains(' ')))
					lines.RemoveAt(i);
			}
		}
		#endregion

		#region API
		override protected string _GetToken()
		{
			// If out of lines, return null.
			if (line >= lines.Count)
				return null;

			string[] tokens = lines[line].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

			// If pos is out of range or if token specifies a comment, skip to next line.
			if (pos >= tokens.Length || tokens[pos].StartsWith("//") || tokens[pos].StartsWith("#"))
			{
				line++;
				pos = 0;
				return GetToken();
			}

			// Store current token and advance pos.
			string token = tokens[pos];
			pos++;

			return token;
		}
		#endregion
	}
}
