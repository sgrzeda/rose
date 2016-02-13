#if XNA
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = System.Drawing.Color;
using Point = System.Drawing.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Rose.Controls
{
    /* Disambiguation */
    using GdiColor = Color;
    using XnaColor = Microsoft.Xna.Framework.Color;

    internal class Window : Control
    {
        private readonly System.Windows.Forms.Control owner;
        private readonly Texture2D[] tiles;

        private string[] tileFiles =
            {
                "WndTile00.tga",
                "WndTile01.tga",
                "WndTile02.tga",
                "WndTile03.tga",
                "WndTile04.tga",
                "WndTile05.tga",
                "WndTile06.tga",
                "WndTile07.tga",
                "WndTile08.tga",
                "WndTile09.tga",
                "WndTile10.tga",
                "WndTile11.tga"
            };

        private string title;

        private Window()
        {
            type = ControlType.Window;
            tiles = new Texture2D[12];
            SetTiles();
        }

        public Window(Rectangle bounds, System.Windows.Forms.Control owner)
            : this()
        {
            Bounds = bounds;
            this.owner = owner;
        }

        [Category("Main")]
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                owner.Text = title;
            }
        }

        [Category("Appearance")]
        public string[] Tiles
        {
            get { return tileFiles; }
            set
            {
                tileFiles = value;
                SetTiles();
            }
        }

        private void SetTiles()
        {
            for (int i = 0; i < 12; i++)
            {
                Texture2D texture = XnaResourceManager.XnaTexture[tileFiles[i]];
                if (texture != null)
                    tiles[i] = texture;
            }
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // header
            spriteBatch.Draw(tiles[0], new Vector2(Bounds.X, Bounds.Y),
                             new Rectangle(0, 0, tiles[0].Width, tiles[0].Height), XnaColor.White, 0.0f, Vector2.Zero,
                             1.0f, SpriteEffects.None, 1.0f);
            spriteBatch.Draw(tiles[1], new Vector2(Bounds.X + tiles[0].Width, Bounds.Y),
                             new Rectangle(0, 0, Bounds.Width - tiles[0].Width - tiles[2].Width, tiles[2].Height),
                             XnaColor.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
            spriteBatch.Draw(tiles[2], new Vector2((Bounds.X + Bounds.Width) - tiles[2].Width, Bounds.Y),
                             new Rectangle(0, 0, tiles[2].Width, tiles[2].Height), XnaColor.White, 0.0f, Vector2.Zero,
                             1.0f, SpriteEffects.None, 1.0f);

            // header(bottom)
            spriteBatch.Draw(tiles[3], new Vector2(Bounds.X, Bounds.Y + tiles[0].Height),
                             new Rectangle(0, 0, tiles[3].Width, tiles[3].Height), XnaColor.White, 0.0f, Vector2.Zero,
                             1.0f, SpriteEffects.None, 1.0f);
            spriteBatch.Draw(tiles[4], new Vector2(Bounds.X + tiles[3].Width, Bounds.Y + tiles[1].Height),
                             new Rectangle(0, 0, Bounds.Width - tiles[3].Width - tiles[5].Width, tiles[5].Height),
                             XnaColor.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
            spriteBatch.Draw(tiles[5],
                             new Vector2((Bounds.X + Bounds.Width) - tiles[5].Width, Bounds.Y + tiles[0].Height),
                             new Rectangle(0, 0, tiles[5].Width, tiles[5].Height), XnaColor.White, 0.0f, Vector2.Zero,
                             1.0f, SpriteEffects.None, 1.0f);

            // body
            spriteBatch.Draw(tiles[6], new Vector2(Bounds.X, Bounds.Y + tiles[0].Height + tiles[3].Height),
                             new Rectangle(0, 0, tiles[6].Width,
                                           Bounds.Height - tiles[0].Height - tiles[3].Height - tiles[9].Height),
                             XnaColor.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
            spriteBatch.Draw(tiles[7],
                             new Vector2(Bounds.X + tiles[6].Width, Bounds.Y + tiles[0].Height + tiles[3].Height),
                             new Rectangle(0, 0, Bounds.Width - tiles[8].Width - tiles[6].Width,
                                           Bounds.Height - tiles[0].Height - tiles[3].Height - tiles[9].Height),
                             XnaColor.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
            spriteBatch.Draw(tiles[8],
                             new Vector2((Bounds.X + Bounds.Width) - tiles[6].Width,
                                         Bounds.Y + tiles[0].Height + tiles[3].Height),
                             new Rectangle(0, 0, tiles[8].Width,
                                           Bounds.Height - tiles[0].Height - tiles[3].Height - tiles[9].Height),
                             XnaColor.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);

            // bottom
            spriteBatch.Draw(tiles[9], new Vector2(Bounds.X, (Bounds.Y + Bounds.Height) - tiles[9].Height),
                             new Rectangle(0, 0, tiles[9].Width, tiles[9].Height), XnaColor.White, 0.0f, Vector2.Zero,
                             1.0f, SpriteEffects.None, 1.0f);
            spriteBatch.Draw(tiles[10],
                             new Vector2(Bounds.X + tiles[9].Width, (Bounds.Y + Bounds.Height) - tiles[9].Height),
                             new Rectangle(0, 0, Bounds.Width - tiles[9].Width - tiles[11].Width, tiles[10].Height),
                             XnaColor.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
            spriteBatch.Draw(tiles[11],
                             new Vector2((Bounds.X + Bounds.Width) - tiles[9].Width,
                                         (Bounds.Y + Bounds.Height) - tiles[9].Height),
                             new Rectangle(0, 0, tiles[11].Width, tiles[11].Height), XnaColor.White, 0.0f, Vector2.Zero,
                             1.0f, SpriteEffects.None, 1.0f);
            base.Draw(spriteBatch);
        }

        public override void DoDrag(Point mousePos)
        {
        }
    }
}
#endif

#if DIRECTX
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XorNet.Rose.DirectX;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Drawing;
using System.ComponentModel;
using XorNet.Rose.Resource;
using System.Collections.ObjectModel;

namespace XorNet.Rose.Controls
{
	[Serializable]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class Window : ID3DX2DObj
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
				Editor.UndoRedoManager.Update(this, BackgroundImage, "BackgroundImage", ExternalUpdate);
				UpdateImage(value);
			}
		}

		[Category("Appearance")]
		public BGLAYOUT_TYPE BackgroundLayout
		{
			get { return m_bgLayoutType; }
			set
			{
				Editor.UndoRedoManager.Update(this, m_bgLayoutType, "BackgroundLayout", ExternalUpdate);
				m_bgLayoutType = value;
				UpdateImage();
			}
		}

		[Category("Appearance")]
		[Browsable(false)]
		public Color ForeColor
		{
			get { return m_color; }
			set { m_color = value; }
		}

		[Category("Appearance")]
		public Color OutlineColor
		{
			get { return m_colorOutline; }
			set { m_colorOutline = value; }
		}

		[Category("Appearance")]
		[DXDescription("Caption of the window")]
		public virtual string Caption
		{
			get { return m_szCaption; }
			set
			{
				Editor.UndoRedoManager.Update(this, m_szCaption, "Caption", ExternalUpdate);
				m_szCaption = value;
			}
		}

		[Category("Appearance")]
		[DXDescription("Help info of the window")]
		public virtual string Help
		{
			get { return m_szHelp; }
			set
			{
				Editor.UndoRedoManager.Update(this, m_szHelp, "Help", ExternalUpdate);
				m_szHelp = value;
			}
		}

		[Category("Behavior")]
		public bool ShowCaption
		{
			get { return m_bShowCaption; }
			set
			{
				if (m_bShowCaption == false && value == true)
				{
					foreach (Control ctrl in m_children)
					{
						ctrl.Location = new Point(ctrl.Location.X, ctrl.Location.Y + 18);
					}
				}
				else if (m_bShowCaption == true && value == false)
				{
					foreach (Control ctrl in m_children)
					{
						ctrl.Location = new Point(ctrl.Location.X, ctrl.Location.Y - 18);
					}
				}
				m_bShowCaption = value;
			}
		}

		[Category("Behavior")]
		public bool SaveBackgroundImage
		{
			get { return m_bBackgroundImage; }
			set { m_bBackgroundImage = value; }
		}

		[Category("Behavior")]
		public bool Sizable
		{
			get { return m_bSizable; }
			set { m_bSizable = value; }
		}

		[Category("Behavior")]
		public bool NoFrame
		{
			get { return m_bNoFrame; }
			set { m_bNoFrame = value; }
		}

		[Category("Behavior")]
		public bool NoDrawFrame
		{
			get { return m_bNoDrawFrame; }
			set { m_bNoDrawFrame = value; }
		}

		[Category("Behavior")]
		public bool Movable
		{
			get { return m_bMovable; }
			set { m_bMovable = value; }
		}

		[Category("Behavior")]
		public bool TopMost
		{
			get { return m_bTopMost; }
			set { m_bTopMost = value; }
		}

		[Category("Behavior")]
		public bool Popup
		{
			get { return m_bPopup; }
			set { m_bPopup = value; }
		}

		[Category("Behavior")]
		public bool NoFocus
		{
			get { return m_bNoFocus; }
			set { m_bNoFocus = value; }
		}

		[Category("Behavior")]
		public bool VerticalScrollBar
		{
			get { return m_bVertScrollBar; }
			set { m_bVertScrollBar = value; }
		}

		[Category("Behavior")]
		public bool HorizontalScrollBar
		{
			get { return m_bHoriScrollBar; }
			set { m_bHoriScrollBar = value; }
		}

		[Category("Design")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Control[] Children
		{
			get { return m_children.ToArray(); }
			set
			{
				m_children = value.ToList();
			}
		}

		[Category("Design")]
		[DXDescription("ID of the window.(ResData.h)")]
		public string Id
		{
			get { return m_szID; }
			set
			{
				if (ResManager.Instance.Data.Apps.Keys.Contains(value))
				{
					System.Windows.Forms.MessageBox.Show("Window ID: \"" + value + "\" already exists in current window.", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
				}
				else
				{
					ResManager.Instance.Data.Apps.Remove(m_szID);
					Editor.UndoRedoManager.Update(this, m_szID, "Id", ExternalUpdate);
					m_szID = value;
					m_editor.Text = m_szID;
					if (m_editor.Mainform.TabbedView1.ActiveDocument != null)
						m_editor.Mainform.TabbedView1.ActiveDocument.Caption = m_szID;
				}
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
				Editor.UndoRedoManager.Update(this, m_rectBounds.Size, "Size", ExternalUpdate);
				m_rectBounds.Width = value.Width;
				m_rectBounds.Height = value.Height;
			}
		}

		[Browsable(false)]
		public EditorWindow Editor
		{
			get
			{
				return m_editor;
			}
		}

		[Browsable(false)]
		public Point Location
		{
			get { return new Point(m_rectBounds.X, m_rectBounds.Y); }
			set { m_rectBounds.X = value.X; m_rectBounds.Y = value.Y; }
		}

		[Browsable(false)]
		public List<string> ControlIDList
		{
			get { return m_controlIDList; }
			set { m_controlIDList = value; }
		}

		[Browsable(false)]
		public Point MousePoint
		{
			get { return m_ptMouse; }
			set { m_ptMouse = value; }
		}
		#endregion

		#region Variables
		protected Rectangle m_rectBounds;
		protected Rectangle m_rectBounds2;
		protected string m_szID;
		protected Point m_ptMouse;
		protected bool m_bMouseDown;
		protected bool m_bResizing;
		protected D3DTexture2D m_pTexture = null;
		protected BGLAYOUT_TYPE m_bgLayoutType = BGLAYOUT_TYPE.Tile;
		protected string m_szTilebase = "WndTile00.tga";
		protected D3DTexture2D[] m_tileTextures = new D3DTexture2D[12];
		[NonSerialized]
		protected EditorWindow m_editor;
		protected string m_szCaption;
		protected string m_szHelp;
		protected Color m_color;
		protected Color m_colorOutline;
		protected List<Control> m_children;
		protected List<string> m_controlIDList;
		protected bool m_bShowCaption = true;
		protected bool m_bMovable = true;
		protected bool m_bTopMost = false;
		protected bool m_bPopup = false;
		protected bool m_bNoFocus = false;
		protected bool m_bVertScrollBar = false;
		protected bool m_bHoriScrollBar = false;
		protected bool m_bNoFrame = false;
		protected bool m_bNoDrawFrame = false;
		protected bool m_bSizable = false;
		protected bool m_bBackgroundImage = true;
		#endregion

		#region Constructors
		public Window(Rectangle bounds, EditorWindow mainform)
		{
			m_rectBounds = bounds;
			m_szID = ResManager.Instance.AddDefinedApp();
			m_szCaption = DefaultCaptions.App;
			m_szHelp = "";
			UpdateImage();
			m_editor = mainform;
			m_color = Skin.Instance.DefaultColors.RootWindow.Caption;
			m_colorOutline = Skin.Instance.DefaultColors.RootWindow.CaptionOutline;
			m_children = new List<Control>();
			m_controlIDList = new List<string>();
		}

		public Window(WindowProperty resource, EditorWindow mainform)
		{
			m_rectBounds = new Rectangle();
			m_szID = resource.m_szID;
			m_szCaption = resource.m_szTitle;
			m_szHelp = resource.m_szHelp;
			m_rectBounds = new Rectangle(ResManager.Instance.DefaultWindowLocation.X, ResManager.Instance.DefaultWindowLocation.Y, resource.m_size.Width, resource.m_size.Height);
			if (resource.m_bTile == 0 && resource.m_szTexture.Length > 3)
				m_bgLayoutType = BGLAYOUT_TYPE.Normal;
			UpdateImage(resource.m_szTexture);
			m_editor = mainform;
			m_color = Skin.Instance.DefaultColors.RootWindow.Caption;
			m_colorOutline = Skin.Instance.DefaultColors.RootWindow.CaptionOutline;
			m_children = new List<Control>();
			m_controlIDList = new List<string>();
			m_bSizable = resource.m_style.HasFlag(WindowStyle.WBS_THICKFRAME);
			m_bNoDrawFrame = resource.m_style.HasFlag(WindowStyle.WBS_NODRAWFRAME);
			m_bNoFrame = resource.m_style.HasFlag(WindowStyle.WBS_NOFRAME);
			m_bShowCaption = resource.m_style.HasFlag(WindowStyle.WBS_CAPTION);
			m_bTopMost = resource.m_style.HasFlag(WindowStyle.WBS_TOPMOST);
			m_bMovable = resource.m_style.HasFlag(WindowStyle.WBS_MOVE);
			m_bPopup = resource.m_style.HasFlag(WindowStyle.WBS_POPUP);
			m_bVertScrollBar = resource.m_style.HasFlag(WindowStyle.WBS_VSCROLL);
			m_bHoriScrollBar = resource.m_style.HasFlag(WindowStyle.WBS_HSCROLL);
			if (resource.m_szTexture == null || resource.m_szTexture.Length < 3)
				m_bBackgroundImage = false;
		}
		#endregion

		#region Functions
		public void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
		{
			if (m_bMouseDown)
			{
				DoDrag(e.Location);
			}
			m_ptMouse = e.Location;
		}

		public void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
		{
			m_bMouseDown = true;
			m_ptMouse = e.Location;
			if (OutlineTest(e.Location))
				m_bResizing = true;
			m_rectBounds2 = m_rectBounds;
		}

		public void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
		{
			if (m_bResizing)
			{
				if (m_rectBounds2 == null || m_rectBounds2.IsEmpty)
					m_rectBounds2 = m_rectBounds;

				if (m_rectBounds.Width != m_rectBounds2.Width || m_rectBounds.Height != m_rectBounds2.Height)
					Editor.UndoRedoManager.Update(this, m_rectBounds2.Size, "Size", ExternalUpdate);

				m_rectBounds2 = m_rectBounds;
				m_bResizing = false;
			}
			m_bMouseDown = false;
		}

		protected virtual void DoDrag(Point ptMouse)
		{
			Point ptDelta = new Point(ptMouse.X - m_ptMouse.X, ptMouse.Y - m_ptMouse.Y);

			if (m_bResizing)
			{
				Rectangle bounds = CalculateBounds();
				if (bounds.Width <= m_rectBounds.Width + ptDelta.X && bounds.Height <= m_rectBounds.Height + ptDelta.Y)
				{
					m_rectBounds.Width += ptDelta.X;
					m_rectBounds.Height += ptDelta.Y;
				}
			}

			m_ptMouse = ptMouse;
		}

		private void UpdateImage(string szImage = "")
		{
			if (m_bgLayoutType == BGLAYOUT_TYPE.Tile)
			{
				if (szImage.Length < 6)
					szImage = m_szTilebase;
				else
					m_szTilebase = szImage;
				string szPart1 = szImage.Substring(0, szImage.Length - 6);
				string szPart2 = szImage.Substring(szImage.Length - 4);

				for (int i = 0; i < 12; i++)
				{
					string sztexture = szPart1 + String.Format("{0:00}", i) + szPart2;
					if (D3DTextureManager.Instance[sztexture] == null)
						return;
					m_tileTextures[i] = D3DTextureManager.Instance[sztexture];
				}
			}
			else
			{
				if (szImage.Length < 3 || D3DTextureManager.Instance[szImage] == null)
					return;
				m_pTexture = D3DTextureManager.Instance[szImage];
				if (m_rectBounds.Width < m_pTexture.Size.Width)
					m_rectBounds.Width = m_pTexture.Size.Width;
				if (m_rectBounds.Height < m_pTexture.Size.Height)
					m_rectBounds.Height = m_pTexture.Size.Height;
			}
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

		public void AddChild(Control child)
		{
			Editor.UndoRedoManager.Update(this, child, "Children", ExternalUpdate);
			m_children.Add(child);
			m_controlIDList.Add(child.Id);
		}

		public void AddChildFromResource(Control child)
		{
			m_children.Add(child);
			m_controlIDList.Add(child.Id);
		}

		public void RemoveChild(Control child)
		{
			Editor.UndoRedoManager.Update(this, child, "Children", ExternalUpdate);
			m_controlIDList.Remove(child.Id);
			m_children.Remove(child);
		}

		public void Save()
		{
			if (!ResManager.Instance.Data.Apps.Keys.Contains(m_szID))
				ResManager.Instance.Data.Apps.Add(m_szID, ResManager.Instance.Data.Apps.Values.LastOrDefault()+1);
			if (!ResManager.Instance.Data.Properties.Keys.Contains(m_szID))
				ResManager.Instance.Data.Properties.Add(m_szID, new WindowProperty());
			WindowProperty prop = ResManager.Instance.Data.Properties[m_szID];
			prop.m_szID = m_szID;
			if (m_bBackgroundImage)
				prop.m_szTexture = BackgroundImage;
			else
				prop.m_szTexture = "";
			prop.m_size = m_rectBounds.Size;
			prop.m_szTitle = m_szCaption;
			prop.m_szHelp = m_szHelp;
			prop.m_style = (WindowStyle)0;
			if (m_bSizable)
				prop.m_style |= WindowStyle.WBS_THICKFRAME;
			if (m_bNoDrawFrame)
				prop.m_style |= WindowStyle.WBS_NODRAWFRAME;
			if (m_bNoFrame)
				prop.m_style |= WindowStyle.WBS_NOFRAME;
			if (m_bShowCaption)
				prop.m_style |= WindowStyle.WBS_CAPTION;
			if (m_bTopMost)
				prop.m_style |= WindowStyle.WBS_TOPMOST;
			if (m_bMovable)
				prop.m_style |= WindowStyle.WBS_MOVE;
			if (m_bPopup)
				prop.m_style |= WindowStyle.WBS_POPUP;
			if (m_bVertScrollBar)
				prop.m_style |= WindowStyle.WBS_VSCROLL;
			if (m_bHoriScrollBar)
				prop.m_style |= WindowStyle.WBS_HSCROLL;
			if (m_bgLayoutType == BGLAYOUT_TYPE.Tile)
				prop.m_bTile = 1;
			else
				prop.m_bTile = 0;
			//if (m_bNoFocus)
				//prop.style |= WindowStyle.WBS_NOFOCUS;

			prop.m_aControls.Clear();
			foreach (Control ctrl in m_children)
			{
				ControlProperty ctrlprop = new ControlProperty();
				ctrl.Save(ref ctrlprop);
				prop.m_aControls.Add(ctrl.Id, ctrlprop);
			}

			ResManager.Instance.Data.Properties[m_szID] = prop;
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

		#region Overrides
		public virtual void Draw()
		{
			if (m_bgLayoutType == BGLAYOUT_TYPE.Tile)
			{
				if (m_tileTextures[0] != null)
				{
					// header
					m_tileTextures[0].Draw(new Point(m_rectBounds.X, m_rectBounds.Y));
					m_tileTextures[1].Draw(new Point(m_rectBounds.X + m_tileTextures[0].Size.Width, m_rectBounds.Y), new Rectangle(0, 0, m_rectBounds.Width - m_tileTextures[0].Size.Width - m_tileTextures[2].Size.Width, m_tileTextures[2].Size.Height));
					m_tileTextures[2].Draw(new Point((m_rectBounds.X + m_rectBounds.Width) - m_tileTextures[2].Size.Width,
												   m_rectBounds.Y));
					// header(bottom)
					m_tileTextures[3].Draw(new Point(m_rectBounds.X, m_rectBounds.Y + m_tileTextures[0].Size.Height));
					m_tileTextures[4].Draw(new Point(m_rectBounds.X + m_tileTextures[3].Size.Width, m_rectBounds.Y + m_tileTextures[1].Size.Height), new Rectangle(0, 0, m_rectBounds.Width - m_tileTextures[3].Size.Width - m_tileTextures[5].Size.Width, m_tileTextures[5].Size.Height));
					m_tileTextures[5].Draw(new Point((m_rectBounds.X + m_rectBounds.Width) - m_tileTextures[3].Size.Width, m_rectBounds.Y + m_tileTextures[2].Size.Height));

					// body
					m_tileTextures[6].Draw(new Point(m_rectBounds.X, m_rectBounds.Y + m_tileTextures[0].Size.Height + m_tileTextures[3].Size.Height), new Rectangle(0, 0, m_tileTextures[6].Size.Width, m_rectBounds.Height - m_tileTextures[0].Size.Height - m_tileTextures[3].Size.Height - m_tileTextures[9].Size.Height));
					m_tileTextures[7].Draw(new Point(m_rectBounds.X + m_tileTextures[6].Size.Width, m_rectBounds.Y + m_tileTextures[0].Size.Height + m_tileTextures[3].Size.Height), new Rectangle(0, 0, m_rectBounds.Width - m_tileTextures[8].Size.Width - m_tileTextures[6].Size.Width, m_rectBounds.Height - m_tileTextures[0].Size.Height - m_tileTextures[3].Size.Height - m_tileTextures[9].Size.Height));
					m_tileTextures[8].Draw(new Point((m_rectBounds.X + m_rectBounds.Width) - m_tileTextures[6].Size.Width, m_rectBounds.Y + m_tileTextures[0].Size.Height + m_tileTextures[3].Size.Height), new Rectangle(0, 0, m_tileTextures[8].Size.Width, m_rectBounds.Height - m_tileTextures[0].Size.Height - m_tileTextures[3].Size.Height - m_tileTextures[9].Size.Height));

					// bottom
					m_tileTextures[9].Draw(new Point(m_rectBounds.X, (m_rectBounds.Y + m_rectBounds.Height) - m_tileTextures[9].Size.Height));
					m_tileTextures[10].Draw(new Point(m_rectBounds.X + m_tileTextures[9].Size.Width, (m_rectBounds.Y + m_rectBounds.Height) - m_tileTextures[10].Size.Height), new Rectangle(0, 0, m_rectBounds.Width - m_tileTextures[9].Size.Width - m_tileTextures[11].Size.Width, m_tileTextures[10].Size.Height));
					m_tileTextures[11].Draw(new Point((m_rectBounds.X + m_rectBounds.Width) - m_tileTextures[9].Size.Width, (m_rectBounds.Y + m_rectBounds.Height) - m_tileTextures[9].Size.Height));
			
				}
			}
			else
			{
				if (m_pTexture != null)
					m_pTexture.Draw(new Point(m_rectBounds.Left, m_rectBounds.Top));
			}

			// Caption
			if (m_szCaption != "" && m_bShowCaption)
			{
				if (Skin.Instance.WindowCaptionCentered == 0)
					D3D2DRender.Instance.TextOut2(new Rectangle(m_rectBounds.Location.X + 10, m_rectBounds.Location.Y + 4, m_rectBounds.Width - 10, m_rectBounds.Height - 4), m_szCaption, m_color, m_colorOutline, DrawTextFormat.Top | DrawTextFormat.Left);
				else
					D3D2DRender.Instance.TextOut2(new Rectangle(m_rectBounds.Location.X + 10, m_rectBounds.Location.Y + 8, m_rectBounds.Width - 10, m_rectBounds.Height - 8), m_szCaption, m_color, m_colorOutline, DrawTextFormat.Top | DrawTextFormat.Center);
			}
		}

		public virtual void Update()
		{
		}

		protected Rectangle CalculateBounds()
		{
			/*
			foreach (Control ctrl in m_children)
			{
				if (ctrl.Location.X + ctrl.Size.Width > m_rectBounds.Width + ptDelta.X || ctrl.Location.Y + ctrl.Size.Height > m_rectBounds.Height + ptDelta.Y)
					return false;
			}
			*/
			Rectangle bounds = D3D2DRender.Instance.Font.MeasureString(null, m_szCaption, Microsoft.DirectX.Direct3D.DrawTextFormat.Left, m_color.ToArgb());
			bounds.X = m_rectBounds.X;
			bounds.Y = m_rectBounds.Y;
			bounds.Width += 10;
			bounds.Height += 4;
			Rectangle bounds2;
			if (m_bgLayoutType == BGLAYOUT_TYPE.Tile && m_tileTextures[0] != null)
				bounds2 = new Rectangle(0, 0, m_tileTextures[0].Size.Width * 3, m_tileTextures[0].Size.Height + m_tileTextures[3].Size.Height + m_tileTextures[9].Size.Height);
			else if (m_bgLayoutType == BGLAYOUT_TYPE.Normal && m_pTexture != null)
				bounds2 = new Rectangle(0, 0, m_pTexture.Size.Width, m_pTexture.Size.Height);
			else
				return bounds;

			if (bounds2.Width > bounds.Width)
				bounds.Width = bounds2.Width;

			if (bounds2.Height > bounds.Height)
				bounds.Height = bounds2.Height;
			return bounds;
		}
		#endregion

		#region UndoRedo
		public Object InternalUpdate(Object data, string identifier)
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
				case "Caption":
					ret = m_szCaption;
					m_szCaption = (string)data;
					break;
				case "Help":
					ret = m_szHelp;
					m_szHelp = (string)data;
					break;
				case "Children":
					ret = data;
					if (m_children.Contains((Control)data))
					{
						if (Editor.Mainform.SelectedControls.Contains((Control)data))
						{
							Editor.Mainform.SelectedControls.Remove((Control)data);
						}
						m_controlIDList.Remove(((Control)data).Id);
						m_children.Remove((Control)data);
					}
					else
					{
						m_children.Add((Control)data);
						m_controlIDList.Add(((Control)data).Id);
						Editor.Mainform.SetPropertyControl((Control)data, Editor);
					}
					break;
				case "Id":
					ResManager.Instance.Data.Apps.Remove(m_szID);
					ret = m_szID;
					m_szID = (string)data;
					m_editor.Text = m_szID;
					if (m_editor.Mainform.TabbedView1.ActiveDocument != null)
						m_editor.Mainform.TabbedView1.ActiveDocument.Caption = m_szID;
					break;
				case "Size":
					ret = m_rectBounds.Size;
					m_rectBounds.Width = ((Size)data).Width;
					m_rectBounds.Height = ((Size)data).Height;
					break;
				default:
					break;
			}
			return ret;
		}

		public static Object ExternalUpdate(Object instance, Object data, string identifier)
		{
			return ((Window)instance).InternalUpdate(data, identifier);
		}
		#endregion
	}
}

#endif