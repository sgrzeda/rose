using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using XorNet.Rose.Properties;
using XorNet.Rose.Resource;

namespace XorNet.Rose.DirectX
{
	[Serializable]
    public class D3DTexture2D
    {
        private Texture m_pTexture;
        private Surface m_pBaseSurface;
        private ImageInformation m_pImageInfo;
        private int m_nFrames;
        private string m_szName;

        public string Name
        {
            get
            {
                return m_szName;
            }
        }
        public Texture BaseTexture
        {
            get
            {
                return m_pTexture;
            }
        }

        public Size Size
        {
            get
            {
                return new Size(m_pImageInfo.Width, m_pImageInfo.Height);
            }
        }

        public Surface BaseSurface
        {
            get
            {
                return m_pBaseSurface;
            }
        }

        public bool Disposed
        {
            get { return m_pTexture.Disposed; }
        }

        public D3DTexture2D(Texture pTexture, ImageInformation pImageInfo, string szName)
        {
            m_pTexture = pTexture;
            m_pImageInfo = pImageInfo;
            m_szName = szName;
            m_pBaseSurface = pTexture.GetSurfaceLevel(0);
            m_nFrames = 1;
        }

        public D3DTexture2D(string szName)
        {
            m_szName = szName;
            m_nFrames = 1;
            LoadTexture(szName);
        }

        private void LoadTexture(string szName)
		{
			string szFullPath = Path.Combine(Settings.Default.ThemeDir + '\\' + Skin.Instance.TextureDirectory, szName);
            m_pImageInfo = new ImageInformation();
            m_pTexture = TextureLoader.FromFile(DirectX.D3DDeviceService.Instance.DeviceInstance, szFullPath, 0, 0, 0, Usage.Dynamic,
                                       Format.Unknown, Pool.Default, Filter.None, Filter.Linear, Color.FromArgb(0xff, 0xff, 0x00, 0xff).ToArgb(), ref m_pImageInfo);

            m_pBaseSurface = m_pTexture.GetSurfaceLevel(0);

        }

        public void SetFrames(int nFrames)
        {
            m_nFrames = nFrames;
        }

        public void Draw( Point pt )
        {
            D3D2DRender.Instance.DrawTexture(pt, new Rectangle(0, 0, m_pImageInfo.Width, m_pImageInfo.Height), this );
        }

        public void Draw( Point pt, Rectangle originRect )
        {
            D3D2DRender.Instance.DrawTexture(pt, originRect, this);
        }

        public void DrawFrame( Point pt, int nFrame )
        {
            D3D2DRender.Instance.DrawTexture(pt, new Rectangle(nFrame * (m_pImageInfo.Width/m_nFrames), 0, m_pImageInfo.Width/m_nFrames, m_pImageInfo.Height), this);
        }

        public void OnDeviceReset()
        {
            LoadTexture(m_szName);
        }
    }
}
