using System;
using System.Windows.Forms;
using XorNet.Rose.Resource;
using System.IO;
using XorNet.Rose.Forms;
using DevExpress.LookAndFeel;
using XorNet.Rose.Properties;
using DevExpress.XtraSplashScreen;
using System.Drawing;
using System.Reflection;

namespace XorNet.Rose 
{
    static class Program
	{
        [STAThread]
        static void Main()
		{
			SplashScreenManager.ShowImage(new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("XorNet.Rose.SplashScreen_Rose.png")), true, false);
			DevExpress.UserSkins.BonusSkins.Register();
			if (Settings.Default.SkinForms)
				DevExpress.Skins.SkinManager.EnableFormSkins();
			else
				DevExpress.Skins.SkinManager.DisableFormSkins();
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Application.Run(new frmMain());
        }
    }
}
