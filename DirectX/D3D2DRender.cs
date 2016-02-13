using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Drawing;

namespace XorNet.Rose.DirectX
{
    class D3D2DRender
    {
        public Microsoft.DirectX.Direct3D.Font Font
        {
            get
            {
                return m_pFont;
            }
        }
        private static D3D2DRender m_sInstance;
        private VertexBuffer m_pVBRect;
        private VertexBuffer m_pVBFilledRect;
        //private VertexBuffer m_pVBTexture;
		private VertexBuffer m_pVBLine;
		private Microsoft.DirectX.Direct3D.Font m_pFont;
        private Device m_d3dDevice;
        private D3D2DRender()
        {
            m_d3dDevice = D3DDeviceService.Instance.DeviceInstance;
            m_pVBRect = new VertexBuffer(typeof(CustomVertex.TransformedColored), 5, m_d3dDevice, Usage.Dynamic | Usage.WriteOnly, CustomVertex.TransformedColored.Format, Pool.Default);
            m_pVBFilledRect = new VertexBuffer(typeof(CustomVertex.TransformedColored), 4, m_d3dDevice, Usage.Dynamic | Usage.WriteOnly, CustomVertex.TransformedColored.Format, Pool.Default);
			m_pVBLine = new VertexBuffer(typeof(CustomVertex.TransformedColored), 2, m_d3dDevice, Usage.Dynamic | Usage.WriteOnly, CustomVertex.TransformedColored.Format, Pool.Default);

			m_pFont = new Microsoft.DirectX.Direct3D.Font(m_d3dDevice, new System.Drawing.Font("Verdana", 9.0f));

			GC.SuppressFinalize(m_pFont);
        }

        public static D3D2DRender Instance
        {
            get { return m_sInstance ?? (m_sInstance = new D3D2DRender()); }
        }

        private void ResizeRectVB( Rectangle rect, Color colorLT, Color colorRT, Color colorLB, Color colorRB )
        {
            CustomVertex.TransformedColored[] vertices = new CustomVertex.TransformedColored[5];

            vertices[0].Position = new Vector4(rect.X, rect.Y, 0.0f, 1.0f);
            vertices[0].Color = colorLT.ToArgb();

            vertices[1].Position = new Vector4(rect.X + rect.Width, rect.Y, 0.0f, 1.0f);
            vertices[1].Color = colorRT.ToArgb();

            vertices[2].Position = new Vector4(rect.X + rect.Width, rect.Y + rect.Height, 0.0f, 1.0f);
            vertices[2].Color = colorRB.ToArgb();

            vertices[3].Position = new Vector4(rect.X, rect.Y + rect.Height, 0.0f, 1.0f);
            vertices[3].Color = colorLB.ToArgb();

            vertices[4].Position = new Vector4(rect.X, rect.Y, 0.0f, 2.0f);
            vertices[4].Color = colorLT.ToArgb();

            m_pVBRect.SetData(vertices, 0, LockFlags.Discard);
        }

        private void ResizeFilledRectVB(Rectangle rect, Color colorLT, Color colorRT, Color colorLB, Color colorRB, Texture texture)
        {
            CustomVertex.TransformedColored[] vertices = new CustomVertex.TransformedColored[4];

            vertices[0].Position = new Vector4(rect.X, rect.Y, 0.0f, 1.0f);
            vertices[0].Color = colorLT.ToArgb();
            vertices[0].Rhw = 1.0f;

            vertices[1].Position = new Vector4(rect.X + rect.Width, rect.Y, 0.0f, 1.0f);
            vertices[1].Color = colorRT.ToArgb();
            vertices[1].Rhw = 1.0f; // [1]?

            vertices[2].Position = new Vector4(rect.X, rect.Y + rect.Height, 0.0f, 1.0f);
            vertices[2].Color = colorLB.ToArgb();
			vertices[2].Rhw = 1.0f; // [2]?

            vertices[3].Position = new Vector4(rect.X + rect.Width, rect.Y + rect.Height, 0.0f, 1.0f);
            vertices[3].Color = colorRB.ToArgb();
			vertices[3].Rhw = 1.0f; // [3]?

            m_pVBFilledRect.SetData(vertices, 0, LockFlags.Discard);
        }

		public void DrawLine(Point src, Point dest, Color color)
		{
			CustomVertex.TransformedColored[] vertices = new CustomVertex.TransformedColored[2];

			vertices[0].Position = new Vector4(src.X, src.Y, 0.0f, 1.0f);
			vertices[0].Color = color.ToArgb();
			vertices[0].Rhw = 1.0f;

			vertices[1].Position = new Vector4(dest.X, dest.Y, 0.0f, 1.0f);
			vertices[1].Color = color.ToArgb();
			vertices[1].Rhw = 1.0f;

			m_pVBLine.SetData(vertices, 0, LockFlags.Discard);
			m_d3dDevice.SetTexture(0, null);
			m_d3dDevice.VertexFormat = CustomVertex.TransformedColored.Format;
			m_d3dDevice.SetStreamSource(0, m_pVBLine, 0);
			m_d3dDevice.DrawPrimitives(PrimitiveType.LineList, 0, 2);
		}

