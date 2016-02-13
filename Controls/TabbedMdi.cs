using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XorNet.Rose.Resource;
using System.Drawing;
using System.ComponentModel;

namespace XorNet.Rose.Controls
{
	[Serializable]
	class TabbedMdi : Static
	{

		#region Properties
		[Category("Appearance")]
		[DXDescription("Vertical Alignment of tab caption")]
		public VALIGN_TYPE VerticalAlignment
		{
			get { return m_alignVert; }
			set
			{
				//m_parent.Editor.UndoRedoManager.Update(this, m_alignVert, "VerticalAlignment", ExternalUpdateDerivative2);
				m_alignVert = value;
			}
		}

		[Category("Appearance")]
		[DXDescription("Horizontal Alignment of tab caption")]
		public HALIGN_TYPE HorizontalAlignment
		{
			get { return m_alignHori; }
			set
			{
				//m_parent.Editor.UndoRedoManager.Update(this, m_alignHori, "HorizontalAlignment", ExternalUpdateDerivative2);
				m_alignHori = value;
			}
		}
		#endregion

		#region Variables
		protected VALIGN_TYPE m_alignVert = VALIGN_TYPE.Top;
		protected HALIGN_TYPE m_alignHori = HALIGN_TYPE.Left;
		#endregion

		#region Constructors
		public TabbedMdi(Point pos)
			: base(pos)
		{
			m_szDefaultID = DefaultStrings.TabControl;
			m_type = CONTROL_TYPE.WTYPE_TABCTRL;
			m_bgLayoutType = BGLAYOUT_TYPE.Tile;
			UpdateImage("WndEditTile00.tga");
			m_rectBounds.Width = 128;
			m_rectBounds.Height = 128;
			m_bShowCaption = false;
		}

		public TabbedMdi(ControlProperty resource)
			: base(resource)
		{
			m_szDefaultID = DefaultStrings.TabControl;
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
			m_bShowCaption = false;
		}
		#endregion

		#region Overrides
		public override void Save(ref ControlProperty prop)
		{
			base.Save(ref prop);

			if (m_alignHori == HALIGN_TYPE.Center)
				prop.m_style |= WindowStyle.WSS_ALIGNHCENTER;
			else if (m_alignHori == HALIGN_TYPE.Right)
				prop.m_style |= WindowStyle.WSS_ALIGNHRIGHT;
			if (m_alignVert == VALIGN_TYPE.Center)
				prop.m_style |= WindowStyle.WSS_ALIGNVCENTER;
			else if (m_alignVert == VALIGN_TYPE.Bottom)
				prop.m_style |= WindowStyle.WSS_ALIGNVBOTTOM;
		}
		#endregion
	}
}
