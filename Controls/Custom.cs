using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using XorNet.Rose.DirectX;
using System.Drawing;
using XorNet.Rose.Resource;

namespace XorNet.Rose.Controls
{
	[Serializable]
	class Custom : Control
	{
		#region Constructors
		public Custom(Point pos)
			: base(pos)
		{
			m_szDefaultID = DefaultStrings.CustomControl;
			m_rectBounds = CalculateBounds();
			m_rectBounds.Width = 16;
			m_rectBounds.Height = 16;
			m_type = CONTROL_TYPE.WTYPE_CUSTOM;
		}

		public Custom(ControlProperty resource)
			: base(resource)
		{
			m_szDefaultID = DefaultStrings.CustomControl;
		}
		#endregion

		#region Overrides
		public override void Draw()
		{
			D3D2DRender.Instance.DrawFilledRect(m_rectBounds, Color.FromArgb(128, 128, 128), null);
		}
		#endregion
	}
}
