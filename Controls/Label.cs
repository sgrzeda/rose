using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using XorNet.Rose.DirectX;
using XorNet.Rose.Resource;
using Microsoft.DirectX.Direct3D;

namespace XorNet.Rose.Controls
{
	[Serializable]
	class Label : Static
	{
		#region Properties
		[Category("Appearance")]
		[DXDescription("Font Color of the caption")]
		public Color ForeColor
		{
			get { return m_color; }
			set
			{
				m_parent.Editor.UndoRedoManager.Update(this, m_color, "ForeColor", ExternalUpdateDerivative2);
				m_color = value;
			}
		}

		[Category("Appearance")]
		[DXDescription("Shadow color of caption")]
		public Color ShadowColor
		{
			get { return m_shadowColor; }
			set
			{
				m_parent.Editor.UndoRedoManager.Update(this, m_shadowColor, "ShadowColor", ExternalUpdateDerivative2);
				m_shadowColor = value;
			}
		}

		[Category("Appearance")]
		[DXDescription("Vertical Alignment of caption")]
		public VALIGN_TYPE VerticalAlignment
		{
			get { return m_alignVert; }
			set
			{
				m_parent.Editor.UndoRedoManager.Update(this, m_alignVert, "VerticalAlignment", ExternalUpdateDerivative2); 
				m_alignVert = value;
			}
		}

		[Category("Appearance")]
		[DXDescription("Horizontal Alignment of caption")]
		public HALIGN_TYPE HorizontalAlignment
		{
			get { return m_alignHori; }
			set
			{
				m_parent.Editor.UndoRedoManager.Update(this, m_alignHori, "HorizontalAlignment", ExternalUpdateDerivative2); 
				m_alignHori = value;
			}
		}

		[Category("Appearance")]
		[DXDescription("Caption of the control")]
		public virtual string Caption
		{
			get { return m_szCaption; }
			set
			{
				m_parent.Editor.UndoRedoManager.Update(this, m_szCaption, "Caption", ExternalUpdateDerivative2); 
				m_szCaption = value; 
				UpdateBounds();
			}
		}
		[Category("Appearance")]
		public bool VerticalScrollBar
		{
			get { return m_bVertScrollBar; }
			set
			{
				m_parent.Editor.UndoRedoManager.Update(this, m_bVertScrollBar, "VerticalScrollBar", ExternalUpdateDerivative);
				m_bVertScrollBar = value;
			}
		}
		#endregion

		#region Variables
		protected Color m_color;
		protected Color m_shadowColor;
		protected VALIGN_TYPE m_alignVert = VALIGN_TYPE.Top;
		protected HALIGN_TYPE m_alignHori = HALIGN_TYPE.Left;
		#endregion

		#region Constructors
		public Label(Point pos)
			: base(pos)
		{
			m_szCaption = DefaultCaptions.StaticControl;
			m_shadowColor = Color.Transparent;
			m_color = Skin.Instance.DefaultColors.StaticControl.Caption;
			m_rectBounds = CalculateBounds();
			m_bShowCaption = true;
			//m_type = CONTROL_TYPE.LABEL;
		}

		public Label(ControlProperty resource)
			: base(resource)
		{
			m_shadowColor = Color.Transparent;
			m_color = Skin.Instance.DefaultColors.StaticControl.Caption;
			//m_rectBounds = CalculateBounds();
			if (resource.m_style.HasFlag(WindowStyle.WSS_ALIGNHCENTER))
				m_alignHori = HALIGN_TYPE.Center;
			else if (resource.m_style.HasFlag(WindowStyle.WSS_ALIGNHRIGHT))
				m_alignHori = HALIGN_TYPE.Right;
			else
				m_alignHori = HALIGN_TYPE.Left;

			if (resource.m_style.HasFlag(WindowStyle.WSS_ALIGNVBOTTOM))
				m_alignVert = VALIGN_TYPE.Bottom;
			else if (resource.m_style.HasFlag(WindowStyle.WSS_ALIGNVCENTER))
				m_alignVert = VALIGN_TYPE.Center;
			else
				m_alignVert = VALIGN_TYPE.Top;

			if (resource.m_style.HasFlag(WindowStyle.WSS_GROUPBOX))
				m_color = Skin.Instance.DefaultColors.StaticControl.CaptionText;
		}
		#endregion

