using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using XorNet.Rose.Resource;
using System.Drawing;
using XorNet.Rose.DirectX;

namespace XorNet.Rose.Controls
{
	[Serializable]
	class Combo : Static
	{
		#region Properties
		#endregion

		#region Variables
		[NonSerialized]
		protected D3DTexture2D m_pDownArrowTexture;
		#endregion

		#region Constructors
		public Combo(Point pos)
			: base(pos)
		{
			m_type = CONTROL_TYPE.WTYPE_COMBOBOX;
			m_szDefaultID = DefaultStrings.ComboControl;
			m_bgLayoutType = BGLAYOUT_TYPE.Tile;
			UpdateImage("WndEditTile00.tga");
			m_rectBounds.Width = 110;
			m_rectBounds.Height = 16;
			UpdateArrowTexture("ButtQuickListDn.tga");
		}

		public Combo(ControlProperty resource)
			: base(resource)
		{
			m_szDefaultID = DefaultStrings.ComboControl;
			UpdateArrowTexture("ButtQuickListDn.tga");
		}
		#endregion

		#region Functions
		protected void UpdateArrowTexture(string name = "ButtQuickListDn.tga")
		{
			if (D3DTextureManager.Instance[name] == null)
				return;
			m_pDownArrowTexture = D3DTextureManager.Instance[name];
			m_pDownArrowTexture.SetFrames(4);
		}
		#endregion

		#region Overrides
		public override void Draw()
		{
			base.Draw();

			if (m_pDownArrowTexture == null)
				return;

			Point pt = new Point((m_rectBounds.Right - (m_pDownArrowTexture.Size.Width / 4)) - 3, m_rectBounds.Top);
			if (!m_bEnabled)
			{
				m_pDownArrowTexture.DrawFrame(pt, 3);
			}
			else if (m_bMouseDown)
			{
				m_pDownArrowTexture.DrawFrame(pt, 2);
			}
			else if (m_bHover)
			{
				m_pDownArrowTexture.DrawFrame(pt, 0);
			}
			else
			{
				m_pDownArrowTexture.DrawFrame(pt, 1);
			}
		}

		public override void OnSerialized()
		{
			base.OnSerialized();

			UpdateArrowTexture("ButtQuickListDn.tga");
		}
		#endregion
	}
}
