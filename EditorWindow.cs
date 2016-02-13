using System;
using System.Collections.Generic;
using System.Drawing;
using XorNet.Rose.Controls;
using XorNet.Rose.DirectX;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Diagnostics;
using System.Linq;
using XorNet.Rose.Resource;
using System.ComponentModel;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Docking2010.Views;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel.Design.Serialization;

namespace XorNet.Rose
{
    public partial class EditorWindow : D3DWindowBase
    {
		#region Properties
		public Controls.Window ContainingWindow
		{
			get { return m_pContainingWindow; }
			set { m_pContainingWindow = value; }
		}
		public DevExpress.XtraBars.Docking2010.Views.BaseDocument Document
		{
			get { return m_document; }
			set { m_document = value; }
		}
		public XorNet.Rose.frmMain Mainform
		{
			get { return m_mainform; }
			set { m_mainform = value; }
		}
		public XorNet.Rose.UndoRedoMng UndoRedoManager
		{
			get { return m_UndoRedoMng; }
			set { m_UndoRedoMng = value; }
		}
		#endregion

		#region Variables
		private BaseDocument m_document;
		private int m_framerate;
		private Controls.Window m_pContainingWindow;
		private frmMain m_mainform;
		private UndoRedoMng m_UndoRedoMng;
		#endregion 

		#region Construction
		public EditorWindow(frmMain mainform)
        {
			m_UndoRedoMng = new UndoRedoMng(this);
            m_framerate = 0;
            this.m_mainform = mainform;
            InitializeComponent();
        }

		~EditorWindow()
		{
			m_mainform = null;
			m_pContainingWindow = null;
			m_document = null;
		}
		#endregion

		#region Overrides
		protected override void OnShown(EventArgs e)
        {
			if (m_pContainingWindow.Children.Count() > 0)
				((frmMain)MdiParent).SetPropertyControl(m_pContainingWindow.Children[0], this);
			else if (m_pContainingWindow != null)
				((frmMain)MdiParent).SetPropertyControl(null, this);
            base.OnShown(e);
        }

        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
		{
			m_mainform.TabbedView1.Controller.Activate(Document);
			this.Focus();
            Debug.Print("OnMouseDown");
            if (m_pContainingWindow.HitTest(e.Location))
            {
				bool bHit = false;
				for (int i = m_pContainingWindow.Children.Count() - 1; i >= 0; i--)
                {
					if (m_pContainingWindow.Children[i].HitTest(e.Location))
					{
						bHit = true;
						m_pContainingWindow.Children[i].OutlineTest(e.Location);
						m_pContainingWindow.Children[i].OnMouseDown(e);
						if ((ModifierKeys & System.Windows.Forms.Keys.Shift) != 0 || (ModifierKeys & System.Windows.Forms.Keys.Control) != 0)
							m_mainform.AddPropertyControl(m_pContainingWindow.Children[i], this);
						else
							m_mainform.SetPropertyControl(m_pContainingWindow.Children[i], this);
                        break;
                    }
                }
                if (!bHit)
                {
                    m_mainform.SetPropertyControl(null, this);
					m_pContainingWindow.OnMouseDown(e);
                }
			}
			Mainform.InitEdit();
			base.OnMouseDown(e);
        }

        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
		{
            Debug.Print("OnMouseUp");
			m_mainform.PropertyGridCtrl.BeginDataUpdate();
			for (int i = m_pContainingWindow.Children.Count() - 1; i >= 0; i--)
            {
				m_pContainingWindow.Children[i].OnMouseUp(e);
            }
            m_pContainingWindow.OnMouseUp(e);
			base.OnMouseUp(e);
            m_mainform.PropertyGridCtrl.EndDataUpdate();
        }

        protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
		{
			if (m_pContainingWindow.HitTest(e.Location))
			{
				this.Cursor = System.Windows.Forms.Cursors.Arrow;
				for (int i = m_pContainingWindow.Children.Count() - 1; i >= 0; i--)
				{
					m_pContainingWindow.Children[i].OnMouseMove(e);
				}
			}
			else
			{
				if (e.Button == System.Windows.Forms.MouseButtons.Left)
					this.Cursor = System.Windows.Forms.Cursors.No;
			}

            m_pContainingWindow.OnMouseMove(e);
            base.OnMouseMove(e);
        }

