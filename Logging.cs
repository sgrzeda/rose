using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Windows;

namespace XorNet.Rose
{
    internal class Logging
    {
        public static void Error(string format, params Object[] args)
        {
            var stackTrace = new StackTrace(true);
            string error = string.Format(format, args);
            error = stackTrace.GetFrame(1).GetFileName() + "[" + stackTrace.GetFrame(1).GetMethod().Name + "] " + error + "\nIgnore the error and continue running the application?";
			if (MessageBox.Show(error, "Rose GUI Editor - Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
				Environment.Exit(-1);
#if DEBUG
            //throw new Exception(error);
#endif
        }
    }
}