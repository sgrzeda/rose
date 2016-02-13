using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.Xna.Framework.Graphics;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Rose.XNA
{
    /* Disambiguation */
    using GdiColor = Color;
    using XnaColor = Microsoft.Xna.Framework.Color;

    public partial class XNAWindowBase : XtraForm /* Form */
    {
        protected GraphicsDevice graphicsDevice;
        private XNAGraphicsDeviceService graphicsDeviceService;

        public XNAWindowBase()
        {
            graphicsDeviceService = XNAGraphicsDeviceService.AddRef(Handle, ClientSize.Width, ClientSize.Height);
            graphicsDevice = graphicsDeviceService.GraphicsDevice;
            Application.Idle += delegate { Invalidate(); };
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            UpdateLogic();
            string beginDrawError = BeginDraw();
            if (string.IsNullOrEmpty(beginDrawError))
            {
                Draw();
                EndDraw();
            }
            else
            {
                PaintUsingSystemDrawing(e.Graphics, beginDrawError);
            }
        }

        protected virtual void Draw()
        {
        }

        // called before draw
        protected virtual void UpdateLogic()
        {
        }

        private string BeginDraw()
        {
            if (graphicsDeviceService == null)
            {
                return Text + "\n\n" + GetType();
            }

            string deviceResetError = HandleDeviceReset();

            if (!string.IsNullOrEmpty(deviceResetError))
            {
                return deviceResetError;
            }

            var viewport = new Viewport();

            viewport.X = 0;
            viewport.Y = 0;

            viewport.Width = ClientSize.Width;
            viewport.Height = ClientSize.Height;

            viewport.MinDepth = 0;
            viewport.MaxDepth = 1;

            graphicsDevice.Viewport = viewport;

            return null;
        }

        private void EndDraw()
        {
            try
            {
                var sourceRectangle = new Rectangle(0, 0, ClientSize.Width,
                                                    ClientSize.Height);

                graphicsDevice.Present(sourceRectangle, null, Handle);

            }
            catch
            {
            }
        }

        private string HandleDeviceReset()
        {
            bool deviceNeedsReset = false;

            switch (graphicsDevice.GraphicsDeviceStatus)
            {
                case GraphicsDeviceStatus.Lost:
                    return "Graphics device lost";

                case GraphicsDeviceStatus.NotReset:
                    deviceNeedsReset = true;
                    break;

                default:
                    PresentationParameters pp = graphicsDevice.PresentationParameters;

                    deviceNeedsReset = (ClientSize.Width > pp.BackBufferWidth) ||
                                       (ClientSize.Height > pp.BackBufferHeight);
                    break;
            }

            if (deviceNeedsReset)
            {
                try
                {
                    graphicsDeviceService.ResetDevice(ClientSize.Width,
                                                      ClientSize.Height);
                }
                catch (Exception e)
                {
                    return "Graphics device reset failed\n\n" + e;
                }
            }

            return null;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (graphicsDeviceService != null)
            {
                graphicsDeviceService.Release(disposing);
                graphicsDeviceService = null;
            }

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected virtual void PaintUsingSystemDrawing(Graphics graphics, string text)
        {
            graphics.Clear(Color.CornflowerBlue);

            using (Brush brush = new SolidBrush(Color.Black))
            {
                using (var format = new StringFormat())
                {
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;

                    graphics.DrawString(text, Font, brush, ClientRectangle, format);
                }
            }
        }

        protected override sealed void OnPaintBackground(PaintEventArgs e)
        {
            /* do not touch */
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
        }
    }
}