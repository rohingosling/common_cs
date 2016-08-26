/*
 * Created by SharpDevelop.
 * User: User1
 * Date: 2011/03/05
 * Time: 09:53 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace WhiteBoardCapturer
{
	partial class FormMain
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuItemFileSeperator1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemFileClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemFileSeperator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemView = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemViewRuler = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemViewToolBar = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemViewStatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemShowShadow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButtonOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSaveAs = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonZoomIn = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonZoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBoxZoom = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonProcessImage = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelImageSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelSpacer1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelImageWidth = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelSpacer2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelImageHeight = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelSpacer3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelProcessingImage = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBarProcessingImage = new System.Windows.Forms.ToolStripProgressBar();
            this.panelWorkSpace = new System.Windows.Forms.Panel();
            this.pictureBoxShadowTopRight = new System.Windows.Forms.PictureBox();
            this.pictureBoxShadowBottomLeft = new System.Windows.Forms.PictureBox();
            this.pictureBoxShadowBottom = new System.Windows.Forms.PictureBox();
            this.pictureBoxShadowBottomRight = new System.Windows.Forms.PictureBox();
            this.pictureBoxShadowRight = new System.Windows.Forms.PictureBox();
            this.pictureBoxDestination = new System.Windows.Forms.PictureBox();
            this.pictureBoxSource = new System.Windows.Forms.PictureBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.panelWorkSpace.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxShadowTopRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxShadowBottomLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxShadowBottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxShadowBottomRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxShadowRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDestination)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSource)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFileSeperator1,
            this.editToolStripMenuItem,
            this.menuItemView,
            this.toolsToolStripMenuItem,
            this.windowToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip.Size = new System.Drawing.Size(792, 24);
            this.menuStrip.TabIndex = 0;
            // 
            // menuItemFileSeperator1
            // 
            this.menuItemFileSeperator1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuItemFileSeperator1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFileNew,
            this.menuItemFileOpen,
            this.menuItemFileClose,
            this.toolStripSeparator1,
            this.menuItemFileSave,
            this.menuItemFileSaveAs,
            this.menuItemFileSeperator2,
            this.menuItemFileExit});
            this.menuItemFileSeperator1.Name = "menuItemFileSeperator1";
            this.menuItemFileSeperator1.Size = new System.Drawing.Size(35, 20);
            this.menuItemFileSeperator1.Text = "File";
            // 
            // menuItemFileNew
            // 
            this.menuItemFileNew.Enabled = false;
            this.menuItemFileNew.Image = ((System.Drawing.Image)(resources.GetObject("menuItemFileNew.Image")));
            this.menuItemFileNew.Name = "menuItemFileNew";
            this.menuItemFileNew.Size = new System.Drawing.Size(152, 22);
            this.menuItemFileNew.Text = "New";
            // 
            // menuItemFileOpen
            // 
            this.menuItemFileOpen.Image = ((System.Drawing.Image)(resources.GetObject("menuItemFileOpen.Image")));
            this.menuItemFileOpen.Name = "menuItemFileOpen";
            this.menuItemFileOpen.Size = new System.Drawing.Size(152, 22);
            this.menuItemFileOpen.Text = "Open...";
            this.menuItemFileOpen.Click += new System.EventHandler(this.menuItemFileOpen_Click);
            // 
            // menuItemFileClose
            // 
            this.menuItemFileClose.Enabled = false;
            this.menuItemFileClose.Name = "menuItemFileClose";
            this.menuItemFileClose.Size = new System.Drawing.Size(152, 22);
            this.menuItemFileClose.Text = "Close";
            this.menuItemFileClose.Click += new System.EventHandler(this.menuItemFileClose_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // menuItemFileSave
            // 
            this.menuItemFileSave.Enabled = false;
            this.menuItemFileSave.Image = ((System.Drawing.Image)(resources.GetObject("menuItemFileSave.Image")));
            this.menuItemFileSave.Name = "menuItemFileSave";
            this.menuItemFileSave.Size = new System.Drawing.Size(152, 22);
            this.menuItemFileSave.Text = "Save";
            this.menuItemFileSave.Click += new System.EventHandler(this.menuItemFileSave_Click);
            // 
            // menuItemFileSaveAs
            // 
            this.menuItemFileSaveAs.Enabled = false;
            this.menuItemFileSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("menuItemFileSaveAs.Image")));
            this.menuItemFileSaveAs.Name = "menuItemFileSaveAs";
            this.menuItemFileSaveAs.Size = new System.Drawing.Size(152, 22);
            this.menuItemFileSaveAs.Text = "Save As...";
            this.menuItemFileSaveAs.Click += new System.EventHandler(this.menuItemFileSaveAs_Click);
            // 
            // menuItemFileSeperator2
            // 
            this.menuItemFileSeperator2.Name = "menuItemFileSeperator2";
            this.menuItemFileSeperator2.Size = new System.Drawing.Size(149, 6);
            // 
            // menuItemFileExit
            // 
            this.menuItemFileExit.Name = "menuItemFileExit";
            this.menuItemFileExit.Size = new System.Drawing.Size(152, 22);
            this.menuItemFileExit.Text = "Exit";
            this.menuItemFileExit.Click += new System.EventHandler(this.menuItemFileExit_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Enabled = false;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // menuItemView
            // 
            this.menuItemView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemViewRuler,
            this.menuItemViewToolBar,
            this.menuItemViewStatusBar,
            this.menuItemShowShadow});
            this.menuItemView.Name = "menuItemView";
            this.menuItemView.Size = new System.Drawing.Size(41, 20);
            this.menuItemView.Text = "View";
            // 
            // menuItemViewRuler
            // 
            this.menuItemViewRuler.CheckOnClick = true;
            this.menuItemViewRuler.Enabled = false;
            this.menuItemViewRuler.Name = "menuItemViewRuler";
            this.menuItemViewRuler.Size = new System.Drawing.Size(174, 22);
            this.menuItemViewRuler.Text = "Ruler";
            // 
            // menuItemViewToolBar
            // 
            this.menuItemViewToolBar.Checked = true;
            this.menuItemViewToolBar.CheckOnClick = true;
            this.menuItemViewToolBar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuItemViewToolBar.Name = "menuItemViewToolBar";
            this.menuItemViewToolBar.Size = new System.Drawing.Size(174, 22);
            this.menuItemViewToolBar.Text = "Tool Bar";
            this.menuItemViewToolBar.Click += new System.EventHandler(this.MenuItemViewToolBar_Click);
            // 
            // menuItemViewStatusBar
            // 
            this.menuItemViewStatusBar.Checked = true;
            this.menuItemViewStatusBar.CheckOnClick = true;
            this.menuItemViewStatusBar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuItemViewStatusBar.Name = "menuItemViewStatusBar";
            this.menuItemViewStatusBar.Size = new System.Drawing.Size(174, 22);
            this.menuItemViewStatusBar.Text = "Status Bar";
            this.menuItemViewStatusBar.Click += new System.EventHandler(this.MenuItemViewStatusBar_Click);
            // 
            // menuItemShowShadow
            // 
            this.menuItemShowShadow.Checked = true;
            this.menuItemShowShadow.CheckOnClick = true;
            this.menuItemShowShadow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuItemShowShadow.Name = "menuItemShowShadow";
            this.menuItemShowShadow.Size = new System.Drawing.Size(174, 22);
            this.menuItemShowShadow.Text = "Show Image Shadow";
            this.menuItemShowShadow.Click += new System.EventHandler(this.menuItemShowShadow_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.Enabled = false;
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.Enabled = false;
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.windowToolStripMenuItem.Text = "Window";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Enabled = false;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripButtonOpen,
            this.toolStripButtonSave,
            this.toolStripButtonSaveAs,
            this.toolStripSeparator3,
            this.toolStripButtonZoomIn,
            this.toolStripButtonZoomOut,
            this.toolStripComboBoxZoom,
            this.toolStripSeparator2,
            this.toolStripButtonProcessImage});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.Size = new System.Drawing.Size(792, 25);
            this.toolStrip.TabIndex = 1;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(10, 22);
            this.toolStripLabel1.Text = " ";
            // 
            // toolStripButtonOpen
            // 
            this.toolStripButtonOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOpen.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOpen.Image")));
            this.toolStripButtonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpen.Name = "toolStripButtonOpen";
            this.toolStripButtonOpen.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonOpen.ToolTipText = "Open image";
            this.toolStripButtonOpen.Click += new System.EventHandler(this.toolStripButtonOpen_Click_1);
            // 
            // toolStripButtonSave
            // 
            this.toolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSave.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSave.Image")));
            this.toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSave.Name = "toolStripButtonSave";
            this.toolStripButtonSave.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonSave.ToolTipText = "Save image";
            this.toolStripButtonSave.Click += new System.EventHandler(this.toolStripButtonSave_Click);
            // 
            // toolStripButtonSaveAs
            // 
            this.toolStripButtonSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSaveAs.Image")));
            this.toolStripButtonSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSaveAs.Name = "toolStripButtonSaveAs";
            this.toolStripButtonSaveAs.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonSaveAs.ToolTipText = "Save image as...";
            this.toolStripButtonSaveAs.Click += new System.EventHandler(this.toolStripButtonSaveAs_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AutoSize = false;
            this.toolStripSeparator3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 20);
            // 
            // toolStripButtonZoomIn
            // 
            this.toolStripButtonZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonZoomIn.Image")));
            this.toolStripButtonZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonZoomIn.Name = "toolStripButtonZoomIn";
            this.toolStripButtonZoomIn.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonZoomIn.ToolTipText = "Zoom in";
            // 
            // toolStripButtonZoomOut
            // 
            this.toolStripButtonZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonZoomOut.Image")));
            this.toolStripButtonZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonZoomOut.Name = "toolStripButtonZoomOut";
            this.toolStripButtonZoomOut.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonZoomOut.ToolTipText = "Zoom out";
            // 
            // toolStripComboBoxZoom
            // 
            this.toolStripComboBoxZoom.AutoSize = false;
            this.toolStripComboBoxZoom.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.toolStripComboBoxZoom.Items.AddRange(new object[] {
            "10 %",
            "25 %",
            "50 %",
            "75 %",
            "100 %",
            "150 %",
            "200 %"});
            this.toolStripComboBoxZoom.Name = "toolStripComboBoxZoom";
            this.toolStripComboBoxZoom.Size = new System.Drawing.Size(60, 21);
            this.toolStripComboBoxZoom.Text = "50%";
            this.toolStripComboBoxZoom.ToolTipText = "Zoom level";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 20);
            // 
            // toolStripButtonProcessImage
            // 
            this.toolStripButtonProcessImage.CheckOnClick = true;
            this.toolStripButtonProcessImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonProcessImage.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonProcessImage.Image")));
            this.toolStripButtonProcessImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonProcessImage.Name = "toolStripButtonProcessImage";
            this.toolStripButtonProcessImage.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonProcessImage.Text = "toolStripButton1";
            this.toolStripButtonProcessImage.Click += new System.EventHandler(this.toolStripButtonProcessImage_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelImageSize,
            this.toolStripStatusLabelSpacer1,
            this.toolStripStatusLabelImageWidth,
            this.toolStripStatusLabelSpacer2,
            this.toolStripStatusLabelImageHeight,
            this.toolStripStatusLabelSpacer3,
            this.toolStripStatusLabelProcessingImage,
            this.toolStripProgressBarProcessingImage});
            this.statusStrip.Location = new System.Drawing.Point(0, 544);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(792, 22);
            this.statusStrip.TabIndex = 2;
            // 
            // toolStripStatusLabelImageSize
            // 
            this.toolStripStatusLabelImageSize.Name = "toolStripStatusLabelImageSize";
            this.toolStripStatusLabelImageSize.Size = new System.Drawing.Size(117, 17);
            this.toolStripStatusLabelImageSize.Text = "File Length: 000000 KB";
            // 
            // toolStripStatusLabelSpacer1
            // 
            this.toolStripStatusLabelSpacer1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabelSpacer1.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.toolStripStatusLabelSpacer1.Name = "toolStripStatusLabelSpacer1";
            this.toolStripStatusLabelSpacer1.Size = new System.Drawing.Size(14, 17);
            this.toolStripStatusLabelSpacer1.Text = " ";
            // 
            // toolStripStatusLabelImageWidth
            // 
            this.toolStripStatusLabelImageWidth.Name = "toolStripStatusLabelImageWidth";
            this.toolStripStatusLabelImageWidth.Size = new System.Drawing.Size(78, 17);
            this.toolStripStatusLabelImageWidth.Text = "Width: 000000";
            // 
            // toolStripStatusLabelSpacer2
            // 
            this.toolStripStatusLabelSpacer2.Name = "toolStripStatusLabelSpacer2";
            this.toolStripStatusLabelSpacer2.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabelImageHeight
            // 
            this.toolStripStatusLabelImageHeight.Name = "toolStripStatusLabelImageHeight";
            this.toolStripStatusLabelImageHeight.Size = new System.Drawing.Size(81, 17);
            this.toolStripStatusLabelImageHeight.Text = "Height: 000000";
            // 
            // toolStripStatusLabelSpacer3
            // 
            this.toolStripStatusLabelSpacer3.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabelSpacer3.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.toolStripStatusLabelSpacer3.Name = "toolStripStatusLabelSpacer3";
            this.toolStripStatusLabelSpacer3.Size = new System.Drawing.Size(14, 17);
            this.toolStripStatusLabelSpacer3.Text = " ";
            // 
            // toolStripStatusLabelProcessingImage
            // 
            this.toolStripStatusLabelProcessingImage.Name = "toolStripStatusLabelProcessingImage";
            this.toolStripStatusLabelProcessingImage.Size = new System.Drawing.Size(95, 17);
            this.toolStripStatusLabelProcessingImage.Text = "Processing Image:";
            // 
            // toolStripProgressBarProcessingImage
            // 
            this.toolStripProgressBarProcessingImage.Name = "toolStripProgressBarProcessingImage";
            this.toolStripProgressBarProcessingImage.Size = new System.Drawing.Size(100, 16);
            // 
            // panelWorkSpace
            // 
            this.panelWorkSpace.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panelWorkSpace.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelWorkSpace.Controls.Add(this.pictureBoxShadowTopRight);
            this.panelWorkSpace.Controls.Add(this.pictureBoxShadowBottomLeft);
            this.panelWorkSpace.Controls.Add(this.pictureBoxShadowBottom);
            this.panelWorkSpace.Controls.Add(this.pictureBoxShadowBottomRight);
            this.panelWorkSpace.Controls.Add(this.pictureBoxShadowRight);
            this.panelWorkSpace.Controls.Add(this.pictureBoxDestination);
            this.panelWorkSpace.Controls.Add(this.pictureBoxSource);
            this.panelWorkSpace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelWorkSpace.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panelWorkSpace.Location = new System.Drawing.Point(0, 49);
            this.panelWorkSpace.Name = "panelWorkSpace";
            this.panelWorkSpace.Padding = new System.Windows.Forms.Padding(16);
            this.panelWorkSpace.Size = new System.Drawing.Size(792, 495);
            this.panelWorkSpace.TabIndex = 3;
            this.panelWorkSpace.Resize += new System.EventHandler(this.panelWorkSpace_Resize);
            // 
            // pictureBoxShadowTopRight
            // 
            this.pictureBoxShadowTopRight.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBoxShadowTopRight.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxShadowTopRight.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxShadowTopRight.BackgroundImage")));
            this.pictureBoxShadowTopRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxShadowTopRight.Location = new System.Drawing.Point(351, 93);
            this.pictureBoxShadowTopRight.Name = "pictureBoxShadowTopRight";
            this.pictureBoxShadowTopRight.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxShadowTopRight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxShadowTopRight.TabIndex = 1;
            this.pictureBoxShadowTopRight.TabStop = false;
            this.pictureBoxShadowTopRight.Visible = false;
            // 
            // pictureBoxShadowBottomLeft
            // 
            this.pictureBoxShadowBottomLeft.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBoxShadowBottomLeft.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxShadowBottomLeft.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxShadowBottomLeft.BackgroundImage")));
            this.pictureBoxShadowBottomLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxShadowBottomLeft.Location = new System.Drawing.Point(299, 145);
            this.pictureBoxShadowBottomLeft.Name = "pictureBoxShadowBottomLeft";
            this.pictureBoxShadowBottomLeft.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxShadowBottomLeft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxShadowBottomLeft.TabIndex = 1;
            this.pictureBoxShadowBottomLeft.TabStop = false;
            this.pictureBoxShadowBottomLeft.Visible = false;
            // 
            // pictureBoxShadowBottom
            // 
            this.pictureBoxShadowBottom.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBoxShadowBottom.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxShadowBottom.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxShadowBottom.BackgroundImage")));
            this.pictureBoxShadowBottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxShadowBottom.Location = new System.Drawing.Point(325, 145);
            this.pictureBoxShadowBottom.Name = "pictureBoxShadowBottom";
            this.pictureBoxShadowBottom.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxShadowBottom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxShadowBottom.TabIndex = 1;
            this.pictureBoxShadowBottom.TabStop = false;
            this.pictureBoxShadowBottom.Visible = false;
            // 
            // pictureBoxShadowBottomRight
            // 
            this.pictureBoxShadowBottomRight.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBoxShadowBottomRight.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxShadowBottomRight.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxShadowBottomRight.BackgroundImage")));
            this.pictureBoxShadowBottomRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxShadowBottomRight.Location = new System.Drawing.Point(351, 145);
            this.pictureBoxShadowBottomRight.Name = "pictureBoxShadowBottomRight";
            this.pictureBoxShadowBottomRight.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxShadowBottomRight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxShadowBottomRight.TabIndex = 1;
            this.pictureBoxShadowBottomRight.TabStop = false;
            this.pictureBoxShadowBottomRight.Visible = false;
            // 
            // pictureBoxShadowRight
            // 
            this.pictureBoxShadowRight.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBoxShadowRight.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxShadowRight.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxShadowRight.BackgroundImage")));
            this.pictureBoxShadowRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxShadowRight.Location = new System.Drawing.Point(351, 119);
            this.pictureBoxShadowRight.Name = "pictureBoxShadowRight";
            this.pictureBoxShadowRight.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxShadowRight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxShadowRight.TabIndex = 1;
            this.pictureBoxShadowRight.TabStop = false;
            this.pictureBoxShadowRight.Visible = false;
            // 
            // pictureBoxDestination
            // 
            this.pictureBoxDestination.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBoxDestination.BackColor = System.Drawing.Color.Black;
            this.pictureBoxDestination.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxDestination.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxDestination.Location = new System.Drawing.Point(185, 19);
            this.pictureBoxDestination.Name = "pictureBoxDestination";
            this.pictureBoxDestination.Size = new System.Drawing.Size(160, 120);
            this.pictureBoxDestination.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxDestination.TabIndex = 0;
            this.pictureBoxDestination.TabStop = false;
            this.pictureBoxDestination.Visible = false;
            // 
            // pictureBoxSource
            // 
            this.pictureBoxSource.BackColor = System.Drawing.Color.Black;
            this.pictureBoxSource.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxSource.Location = new System.Drawing.Point(19, 19);
            this.pictureBoxSource.Name = "pictureBoxSource";
            this.pictureBoxSource.Size = new System.Drawing.Size(160, 120);
            this.pictureBoxSource.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxSource.TabIndex = 0;
            this.pictureBoxSource.TabStop = false;
            this.pictureBoxSource.Visible = false;
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "jpg";
            this.openFileDialog.Filter = "JPEG (*.jpg)|*.jpg|PNG (*.png)|*.png|All files (*.*)|*.*";
            this.openFileDialog.FilterIndex = 2;
            this.openFileDialog.RestoreDirectory = true;
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = global::WhiteBoardCapturer.Properties.Settings.Default.defaultFileExtention;
            this.saveFileDialog.Filter = "JPEG (*.jpg)|*.jpg|PNG (*.png)|*.png";
            this.saveFileDialog.FilterIndex = global::WhiteBoardCapturer.Properties.Settings.Default.defaultFileExtentionFilterIndex;
            this.saveFileDialog.RestoreDirectory = true;
            this.saveFileDialog.SupportMultiDottedExtensions = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.panelWorkSpace);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::WhiteBoardCapturer.Properties.Settings.Default, "ApplicationName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(320, 240);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = global::WhiteBoardCapturer.Properties.Settings.Default.ApplicationName;
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormMain_KeyPress);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panelWorkSpace.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxShadowTopRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxShadowBottomLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxShadowBottom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxShadowBottomRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxShadowRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDestination)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.PictureBox pictureBoxDestination;
        private System.Windows.Forms.PictureBox pictureBoxSource;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton toolStripButtonZoomOut;
		private System.Windows.Forms.ToolStripButton toolStripButtonZoomIn;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripComboBox toolStripComboBoxZoom;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem menuItemView;
		private System.Windows.Forms.ToolStripMenuItem menuItemViewStatusBar;
		private System.Windows.Forms.ToolStripMenuItem menuItemViewToolBar;
		private System.Windows.Forms.ToolStripMenuItem menuItemViewRuler;
		private System.Windows.Forms.ToolStripMenuItem menuItemFileSaveAs;
		private System.Windows.Forms.ToolStripSeparator menuItemFileSeperator2;
		private System.Windows.Forms.ToolStripMenuItem menuItemFileExit;
		private System.Windows.Forms.ToolStripMenuItem menuItemFileSeperator1;
		private System.Windows.Forms.ToolStripMenuItem menuItemFileNew;
		private System.Windows.Forms.ToolStripMenuItem menuItemFileOpen;
		private System.Windows.Forms.ToolStripMenuItem menuItemFileClose;
		private System.Windows.Forms.ToolStripMenuItem menuItemFileSave;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.Panel panelWorkSpace;
		private System.Windows.Forms.ToolStripButton toolStripButtonSaveAs;
		private System.Windows.Forms.ToolStripButton toolStripButtonSave;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton toolStripButtonOpen;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripButton toolStripButtonProcessImage;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelImageSize;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelSpacer1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelImageWidth;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelSpacer2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelImageHeight;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelSpacer3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelProcessingImage;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBarProcessingImage;
        private System.Windows.Forms.PictureBox pictureBoxShadowTopRight;
        private System.Windows.Forms.PictureBox pictureBoxShadowBottomLeft;
        private System.Windows.Forms.PictureBox pictureBoxShadowBottom;
        private System.Windows.Forms.PictureBox pictureBoxShadowBottomRight;
        private System.Windows.Forms.PictureBox pictureBoxShadowRight;
        private System.Windows.Forms.ToolStripMenuItem menuItemShowShadow;
	}
}
