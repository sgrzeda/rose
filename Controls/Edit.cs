using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing;
using XorNet.Rose.Resource;
using Microsoft.DirectX.Direct3D;
using XorNet.Rose.DirectX;

namespace XorNet.Rose.Controls
{
	[Serializable]
	class Edit : Static
	{
		#region Properties
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

		#region Constructors
		public Edit(Point pos)
			: base(pos)
		{
			m_szDefaultID = DefaultStrings.EditControl;
			m_type = CONTROL_TYPE.WTYPE_EDITCTRL;
			m_bgLayoutType = BGLAYOUT_TYPE.Tile;
			UpdateImage("WndEditTile00.tga");
			m_rectBounds.Width = 110;
			m_rectBounds.Height = 16;
		}

		public Edit(ControlProperty resource)
			: base(resource)
		{
			m_szDefaultID = DefaultStrings.EditControl;
		}
		#endregion
	}
}
