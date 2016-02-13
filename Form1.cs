using System;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraEditors;

namespace Rose
{
    public partial class Form1 : XtraForm
    {
        int n;
        public EditorWindow selectedOwner;
        public Controls.Control selectedControl;
        public Form1()
        {
#if DIRECTX
            DirectX.D3DDeviceService.CreateDeviceService(this.Handle);
#endif
            InitializeComponent();
            InitSkinGallery();
        }

        private void InitSkinGallery()
        {
            SkinHelper.InitSkinGallery(rgbiSkins, true);
        }

        private void iNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            n++;
            var temp = new EditorWindow(this);
#if XNA
            temp.Creator = this;
#endif
            temp.MdiParent = this;
            temp.Text = "APP_NEWDIALOG0" + n;
            temp.Show();
        }

        public void SetPropertyControl(Controls.Control control, EditorWindow owner)
        {
            selectedControl = control;
            selectedOwner = owner;
            propertyGridControl1.SelectedObject = control;
        }
    }
}