using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using XorNet.Collections;
using System.Drawing;
using XorNet.Rose.Controls;
using XorNet.Rose.Properties;

namespace XorNet.Rose.Resource
{
	public class DefaultCaptions
	{
		public static string App = "Applet";
		public static string StaticControl = "Static";
		public static string TextControl = "Text";
		public static string EditControl = "Edit";
		public static string RadioControl = "Radio";
		public static string CheckboxControl = "Check";
		public static string CustomControl = "Custom";
	}

	public class DefaultStrings
	{
		public static string App = "APP_APPLET";
		public static string StaticControl = "WIDC_STATIC";
		public static string TextControl = "WIDC_TEXT";
		public static string ButtonControl = "WIDC_BUTTON";
		public static string EditControl = "WIDC_EDIT";
		public static string RadioControl = "WIDC_RADIO";
		public static string ComboControl = "WIDC_COMBO";
		public static string CheckboxControl = "WIDC_CHECK";
		public static string TabControl = "WIDC_TAB";
		public static string ListControl = "WIDC_LIST";
		public static string CustomControl = "WIDC_CUSTOM";
	}

	public class ResData
	{
		#region Properties
		public OrderedDictionary<string, string> Strings { get { return m_mapStrings; } }
		public OrderedDictionary<string, int> Apps { get { return m_mapApps; } }
		public OrderedDictionary<string, int> Controls { get { return m_mapControls; } }
		public Dictionary<string, WindowProperty> Properties { get { return m_mapProperties; } }
		#endregion

		#region Variables
		private OrderedDictionary<string, string> m_mapStrings = new OrderedDictionary<string, string>();
		private OrderedDictionary<string, int> m_mapApps = new OrderedDictionary<string, int>();
		private OrderedDictionary<string, int> m_mapControls = new OrderedDictionary<string, int>();
		private Dictionary<string, WindowProperty> m_mapProperties = new Dictionary<string, WindowProperty>();
		#endregion

		#region Construction
		public ResData()
		{
		}

		public void SortProperties()
		{
			m_mapProperties = (from entry in m_mapProperties orderby entry.Key ascending select entry).ToDictionary(pair => pair.Key, pair => pair.Value);
		}

		public void Clear()
		{
			m_mapStrings.Clear();
			m_mapApps.Clear();
			m_mapControls.Clear();
			m_mapProperties.Clear();
		}
		#endregion
	}

	public class ResManager
	{
		#region Properties
		public static ResManager Instance
		{
			get
			{
				return instance;
			}
		}

		public ResData Data
		{
			get
			{
				return m_data;
			}
		}

		public string SkinName
		{
			get { return m_szSkinName; }
			set { m_szSkinName = value; }
		}

		public string Directory
		{
			get { return m_szDirectory; }
			set { m_szDirectory = value; }
		}

		public Point DefaultWindowLocation = new Point(5, 5);
		public Rectangle DefaultDeflateRect = new Rectangle(4, 4, 8, 10);
		public int CaptionOffset = 18;
		// top: 16(4 + 8)
		// left: 16(4 + 8)
		// right: 8
		// bottom: 10
		#endregion

		#region Variables
		private static readonly ResManager instance = new ResManager();
		private ResData m_data = new ResData();
		private string m_szSkinName = "";
		private string m_szDirectory = "";
		#endregion

		#region Construction
		private ResManager() { }
		#endregion

		#region Functions
		public void Initialize(string szSkinName)
		{
			Data.Clear();
			FileInfo fi = new FileInfo(szSkinName);
			m_szSkinName = fi.Name;
			m_szDirectory = fi.DirectoryName;
			LoadSkin(Path.Combine(m_szDirectory, m_szSkinName));
			if(Skin.Instance.Offi == 0)
			{
				LoadHeader(Path.Combine(m_szDirectory, "resData.h"));
				LoadStrings(Path.Combine(m_szDirectory, "resData.txt.txt"));
				LoadProperties(Path.Combine(m_szDirectory, "resData.inc"));
			}
			else
			{
				LoadHeader(Path.Combine(m_szDirectory, "resDataOffi.h"));
				LoadStrings(Path.Combine(m_szDirectory, "resDataOffi.txt.txt"));
				LoadProperties(Path.Combine(m_szDirectory, "resDataOffi.inc"));
			}
		}

