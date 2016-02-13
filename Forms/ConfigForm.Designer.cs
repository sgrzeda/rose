namespace XorNet.Rose.Forms
{
	partial class ConfigForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
			this.buttonEdit1 = new DevExpress.XtraEditors.ButtonEdit();
			this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
			this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonEdit1
			// 
			this.buttonEdit1.Location = new System.Drawing.Point(46, 51);
			this.buttonEdit1.Name = "buttonEdit1";
			this.buttonEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.buttonEdit1.Size = new System.Drawing.Size(293, 20);
			this.buttonEdit1.TabIndex = 5;
			this.buttonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEdit1_ButtonClick);
			// 
			// labelControl2
			// 
			this.labelControl2.Location = new System.Drawing.Point(46, 32);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(79, 13);
			this.labelControl2.TabIndex = 7;
			this.labelControl2.Text = "Theme Directory";
			// 
			// simpleButton1
			// 
			this.simpleButton1.Location = new System.Drawing.Point(153, 88);
			this.simpleButton1.Name = "simpleButton1";
			this.simpleButton1.Size = new System.Drawing.Size(75, 23);
			this.simpleButton1.TabIndex = 9;
			this.simpleButton1.Text = "Save!";
			this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
			// 
			// ConfigForm
			// 
			this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
			this.Appearance.Options.UseBackColor = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(390, 142);
			this.Controls.Add(this.simpleButton1);
			this.Controls.Add(this.labelControl2);
			this.Controls.Add(this.buttonEdit1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.LookAndFeel.SkinName = "Office 2013";
			this.MaximizeBox = false;
			this.Name = "ConfigForm";
			this.Text = "Rose - Configuration";
			((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.ButtonEdit buttonEdit1;
		private DevExpress.XtraEditors.LabelControl labelControl2;
		private DevExpress.XtraEditors.SimpleButton simpleButton1;
	}
}