using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraRichEdit;
using System.Diagnostics;
using DevExpress.XtraVerticalGrid;
using XorNet.Rose.Resource;
using XorNet.Rose.Forms;
using DevExpress.XtraBars.Docking2010.Views.Tabbed;
using DevExpress.Skins;
using XorNet.Rose.Properties;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using System.Threading;

namespace XorNet.Rose {
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
	{
		#region Variables
		protected Cursor m_currentCursor;
		protected EditorWindow m_editorSelected;
		protected List<Controls.Control> m_listCtrlsSelected;
		protected bool m_bWindowSelected;
		protected Bitmap splashScreenImageCore;
		protected Assembly currentAssemblyCore;
		#endregion

		#region Accessibility
		public List<Controls.Control> SelectedControls
		{
			get { return m_listCtrlsSelected; }
			set { m_listCtrlsSelected = value; }
		}

		public EditorWindow SelectedEditor
		{
			get { return m_editorSelected; }
			set { m_editorSelected = value; }
		}

		public TabbedView TabbedView1
		{
			get { return tabbedView1; }
			set { tabbedView1 = value; }
		}

		public PropertyGridControl PropertyGridCtrl
		{
			get { return propertyGridControl1; }
			set { propertyGridControl1 = value; }
		}
		#endregion

		#region Construction
		public frmMain()
		{
			DirectX.D3DDeviceService.CreateDeviceService(this.Handle);
			InitializeComponent();
			defaultLookAndFeel1.LookAndFeel.SkinName = Settings.Default.SkinName;
			m_listCtrlsSelected = new List<Controls.Control>();
        }
		#endregion

		#region functions
		private void frmMain_Load(object sender, System.EventArgs e)
		{
			SplashScreenManager.HideImage();
			SkinHelper.InitSkinPopupMenu(iPaintStyle);
			LoadLayout();

			if (Settings.Default.ThemeDir == "")
			{
				ConfigForm form = new ConfigForm();
				form.Owner = this;
                form.ShowDialog();
				if (form.Success == false)
					this.Close();
			}

			if (!Directory.Exists(Settings.Default.ThemeDir))
			{
				if (MessageBox.Show("Theme Directory (" + Settings.Default.ThemeDir + ") does not exist.", "Rose GUI Editor - Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Cancel)
					this.Close();
				ConfigForm form = new ConfigForm();
                form.Owner = this;
                form.ShowDialog();
				if (form.Success == false)
					this.Close();
			}
        }

		private void LoadLayout()
		{
			Refresh(true);
			//Width = Settings.Default.Size.Width;
			//Height = Settings.Default.Size.Height;
			//if (Settings.Default.Location.X != -1 && Settings.Default.Location.Y != -1)
				//Location = new Point(Settings.Default.Location.X, Settings.Default.Location.Y);
			if (Settings.Default.Maximized)
				WindowState = FormWindowState.Maximized;
			Refresh(false);
		}

		private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			SaveLayout();
			//BeginInvoke(new MethodInvoker(SaveLayout));
		}

		private void SaveLayout()
		{
			//Refresh(true);
			if (WindowState == FormWindowState.Maximized)
			{
				Settings.Default.Maximized = true;
			}
			else
			{
				Settings.Default.Maximized = false;
				Settings.Default.Location = new Point(Location.X, Location.Y);
				Settings.Default.Size = new Size(Width, Height);
			}
			Settings.Default.SkinName = defaultLookAndFeel1.LookAndFeel.ActiveSkinName;
			Settings.Default.Save();
			//Refresh(false);
		}

		private void AddNewForm(string s = "UNDEFINED")
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			EditorWindow editor = new EditorWindow(this);
			editor.MdiParent = this;
			editor.Icon = (Icon)resources.GetObject("XorNet");
			editor.Text = s;
			editor.CreateControls();
			m_editorSelected = editor;
			editor.Show();
            tabbedView1.BeginUpdate();
            BaseDocument document = tabbedView1.Controller.AddDocument(editor);
			
			document.Image = (Image)(resources.GetObject("XorNet1"));//imageList4.Images[10];
            document.Form.Text = m_editorSelected.Text;
			document.Form.Icon = (Icon)resources.GetObject("XorNet");
			document.Caption = m_editorSelected.Text;
			editor.Document = document;
            tabbedView1.EndUpdate();
        }

		private void AddControls(Control container, DevExpress.XtraEditors.ComboBoxEdit cb) 
		{
            foreach(object obj in container.Controls) 
			{
                cb.Properties.Items.Add(obj);
                if(obj is Control) 
					AddControls(obj as Control, cb);
            }
        }

        private void repositoryItemComboBox1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) 
		{
            if(e.KeyCode == Keys.Enter && eFind.EditValue != null)
                repositoryItemComboBox1.Items.Add(eFind.EditValue.ToString());
        }

