using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using XorNet.Rose.Resource;
using System.ComponentModel;

namespace XorNet.Rose.Controls
{
	[Serializable]
	class ListBox : Static
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
		public ListBox(Point pos)
			: base(pos)
		{
			m_szDefaultID = DefaultStrings.ListControl;
			m_bgLayoutType = BGLAYOUT_TYPE.Tile;
			UpdateImage("WndEditTile00.tga");
			m_rectBounds.Width = 128;
			m_rectBounds.Height = 128;
			m_type = CONTROL_TYPE.WTYPE_LISTBOX;
		}

		public ListBox(ControlProperty resource)
			: base(resource)
		{
			m_szDefaultID = DefaultStrings.ListControl;
		}
		#endregion
	}
}
