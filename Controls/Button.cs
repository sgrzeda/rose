#if XNA
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = System.Drawing.Color;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Rose.Controls
{
    /* Disambiguation */
    using GdiColor = Color;
    using XnaColor = Microsoft.Xna.Framework.Color;
    using XnaPoint = Point;
    using GdiPoint = System.Drawing.Point;

    internal class Button : Control
    {
        private int frame;
        private Texture2D texture;
        private string textureName;

        private Button()
        {
            frame = 1;
            type = ControlType.BUTTON;
        }

        public Button(Point position, string textureName)
            : this(position)
        {
            this.textureName = textureName;
            texture = XnaResourceManager.XnaTexture[textureName];
        }

        public Button(Point position)
            : this()
        {
            if (texture == null)
            {
                textureName = "ButtOk.tga";
                texture = XnaResourceManager.XnaTexture[textureName];
            }
            Bounds = new Rectangle(position.X, position.Y, texture.Width/4, texture.Height);
        }

        [Category("Appearance")]
        public string Texture
        {
            get { return textureName; }
            set
            {
                texture = XnaResourceManager.XnaTexture[value];
                textureName = value;
            }
        }

        public int Frame
        {
            get { return frame; }
            set { if ((value > 0) && (value <= 4)) frame = value; }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(Bounds.X, Bounds.Y),
                             new Rectangle(frame*Bounds.Width, 0, Bounds.Width, Bounds.Height), XnaColor.White, 0.0f,
                             Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
            base.Draw(spriteBatch);
        }
    }
}
#endif
using System.ComponentModel;
using System.Drawing;
using XorNet.Rose.DirectX;
using XorNet.Rose.Resource;
using Microsoft.DirectX.Direct3D;
using System;

namespace XorNet.Rose.Controls
{
	[Serializable]
    class Button : Control
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
		[DXDescription("Caption of the control")]
		public virtual string Caption
		{
			get { return m_szCaption; }
			set
			{
				//m_parent.Editor.UndoRedoManager.Update(this, m_szCaption, "Caption", ExternalUpdateDerivative2);
				m_szCaption = value;
				UpdateBounds();
			}
		}

		[Category("Appearance")]
		[DXDescription("Vertical Alignment of caption")]
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
		[DXDescription("Horizontal Alignment of caption")]
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
		[NonSerialized]
		protected D3DTexture2D m_pTexture;
		protected VALIGN_TYPE m_alignVert = VALIGN_TYPE.Center;
		protected HALIGN_TYPE m_alignHori = HALIGN_TYPE.Center;
		protected string m_szTexture = "";
		#endregion

		#region Constructors
		public Button(Point ptLocation)
			: base(ptLocation)
		{
			m_szDefaultID = DefaultStrings.ButtonControl;
			m_type = CONTROL_TYPE.WTYPE_BUTTON;
			Image = "ButtNormal00.tga";
			m_szCaption = "OK";
			m_bResizable = false;
		}

		public Button(ControlProperty resource)
			: base(resource)
		{
			m_szDefaultID = DefaultStrings.ButtonControl;
			if (resource.m_szTexture.Length > 3)
				Image = resource.m_szTexture;
			m_bResizable = false;
		}
		#endregion

		#region Functions
		protected void UpdateTexture(string szImage="")
		{
			if (szImage.Length < 3 || D3DTextureManager.Instance[szImage] == null)
			{
				if (D3DTextureManager.Instance[m_szTexture] == null)
					return;
				szImage = m_szTexture;
			}
			m_szTexture = szImage;
			m_pTexture = D3DTextureManager.Instance[szImage];
			m_pTexture.SetFrames(4);
			m_rectBounds = new Rectangle(m_rectBounds.X, m_rectBounds.Y, m_pTexture.Size.Width / 4, m_pTexture.Size.Height);
		}
		#endregion

		#region Overrides
		public override void Draw()
		{
			if (m_pTexture == null)
				return;

            if (!m_bEnabled)
            {
				m_pTexture.DrawFrame(m_rectBounds.Location, 3);
            }
			else if (m_bMouseDown)
            {
				m_pTexture.DrawFrame(m_rectBounds.Location, 2);
            }
			else if (m_bHover)
            {
				m_pTexture.DrawFrame(m_rectBounds.Location, 0);
            }
			else
			{
				m_pTexture.DrawFrame(m_rectBounds.Location, 1);
			}


			if (m_szCaption != "")
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

				if (!m_bEnabled)
				{
					D3D2DRender.Instance.TextOut2(m_rectBounds, m_szCaption, Skin.Instance.DefaultColors.ButtonControl.CaptionDisabled, Skin.Instance.DefaultColors.ButtonControl.CaptionDisabledOutline, format);
				}
				else if (m_bMouseDown)
				{
					Rectangle tmprect = m_rectBounds;
					tmprect.Y += 1;
					D3D2DRender.Instance.TextOut2(tmprect, m_szCaption, Skin.Instance.DefaultColors.ButtonControl.CaptionPushed, Skin.Instance.DefaultColors.ButtonControl.CaptionPushedOutline, format);
				}
				else if (m_bHover)
				{
					D3D2DRender.Instance.TextOut2(m_rectBounds, m_szCaption, Skin.Instance.DefaultColors.ButtonControl.CaptionHighlighted, Skin.Instance.DefaultColors.ButtonControl.CaptionHighlightedOutline, format);
				}
				else
				{
					D3D2DRender.Instance.TextOut2(m_rectBounds, m_szCaption, Skin.Instance.DefaultColors.ButtonControl.Caption, Skin.Instance.DefaultColors.ButtonControl.CaptionOutline, format);
				}
			}
		}

		public override void Save(ref ControlProperty prop)
		{
			base.Save(ref prop);

			prop.m_szTexture = Image;
		}

		public override void OnSerialized()
		{
			base.OnSerialized();

			UpdateTexture();
		}
		#endregion
	}
}