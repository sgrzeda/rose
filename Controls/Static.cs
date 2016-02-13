using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using XorNet.Rose.DirectX;
using XorNet.Rose.Resource;
using Microsoft.DirectX.Direct3D;

namespace XorNet.Rose.Controls
{
	[Serializable]
	class Static : Control
	{
		#region Properties
		[Category("Appearance")]
		public string BackgroundImage
		{
			get
			{
				if (m_bgLayoutType == BGLAYOUT_TYPE.Tile)
				{
					return m_szTilebase;
				}
				else
				{
					if (m_pTexture == null)
						return "";
					else
						return m_pTexture.Name;
				}
			}
			set
			{
				m_parent.Editor.UndoRedoManager.Update(this, BackgroundImage, "BackgroundImage", ExternalUpdateDerivative);
				UpdateImage(value);
			}
		}

		[Category("Appearance")]
		public BGLAYOUT_TYPE BackgroundLayout
		{
			get { return m_bgLayoutType; }
			set
			{
				m_parent.Editor.UndoRedoManager.Update(this, m_bgLayoutType, "BackgroundLayout", ExternalUpdateDerivative);
				m_bgLayoutType = value;
				UpdateImage();
			}
		}
		#endregion

		#region Variables
		protected string m_szTilebase = "WndTile00.tga";
		protected BGLAYOUT_TYPE m_bgLayoutType = BGLAYOUT_TYPE.Normal;
		[NonSerialized]
		protected D3DTexture2D m_pTexture = null;
		[NonSerialized]
		protected D3DTexture2D[] m_tileTextures = null;
		[NonSerialized]
		protected static D3DTexture2D m_texScrollUp = D3DTextureManager.Instance["ButtVScrUp.tga"];
		[NonSerialized]
		protected static D3DTexture2D m_texScrollDown = D3DTextureManager.Instance["ButtVScrDown.tga"];
		[NonSerialized]
		protected static D3DTexture2D m_texScrollBar = D3DTextureManager.Instance["ButtVScrBar.tga"];
		protected bool m_bVertScrollBar = false;
		protected string m_szTexture = "";
		#endregion 

		#region Constructors
		public Static(Point pos)
			: base(pos)
		{
			m_szDefaultID = DefaultStrings.StaticControl;
			m_type = CONTROL_TYPE.WTYPE_STATIC;
			m_bgLayoutType = BGLAYOUT_TYPE.Tile;
			UpdateImage("WndEditTile00.tga");
			m_rectBounds.Width = 32;
			m_rectBounds.Height = 32;

			m_texScrollUp.SetFrames(4);
			m_texScrollDown.SetFrames(4);
		}

		public Static(ControlProperty resource)
			: base(resource)
		{
			m_szDefaultID = DefaultStrings.StaticControl;
			if (resource.m_bTile != 0)
				m_bgLayoutType = BGLAYOUT_TYPE.Tile;
			else
				m_bgLayoutType = BGLAYOUT_TYPE.Normal;
			if (resource.m_szTexture.Length > 3)
			{
				UpdateImage(resource.m_szTexture);
			}
			else
			{
				m_pTexture = null;
				m_bgLayoutType = BGLAYOUT_TYPE.Normal;
			}
			m_bVertScrollBar = resource.m_style.HasFlag(WindowStyle.WBS_VSCROLL);

			m_texScrollUp.SetFrames(4);
			m_texScrollDown.SetFrames(4);
		}
		#endregion