		#region Overrides
		public override void Draw()
		{
			base.Draw();

			if (m_szCaption != "" && m_bShowCaption)
			{
				DrawTextFormat format = DrawTextFormat.None;
				if (m_alignHori == HALIGN_TYPE.Center)
					format = DrawTextFormat.Center;
				else if (m_alignHori == HALIGN_TYPE.Left)
					format = DrawTextFormat.Left;
				else if (m_alignHori == HALIGN_TYPE.Right)
					format = DrawTextFormat.Right;

				if (m_alignVert == VALIGN_TYPE.Center)
					format |= DrawTextFormat.VerticalCenter;
				else if (m_alignVert == VALIGN_TYPE.Top)
					format |= DrawTextFormat.Top;
				else if (m_alignVert == VALIGN_TYPE.Bottom)
					format |= DrawTextFormat.Bottom;

				Rectangle drawrect = m_rectBounds;
				if (m_alignHori == HALIGN_TYPE.Left)
					drawrect.X += 2;
				if (m_alignVert == VALIGN_TYPE.Top)
					drawrect.Y += 2;
				D3D2DRender.Instance.TextOut(drawrect, m_szCaption.Replace("\\n", "\n"), m_color, m_shadowColor, format | DrawTextFormat.WordBreak);
			}
		}

		protected override Rectangle CalculateBounds()
		{
			DrawTextFormat format = DrawTextFormat.None;
			if (m_alignHori == HALIGN_TYPE.Center)
				format = DrawTextFormat.Center;
			else if (m_alignHori == HALIGN_TYPE.Left)
				format = DrawTextFormat.Left;
			else if (m_alignHori == HALIGN_TYPE.Right)
				format = DrawTextFormat.Right;
			Rectangle bounds = base.CalculateBounds();
			Rectangle bounds2 = D3D2DRender.Instance.Font.MeasureString(null, m_szCaption.Replace("\\n", "\n"), format | DrawTextFormat.WordBreak, m_color.ToArgb());
			if (m_alignHori == HALIGN_TYPE.Left)
				bounds2.Width += 4;
			bounds2.Height += 4;

			if (bounds2.Width > bounds.Width)
				bounds.Width = bounds2.Width;

			if (bounds2.Height > bounds.Height)
				bounds.Height = bounds2.Height;
			return bounds;
		}

		public override void Save(ref ControlProperty prop)
		{
			base.Save(ref prop);

			prop.m_style |= WindowStyle.WBS_CAPTION;
			if (m_alignHori == HALIGN_TYPE.Center)
				prop.m_style |= WindowStyle.WSS_ALIGNHCENTER;
			else if (m_alignHori == HALIGN_TYPE.Right)
				prop.m_style |= WindowStyle.WSS_ALIGNHRIGHT;
			if (m_alignVert == VALIGN_TYPE.Center)
				prop.m_style |= WindowStyle.WSS_ALIGNVCENTER;
			else if (m_alignVert == VALIGN_TYPE.Bottom)
				prop.m_style |= WindowStyle.WSS_ALIGNVBOTTOM;

			//prop.m_fontcolor = m_color;
			if (m_color == Skin.Instance.DefaultColors.StaticControl.CaptionText)
				prop.m_style |= WindowStyle.WBS_TEXT;
		}
		#endregion

		#region UndoRedo
		public Object InternalUpdateDerivative2(Object data, string identifier)
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
				case "ShadowColor":
					ret = m_shadowColor;
					m_shadowColor = (Color)data;
					break;
				case "VerticalAlignment":
					ret = m_alignVert;
					m_alignVert = (VALIGN_TYPE)data;
					break;
				case "HorizontalAlignment":
					ret = m_alignHori;
					m_alignHori = (HALIGN_TYPE)data;
					break;
				default:
					break;
			}
			return ret;
		}

		public static Object ExternalUpdateDerivative2(Object instance, Object data, string identifier)
		{
			return ((Label)instance).InternalUpdateDerivative2(data, identifier);
		}
		#endregion
	}
}