        public void DrawRect( Rectangle rect, Color colorLT, Color colorRT, Color colorLB, Color colorRB )
        {
            ResizeRectVB(rect, colorLT, colorRT, colorLB, colorRB);

            m_d3dDevice.SetTexture(0, null);
            m_d3dDevice.VertexFormat = CustomVertex.TransformedColored.Format;
            m_d3dDevice.SetStreamSource(0, m_pVBRect, 0);
            m_d3dDevice.DrawPrimitives(PrimitiveType.LineStrip, 0, 4);
        }

        public void DrawRect( Rectangle rect, Color color )
        {
            DrawRect(rect, color, color, color, color);
        }

        public void DrawFilledRect(Rectangle rect, Color colorLT, Color colorRT, Color colorLB, Color colorRB, Texture texture)
        {
            ResizeFilledRectVB(rect, colorLT, colorRT, colorLB, colorRB, texture);

            m_d3dDevice.SetRenderState(RenderStates.AlphaBlendEnable, true);
            m_d3dDevice.SetTextureStageState(0, TextureStageStates.AlphaOperation, (int)TextureOperation.Modulate);
            m_d3dDevice.SetTextureStageState(0, TextureStageStates.AlphaArgument1, (int)TextureArgument.TextureColor);
            m_d3dDevice.SetTextureStageState(0, TextureStageStates.AlphaArgument2, (int)TextureArgument.Diffuse);
            
            m_d3dDevice.SetTexture(0, texture);
            m_d3dDevice.VertexFormat = CustomVertex.TransformedColored.Format;
            m_d3dDevice.SetStreamSource(0, m_pVBFilledRect, 0);
            m_d3dDevice.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
        }

        public void DrawFilledRect(Rectangle rect, Color color, Texture texture)
        {
            DrawFilledRect(rect, color, color, color, color, texture);
        }

        public void DrawTexture( Point pt, Rectangle rectOrigin, D3DTexture2D pTexture)
        {
            CustomVertex.TransformedTextured[] vertices = new CustomVertex.TransformedTextured[4];

            int witWidth = pTexture.BaseSurface.Description.Width;
            int witHeight = pTexture.BaseSurface.Description.Height;

            vertices[0].Position = new Vector4(pt.X, pt.Y, 0.0f, 1.0f);
            vertices[0].Rhw = 1.0f;
            vertices[0].Tu = ((float)rectOrigin.X/witWidth);
            vertices[0].Tv = ((float)rectOrigin.Y / witHeight);

            vertices[1].Position = new Vector4(pt.X + rectOrigin.Width, pt.Y, 0.0f, 1.0f);
            vertices[1].Rhw = 1.0f;
            vertices[1].Tu = ((float)(rectOrigin.X + rectOrigin.Width)/witWidth);
            vertices[1].Tv = ((float)rectOrigin.Y/witHeight);

            vertices[2].Position = new Vector4(pt.X, pt.Y + rectOrigin.Height, 0.0f, 1.0f);
            vertices[2].Rhw = 1.0f;
            vertices[2].Tu = ((float) rectOrigin.X/witWidth);
            vertices[2].Tv = ((float)(rectOrigin.Y + rectOrigin.Height) / witHeight);

            vertices[3].Position = new Vector4(pt.X + rectOrigin.Width, pt.Y + rectOrigin.Height, 0.0f, 1.0f);
            vertices[3].Rhw = 1.0f;
            vertices[3].Tu = ((float)(rectOrigin.X + rectOrigin.Width) / witWidth);
            vertices[3].Tv = ((float)(rectOrigin.Y + rectOrigin.Height) / witHeight);

            m_d3dDevice.SetSamplerState(0, SamplerStageStates.AddressU, true);
			m_d3dDevice.SetSamplerState(0, SamplerStageStates.AddressV, true);
			m_d3dDevice.SetSamplerState(0, SamplerStageStates.MinFilter, (int)TextureFilter.Linear);
			//m_d3dDevice.SetSamplerState(0, SamplerStageStates.MagFilter, (int)TextureFilter.Linear);

            m_d3dDevice.SetRenderState(RenderStates.CullMode, (int)Cull.None);
            m_d3dDevice.SetRenderState(RenderStates.AlphaBlendEnable, true);
            m_d3dDevice.SetRenderState(RenderStates.SourceBlend, (int)Blend.SourceAlpha);
            m_d3dDevice.SetRenderState(RenderStates.DestinationBlend, (int)Blend.InvSourceAlpha);
            m_d3dDevice.SetRenderState(RenderStates.ZEnable, false);

			m_d3dDevice.SetTextureStageState( 0, TextureStageStates.ColorOperation, (int)TextureOperation.SelectArg1 );
            m_d3dDevice.SetTextureStageState( 0, TextureStageStates.ColorArgument1, (int)TextureArgument.TextureColor);
            m_d3dDevice.SetTextureStageState( 0, TextureStageStates.AlphaOperation, (int)TextureOperation.Modulate );
			m_d3dDevice.SetTextureStageState(0, TextureStageStates.AlphaArgument1, (int)TextureArgument.TextureColor);
			m_d3dDevice.SetTextureStageState(0, TextureStageStates.AlphaArgument2, (int)TextureArgument.TFactor);
			//m_d3dDevice.SetRenderState(RenderStates.TextureFactor, Color.FromArgb(255, 0, 0, 0).ToArgb());
            
            m_d3dDevice.VertexShader = null;
            m_d3dDevice.SetTexture(0, pTexture.BaseTexture);
            m_d3dDevice.VertexFormat = CustomVertex.TransformedTextured.Format;
            m_d3dDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, 2, vertices);
            m_d3dDevice.SetTexture( 0, null );
        }