		#region Functions
		protected void UpdateImage(string szImage = "")
		{
			if (m_bgLayoutType == BGLAYOUT_TYPE.Tile)
			{
				if (szImage.Length < 6)
					szImage = m_szTilebase;
				else
					m_szTilebase = szImage;

				string szPart1 = szImage.Substring(0, szImage.Length - 6);
				string szPart2 = szImage.Substring(szImage.Length - 4);

				if (m_tileTextures == null)
					m_tileTextures = new D3DTexture2D[9];
				for (int i = 0; i < 9; i++)
				{
					string sztexture = szPart1 + String.Format("{0:00}", i) + szPart2;
					if (D3DTextureManager.Instance[sztexture] == null)
						return;
					m_tileTextures[i] = D3DTextureManager.Instance[sztexture];
				}
			}
			else
			{
				if (szImage == null || szImage.Length < 2)
				{
					m_szTexture = "";
					m_pTexture = null;
					return;
				}
				if (szImage.Length < 3 || D3DTextureManager.Instance[szImage] == null)
				{
					if (D3DTextureManager.Instance[m_szTexture] == null)
						return;
					szImage = m_szTexture;
				}
				m_szTexture = szImage;
				m_pTexture = D3DTextureManager.Instance[szImage];
				if (m_rectBounds.Width < m_pTexture.Size.Width)
					m_rectBounds.Width = m_pTexture.Size.Width;
				if (m_rectBounds.Height < m_pTexture.Size.Height)
					m_rectBounds.Height = m_pTexture.Size.Height;
			}
		}
		#endregion

		#region Overrides
		public override void OnSerialized()
		{
			base.OnSerialized();

			UpdateImage();
		}

		public override void Draw()
		{
			Rectangle tmpRect = m_rectBounds;
			if (m_bVertScrollBar)
				tmpRect.Width -= 15;

			if (m_bgLayoutType == BGLAYOUT_TYPE.Tile)
			{
				if (m_tileTextures[0] != null)
				{
					// header
					m_tileTextures[0].Draw(new Point(tmpRect.X, tmpRect.Y));
					m_tileTextures[1].Draw(new Point(tmpRect.X + m_tileTextures[0].Size.Width, tmpRect.Y), new Rectangle(0, 0, tmpRect.Width - m_tileTextures[0].Size.Width - m_tileTextures[2].Size.Width, m_tileTextures[2].Size.Height));
					m_tileTextures[2].Draw(new Point((tmpRect.X + tmpRect.Width) - m_tileTextures[2].Size.Width, tmpRect.Y));

					// body
					m_tileTextures[3].Draw(new Point(tmpRect.X, tmpRect.Y + m_tileTextures[0].Size.Height), new Rectangle(0, 0, m_tileTextures[3].Size.Width, tmpRect.Height - m_tileTextures[0].Size.Height - m_tileTextures[6].Size.Height));
					m_tileTextures[4].Draw(new Point(tmpRect.X + m_tileTextures[3].Size.Width, tmpRect.Y + m_tileTextures[1].Size.Height), new Rectangle(0, 0, tmpRect.Width - m_tileTextures[5].Size.Width - m_tileTextures[3].Size.Width, tmpRect.Height - m_tileTextures[0].Size.Height - m_tileTextures[6].Size.Height));
					m_tileTextures[5].Draw(new Point((tmpRect.X + tmpRect.Width) - m_tileTextures[5].Size.Width, tmpRect.Y + m_tileTextures[2].Size.Height), new Rectangle(0, 0, m_tileTextures[5].Size.Width, tmpRect.Height - m_tileTextures[0].Size.Height - m_tileTextures[6].Size.Height));

					// bottom
					m_tileTextures[6].Draw(new Point(tmpRect.X, (tmpRect.Y + tmpRect.Height) - m_tileTextures[6].Size.Height));
					m_tileTextures[7].Draw(new Point(tmpRect.X + m_tileTextures[6].Size.Width, (tmpRect.Y + tmpRect.Height) - m_tileTextures[7].Size.Height), new Rectangle(0, 0, tmpRect.Width - m_tileTextures[6].Size.Width - m_tileTextures[8].Size.Width, m_tileTextures[7].Size.Height));
					m_tileTextures[8].Draw(new Point((tmpRect.X + tmpRect.Width) - m_tileTextures[8].Size.Width, (tmpRect.Y + tmpRect.Height) - m_tileTextures[8].Size.Height));
				}
			}
			else
			{
				if (m_pTexture != null)
					m_pTexture.Draw(new Point(tmpRect.Left, tmpRect.Top));
			}

			if (m_bVertScrollBar)
			{
				if (m_texScrollUp == null || m_texScrollDown == null || m_texScrollBar == null)
					return;

				if(Skin.Instance.Version >= 2)
					m_texScrollBar.Draw(new Point(m_rectBounds.Right - m_texScrollBar.Size.Width + 2, m_rectBounds.Top + m_texScrollUp.Size.Height), new Rectangle(0, 0, m_texScrollBar.Size.Width, m_rectBounds.Height - m_texScrollUp.Size.Height - m_texScrollDown.Size.Height));
				else
					m_texScrollBar.Draw(new Point(m_rectBounds.Right - m_texScrollBar.Size.Width, m_rectBounds.Top + m_texScrollUp.Size.Height), new Rectangle(0, 0, m_texScrollBar.Size.Width, m_rectBounds.Height - m_texScrollUp.Size.Height - m_texScrollDown.Size.Height));

				Rectangle rect = new Rectangle(m_rectBounds.Right - (m_texScrollUp.Size.Width / 4), m_rectBounds.Top, (m_texScrollUp.Size.Width / 4), m_texScrollUp.Size.Height);
				Point pt = new Point(m_rectBounds.Right - (m_texScrollUp.Size.Width / 4), m_rectBounds.Top);
				if (!m_bEnabled)
				{
					m_texScrollUp.DrawFrame(pt, 3);
				}
				else if (m_bMouseDown && PointInRect(m_parent.MousePoint, rect))
				{
					m_texScrollUp.DrawFrame(pt, 2);
				}
				else if (m_bHover && PointInRect(m_parent.MousePoint, rect))
				{
					m_texScrollUp.DrawFrame(pt, 0);
				}
				else
				{
					m_texScrollUp.DrawFrame(pt, 1);
				}

				if (m_texScrollDown == null)
					return;

				rect = new Rectangle(m_rectBounds.Right - (m_texScrollUp.Size.Width / 4), m_rectBounds.Bottom - m_texScrollUp.Size.Height, (m_texScrollUp.Size.Width / 4), m_texScrollUp.Size.Height);
				pt = new Point(m_rectBounds.Right - (m_texScrollUp.Size.Width / 4), m_rectBounds.Bottom - m_texScrollUp.Size.Height);
				if (!m_bEnabled)
				{
					m_texScrollDown.DrawFrame(pt, 3);
				}
				else if (m_bMouseDown && PointInRect(m_parent.MousePoint, rect))
				{
					m_texScrollDown.DrawFrame(pt, 2);
				}
				else if (m_bHover && PointInRect(m_parent.MousePoint, rect))
				{
					m_texScrollDown.DrawFrame(pt, 0);
				}
				else
				{
					m_texScrollDown.DrawFrame(pt, 1);
				}
			}
		}

