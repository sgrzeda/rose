using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace XorNet.Rose.DirectX
{
    public partial class D3DWindowBase : XtraForm /*Form*/
    {
        private D3DDeviceService d3DDeviceService;

        public D3DWindowBase()
        {
            d3DDeviceService = D3DDeviceService.Instance;
            Application.Idle += delegate { Invalidate(); };
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
			UpdateLogic();
			if (d3DDeviceService == null)
				DirectX.D3DDeviceService.CreateDeviceService(this.Handle);
            if (d3DDeviceService.BeginRender())
            {
                Draw();
                d3DDeviceService.EndRender(this.Handle, ClientSize.Width, ClientSize.Height);
            }
        }

        public virtual void Draw()
        {

        }

        public virtual void UpdateLogic()
        {
            
        }

        protected override sealed void OnPaintBackground(PaintEventArgs e)
        {
            /* do not touch */
        }
    }
}