		public void Save()
		{
			if (Skin.Instance.Offi == 0)
			{
				Writer.SaveHeader(Path.Combine(m_szDirectory, "resData.h"), Data.Apps, Data.Controls);
				Writer.SaveProperties(Path.Combine(m_szDirectory, "resData.inc"), Data.Apps, Data.Properties);
				Writer.SaveStrings(Path.Combine(m_szDirectory, "resData.txt.txt"), Data.Strings);
			}
			else
			{
				Writer.SaveHeader(Path.Combine(m_szDirectory, "resDataOffi.h"), Data.Apps, Data.Controls);
				Writer.SaveProperties(Path.Combine(m_szDirectory, "resDataOffi.inc"), Data.Apps, Data.Properties);
				Writer.SaveStrings(Path.Combine(m_szDirectory, "resDataOffi.txt.txt"), Data.Strings);
			}
		}

		public string AddString(string szString="")
		{
			string szLast = "IDS_RESDATA_000000";
			if(Data.Strings.Count > 0)
				szLast = ((string)Data.Strings.Keys.Last());
			string szIndex = szLast.Substring(szLast.Length - 6);
			string szNewIndex = szLast.Substring(0, szLast.Length - 6) + (String.Format("{0:000000}", Int32.Parse(szIndex) + 1));

			Data.Strings.Add(szNewIndex, szString);
			return szNewIndex;
		}

		public string AddDefinedApp()
		{
			int nIndex = 1;
			string szApp = DefaultStrings.App;
			while (Data.Apps.Keys.Contains(szApp + nIndex))
				nIndex++;

			int nNewIndex = Data.Apps.Values.LastOrDefault() + 1;
			Data.Apps.Add(szApp + nIndex, nNewIndex);
			return szApp + nIndex;
		}

		public string AddDefaultControl(string szBase, List<string> ids)
		{
			int nIndex = 1;
			while (ids.Contains(szBase + nIndex))
				nIndex++;

			if (!Data.Controls.Keys.Contains(szBase + nIndex))
			{
				int nNewIndex = Data.Controls.Values.LastOrDefault() + 1;
				Data.Controls.Add(szBase + nIndex, nNewIndex);
			}
			return szBase + nIndex;
		}

		private void LoadProperties(string Filename)
		{
			Scanner s = new Scanner(Filename);

			string id = s.GetToken();
			while (id != null)
			{
				WindowProperty res = new WindowProperty();

				// id
				res.m_szID = id;

				// tex
				res.m_szTexture = s.GetToken();
				if (Skin.Instance.Offi != 0)
					s.GetToken();

				res.m_bTile = Convert.ToInt32(Convert.ToBoolean(s.GetNumber()));
				res.m_size = new Size(s.GetNumber(), s.GetNumber());
				res.m_style = (WindowStyle)s.GetNumber();
				//s.GetToken();

				s.GetToken(); // d3dFormat

				s.GetToken(); // {
				// title
				res.m_szTitle = s.GetToken();
				if (Data.Strings.Keys.Contains(res.m_szTitle))
					res.m_szTitle = Data.Strings[res.m_szTitle];
				s.GetToken(); // }

				s.GetToken(); // {
				// help key
				res.m_szHelp = s.GetToken();
				if (Data.Strings.Keys.Contains(res.m_szHelp))
					res.m_szHelp = Data.Strings[res.m_szHelp];
				s.GetToken(); // }

				s.GetToken(); // {
				string type = s.GetToken();
				while (!type.Equals("}"))
				{
					ControlProperty con = new ControlProperty();

					// type
					con.m_szType = type;

					// id
					con.m_szID = s.GetToken();

					// tex
					con.m_szTexture = s.GetToken();

					// tile
					con.m_bTile = Convert.ToInt32(Convert.ToBoolean(s.GetNumber()));

					// rect.left
					int left = s.GetNumber();
					int top = s.GetNumber();
					int right = s.GetNumber();
					int bottom = s.GetNumber();

					con.m_rectBounds = new Rectangle(left, top, right - left, bottom - top);

					con.m_style = (WindowStyle)s.GetNumber();

					con.m_bVisible = s.GetNumber();
					con.m_bGroup = s.GetNumber();
					con.m_bDisabled = s.GetNumber();
					con.m_bTabstop = s.GetNumber();

					if (Skin.Instance.Offi != 0)
					{
						s.GetToken();
						s.GetToken();
						s.GetToken();
					}
					else
					{
						//con.m_nfontcolor = s.GetNumber();
					}

					s.GetToken(); // {
					// title
					con.m_szTitle = s.GetToken();
					if (Data.Strings.Keys.Contains(con.m_szTitle))
						con.m_szTitle = Data.Strings[con.m_szTitle];
					s.GetToken(); // }

					s.GetToken(); // {
					// tooltip
					con.m_szTooltip = s.GetToken();
					if (Data.Strings.Keys.Contains(con.m_szTooltip))
						con.m_szTooltip = Data.Strings[con.m_szTooltip];
					s.GetToken(); // }

					// Store control.
					res.m_aControls.Add(con.m_szID, con);

					type = s.GetToken();
				} // }

				// Store resource.
				Data.Properties.Add(id, res);
				//m_szLastWindowResource = id;

				id = s.GetToken();
			}
			Data.SortProperties();
		}