		protected override Rectangle CalculateBounds()
		{
			if (m_bgLayoutType == BGLAYOUT_TYPE.Tile && m_tileTextures[0] != null)
				return new Rectangle(new Point(Location.X, Location.Y), new Size(m_tileTextures[0].Size.Width * 2, m_tileTextures[0].Size.Height * 2));
			else if (m_pTexture != null)
				return new Rectangle(new Point(Location.X, Location.Y), m_pTexture.Size);
			else
				return base.CalculateBounds();
		}

		public override void Save(ref ControlProperty prop)
		{
			base.Save(ref prop);

			prop.m_szTexture = BackgroundImage;
			if(m_bgLayoutType == BGLAYOUT_TYPE.Tile)
				prop.m_bTile = 1;
			else
				prop.m_bTile = 0;
			if (m_bVertScrollBar)
				prop.m_style |= WindowStyle.WBS_VSCROLL;
		}
		#endregion

		#region UndoRedo
		public Object InternalUpdateDerivative(Object data, string identifier)
		{
			Object ret = null;
			switch (identifier)
			{
				case "BackgroundImage":
					ret = BackgroundImage;
					UpdateImage((string)data);
					break;
				case "BackgroundLayout":
					ret = m_bgLayoutType;
					m_bgLayoutType = (BGLAYOUT_TYPE)data;
					break;
				case "VerticalScrollBar":
					ret = m_bVertScrollBar;
					m_bVertScrollBar = (bool)data;
					break;
				default:
					break;
			}
			return ret;
		}

		public static Object ExternalUpdateDerivative(Object instance, Object data, string identifier)
		{
			return ((Static)instance).InternalUpdateDerivative(data, identifier);
		}
		#endregion
	}
}