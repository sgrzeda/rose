namespace Rose
{
    partial class RoseForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoseForm));
			this.ribbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
			this.barAndDockingController1 = new DevExpress.XtraBars.BarAndDockingController();
			this.ribbonImageCollection = new DevExpress.Utils.ImageCollection();
			this.iNew = new DevExpress.XtraBars.BarButtonItem();
			this.iOpen = new DevExpress.XtraBars.BarButtonItem();
			this.iClose = new DevExpress.XtraBars.BarButtonItem();
			this.iFind = new DevExpress.XtraBars.BarButtonItem();
			this.iSave = new DevExpress.XtraBars.BarButtonItem();
			this.iSaveAs = new DevExpress.XtraBars.BarButtonItem();
			this.iExit = new DevExpress.XtraBars.BarButtonItem();
			this.iHelp = new DevExpress.XtraBars.BarButtonItem();
			this.iAbout = new DevExpress.XtraBars.BarButtonItem();
			this.alignButtonGroup = new DevExpress.XtraBars.BarButtonGroup();
			this.iBoldFontStyle = new DevExpress.XtraBars.BarButtonItem();
			this.iItalicFontStyle = new DevExpress.XtraBars.BarButtonItem();
			this.iUnderlinedFontStyle = new DevExpress.XtraBars.BarButtonItem();
			this.fontStyleButtonGroup = new DevExpress.XtraBars.BarButtonGroup();
			this.iLeftTextAlign = new DevExpress.XtraBars.BarButtonItem();
			this.iCenterTextAlign = new DevExpress.XtraBars.BarButtonItem();
			this.iRightTextAlign = new DevExpress.XtraBars.BarButtonItem();
			this.rgbiSkins = new DevExpress.XtraBars.RibbonGalleryBarItem();
			this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
			this.ribbonImageCollectionLarge = new DevExpress.Utils.ImageCollection();
			this.homeRibbonPage = new DevExpress.XtraBars.Ribbon.RibbonPage();
			this.fileRibbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
			this.formatRibbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
			this.skinsRibbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
			this.exitRibbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
			this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
			this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
			this.helpRibbonPage = new DevExpress.XtraBars.Ribbon.RibbonPage();
			this.helpRibbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
			this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
			this.propertyGridControl1 = new DevExpress.XtraVerticalGrid.PropertyGridControl();
			this.treeList1 = new DevExpress.XtraTreeList.TreeList();
			this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager();
			this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
			this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
			this.xtraTabbedMdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager();
			((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ribbonImageCollection)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ribbonImageCollectionLarge)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.propertyGridControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
			this.dockPanel1.SuspendLayout();
			this.dockPanel1_Container.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).BeginInit();
			this.SuspendLayout();
			// 
			// ribbonControl
			// 
			this.ribbonControl.ApplicationButtonText = null;
			this.ribbonControl.Controller = this.barAndDockingController1;
			this.ribbonControl.ExpandCollapseItem.Id = 0;
			this.ribbonControl.ExpandCollapseItem.Name = "";
			this.ribbonControl.Images = this.ribbonImageCollection;
			this.ribbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl.ExpandCollapseItem,
            this.iNew,
            this.iOpen,
            this.iClose,
            this.iFind,
            this.iSave,
            this.iSaveAs,
            this.iExit,
            this.iHelp,
            this.iAbout,
            this.alignButtonGroup,
            this.iBoldFontStyle,
            this.iItalicFontStyle,
            this.iUnderlinedFontStyle,
            this.fontStyleButtonGroup,
            this.iLeftTextAlign,
            this.iCenterTextAlign,
            this.iRightTextAlign,
            this.rgbiSkins,
            this.barButtonItem1,
            this.barButtonItem2,
            this.barButtonItem3});
			this.ribbonControl.LargeImages = this.ribbonImageCollectionLarge;
			this.ribbonControl.Location = new System.Drawing.Point(0, 0);
			this.ribbonControl.MaxItemId = 74;
			this.ribbonControl.Name = "ribbonControl";
			this.ribbonControl.PageHeaderItemLinks.Add(this.iAbout);
			this.ribbonControl.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.homeRibbonPage,
            this.ribbonPage1,
            this.helpRibbonPage});
			this.ribbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2007;
			this.ribbonControl.Size = new System.Drawing.Size(950, 135);
			this.ribbonControl.StatusBar = this.ribbonStatusBar;
			this.ribbonControl.Toolbar.ItemLinks.Add(this.iNew);
			this.ribbonControl.Toolbar.ItemLinks.Add(this.iOpen);
			this.ribbonControl.Toolbar.ItemLinks.Add(this.iSave);
			this.ribbonControl.Toolbar.ItemLinks.Add(this.iSaveAs);
			this.ribbonControl.Toolbar.ItemLinks.Add(this.iHelp);
			// 
			// barAndDockingController1
			// 
			this.barAndDockingController1.PropertiesBar.AllowLinkLighting = false;
			// 
			// ribbonImageCollection
			// 
			this.ribbonImageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("ribbonImageCollection.ImageStream")));
			this.ribbonImageCollection.Images.SetKeyName(0, "Ribbon_New_16x16.png");
			this.ribbonImageCollection.Images.SetKeyName(1, "Ribbon_Open_16x16.png");
			this.ribbonImageCollection.Images.SetKeyName(2, "Ribbon_Close_16x16.png");
			this.ribbonImageCollection.Images.SetKeyName(3, "Ribbon_Find_16x16.png");
			this.ribbonImageCollection.Images.SetKeyName(4, "Ribbon_Save_16x16.png");
			this.ribbonImageCollection.Images.SetKeyName(5, "Ribbon_SaveAs_16x16.png");
			this.ribbonImageCollection.Images.SetKeyName(6, "Ribbon_Exit_16x16.png");
			this.ribbonImageCollection.Images.SetKeyName(7, "Ribbon_Content_16x16.png");
			this.ribbonImageCollection.Images.SetKeyName(8, "Ribbon_Info_16x16.png");
			this.ribbonImageCollection.Images.SetKeyName(9, "Ribbon_Bold_16x16.png");
			this.ribbonImageCollection.Images.SetKeyName(10, "Ribbon_Italic_16x16.png");
			this.ribbonImageCollection.Images.SetKeyName(11, "Ribbon_Underline_16x16.png");
			this.ribbonImageCollection.Images.SetKeyName(12, "Ribbon_AlignLeft_16x16.png");
			this.ribbonImageCollection.Images.SetKeyName(13, "Ribbon_AlignCenter_16x16.png");
			this.ribbonImageCollection.Images.SetKeyName(14, "Ribbon_AlignRight_16x16.png");
			// 
			// iNew
			// 
			this.iNew.Caption = "New";
			this.iNew.Description = "Creates a new, blank file.";
			this.iNew.Hint = "Creates a new, blank file";
			this.iNew.Id = 1;
			this.iNew.ImageIndex = 0;
			this.iNew.LargeImageIndex = 0;
			this.iNew.Name = "iNew";
			this.iNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.iNew_ItemClick);
			// 
			// iOpen
			// 
			this.iOpen.Caption = "&Open";
			this.iOpen.Description = "Opens a file.";
			this.iOpen.Hint = "Opens a file";
			this.iOpen.Id = 2;
			this.iOpen.ImageIndex = 1;
			this.iOpen.LargeImageIndex = 1;
			this.iOpen.Name = "iOpen";
			this.iOpen.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)((DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
			// 
			// iClose
			// 
			this.iClose.Caption = "&Close";
			this.iClose.Description = "Closes the active document.";
			this.iClose.Hint = "Closes the active document";
			this.iClose.Id = 3;
			this.iClose.ImageIndex = 2;
			this.iClose.LargeImageIndex = 2;
			this.iClose.Name = "iClose";
			this.iClose.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)((DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
			// 
			// iFind
			// 
			this.iFind.Caption = "Find";
			this.iFind.Description = "Searches for the specified info.";
			this.iFind.Hint = "Searches for the specified info";
			this.iFind.Id = 15;
			this.iFind.ImageIndex = 3;
			this.iFind.LargeImageIndex = 3;
			this.iFind.Name = "iFind";
			this.iFind.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)((DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
			// 
			// iSave
			// 
			this.iSave.Caption = "&Save";
			this.iSave.Description = "Saves the active document.";
			this.iSave.Hint = "Saves the active document";
			this.iSave.Id = 16;
			this.iSave.ImageIndex = 4;
			this.iSave.LargeImageIndex = 4;
			this.iSave.Name = "iSave";
			// 
			// iSaveAs
			// 
			this.iSaveAs.Caption = "Save As";
			this.iSaveAs.Description = "Saves the active document in a different location.";
			this.iSaveAs.Hint = "Saves the active document in a different location";
			this.iSaveAs.Id = 17;
			this.iSaveAs.ImageIndex = 5;
			this.iSaveAs.LargeImageIndex = 5;
			this.iSaveAs.Name = "iSaveAs";
			// 
			// iExit
			// 
			this.iExit.Caption = "Exit";
			this.iExit.Description = "Closes this program after prompting you to save unsaved data.";
			this.iExit.Hint = "Closes this program after prompting you to save unsaved data";
			this.iExit.Id = 20;
			this.iExit.ImageIndex = 6;
			this.iExit.LargeImageIndex = 6;
			this.iExit.Name = "iExit";
			this.iExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.iExit_ItemClick);
			// 
			// iHelp
			// 
			this.iHelp.Caption = "Help";
			this.iHelp.Description = "Start the program help system.";
			this.iHelp.Hint = "Start the program help system";
			this.iHelp.Id = 22;
			this.iHelp.ImageIndex = 7;
			this.iHelp.LargeImageIndex = 7;
			this.iHelp.Name = "iHelp";
			// 
			// iAbout
			// 
			this.iAbout.Caption = "About";
			this.iAbout.Description = "Displays general program information.";
			this.iAbout.Hint = "Displays general program information";
			this.iAbout.Id = 24;
			this.iAbout.ImageIndex = 8;
			this.iAbout.LargeImageIndex = 8;
			this.iAbout.Name = "iAbout";
			// 
			// alignButtonGroup
			// 
			this.alignButtonGroup.Caption = "Align Commands";
			this.alignButtonGroup.Id = 52;
			this.alignButtonGroup.ItemLinks.Add(this.iBoldFontStyle);
			this.alignButtonGroup.ItemLinks.Add(this.iItalicFontStyle);
			this.alignButtonGroup.ItemLinks.Add(this.iUnderlinedFontStyle);
			this.alignButtonGroup.Name = "alignButtonGroup";
			// 
			// iBoldFontStyle
			// 
			this.iBoldFontStyle.Caption = "Bold";
			this.iBoldFontStyle.Id = 53;
			this.iBoldFontStyle.ImageIndex = 9;
			this.iBoldFontStyle.Name = "iBoldFontStyle";
			// 
			// iItalicFontStyle
			// 
			this.iItalicFontStyle.Caption = "Italic";
			this.iItalicFontStyle.Id = 54;
			this.iItalicFontStyle.ImageIndex = 10;
			this.iItalicFontStyle.Name = "iItalicFontStyle";
			// 
			// iUnderlinedFontStyle
			// 
			this.iUnderlinedFontStyle.Caption = "Underlined";
			this.iUnderlinedFontStyle.Id = 55;
			this.iUnderlinedFontStyle.ImageIndex = 11;
			this.iUnderlinedFontStyle.Name = "iUnderlinedFontStyle";
			// 
			// fontStyleButtonGroup
			// 
			this.fontStyleButtonGroup.Caption = "Font Style";
			this.fontStyleButtonGroup.Id = 56;
			this.fontStyleButtonGroup.ItemLinks.Add(this.iLeftTextAlign);
			this.fontStyleButtonGroup.ItemLinks.Add(this.iCenterTextAlign);
			this.fontStyleButtonGroup.ItemLinks.Add(this.iRightTextAlign);
			this.fontStyleButtonGroup.Name = "fontStyleButtonGroup";
			// 
			// iLeftTextAlign
			// 
			this.iLeftTextAlign.Caption = "Left";
			this.iLeftTextAlign.Id = 57;
			this.iLeftTextAlign.ImageIndex = 12;
			this.iLeftTextAlign.Name = "iLeftTextAlign";
			// 
			// iCenterTextAlign
			// 
			this.iCenterTextAlign.Caption = "Center";
			this.iCenterTextAlign.Id = 58;
			this.iCenterTextAlign.ImageIndex = 13;
			this.iCenterTextAlign.Name = "iCenterTextAlign";
			// 
			// iRightTextAlign
			// 
			this.iRightTextAlign.Caption = "Right";
			this.iRightTextAlign.Id = 59;
			this.iRightTextAlign.ImageIndex = 14;
			this.iRightTextAlign.Name = "iRightTextAlign";
			// 
			// rgbiSkins
			// 
			this.rgbiSkins.Caption = "Skins";
			// 
			// rgbiSkins
			// 
			this.rgbiSkins.Gallery.AllowHoverImages = true;
			this.rgbiSkins.Gallery.Appearance.ItemCaptionAppearance.Normal.Options.UseFont = true;
			this.rgbiSkins.Gallery.Appearance.ItemCaptionAppearance.Normal.Options.UseTextOptions = true;
			this.rgbiSkins.Gallery.Appearance.ItemCaptionAppearance.Normal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.rgbiSkins.Gallery.ColumnCount = 4;
			this.rgbiSkins.Gallery.FixedHoverImageSize = false;
			this.rgbiSkins.Gallery.ImageSize = new System.Drawing.Size(32, 17);
			this.rgbiSkins.Gallery.ItemImageLocation = DevExpress.Utils.Locations.Top;
			this.rgbiSkins.Gallery.RowCount = 4;
			this.rgbiSkins.Id = 60;
			this.rgbiSkins.Name = "rgbiSkins";
			// 
			// barButtonItem1
			// 
			this.barButtonItem1.Caption = "Text Control";
			this.barButtonItem1.Id = 71;
			this.barButtonItem1.Name = "barButtonItem1";
			// 
			// barButtonItem2
			// 
			this.barButtonItem2.Caption = "Static Control";
			this.barButtonItem2.Id = 72;
			this.barButtonItem2.Name = "barButtonItem2";
			// 
			// barButtonItem3
			// 
			this.barButtonItem3.Caption = "Edit Control";
			this.barButtonItem3.Id = 73;
			this.barButtonItem3.Name = "barButtonItem3";
			// 
			// ribbonImageCollectionLarge
			// 
			this.ribbonImageCollectionLarge.ImageSize = new System.Drawing.Size(32, 32);
			this.ribbonImageCollectionLarge.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("ribbonImageCollectionLarge.ImageStream")));
			this.ribbonImageCollectionLarge.Images.SetKeyName(0, "Ribbon_New_32x32.png");
			this.ribbonImageCollectionLarge.Images.SetKeyName(1, "Ribbon_Open_32x32.png");
			this.ribbonImageCollectionLarge.Images.SetKeyName(2, "Ribbon_Close_32x32.png");
			this.ribbonImageCollectionLarge.Images.SetKeyName(3, "Ribbon_Find_32x32.png");
			this.ribbonImageCollectionLarge.Images.SetKeyName(4, "Ribbon_Save_32x32.png");
			this.ribbonImageCollectionLarge.Images.SetKeyName(5, "Ribbon_SaveAs_32x32.png");
			this.ribbonImageCollectionLarge.Images.SetKeyName(6, "Ribbon_Exit_32x32.png");
			this.ribbonImageCollectionLarge.Images.SetKeyName(7, "Ribbon_Content_32x32.png");
			this.ribbonImageCollectionLarge.Images.SetKeyName(8, "Ribbon_Info_32x32.png");
			// 
			// homeRibbonPage
			// 
			this.homeRibbonPage.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.fileRibbonPageGroup,
            this.formatRibbonPageGroup,
            this.skinsRibbonPageGroup,
            this.exitRibbonPageGroup});
			this.homeRibbonPage.Name = "homeRibbonPage";
			this.homeRibbonPage.Text = "Home";
			// 
			// fileRibbonPageGroup
			// 
			this.fileRibbonPageGroup.ItemLinks.Add(this.iNew);
			this.fileRibbonPageGroup.ItemLinks.Add(this.iOpen);
			this.fileRibbonPageGroup.ItemLinks.Add(this.iClose);
			this.fileRibbonPageGroup.ItemLinks.Add(this.iFind);
			this.fileRibbonPageGroup.ItemLinks.Add(this.iSave);
			this.fileRibbonPageGroup.ItemLinks.Add(this.iSaveAs);
			this.fileRibbonPageGroup.Name = "fileRibbonPageGroup";
			this.fileRibbonPageGroup.Text = "File";
			// 
			// formatRibbonPageGroup
			// 
			this.formatRibbonPageGroup.ItemLinks.Add(this.alignButtonGroup);
			this.formatRibbonPageGroup.ItemLinks.Add(this.fontStyleButtonGroup);
			this.formatRibbonPageGroup.Name = "formatRibbonPageGroup";
			this.formatRibbonPageGroup.Text = "Format";
			// 
			// skinsRibbonPageGroup
			// 
			this.skinsRibbonPageGroup.ItemLinks.Add(this.rgbiSkins);
			this.skinsRibbonPageGroup.Name = "skinsRibbonPageGroup";
			this.skinsRibbonPageGroup.ShowCaptionButton = false;
			this.skinsRibbonPageGroup.Text = "Skins";
			// 
			// exitRibbonPageGroup
			// 
			this.exitRibbonPageGroup.ItemLinks.Add(this.iExit);
			this.exitRibbonPageGroup.Name = "exitRibbonPageGroup";
			this.exitRibbonPageGroup.Text = "Exit";
			// 
			// ribbonPage1
			// 
			this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
			this.ribbonPage1.Name = "ribbonPage1";
			this.ribbonPage1.Text = "Controls";
			// 
			// ribbonPageGroup1
			// 
			this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItem1);
			this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItem2);
			this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItem3);
			this.ribbonPageGroup1.Name = "ribbonPageGroup1";
			this.ribbonPageGroup1.Text = "Controls";
			// 
			// helpRibbonPage
			// 
			this.helpRibbonPage.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.helpRibbonPageGroup});
			this.helpRibbonPage.Name = "helpRibbonPage";
			this.helpRibbonPage.Text = "Help";
			// 
			// helpRibbonPageGroup
			// 
			this.helpRibbonPageGroup.ItemLinks.Add(this.iHelp);
			this.helpRibbonPageGroup.ItemLinks.Add(this.iAbout);
			this.helpRibbonPageGroup.Name = "helpRibbonPageGroup";
			this.helpRibbonPageGroup.Text = "Help";
			// 
			// ribbonStatusBar
			// 
			this.ribbonStatusBar.Location = new System.Drawing.Point(0, 524);
			this.ribbonStatusBar.Name = "ribbonStatusBar";
			this.ribbonStatusBar.Ribbon = this.ribbonControl;
			this.ribbonStatusBar.Size = new System.Drawing.Size(950, 26);
			// 
			// propertyGridControl1
			// 
			this.propertyGridControl1.AutoGenerateRows = true;
			this.propertyGridControl1.Location = new System.Drawing.Point(0, 0);
			this.propertyGridControl1.Margin = new System.Windows.Forms.Padding(0);
			this.propertyGridControl1.Name = "propertyGridControl1";
			this.propertyGridControl1.OptionsView.ShowFocusedFrame = false;
			this.propertyGridControl1.Size = new System.Drawing.Size(192, 354);
			this.propertyGridControl1.TabIndex = 11;
			// 
			// treeList1
			// 
			this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
			this.treeList1.Dock = System.Windows.Forms.DockStyle.Left;
			this.treeList1.Location = new System.Drawing.Point(0, 135);
			this.treeList1.Name = "treeList1";
			this.treeList1.BeginUnboundLoad();
			this.treeList1.AppendNode(new object[] {
            "Dialog"}, -1);
			this.treeList1.AppendNode(new object[] {
            "wew"}, 0);
			this.treeList1.AppendNode(new object[] {
            "Icon"}, -1);
			this.treeList1.AppendNode(new object[] {
            "wew"}, 2);
			this.treeList1.AppendNode(new object[] {
            "Menu"}, -1);
			this.treeList1.AppendNode(new object[] {
            "wew"}, 4);
			this.treeList1.AppendNode(new object[] {
            "Toolbar"}, -1);
			this.treeList1.AppendNode(new object[] {
            "wew"}, 6);
			this.treeList1.EndUnboundLoad();
			this.treeList1.OptionsBehavior.Editable = false;
			this.treeList1.OptionsView.ShowFocusedFrame = false;
			this.treeList1.Size = new System.Drawing.Size(234, 389);
			this.treeList1.TabIndex = 8;
			// 
			// treeListColumn1
			// 
			this.treeListColumn1.Caption = "Rose";
			this.treeListColumn1.FieldName = "Rose";
			this.treeListColumn1.MinWidth = 52;
			this.treeListColumn1.Name = "treeListColumn1";
			this.treeListColumn1.Visible = true;
			this.treeListColumn1.VisibleIndex = 0;
			// 
			// dockManager1
			// 
			this.dockManager1.Controller = this.barAndDockingController1;
			this.dockManager1.Form = this;
			this.dockManager1.Images = this.ribbonImageCollection;
			this.dockManager1.MenuManager = this.ribbonControl;
			this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel1});
			this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
			// 
			// dockPanel1
			// 
			this.dockPanel1.Controls.Add(this.dockPanel1_Container);
			this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
			this.dockPanel1.ID = new System.Guid("d54875a7-5095-492b-9c35-8d58a7221d4b");
			this.dockPanel1.Location = new System.Drawing.Point(750, 135);
			this.dockPanel1.Name = "dockPanel1";
			this.dockPanel1.OriginalSize = new System.Drawing.Size(200, 200);
			this.dockPanel1.Size = new System.Drawing.Size(200, 389);
			this.dockPanel1.Text = "dockPanel1";
			// 
			// dockPanel1_Container
			// 
			this.dockPanel1_Container.Controls.Add(this.propertyGridControl1);
			this.dockPanel1_Container.Location = new System.Drawing.Point(3, 24);
			this.dockPanel1_Container.Name = "dockPanel1_Container";
			this.dockPanel1_Container.Size = new System.Drawing.Size(194, 362);
			this.dockPanel1_Container.TabIndex = 0;
			// 
			// xtraTabbedMdiManager1
			// 
			this.xtraTabbedMdiManager1.Controller = this.barAndDockingController1;
			this.xtraTabbedMdiManager1.MdiParent = this;
			// 
			// RoseForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(950, 550);
			this.Controls.Add(this.treeList1);
			this.Controls.Add(this.dockPanel1);
			this.Controls.Add(this.ribbonControl);
			this.Controls.Add(this.ribbonStatusBar);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.IsMdiContainer = true;
			this.Name = "RoseForm";
			this.Text = "Rose";
			((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ribbonImageCollection)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ribbonImageCollectionLarge)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.propertyGridControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
			this.dockPanel1.ResumeLayout(false);
			this.dockPanel1_Container.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.Utils.ImageCollection ribbonImageCollection;
        private DevExpress.Utils.ImageCollection ribbonImageCollectionLarge;
		private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraVerticalGrid.PropertyGridControl propertyGridControl1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl;
        private DevExpress.XtraBars.BarButtonItem iNew;
        private DevExpress.XtraBars.BarButtonItem iOpen;
        private DevExpress.XtraBars.BarButtonItem iClose;
        private DevExpress.XtraBars.BarButtonItem iFind;
        private DevExpress.XtraBars.BarButtonItem iSave;
        private DevExpress.XtraBars.BarButtonItem iSaveAs;
        private DevExpress.XtraBars.BarButtonItem iExit;
        private DevExpress.XtraBars.BarButtonItem iHelp;
        private DevExpress.XtraBars.BarButtonItem iAbout;
        private DevExpress.XtraBars.BarButtonGroup alignButtonGroup;
        private DevExpress.XtraBars.BarButtonItem iBoldFontStyle;
        private DevExpress.XtraBars.BarButtonItem iItalicFontStyle;
        private DevExpress.XtraBars.BarButtonItem iUnderlinedFontStyle;
        private DevExpress.XtraBars.BarButtonGroup fontStyleButtonGroup;
        private DevExpress.XtraBars.BarButtonItem iLeftTextAlign;
        private DevExpress.XtraBars.BarButtonItem iCenterTextAlign;
        private DevExpress.XtraBars.BarButtonItem iRightTextAlign;
        private DevExpress.XtraBars.RibbonGalleryBarItem rgbiSkins;
        private DevExpress.XtraBars.Ribbon.RibbonPage homeRibbonPage;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup fileRibbonPageGroup;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup formatRibbonPageGroup;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup skinsRibbonPageGroup;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup exitRibbonPageGroup;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPage helpRibbonPage;
		private DevExpress.XtraBars.Ribbon.RibbonPageGroup helpRibbonPageGroup;
		private DevExpress.XtraBars.BarButtonItem barButtonItem1;
		private DevExpress.XtraBars.BarButtonItem barButtonItem2;
		private DevExpress.XtraBars.BarButtonItem barButtonItem3;
		private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
		private DevExpress.XtraBars.BarAndDockingController barAndDockingController1;
		private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
		private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
		private DevExpress.XtraBars.Docking.DockManager dockManager1;
		private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;

    }
}