		private void LoadStrings(string Filename)
		{
			DefineScanner s = new DefineScanner(Filename);

			string id = s.GetToken();
			while (id != null)
			{
				Data.Strings.Add(id, s.GetToken());
				id = s.GetToken();
			}
		}

		private void LoadHeader(string Filename)
		{
			HeaderScanner s = new HeaderScanner(Filename);

			string id = s.GetToken();
			while (id != null)
			{
				if (id.StartsWith("APP_"))
					Data.Apps.Add(id, s.GetNumber());
				else
					Data.Controls.Add(id, s.GetNumber());
				id = s.GetToken();
			}
		}

		private void LoadSkin(string Filename)
		{
			Scanner s = new Scanner(Filename);

			Skin.Instance.TextureDirectory = s.GetToken();
			Skin.Instance.Version = s.GetNumber();
			Skin.Instance.WindowCaptionCentered = s.GetNumber();
			Skin.Instance.Offi = s.GetNumber();
			Skin.Instance.DefaultColors.RootWindow.Text = Color.FromArgb(s.GetNumber());
			Skin.Instance.DefaultColors.RootWindow.Caption = Color.FromArgb(s.GetNumber());
			Skin.Instance.DefaultColors.RootWindow.CaptionOutline = Color.FromArgb(s.GetNumber());
			Skin.Instance.DefaultColors.StaticControl.Caption = Color.FromArgb(s.GetNumber());
			Skin.Instance.DefaultColors.StaticControl.CaptionText = Color.FromArgb(s.GetNumber());
			Skin.Instance.DefaultColors.ButtonControl.Caption = Color.FromArgb(s.GetNumber());
			Skin.Instance.DefaultColors.ButtonControl.CaptionOutline = Color.FromArgb(s.GetNumber());
			Skin.Instance.DefaultColors.ButtonControl.CaptionDisabled = Color.FromArgb(s.GetNumber());
			Skin.Instance.DefaultColors.ButtonControl.CaptionDisabledOutline = Color.FromArgb(s.GetNumber());
			Skin.Instance.DefaultColors.ButtonControl.CaptionPushed = Color.FromArgb(s.GetNumber());
			Skin.Instance.DefaultColors.ButtonControl.CaptionPushedOutline = Color.FromArgb(s.GetNumber());
			Skin.Instance.DefaultColors.ButtonControl.CaptionHighlighted = Color.FromArgb(s.GetNumber());
			Skin.Instance.DefaultColors.ButtonControl.CaptionHighlightedOutline = Color.FromArgb(s.GetNumber());
			Skin.Instance.DefaultColors.RadioControl.Caption = Color.FromArgb(s.GetNumber());
			Skin.Instance.DefaultColors.RadioControl.CaptionHighlighted = Color.FromArgb(s.GetNumber());
			Skin.Instance.DefaultColors.CheckboxControl.Caption = Color.FromArgb(s.GetNumber());
			Skin.Instance.DefaultColors.CheckboxControl.CaptionHighlighted = Color.FromArgb(s.GetNumber());
		}
		#endregion
	}
}
