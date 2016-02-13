using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using Microsoft.SDK.Samples.VistaBridge.Library;
using System.Runtime.InteropServices;
using XorNet.Rose.Properties;

namespace XorNet.Rose.Forms
{
	public partial class ConfigForm : DevExpress.XtraEditors.XtraForm
	{
		public bool Success = false;

		public ConfigForm()
		{
			InitializeComponent();
		}

		private void simpleButton1_Click(object sender, EventArgs e)
		{
			if (!Directory.Exists(buttonEdit1.Text))
			{
				MessageBox.Show("Theme Directory does not exist!",
					"Invalid File Path",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				return;
			}

			Settings.Default.ThemeDir = buttonEdit1.Text;
			Settings.Default.Save();

			Success = true;
		    Close();
		}

		private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
            CommonOpenFileDialog openfiledialog = new CommonOpenFileDialog();
            
            
            openfiledialog.FoldersOnly = true;
            openfiledialog.CheckPathExists = true;
			openfiledialog.Title = "Select Theme Directory";

            CommonFileDialogResult result = openfiledialog.ShowDialog();
            if (result.Canceled == true)
				return;

            buttonEdit1.Text = openfiledialog.FileName;
		}

		private void AlertForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Enter)
				simpleButton1.PerformClick();
		}
	}
}