		private void ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			AddNewForm();
        }

		private void iAbout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 
		{
            DevExpress.Utils.About.frmAbout dlg = new DevExpress.Utils.About.frmAbout("Rose, Flyff GUI Editor developed by XorNet");
            dlg.ShowDialog();
        }

		private void iSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			foreach (BaseDocument doc in tabbedView1.Documents)
			{
				((EditorWindow)doc.Control).Save();
				ResManager.Instance.Save();
			}
		}

		private void iCut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (tabbedView1.ActiveDocument != null && tabbedView1.ActiveDocument.Control != null)
				((EditorWindow)tabbedView1.ActiveDocument.Control).Cut();

			if (propertyGridControl1.ActiveEditor != null && propertyGridControl1.ActiveEditor.EditorTypeName == "TextEdit")
				((TextEdit)propertyGridControl1.ActiveEditor).Cut();
			InitEdit();
        }

		private void iCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (tabbedView1.ActiveDocument != null && tabbedView1.ActiveDocument.Control != null)
				((EditorWindow)tabbedView1.ActiveDocument.Control).Copy();

			if (propertyGridControl1.ActiveEditor != null && propertyGridControl1.ActiveEditor.EditorTypeName == "TextEdit")
				((TextEdit)propertyGridControl1.ActiveEditor).Copy();
			InitEdit();
        }

		private void iPaste_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (tabbedView1.ActiveDocument != null && tabbedView1.ActiveDocument.Control != null)
				((EditorWindow)tabbedView1.ActiveDocument.Control).Paste();

			if (propertyGridControl1.ActiveEditor != null && propertyGridControl1.ActiveEditor.EditorTypeName == "TextEdit")
				((TextEdit)propertyGridControl1.ActiveEditor).Paste();
			InitEdit();
        }

		private void iSelectAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (propertyGridControl1.ActiveEditor != null && propertyGridControl1.ActiveEditor.EditorTypeName == "TextEdit")
				((TextEdit)propertyGridControl1.ActiveEditor).SelectAll();
			else if (tabbedView1.ActiveDocument != null && tabbedView1.ActiveDocument.Control != null)
				((EditorWindow)tabbedView1.ActiveDocument.Control).SelectAll();
			InitEdit();
        }

		public void InitEdit()
		{
			if (tabbedView1.ActiveDocument != null && tabbedView1.ActiveDocument.Control != null && tabbedView1.ActiveDocument.Control.Focused)
			{
				EditorWindow editor = (EditorWindow)tabbedView1.ActiveDocument.Control;
				iCut.Enabled = iCopy.Enabled = m_listCtrlsSelected.Count > 0;
				iPaste.Enabled = Clipboard.GetDataObject() != null;
				iUndo.Enabled = editor.UndoRedoManager.StackUndo.Count > 0;
				iRedo.Enabled = editor.UndoRedoManager.StackRedo.Count > 0;
			}
			else if (propertyGridControl1.ActiveEditor != null && propertyGridControl1.ActiveEditor.EditorTypeName == "TextEdit")
			{
				TextEdit editor = ((TextEdit)propertyGridControl1.ActiveEditor);
				iCut.Enabled = iCopy.Enabled = editor.SelectedText != null && editor.SelectedText.Length != 0;
				iPaste.Enabled = Clipboard.GetData(System.Windows.Forms.DataFormats.StringFormat) != null;
				iUndo.Enabled = iRedo.Enabled = false;
				//iUndo.Enabled = editor.CanUndo;
				//iRedo.Enabled = editor.His;
			}
			else
			{
				iCut.Enabled = iCopy.Enabled = iPaste.Enabled = iUndo.Enabled = iRedo.Enabled = false;
			}
		}

		private void iUndo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 
		{
			if (tabbedView1.ActiveDocument != null && tabbedView1.ActiveDocument.Control != null)
				((EditorWindow)tabbedView1.ActiveDocument.Control).UndoRedoManager.Undo();
			InitEdit();
        }

		private void iRedo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 
		{
			if (tabbedView1.ActiveDocument != null && tabbedView1.ActiveDocument.Control != null)
				((EditorWindow)tabbedView1.ActiveDocument.Control).UndoRedoManager.Redo();
			InitEdit();
        }

        protected DockPanel GetTopDockPanelCore(DockPanel panel) 
		{
            if(panel.ParentPanel != null) 
				return GetTopDockPanel(panel.ParentPanel);
            else 
				return panel;
        }

        protected DockPanel GetTopDockPanel(DockPanel panel) 
		{
            DockPanel floatPanelCandidate = GetTopDockPanelCore(panel);
            if(floatPanelCandidate.Dock == DockingStyle.Float) 
				return floatPanelCandidate;
            else 
				return panel;
        }

		private void iSolutionExplorer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 
		{
            GetTopDockPanel(dockPanel1).Show();
            tabbedView1.ActivateDocument(dockPanel1.Parent);
        }

		private void iToolbox_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 
		{
            GetTopDockPanel(dockPanel6).Show();
            tabbedView1.ActivateDocument(dockPanel6.Parent);
        }

		private void solutionExplorer1_TreeViewItemClick(object sender, System.EventArgs e)
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            DevExpress.XtraTreeList.TreeList treeView = sender as DevExpress.XtraTreeList.TreeList;
			string s = treeView.FocusedNode.GetDisplayText(0);
			foreach (BaseDocument doc in tabbedView1.Documents)
			{
				if (doc.Caption == s)
				{
					tabbedView1.ActivateDocument(doc.Control);
					return;
				}
			}

			EditorWindow editor = new EditorWindow(this);
			editor.MdiParent = this;
			editor.Text = s;
			editor.Icon = (Icon)resources.GetObject("XorNet");
			editor.CreateControls();
			m_editorSelected = editor;
			editor.Show();

			tabbedView1.BeginUpdate();
			BaseDocument document = tabbedView1.Controller.AddDocument(editor);

			document.Image = (Image)(resources.GetObject("XorNet1"));//imageList4.Images[10];
			document.Form.Text = s;
			document.Form.Icon = (Icon)resources.GetObject("XorNet");
			document.Caption = m_editorSelected.Text;
			editor.Document = document;
			tabbedView1.EndUpdate();
        }

        private void iSaveLayout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 
		{
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "XML files (*.xml)|*.xml";
            dlg.Title = "Save Layout";
            if(dlg.ShowDialog() == DialogResult.OK) 
			{
                Refresh(true);
                barManager1.SaveToXml(dlg.FileName);
                Refresh(false);
            }
        }

		private void iLoadLayout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 
		{
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "XML files (*.xml)|*.xml|All files|*.*";
            dlg.Title = "Restore Layout";
            if(dlg.ShowDialog() == DialogResult.OK) 
			{
                Refresh(true);
                try 
				{
                    barManager1.RestoreFromXml(dlg.FileName);
                    SkinHelper.InitSkinPopupMenu(iPaintStyle);
                }
                catch { }
                Refresh(false);
            }
        }

		private void Refresh(bool isWait) 
		{
            if(isWait) 
			{
                m_currentCursor = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;
            }
            else
                Cursor.Current = m_currentCursor;
            this.Refresh();
        }

		private void iExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 
		{
            this.Close();
        }

		private void tabbedView1_DocumentAdded(object sender, DocumentEventArgs e) 
		{
        }

		private void tabbedView1_LostFocus(object sender, EventArgs e) 
		{
            InitEdit();
        }

		private void tabbedView1_GotFocus(object sender, EventArgs e)
		{
		}

		private void tabbedView1_DocumentActivated(object sender, DocumentEventArgs e)
		{
			((EditorWindow)e.Document.Control).Focus();
			SetPropertyControl(null, ((EditorWindow)e.Document.Control));
			InitEdit();
		}

		public void SetPropertyControl(Controls.Control control, EditorWindow owner)
		{
			m_listCtrlsSelected.Clear();
			if (control != null)
				m_listCtrlsSelected.Add(control);
			m_editorSelected = owner;
			if (control == null)
				propertyGridControl1.SelectedObject = m_editorSelected.ContainingWindow;
			else
				propertyGridControl1.SelectedObject = control;
		}

		public void AddPropertyControl(Controls.Control control, EditorWindow owner)
		{
			if (m_listCtrlsSelected.Contains(control))
				m_listCtrlsSelected.Remove(control);
			m_listCtrlsSelected.Add(control);
			m_editorSelected = owner;
			propertyGridControl1.SelectedObjects = m_listCtrlsSelected.ToArray();
		}

		private void navBarItem1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			if (SelectedEditor != null)
				SelectedEditor.AddLabel();
		}

		private void navBarItem2_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			if (SelectedEditor != null)
				SelectedEditor.AddButton();
		}

		private void navBarItem3_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			if (SelectedEditor != null)
				SelectedEditor.AddEdit();
		}

		private void navBarItem4_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			if (SelectedEditor != null)
				SelectedEditor.AddImage();
		}

		private void navBarItem11_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			if (SelectedEditor != null)
				SelectedEditor.AddCheckBox();
		}

		private void navBarItem12_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			if (SelectedEditor != null)
				SelectedEditor.AddRadioBox();
		}

		private void navBarItem5_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			if (SelectedEditor != null)
				SelectedEditor.AddCombo();
		}

		private void navBarItem30_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			if (SelectedEditor != null)
				SelectedEditor.AddCustom();
		}

		private void navBarItem27_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			if (SelectedEditor != null)
				SelectedEditor.AddListBox();
		}

		private void navBarItem28_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			if (SelectedEditor != null)
				SelectedEditor.AddTabbedMdi();
		}

		private void navBarItem9_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			if (SelectedEditor != null)
				SelectedEditor.AddTabbedMdi();
		}

		private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (DevExpress.Skins.SkinManager.AllowFormSkins == true)
				DevExpress.Skins.SkinManager.DisableFormSkins();
			else
				DevExpress.Skins.SkinManager.EnableFormSkins();
			Settings.Default.SkinForms = !Settings.Default.SkinForms;
			DevExpress.LookAndFeel.LookAndFeelHelper.ForceDefaultLookAndFeelChanged();
		}

		private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			string szTmp1 = Settings.Default.ThemeDir;
			Settings.Default.Reset();
			Settings.Default.ThemeDir = szTmp1;
			barManager1.BeginUpdate();
			try
			{
				foreach (DevExpress.XtraBars.Bar bar in barManager1.Bars)
					bar.Reset();
			}
			catch
			{
			}
			barManager1.EndUpdate();
			SaveLayout();
			SkinHelper.InitSkinPopupMenu(iPaintStyle);
		}

		private void barManager1_BeforeLoadLayout(object sender, DevExpress.Utils.LayoutAllowEventArgs e)
		{
			if (e.PreviousVersion != barManager1.LayoutVersion)
			{
				e.Allow = false;
			}
		}

		private void iOpenFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.CheckFileExists = true;
			dialog.CheckPathExists = true;
			dialog.Filter = "Resource Files|*.skin";
			if (File.Exists(ResManager.Instance.SkinName))
				dialog.InitialDirectory = ResManager.Instance.Directory;

			if(dialog.ShowDialog() == DialogResult.OK)
			{
				ResManager.Instance.Initialize(dialog.FileName);
				solutionExplorer1.AddAllNodes(true);
			}
		}

		private void iOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.CheckFileExists = true;
			dialog.CheckPathExists = true;
			dialog.Filter = "Resource Files|*.skin";
			if (File.Exists(Path.Combine(ResManager.Instance.Directory, ResManager.Instance.SkinName)))
				dialog.InitialDirectory = ResManager.Instance.Directory;

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				ResManager.Instance.Initialize(dialog.FileName);
				solutionExplorer1.AddAllNodes(true);
			}
		}

		private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			ConfigForm form = new ConfigForm();
			form.ShowDialog();
		}
		#endregion
	}
}
