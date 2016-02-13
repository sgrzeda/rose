using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XorNet.Collections;
using System.Drawing;

namespace XorNet.Rose.Resource
{
	public enum WindowStyle
	{
		WBS_MINIMIZEBOX = 0x00000001,
		WBS_MAXIMIZEBOX = 0x00000002,
		WBS_HELP = 0x00000004,
		WBS_VIEW = 0x00000008,
		WBS_PIN = 0x00000010,
		WBS_EXTENSION = 0x00000020,
		WBS_THICKFRAME = 0x00000040,
		WBS_TEXT = 0x00000001,
		WBS_SPRITE = 0x00000002,
		WBS_RADIO = 0x00000004,
		WBS_CHECK = 0x00000008,
		WBS_MOVE = 0x00010000,
		WBS_CHILD = 0x00020000,
		WBS_NODRAWFRAME = 0x00040000,
		WBS_MODAL = 0x00080000,
		WBS_MANAGER = 0x00100000,
		WBS_NOFRAME = 0x00200000,
		WBS_SOUND = 0x00400000,
		WBS_CHILDFRAME = 0x00800000,
		WBS_KEY = 0x01000000,
		WBS_CAPTION = 0x02000000,
		WBS_DOCKING = 0x04000000,
		WBS_POPUP = 0x08000000,
		WBS_TOPMOST = 0x10000000,
		WBS_VSCROLL = 0x20000000,
		WBS_HSCROLL = 0x40000000,
		WBS_NOFOCUS = -1,
		//
		WSS_GROUPBOX = 0x00000001,
		WSS_PICTURE = 0x00000002,
		WSS_MONEY = 0x00000004,
		WSS_ALIGNHRIGHT = 0x00000008,
		WSS_ALIGNHCENTER = 0x00000010,
		WSS_ALIGNVBOTTOM = 0x00000020,
		WSS_ALIGNVCENTER = 0x00000040
	}

	/// <summary>
	/// Window resource structure.
	/// </summary>
	public class WindowProperty
	{
		public string m_szID;
		public string m_szTexture;
		public string m_szTitle;
		public string m_szHelp;

		public string m_idTitle;
		public string m_idHelp;

		public Size m_size;
		public int m_bTile;

		public WindowStyle m_style;
		public OrderedDictionary<string, ControlProperty> m_aControls = new OrderedDictionary<string, ControlProperty>();
	}

	/// <summary>
	/// Window control structure.
	/// </summary>
	public class ControlProperty
	{
		public string m_szType;
		public string m_szID;
		public string m_szTexture;
		public string m_szTitle;
		public string m_szTooltip;

		public string m_idTitle;
		public string m_idTooltip;

		public Rectangle m_rectBounds;

		public int m_bVisible;
		public int m_bGroup;
		public int m_bDisabled;
		public int m_bTabstop;
		public int m_bTile;

		public int m_nfontcolor;

		public WindowStyle m_style;
	}
}
