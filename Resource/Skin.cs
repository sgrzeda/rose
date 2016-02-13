using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace XorNet.Rose.Resource
{
	class Skin
	{
		#region Accessibility
		public static Skin Instance
		{
			get
			{
				return instance;
			}
		}
		#endregion

		#region Variables
		public string TextureDirectory = "Default";
		public int Version = 0;
		public int WindowCaptionCentered = 0;
		public int Offi = 0;
		public class tagDefaultColors
		{
			public class tagRootWindow
			{
				public Color Caption = Color.White;
				public Color CaptionOutline = Color.Red;
				public Color Text = Color.Black;
			}
			public tagRootWindow RootWindow = new tagRootWindow();

			public class tagStaticCtrl
			{
				public Color Caption = Color.FromArgb(46, 112, 169);
				public Color CaptionText = Color.White;
			}
			public tagStaticCtrl StaticControl = new tagStaticCtrl();

			public class tagButtonCtrl
			{
				public Color Caption = Color.White;
				public Color CaptionOutline = Color.FromArgb(255, 109, 109, 109);
				public Color CaptionPushed = Color.White;
				public Color CaptionPushedOutline = Color.FromArgb(255, 109, 109, 109);
				public Color CaptionDisabled = Color.White;
				public Color CaptionDisabledOutline = Color.FromArgb(255, 182, 182, 182);
				public Color CaptionHighlighted = Color.FromArgb(255, 244, 205, 182);
				public Color CaptionHighlightedOutline = Color.FromArgb(255, 225, 114, 48);
			}
			public tagButtonCtrl ButtonControl = new tagButtonCtrl();

			public class tagRadioCtrl
			{
				public Color Caption = Color.FromArgb(64, 64, 64);
				public Color CaptionHighlighted = Color.FromArgb(64, 64, 255);
			}
			public tagRadioCtrl RadioControl = new tagRadioCtrl();

			public class tagCheckboxCtrl
			{
				public Color Caption = Color.FromArgb(64, 64, 64);
				public Color CaptionHighlighted = Color.FromArgb(64, 64, 255);
			}
			public tagCheckboxCtrl CheckboxControl = new tagCheckboxCtrl();
		}
		public tagDefaultColors DefaultColors = new tagDefaultColors();

		private static readonly Skin instance = new Skin();
		#endregion

		public Skin()
		{

		}

		~Skin()
		{

		}
	}
}
