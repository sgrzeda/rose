using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using XorNet.Rose.DirectX;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Drawing;
using System.Diagnostics;
using System.Collections.ObjectModel;
using XorNet.Rose.Resource;
using System.Windows.Forms;

[Serializable]
public enum CONTROL_TYPE
{
    WTYPE_WINDOW,
	WTYPE_STATIC,
	WTYPE_BUTTON,
	WTYPE_TEXT,
	WTYPE_LISTBOX,
	WTYPE_COMBOBOX,
	WTYPE_CUSTOM,
	WTYPE_EDITCTRL,
	WTYPE_TABCTRL
}

[Serializable]
public enum BGLAYOUT_TYPE
{
	Normal,
	Tile
}

[Serializable]
public enum VISIBLITY_TYPE
{
	Visible,
	Hidden
}

[Serializable]
public enum HALIGN_TYPE
{
	Left,
	Right,
	Center
}

[Serializable]
public enum VALIGN_TYPE
{
	Top,
	Bottom,
	Center
}
namespace XorNet.Rose.Controls
{
	[Serializable]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class Control : ID3DX2DObj
    {
		#region Properties
		[Category("Appearance")]
		public VISIBLITY_TYPE Visibility
		{
			get { return m_visibility; }
			set
			{
				m_parent.Editor.UndoRedoManager.Update(new UndoRedo(this, m_visibility, "Visibility", ExternalUpdate)); 
				m_visibility = value;
			}
		}

		[Category("Behavior")]
		public bool Enabled
		{
			get { return m_bEnabled; }
			set
			{
				m_parent.Editor.UndoRedoManager.Update(new UndoRedo(this, m_bEnabled, "Enabled", ExternalUpdate)); 
				m_bEnabled = value;
			}
		}

		[Category("Design")]
		[DXDescription("ID of the control.(ResData.h)")]
		public string Id
		{
			get { return m_szID; }
			set
			{
				if (m_parent.ControlIDList.Contains(value))
				{
					MessageBox.Show("Control ID: \""+value+"\" already exists in current window.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else
				{
					m_parent.ControlIDList.Remove(m_szID);
					m_parent.ControlIDList.Add(value);
					m_parent.Editor.UndoRedoManager.Update(new UndoRedo(this, m_szID, "Id", ExternalUpdate));
					m_szID = value;
				}
			}
		}

		[Category("Design")]
		public CONTROL_TYPE ControlType
		{
			get { return m_type; }
		}

		[Category("Design")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Window Parent
		{
			get { return m_parent; }
			set 
			{ 
				m_parent = value;
				if (m_parent.NoFrame == false && m_bOffset == true)
				{
					if (m_parent.ShowCaption == true)
						m_rectBounds.Y += ResManager.Instance.CaptionOffset;
					m_rectBounds.X += ResManager.Instance.DefaultDeflateRect.X;
					m_rectBounds.Y += ResManager.Instance.DefaultDeflateRect.Y;
				}
				if (m_szID == null || (m_bPasting && m_parent.ControlIDList.Contains(m_szID)))
					m_szID = ResManager.Instance.AddDefaultControl(m_szDefaultID, m_parent.ControlIDList);
				m_bOffset = false;
			}
		}

		[Category("Layout")]
		[DXDescription("Coordinates of the control.")]
		public Point Location
		{
			get { return new Point(m_rectBounds.X, m_rectBounds.Y); }
			set
			{
				m_parent.Editor.UndoRedoManager.Update(new UndoRedo(this, m_rectBounds.Location, "Location", ExternalUpdate)); 
				m_rectBounds.X = value.X; 
				m_rectBounds.Y = value.Y;
			}
		}

		[Category("Layout")]
		public Size Size
		{
			get
			{
				return new Size(m_rectBounds.Width, m_rectBounds.Height);
			}
			set
			{
				m_parent.Editor.UndoRedoManager.Update(new UndoRedo(this, m_rectBounds.Size, "Size", ExternalUpdate));
				m_rectBounds.Width = value.Width;
				m_rectBounds.Height = value.Height;
			}
		}

		[Category("Tooltip")]
		[DXDescription("Tooltip of the control")]
		public string Tooltip
		{
			get { return m_szTooltip; }
			set
			{
				m_parent.Editor.UndoRedoManager.Update(new UndoRedo(this, m_szTooltip, "Tooltip", ExternalUpdate)); 
				m_szTooltip = value;
			}
		}

		[Browsable(false)]
		public string DefaultID
		{
			get { return m_szDefaultID; }
			set { m_szDefaultID = value; }
		}

		[Browsable(false)]
		public bool Pasting
		{
			get { return m_bPasting; }
			set { m_bPasting = value; }
		}

		[Browsable(false)]
		public bool OffsetControl
		{
			get { return m_bOffset; }
			set { m_bOffset = value; }
		}
        #endregion

		#region Variables
		protected Rectangle m_rectBounds;
		protected CONTROL_TYPE m_type;
		protected VISIBLITY_TYPE m_visibility;
		protected string m_szID;
		protected string m_szDefaultID;
		[NonSerialized]
		protected Window m_parent;
		protected Point m_ptMouse;
        protected bool m_bHover;
        protected bool m_bMouseDown;
		protected bool m_bResizing;
		protected bool m_bDragging;
		protected bool m_bEnabled;
		protected bool m_bDraggable;
		protected bool m_bResizable;
		protected Rectangle m_rectBounds2;
		protected string m_szTooltip;
		protected string m_szCaption;
		protected bool m_bShowCaption;
		protected bool m_bOffset;
		protected bool m_bPasting;
        #endregion

        #region Constructors

        /// <summary>
        /// Basic Initializer.
        /// </summary>
        public Control()
        {
			m_visibility = VISIBLITY_TYPE.Visible;
            m_bEnabled = true;
            m_bDraggable = true;
			m_bResizable = true;
			m_bDragging = false;
            m_rectBounds = new Rectangle();
			m_parent = null;
			m_szTooltip = "";
			m_szDefaultID = "WIDC_CONTROL";
			m_bShowCaption = false;
			m_bOffset = true;
			m_bPasting = false;
        }

        /// <summary>
        /// Initializes the control and sets bounds.
        /// </summary>
		/// <param name="rectBounds">Bounds of the control.</param>
		public Control(Rectangle rectBounds)
			: this()
		{
			m_rectBounds = rectBounds;
		}

		public Control(Point ptLocation)
			: this()
		{
			m_rectBounds.Location = ptLocation;
		}

		public Control(ControlProperty resource)
			: this()
		{
			m_rectBounds = new Rectangle(resource.m_rectBounds.Left + ResManager.Instance.DefaultWindowLocation.X, resource.m_rectBounds.Top + ResManager.Instance.DefaultWindowLocation.Y, resource.m_rectBounds.Width, resource.m_rectBounds.Height);
			m_szID = resource.m_szID;
			m_bEnabled = (resource.m_bDisabled == 0);
			m_szTooltip = resource.m_szTooltip;
			m_szCaption = resource.m_szTitle;
			m_bShowCaption = resource.m_style.HasFlag(WindowStyle.WBS_CAPTION);
			m_visibility = VISIBLITY_TYPE.Visible;
			m_type = (CONTROL_TYPE)System.Enum.Parse(typeof(CONTROL_TYPE), resource.m_szType);
			/*
			if (resource.visible == 0)
				m_visibility = VISIBLITY_TYPE.Visible;
			else
				m_visibility = VISIBLITY_TYPE.Hidden;
			*/
		}
        #endregion

        #region Implementation of ID3DX2DObj

        /// <summary>
        /// Draws the control.
        /// </summary>
        /// <param name="pSprite">Pointer to ID3DXSprite</param>
        public virtual void Draw()
		{
            Logging.Error("Draw call for this control is not implemented!");
        }

        /// <summary>
        /// Handles all miscellaneous updates.
        /// </summary>
        public virtual void Update()
        {
        }
        #endregion

        #region Control Functions
        #region Updating Functions
        public virtual void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            if (HitTest(e.Location))
            {
                m_bHover = true;
                OnMouseHover();
            }
            else
            {
                m_bHover = false;
                OnMouseLeave();
            }

            if (m_bMouseDown)
            {
                DoDrag(e.Location);
            }

        }

        public virtual void OnMouseHover()
        {

        }

        public virtual void OnMouseLeave()
        {

        }

        public virtual void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
			m_bMouseDown = true;
			m_ptMouse = e.Location;
			if (OutlineTest(e.Location) && m_bResizable)
				m_bResizing = true;
			m_rectBounds2 = m_rectBounds;
        }

        public virtual void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
		{
			if (m_bDragging || m_bResizing)
			{
				if (m_rectBounds2 == null || m_rectBounds2.IsEmpty)
					m_rectBounds2 = m_rectBounds;

				if (m_rectBounds2.Width != m_rectBounds.Width || m_rectBounds2.Height != m_rectBounds.Height)
					m_parent.Editor.UndoRedoManager.Update(new UndoRedo(this, m_rectBounds2.Size, "Size", ExternalUpdate));

				if (m_rectBounds2.Location.X != m_rectBounds.Location.X || m_rectBounds2.Location.Y != m_rectBounds.Location.Y)
					m_parent.Editor.UndoRedoManager.Update(new UndoRedo(this, m_rectBounds2.Location, "Location", ExternalUpdate));

				m_rectBounds2 = m_rectBounds;
				m_bDragging = false;
				m_bResizing = false;
			}
            m_bMouseDown = false;
        }

        protected virtual void DoDrag(Point ptMouse)
        {
			Point ptDelta = new Point(ptMouse.X - m_ptMouse.X, ptMouse.Y - m_ptMouse.Y);
			
			/*
			bool canUpdate = true;
			if (m_parent != null)
			{
				Rectangle bounds = new Rectangle(m_parent.Location, m_parent.Size);
				if (bounds.X > m_rectBounds.X + ptDelta.X || bounds.Y > m_rectBounds.Y + ptDelta.Y ||
					bounds.X + bounds.Width < m_rectBounds.X + m_rectBounds.Width + ptDelta.X || bounds.Y + bounds.Height < m_rectBounds.Y + m_rectBounds.Height + ptDelta.Y)
				{
					canUpdate = false;
				}
			}
			if (canUpdate)
			*/
			{
				if (m_bResizing)
				{
					Rectangle bounds = CalculateBounds();
					if (bounds.Width <= m_rectBounds.Width + ptDelta.X && bounds.Height <= m_rectBounds.Height + ptDelta.Y)
					{
						m_rectBounds.Width += ptDelta.X;
						m_rectBounds.Height += ptDelta.Y;
					}
				}
				else if (m_bDraggable)
				{
					m_rectBounds.X += ptDelta.X;
					m_rectBounds.Y += ptDelta.Y;
					m_bDragging = true;
				}
			}

			m_ptMouse = ptMouse;
        }
        #endregion
        #region Drawing Functions
        /// <summary>
        /// Draws a rectangle around the control showing its bounds.
        /// </summary>
        public virtual void DrawBounds()
        {
            D3D2DRender.Instance.DrawRect(new Rectangle(m_rectBounds.X - 1, m_rectBounds.Y - 1, m_rectBounds.Width + 2, m_rectBounds.Height + 2), Color.Orange);
            D3D2DRender.Instance.DrawRect(new Rectangle(m_rectBounds.X - 2, m_rectBounds.Y - 2, m_rectBounds.Width + 4, m_rectBounds.Height + 4), Color.Orange);
            DirectX.D3D2DRender.Instance.DrawRect(m_rectBounds, Color.Orange);
        }
        #endregion
		#region Misc Functions
		public bool PointInRect(Point pt, Rectangle rect)
		{
			return pt.X >= (rect.X) && pt.Y >= (rect.Y)
				&& pt.X <= (rect.X + rect.Width)
				&& pt.Y <= (rect.Y + rect.Height);
		}
        public bool HitTest(Point pt)
        {
            return pt.X > (m_rectBounds.X - 3) && pt.Y > (m_rectBounds.Y - 3) 
                && pt.X < (m_rectBounds.X + m_rectBounds.Width + 3) 
                && pt.Y < (m_rectBounds.Y + m_rectBounds.Height + 3);
        }

        public bool OutlineTest(Point pt)
        {
            bool bInOutline = !(pt.X >= m_rectBounds.X && pt.Y >= m_rectBounds.Y
                && pt.X <= (m_rectBounds.X + m_rectBounds.Width)
                && pt.Y <= (m_rectBounds.Y + m_rectBounds.Height));

            return bInOutline;
        }

		protected virtual Rectangle CalculateBounds()
		{
			/*
			Rectangle bounds = D3D2DRender.Instance.Font.MeasureString(null, m_szCaption, Microsoft.DirectX.Direct3D.DrawTextFormat.Left, m_color.ToArgb());
			bounds.Width += 10;
			bounds.Height += 4;
			bounds.X = m_rectBounds.X;
			bounds.Y = m_rectBounds.Y;
			*/
			return new Rectangle(m_rectBounds.X, m_rectBounds.Y, 0, 0); 
		}

		protected void UpdateBounds()
		{
			Rectangle rect = CalculateBounds();
			if (rect.Width > m_rectBounds.Width)
				m_rectBounds.Width = rect.Width;
			if (rect.Height > m_rectBounds.Height)
				m_rectBounds.Height = rect.Height;
		}

		public virtual void Save(ref ControlProperty prop)
		{
			if (!ResManager.Instance.Data.Controls.Keys.Contains(m_szID))
				ResManager.Instance.Data.Controls.Add(m_szID, ResManager.Instance.Data.Controls.Values.LastOrDefault() + 1);

			prop.m_szID = m_szID;
			prop.m_szType = ((System.Enum)m_type).ToString();
			prop.m_szTitle = m_szCaption;
			prop.m_szTooltip = m_szTooltip;

			prop.m_rectBounds = m_rectBounds;
			prop.m_rectBounds.X -= ResManager.Instance.DefaultWindowLocation.X;
			prop.m_rectBounds.Y -= ResManager.Instance.DefaultWindowLocation.Y;
			if (m_parent.NoFrame == false)
			{
				if (m_parent.ShowCaption == true)
					prop.m_rectBounds.Y -= ResManager.Instance.CaptionOffset;
				prop.m_rectBounds.X -= ResManager.Instance.DefaultDeflateRect.X;
				prop.m_rectBounds.Y -= ResManager.Instance.DefaultDeflateRect.Y;
			}
			if (m_bEnabled)
				prop.m_bDisabled = 0;
			else
				prop.m_bDisabled = 1;
			if (m_visibility == VISIBLITY_TYPE.Visible)
				prop.m_bVisible = 1;
			else
				prop.m_bVisible = 0;

			prop.m_style = WindowStyle.WBS_CHILD;
			if (m_bShowCaption && m_type != CONTROL_TYPE.WTYPE_TABCTRL)
				prop.m_style |= WindowStyle.WBS_CAPTION;
		}

		public virtual void OnSerialized()
		{

		}
        #endregion
		#endregion

		#region UndoRedo
		public Object InternalUpdate(Object data, string identifier)
		{
			Object ret = null;
			switch (identifier)
			{
				case "Id":
					ret = m_szID;
					m_szID = (string)data;
					break;
				case "Size":
					ret = m_rectBounds.Size;
					m_rectBounds.Width = ((Size)data).Width;
					m_rectBounds.Height = ((Size)data).Height;
					break;
				case "Location":
					ret = m_rectBounds.Location;
					m_rectBounds.X = ((Point)data).X;
					m_rectBounds.Y = ((Point)data).Y;
					break;
				case "Enabled":
					ret = m_bEnabled;
					m_bEnabled = (bool)data;
					break;
				case "Visibility":
					ret = m_visibility;
					m_visibility = (VISIBLITY_TYPE)data;
					break;
				case "Tooltip":
					ret = m_szTooltip;
					m_szTooltip = (string)data;
					break;
				default:
					break;
			}
			return ret;
		}

		public static Object ExternalUpdate(Object instance, Object data, string identifier)
		{
			return ((Control)instance).InternalUpdate(data, identifier);
		}
		#endregion
    }
}
