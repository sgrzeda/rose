#if XNA
using System;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;

namespace Rose.XNA
{
    internal class XNAGraphicsDeviceService : IGraphicsDeviceService
    {
        #region Fields

        private static XNAGraphicsDeviceService singletonInstance;
        private static int referenceCount;

        #endregion

        private readonly GraphicsDevice graphicsDevice;

        private readonly PresentationParameters parameters;

        private XNAGraphicsDeviceService(IntPtr windowHandle, int width, int height)
        {
            parameters = new PresentationParameters();

            parameters.BackBufferWidth = Math.Max(width, 1);
            parameters.BackBufferHeight = Math.Max(height, 1);
            parameters.BackBufferFormat = SurfaceFormat.Color;
            parameters.DepthStencilFormat = DepthFormat.Depth16;
            parameters.DeviceWindowHandle = windowHandle;
            parameters.PresentationInterval = PresentInterval.Immediate;
            parameters.IsFullScreen = false;

            if (GraphicsAdapter.DefaultAdapter.IsProfileSupported(GraphicsProfile.HiDef))
                graphicsDevice = new GraphicsDevice(GraphicsAdapter.DefaultAdapter,
                                                    GraphicsProfile.HiDef,
                                                    parameters);
            else
            {
                MessageBox.Show(
                    "WARNING: Your graphics device does not support the HiDef Profile, switching to the Reach profile.",
                    "Rose");
                graphicsDevice = new GraphicsDevice(GraphicsAdapter.DefaultAdapter,
                                                    GraphicsProfile.Reach,
                                                    parameters);
            }
            XnaResourceManager.Instance.DeviceCreate(graphicsDevice);
        }

        #region IGraphicsDeviceService Members

        public GraphicsDevice GraphicsDevice
        {
            get { return graphicsDevice; }
        }

        public event EventHandler<EventArgs> DeviceCreated;
        public event EventHandler<EventArgs> DeviceDisposing;
        public event EventHandler<EventArgs> DeviceReset;
        public event EventHandler<EventArgs> DeviceResetting;

        #endregion

        ~XNAGraphicsDeviceService()
        {
            if (DeviceDisposing != null)
                DeviceDisposing(this, EventArgs.Empty);

            graphicsDevice.Dispose();
        }


        public static XNAGraphicsDeviceService AddRef(IntPtr windowHandle,
                                                   int width, int height)
        {
            Interlocked.Increment(ref referenceCount);
            if (singletonInstance == null)
            {
                singletonInstance = new XNAGraphicsDeviceService(windowHandle,
                                                              width, height);
            }

            return singletonInstance;
        }


        public void Release(bool disposing)
        {
            Interlocked.Decrement(ref referenceCount);
        }

        public void ResetDevice(int width, int height)
        {
            if (DeviceResetting != null)
                DeviceResetting(this, EventArgs.Empty);

            parameters.BackBufferWidth = Math.Max(parameters.BackBufferWidth, width);
            parameters.BackBufferHeight = Math.Max(parameters.BackBufferHeight, height);

            graphicsDevice.Reset(parameters);

            if (DeviceReset != null)
                DeviceReset(this, EventArgs.Empty);
        }
    }
}
#endif