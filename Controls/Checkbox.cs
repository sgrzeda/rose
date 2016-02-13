using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using XorNet.Rose.DirectX;
using System.ComponentModel;
using XorNet.Rose.Resource;

namespace XorNet.Rose.Controls
{
	[Serializable]
	class Checkbox : Control
	{
		#region Properties
		[Category("Appearance")]
		public string Image
		{
			get
			{
				if (m_pTexture == null)
					return "";
				else
					return m_pTexture.Name;
			}
			set
			{
				UpdateTexture(value);
			}
		}

		[Category("Appearance")]
		[DXDescription("Font Color of the caption")]
		public Color ForeColor
		{
			get { return m_color; }
			set
			{
				m_parent.Editor.UndoRedoManager.Update(this, m_color, "ForeColor", ExternalUpdateDerivative);
				m_color = value;
			}
		}

		[Category("Appearance")]
		[DXDescription("Caption of the control")]
		public virtual string Caption
		{
			get { return m_szCaption; }
			set
			{
				m_parent.Editor.UndoRedoManager.Update(this, m_szCaption, "Caption", ExternalUpdateDerivative);
				m_szCaption = value;
				UpdateBounds();
			}
		}

		[Category("Appearance")]
		[DXDescription("Value of Checkbox")]
		public bool Checked
		{
			get { return m_bChecked; }
			set
			{
				m_parent.Editor.UndoRedoManager.Update(this, m_bChecked, "Checked", ExternalUpdateDerivative); 
				m_bChecked = value;
			}
		}
		#endregion

		#region Variables
		protected bool m_bChecked;
		[NonSerialized]
		protected D3DTexture2D m_pTexture;
		protected Color m_color;
		protected string m_szTexture = "";
		#endregion

		#region Construction
		public Checkbox(Point ptLocation)
			: base(ptLocation)
		{
			m_szDefaultID = DefaultStrings.CheckboxControl;
			m_type = CONTROL_TYPE.WTYPE_BUTTON;
			m_color = Skin.Instance.DefaultColors.CheckboxControl.Caption;
			m_bChecked = false;
			UpdateTexture("ButtCheck.tga");
        }

		public Checkbox(ControlProperty resource)
			: base(resource)
		{
			m_szDefaultID = DefaultStrings.CheckboxControl;
			m_color = Skin.Instance.DefaultColors.CheckboxControl.Caption;
			m_bChecked = false;
			if (resource.m_szTexture.Length > 3)
				UpdateTexture(resource.m_szTexture);
			else
				UpdateTexture("ButtCheck.tga");
		}
		#endregion

		#region Overrides
		protected override Rectangle CalculateBounds()
		{
			Rectangle bounds = D3D2DRender.Instance.Font.MeasureString(null, m_szCaption, Microsoft.DirectX.Direct3D.DrawTextFormat.Left, m_color.ToArgb());
			bounds.Width += 16;
			bounds.X = m_rectBounds.X;
			bounds.Y = m_rectBounds.Y;
			return bounds;
		}

		public override void Draw()
		{
			if (m_szCaption != "")
			{
				if (m_bHover)
					D3D2DRender.Instance.TextOut(new Point(m_rectBounds.Left + 16, m_rectBounds.Top), m_szCaption, Skin.Instance.DefaultColors.CheckboxControl.CaptionHighlighted, Color.Transparent);
				else
					D3D2DRender.Instance.TextOut(new Point(m_rectBounds.Left + 16, m_rectBounds.Top), m_szCaption, m_color, Color.Transparent);
			}

			if (m_pTexture == null)
				return;

			if(m_bEnabled)
			{
				if (!m_bChecked)
				{
					if(m_bHover)
						m_pTexture.DrawFrame(new Point(m_rectBounds.X, m_rectBounds.Y + 1), 0);
					else
						m_pTexture.DrawFrame(new Point(m_rectBounds.X, m_rectBounds.Y + 1), 1);
				}
				else
				{
					if (m_bHover)
						m_pTexture.DrawFrame(new Point(m_rectBounds.X, m_rectBounds.Y + 1), 2);
					else
						m_pTexture.DrawFrame(new Point(m_rectBounds.X, m_rectBounds.Y + 1), 3);
				}
			}
			else
			{
				if(!m_bChecked)
					m_pTexture.DrawFrame(new Point(m_rectBounds.X, m_rectBounds.Y + 1), 4);
				else
					m_pTexture.DrawFrame(new Point(m_rectBounds.X, m_rectBounds.Y + 1), 5);
			}
		}

		public override void Save(ref ControlProperty prop)
		{
			base.Save(ref prop);

			prop.m_style |= WindowStyle.WBS_CHECK;
			prop.m_style |= WindowStyle.WBS_CAPTION;

			prop.m_szTexture = Image;
		}
		#endregion

		#region Functions
		protected void UpdateTexture(string szImage = "")
		{
			if (szImage.Length < 3 || D3DTextureManager.Instance[szImage] == null)
			{
				if (D3DTextureManager.Instance[m_szTexture] == null)
					return;
				szImage = m_szTexture;
			}
			m_szTexture = szImage;
			m_pTexture = D3DTextureManager.Instance[szImage];
			m_pTexture.SetFrames(6);
			//m_rectBounds = new Rectangle(m_rectBounds.X, m_rectBounds.Y, m_pTexture.Size.Width / 4, m_pTexture.Size.Height);
		}
		#endregion

		#region UndoRedo
		public Object InternalUpdateDerivative(Object data, string identifier)
		{
			Object ret = null;
			switch (identifier)
			{
				case "Caption":
					ret = Caption;
					m_szCaption = (string)data;
					break;
				case "ForeColor":
					ret = m_color;
					m_color = (Color)data;
					break;
				case "Checked":
					ret = m_bChecked;
					m_bChecked = (bool)data;
					break;
				default:
					break;
			}
			return ret;
		}

		public static Object ExternalUpdateDerivative(Object instance, Object data, string identifier)
		{
			return ((Checkbox)instance).InternalUpdateDerivative(data, identifier);
		}

		public override void OnSerialized()
		{
			base.OnSerialized();

			UpdateTexture();
		}
		#endregion
	}
}