        public override void UpdateLogic()
        {
            /*// call to control updates.
            Point mousept = PointToClient(D3DInputService.Instance.MouseDevice.CurrentLocation);

            if (mousept.X < 0 || mousept.Y < 0 || mousept.X > Width || mousept.Y > Height)
                return;

            if (D3DInputService.Instance.MouseDevice[MouseRose.Controls.Button.LeftRose.Controls.Button] && !D3DInputService.Instance.MouseDevice.IsDrag(MouseRose.Controls.Button.LeftRose.Controls.Button))
            {
                m_pSelectedControl = m_pContainingWindow;
                foreach (Control pCurrControl in m_arrControl)
                {
                    if (pCurrControl.HitTest(mousept))
                    {
                        pCurrControl.OnLeftClick();
                        m_pSelectedControl = pCurrControl;
                        break;
                    }
                    ((Form1)MdiParent).SetPropertyObject(m_pSelectedControl);
                }
            }
            else if(D3DInputService.Instance.MouseDevice[MouseRose.Controls.Button.LeftRose.Controls.Button])
            {
                if(m_pSelectedControl != null) m_pSelectedControl.OnLeftDrag();
            }*/
            base.UpdateLogic();
        }

        public override void Draw()
        {
			if (m_mainform != null && m_mainform.SelectedEditor == this)
			{
				m_framerate++;
				m_pContainingWindow.Draw();

				//for (int i = m_pContainingWindow.Children.Count() - 1; i >= 0; i--)
				foreach (Controls.Control ctrl in m_pContainingWindow.Children)
				{
					ctrl.Draw();
				}
				foreach(Control ctrl in m_mainform.SelectedControls)
				{
					ctrl.DrawBounds();
				}
				if (m_mainform.SelectedControls.Count == 0 && m_pContainingWindow != null)
					m_pContainingWindow.DrawBounds();
				base.Draw();
			}
        }
		#endregion

		#region Functions
		private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "FPS: " + m_framerate;
            m_framerate = 0;
        }

		private void EditorWindow_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
		{
			if (e.Data.GetDataPresent("DevExpress.XtraNavBar.NavBarItemLink") && m_pContainingWindow.HitTest(PointToClient(new Point(e.X, e.Y))))
				e.Effect = System.Windows.Forms.DragDropEffects.Copy;
			else
				e.Effect = System.Windows.Forms.DragDropEffects.None;
		}

		private void EditorWindow_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			string szCaption = null;
			try
			{
				szCaption = ((DevExpress.XtraNavBar.NavBarItemLink)e.Data.GetData("DevExpress.XtraNavBar.NavBarItemLink", true)).Caption;
			}
			catch(System.Exception ex)
			{
				Debug.Print(ex.Message);
			}

