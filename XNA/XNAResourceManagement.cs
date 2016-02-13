#if XNA
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Rose.Common;

namespace Rose.XNA
{
    /* Disambiguation */
    using GdiColor = Color;
    using XnaColor = Microsoft.Xna.Framework.Color;

    internal class XnaResourceManager
    {
        private static XnaResourceManager _xnaResourceManagerSingleton;
        private static XnaTextureManager _xnaTextureManagerSingleton;
        private GraphicsDevice _graphicsDevice;

        private XnaResourceManager()
        {
            _xnaTextureManagerSingleton = XnaTextureManager.Instance;
        }

        public static XnaResourceManager Instance
        {
            get { return _xnaResourceManagerSingleton ?? (_xnaResourceManagerSingleton = new XnaResourceManager()); }
        }

        public XnaTextureManager XnaTexture
        {
            get { return _xnaTextureManagerSingleton; }
        }

        public void DeviceCreate(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
            _xnaTextureManagerSingleton.DeviceCreate(graphicsDevice);
        }
    }

    internal class XnaTextureManager : Manager<string, Texture2D>
    {
        private static XnaTextureManager _xnaTextureManagerSingleton;

        private XnaTextureManager()
        {
        }

        public static XnaTextureManager Instance
        {
            get { return _xnaTextureManagerSingleton ?? (_xnaTextureManagerSingleton = new XnaTextureManager()); }
        }

        protected override Texture2D CreateResource(string index)
        {
            if (ResourceMap.ContainsKey(index) && ResourceMap[index].IsDisposed)
            {
                Logging.Error("Resource at index \"{0}\" has been disposed!", index);
                return null;
            }

            string fullPath = Path.Combine(Settings.TextureDir, index);
            if (!File.Exists(fullPath))
            {
                Logging.Error("The texture \"{0}\" was not found in the directory \"{1}\"", index, Settings.TextureDir);
                return null;
            }

            Bitmap textureBitmap = DevIL.DevIL.LoadBitmap(fullPath);
            var stream = new MemoryStream();

            textureBitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
            textureBitmap.Save(stream, ImageFormat.Png);

            Texture2D texture = Texture2D.FromStream(_graphicsDevice, stream);
            ResourceMap[index] = texture;

            return texture;
        }

        public override void DeviceCreate(GraphicsDevice graphicsDevice)
        {
            base.DeviceCreate(graphicsDevice);
            var line = new Texture2D(graphicsDevice, 1, 1);
            line.SetData(new[] {XnaColor.Pink});
            ResourceMap["line"] = line;
        }
    }
}
#endif