        public void TextOut(Point pos, string szString, Color color, Color shadowColor)
        {
            if ((shadowColor.ToArgb() & 0xff000000) != 0x00)
            {
                m_pFont.DrawText(null, szString, new Point(pos.X + 1, pos.Y + 1), shadowColor);
			}
			m_pFont.DrawText(null, szString, pos, color);
        }

		public void TextOut(Rectangle rect, string szString, Color color, Color shadowColor, DrawTextFormat format = DrawTextFormat.Top | DrawTextFormat.Left)
		{
			if ((shadowColor.ToArgb() & 0xff000000) != 0x00)
			{
				m_pFont.DrawText(null, szString, new Rectangle(rect.X + 1, rect.Y + 1, rect.Width, rect.Height), format, shadowColor);
			}
			m_pFont.DrawText(null, szString, new Rectangle(rect.X + 0, rect.Y, rect.Width, rect.Height), format, color);
			m_pFont.DrawText(null, szString, new Rectangle(rect.X + 1, rect.Y, rect.Width, rect.Height), format, color);
		}

		public void TextOut2(Rectangle rect, string szString, Color color, Color outlineColor, DrawTextFormat format = DrawTextFormat.Top | DrawTextFormat.Left)
		{
			if ((outlineColor.ToArgb() & 0xff000000) != 0x00)
			{
				m_pFont.DrawText(null, szString, new Rectangle(rect.X + 2, rect.Y + 1, rect.Width, rect.Height), format, outlineColor);
				m_pFont.DrawText(null, szString, new Rectangle(rect.X + 2, rect.Y + 0, rect.Width, rect.Height), format, outlineColor);
				m_pFont.DrawText(null, szString, new Rectangle(rect.X + 2, rect.Y - 1, rect.Width, rect.Height), format, outlineColor);
				m_pFont.DrawText(null, szString, new Rectangle(rect.X + 1, rect.Y + 1, rect.Width, rect.Height), format, outlineColor);
				m_pFont.DrawText(null, szString, new Rectangle(rect.X + 1, rect.Y + 0, rect.Width, rect.Height), format, outlineColor);
				m_pFont.DrawText(null, szString, new Rectangle(rect.X + 1, rect.Y - 1, rect.Width, rect.Height), format, outlineColor);
				m_pFont.DrawText(null, szString, new Rectangle(rect.X + 0, rect.Y + 1, rect.Width, rect.Height), format, outlineColor);
				m_pFont.DrawText(null, szString, new Rectangle(rect.X + 0, rect.Y + 0, rect.Width, rect.Height), format, outlineColor);
				m_pFont.DrawText(null, szString, new Rectangle(rect.X + 0, rect.Y - 1, rect.Width, rect.Height), format, outlineColor);
				m_pFont.DrawText(null, szString, new Rectangle(rect.X - 1, rect.Y + 1, rect.Width, rect.Height), format, outlineColor);
				m_pFont.DrawText(null, szString, new Rectangle(rect.X - 1, rect.Y + 0, rect.Width, rect.Height), format, outlineColor);
				m_pFont.DrawText(null, szString, new Rectangle(rect.X - 1, rect.Y - 1, rect.Width, rect.Height), format, outlineColor);
			}
			m_pFont.DrawText(null, szString, new Rectangle(rect.X + 0, rect.Y, rect.Width, rect.Height), format, color);
			m_pFont.DrawText(null, szString, new Rectangle(rect.X + 1, rect.Y, rect.Width, rect.Height), format, color);
		}
    }

	/*
	class OutlineHelper
	{
		static int[] g_adwOutLine1 = 
		{
			0x0, 0xe, 0x0,
			0xe, 0xe, 0xe,
			0x0, 0xe, 0x0
		};
		static int[] g_adwOutLine2 = 
		{
			0x3, 0x7, 0x7, 0x7, 0x3,
			0x7, 0xa, 0xe, 0xa, 0x7,
			0x7, 0xe, 0xe, 0xe, 0x7,
			0x7, 0xa, 0xe, 0xa, 0x7,
			0x3, 0x7, 0x7, 0x7, 0x3
		};
		static int[] g_adwOutLine3 = 
		{
			0x0, 0x4, 0x4, 0x4, 0x4, 0x4, 0x0,
			0x4, 0x7, 0xe, 0xe, 0xe, 0x7, 0x4,
			0x4, 0xe, 0xe, 0xe, 0xe, 0xe, 0x4,
			0x4, 0xe, 0xe, 0xe, 0xe, 0xe, 0x4,
			0x4, 0xa, 0xe, 0xe, 0xa, 0xe, 0x4,
			0x4, 0x7, 0xe, 0xe, 0xe, 0x7, 0x4,
			0x0, 0x4, 0x4, 0x4, 0x4, 0x4, 0x0
		};
		static int[] g_adwOutLine4 = 
		{
			0x0, 0x4, 0x4, 0x4, 0x4, 0x4, 0x4, 0x4, 0x0,
			0x4, 0x7, 0xe, 0xe, 0xe, 0xe, 0xe, 0x7, 0x4,
			0x4, 0xe, 0xe, 0xe, 0xe, 0xe, 0xe, 0xe, 0x4,
			0x4, 0xe, 0xe, 0xe, 0xe, 0xe, 0xe, 0xe, 0x4,
			0x4, 0xa, 0xe, 0xe, 0xa, 0xe, 0xa, 0xe, 0x4,
			0x4, 0xe, 0xe, 0xe, 0xe, 0xe, 0xe, 0xe, 0x4,
			0x4, 0xa, 0xe, 0xe, 0xa, 0xe, 0xa, 0xe, 0x4,
			0x4, 0x7, 0xe, 0xe, 0xe, 0xe, 0xe, 0x7, 0x4,
			0x0, 0x4, 0x4, 0x4, 0x4, 0x4, 0x4, 0x4, 0x0
		};

		void MakeOutLine( int nWidth, int* pDst16 )
		{
			int nOffset = 0;
			int nDstOffset;
			WORD* pDst16_,* pDstCur;
			int nCurX = m_nCurX + m_nOutLine ;
			int nCurY = m_nCurY + m_nOutLine ;
			nDstOffset = ( nCurY * nWidth + nCurX );
			pDst16_ = &pDst16[ nDstOffset ] ;//d3dlr.Pitch;

			DWORD* apdwOutLine;
	
			switch( m_nOutLine )
			{
			case 1: apdwOutLine = g_adwOutLine1; break;
			case 2: apdwOutLine = g_adwOutLine2; break;
			case 3: apdwOutLine = g_adwOutLine3; break;
			case 4: apdwOutLine = g_adwOutLine4; break;
			}
			int nOutLineLength = m_nOutLine * 2 + 1;

			for( int y = 0; y < m_sizeBitmap.cy; y++ )
			{
				pDstCur = pDst16_;
				for( int x = 0; x < m_sizeBitmap.cx; x++ )
				{
					if( ( *pDstCur & 0xf000 ) == 0xf000)
					{
						for( int y2 = (int)y + nCurY - m_nOutLine, y3 = 0; y2 <= (int)( ( y + nCurY + m_nOutLine ) ); y2++, y3++ )
						{
							for( int x2 = (int)x + nCurX - m_nOutLine, x3 = 0; x2 <= (int)( ( x + nCurX + m_nOutLine ) ); x2++, x3++ )
							{
								if( x2 >= 0 && x2 < nWidth && y2 >= 0 && y2 < nWidth )
								{
							
									nOffset = ( y2 * nWidth + x2 );
									if( ( pDst16[ nOffset] & 0xf000 ) != 0xf000 )
									{
										DWORD dwColor = apdwOutLine[ y3 * nOutLineLength + x3 ] << 12;
										if( dwColor )
										{
											if( (DWORD)( ( pDst16[ nOffset] & 0xf000 ) ) < dwColor )///&& ( pDst16[ nOffset] & 0xf000 ) != 0xf000 )
												pDst16[ nOffset] = (WORD)( ( pDst16[ nOffset] & 0x0fff ) | dwColor );
										}
									}
								}
							}
						}
					}
					pDstCur++;
				}
				pDst16_ += nWidth;
			}
		}
	}
	*/
}