			if (m_pContainingWindow.HitTest(PointToClient(new Point(e.X, e.Y))))
			{
				if (szCaption == "Label")
				{
					AddControl(new Rose.Controls.Label(PointToClient(new Point(e.X, e.Y))));
				}
				else if (szCaption == "Button")
				{
					AddControl(new Rose.Controls.Button(PointToClient(new Point(e.X, e.Y))));
				}
				else if (szCaption == "Edit Box")
				{
					AddControl(new Rose.Controls.Edit(PointToClient(new Point(e.X, e.Y)))); // TODO
				}
				else if (szCaption == "Image")
				{
					AddControl(new Rose.Controls.Static(PointToClient(new Point(e.X, e.Y))));
				}
				else if (szCaption == "Check Box")
				{
					AddControl(new Rose.Controls.Checkbox(PointToClient(new Point(e.X, e.Y))));
				}
				else if (szCaption == "Radio Button")
				{
					AddControl(new Rose.Controls.Radiobox(PointToClient(new Point(e.X, e.Y))));
				}
				else if (szCaption == "Combo Box")
				{
					AddControl(new Rose.Controls.Combo(PointToClient(new Point(e.X, e.Y))));
				}
				else if (szCaption == "Custom")
				{
					AddControl(new Rose.Controls.Custom(PointToClient(new Point(e.X, e.Y))));
				}
				else if (szCaption == "List Box")
				{
					AddControl(new Rose.Controls.ListBox(PointToClient(new Point(e.X, e.Y))));
				}
				else if (szCaption == "Tabbed Mdi")
				{
					AddControl(new Rose.Controls.TabbedMdi(PointToClient(new Point(e.X, e.Y))));
				}
			}
		}

		public void AddControl(Control ctrl)
		{
			m_mainform.TabbedView1.Controller.Activate(Document);
			Focus();
			ctrl.Parent = m_pContainingWindow;
			m_pContainingWindow.AddChild(ctrl);
			m_mainform.SetPropertyControl(ctrl, this);
		}

		public void AddControlFromResource(Control ctrl)
		{
			//m_mainform.TabbedView1.Controller.Activate(Document);
			//Focus();
			ctrl.Parent = m_pContainingWindow;
			m_pContainingWindow.AddChildFromResource(ctrl);
			m_mainform.SetPropertyControl(ctrl, this);
		}

		public void AddLabel()
		{
			AddControl(new Rose.Controls.Label(new Point(m_pContainingWindow.Location.X + 12, m_pContainingWindow.Location.Y + 28)));
		}

		public void AddButton()
		{
			AddControl(new Rose.Controls.Button(new Point(m_pContainingWindow.Location.X + 12, m_pContainingWindow.Location.Y + 28)));
		}

		public void AddEdit()
		{
			AddControl(new Rose.Controls.Edit(new Point(m_pContainingWindow.Location.X + 12, m_pContainingWindow.Location.Y + 28)));
		}

		public void AddImage()
		{
			AddControl(new Rose.Controls.Static(new Point(m_pContainingWindow.Location.X + 12, m_pContainingWindow.Location.Y + 28)));
		}

		public void AddCheckBox()
		{
			AddControl(new Rose.Controls.Checkbox(new Point(m_pContainingWindow.Location.X + 12, m_pContainingWindow.Location.Y + 28)));
		}

		public void AddRadioBox()
		{
			AddControl(new Rose.Controls.Radiobox(new Point(m_pContainingWindow.Location.X + 12, m_pContainingWindow.Location.Y + 28)));
		}

		public void AddCombo()
		{
			AddControl(new Rose.Controls.Combo(new Point(m_pContainingWindow.Location.X + 12, m_pContainingWindow.Location.Y + 28)));
		}

		public void AddCustom()
		{
			AddControl(new Rose.Controls.Custom(new Point(m_pContainingWindow.Location.X + 12, m_pContainingWindow.Location.Y + 28)));
		}

		public void AddListBox()
		{
			AddControl(new Rose.Controls.ListBox(new Point(m_pContainingWindow.Location.X + 12, m_pContainingWindow.Location.Y + 28)));
		}

		public void AddTabbedMdi()
		{
			AddControl(new Rose.Controls.TabbedMdi(new Point(m_pContainingWindow.Location.X + 12, m_pContainingWindow.Location.Y + 28)));
		}

		public void AddTextBox()
		{
			AddControl(new Rose.Controls.TextBox(new Point(m_pContainingWindow.Location.X + 12, m_pContainingWindow.Location.Y + 28)));
		}

		public void CreateControls()
		{
			if (!ResManager.Instance.Data.Properties.ContainsKey(Text))
			{
				m_pContainingWindow = new Window(new Rectangle(ResManager.Instance.DefaultWindowLocation.X, ResManager.Instance.DefaultWindowLocation.Y, 200, 200), this);
				Text = m_pContainingWindow.Id;
			}
			else
			{
				WindowProperty resource = ResManager.Instance.Data.Properties[Text];
				m_pContainingWindow = new Window(resource, this);
				foreach (ControlProperty ResourceControl in resource.m_aControls.Values)
				{
					switch (ResourceControl.m_szType)
					{
						case "WTYPE_BUTTON":
							{
								if(ResourceControl.m_style.HasFlag(WindowStyle.WBS_RADIO))
									AddControlFromResource(new Rose.Controls.Radiobox(ResourceControl));
								else if(ResourceControl.m_style.HasFlag(WindowStyle.WBS_CHECK))
									AddControlFromResource(new Rose.Controls.Checkbox(ResourceControl));
								else
									AddControlFromResource(new Rose.Controls.Button(ResourceControl));
							}
							break;

						case "WTYPE_TEXT":
						case "WTYPE_STATIC":
							{
								//if (ResourceControl.m_szTitle == "")
									//AddControlFromResource(new Rose.Controls.Static(ResourceControl));
								//else
								AddControlFromResource(new Rose.Controls.Label(ResourceControl));
							}
							break;

						case "WTYPE_EDITCTRL":
							{
								AddControlFromResource(new Rose.Controls.Edit(ResourceControl));
							}
							break;

						case "WTYPE_CUSTOM":
							{
								AddControlFromResource(new Rose.Controls.Custom(ResourceControl));
							}
							break;

						case "WTYPE_COMBOBOX":
							{
								AddControlFromResource(new Rose.Controls.Combo(ResourceControl));
							}
							break;

						case "WTYPE_LISTBOX":
							{
								AddControlFromResource(new Rose.Controls.ListBox(ResourceControl));
							}
							break;

						case "WTYPE_TABCTRL":
							{
								AddControlFromResource(new Rose.Controls.TabbedMdi(ResourceControl));
							}
							break;
					}
				}
			}
		}

		private void EditorWindow_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case System.Windows.Forms.Keys.Delete:
					if (m_mainform.SelectedEditor == this && m_mainform.SelectedControls.Count > 0)
					{
						foreach(Control ctrl in m_mainform.SelectedControls)
						{
							m_pContainingWindow.RemoveChild(ctrl);
						}
						m_mainform.SetPropertyControl(null, this);
					}
					break;
			}
		}

		public void Save()
		{
			m_pContainingWindow.Save();
		}

		public void Copy()
		{
			if (m_mainform.SelectedControls == null || m_mainform.SelectedControls.Count == 0)
				return;

			System.Windows.Forms.Clipboard.SetData(System.Windows.Forms.DataFormats.Serializable, m_mainform.SelectedControls);
		}

		public void Cut()
		{
			if (m_mainform.SelectedControls == null || m_mainform.SelectedControls.Count == 0)
				return;

			System.Windows.Forms.Clipboard.SetData(System.Windows.Forms.DataFormats.Serializable, m_mainform.SelectedControls);
			foreach(Control ctrl in m_mainform.SelectedControls)
			{
				m_pContainingWindow.RemoveChild(ctrl);
			}
			m_mainform.SelectedControls.Clear();
		}

		public void SelectAll()
		{
			m_mainform.SelectedControls.Clear();
			foreach (Control ctrl in m_pContainingWindow.Children)
			{
				m_mainform.SelectedControls.Add(ctrl);
			}
		}

		public void Paste()
		{
			List<Control> list = (List<Control>)System.Windows.Forms.Clipboard.GetData(System.Windows.Forms.DataFormats.Serializable);
			if (list != null)
			{
				m_mainform.SelectedControls.Clear();
				foreach (Control ctrl in list)
				{
					ctrl.OffsetControl = false;
					ctrl.Pasting = true;
					ctrl.Parent = m_pContainingWindow;
					ctrl.OnSerialized();
					m_pContainingWindow.AddChild(ctrl);
					m_mainform.SelectedControls.Add(ctrl);
					ctrl.Pasting = false;
				}
			}
		}
		#endregion
    }

	public class UndoRedo
	{
		#region Properties
		#endregion

		#region Variables
		private Object m_data;
		private Object m_instance;
		private string m_identifier;
		private UndoRedoUpdate m_delegate;
		#endregion

		#region Construction
		public UndoRedo(Object instance, Object data, string identifier, UndoRedoUpdate del)
		{
			m_data = data;
			m_delegate = del;
			m_identifier = identifier;
			m_instance = instance;
		}
		#endregion

		#region Functions
		public UndoRedo Undo()
		{
			m_data = m_delegate(m_instance, m_data, m_identifier);
			return new UndoRedo(m_instance, m_data, m_identifier, m_delegate);
		}
		public UndoRedo Redo()
		{
			m_data = m_delegate(m_instance, m_data, m_identifier);
			return new UndoRedo(m_instance, m_data, m_identifier, m_delegate);
		}
		#endregion
	}

	public class UndoRedoMng
	{
		#region Properties
		public Stack<UndoRedo> StackUndo
		{
			get { return m_stackUndo; }
			set { m_stackUndo = value; }
		}
		public Stack<UndoRedo> StackRedo
		{
			get { return m_stackRedo; }
			set { m_stackRedo = value; }
		}
		#endregion

		#region Variables
		private Stack<UndoRedo> m_stackUndo = new Stack<UndoRedo>();
		private Stack<UndoRedo> m_stackRedo = new Stack<UndoRedo>();
		private EditorWindow m_editor = null;
		#endregion

		#region Construction
		public UndoRedoMng(EditorWindow editor)
		{
			m_editor = editor;
		}
		#endregion

		#region Functions
		public void Update(UndoRedo undoredo)
		{
			m_stackRedo.Clear();
			m_stackUndo.Push(undoredo);
			m_editor.Mainform.InitEdit();
		}

		public void Update(Object instance, Object data, string identifier, UndoRedoUpdate del)
		{
			m_stackRedo.Clear();
			m_stackUndo.Push(new UndoRedo(instance, data, identifier, del));
			m_editor.Mainform.InitEdit();
		}

		public void Undo()
		{
			if (m_stackUndo.Count > 0)
			{
				m_stackRedo.Push(m_stackUndo.Pop().Undo());
				m_editor.Mainform.InitEdit();
			}
		}

		public void Redo()
		{
			if (m_stackRedo.Count > 0)
			{
				m_stackUndo.Push(m_stackRedo.Pop().Redo());
				m_editor.Mainform.InitEdit();
			}
		}
		#endregion
	}

	public class CopyCutPasteMng
	{
		#region Construction
		public CopyCutPasteMng()
		{

		}
		~CopyCutPasteMng()
		{

		}
		#endregion
	}

	#region Delegates
	public delegate Object UndoRedoUpdate(Object instance, Object data, string identifier);
	#endregion
}