namespace GenomeNotepad
{
    partial class AboutBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose ( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose ();
            }
            base.Dispose ( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
            this.pictureBoxHeader = new System.Windows.Forms.PictureBox();
            this.pictureBoxFooter = new System.Windows.Forms.PictureBox();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelReleaseNotes = new System.Windows.Forms.Panel();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.labelAllRightsReserved = new System.Windows.Forms.Label();
            this.labelCopyright = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelCompany = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelButtonBar = new System.Windows.Forms.Panel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.panelSeperator = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFooter)).BeginInit();
            this.panelMain.SuspendLayout();
            this.panelReleaseNotes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelButtonBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxHeader
            // 
            this.pictureBoxHeader.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBoxHeader.Image = global::GenomeNotepad.Properties.Resources.AboutHeader;
            this.pictureBoxHeader.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxHeader.Name = "pictureBoxHeader";
            this.pictureBoxHeader.Size = new System.Drawing.Size(484, 44);
            this.pictureBoxHeader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxHeader.TabIndex = 0;
            this.pictureBoxHeader.TabStop = false;
            // 
            // pictureBoxFooter
            // 
            this.pictureBoxFooter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pictureBoxFooter.Image = global::GenomeNotepad.Properties.Resources.AboutFooter;
            this.pictureBoxFooter.Location = new System.Drawing.Point(0, 322);
            this.pictureBoxFooter.Name = "pictureBoxFooter";
            this.pictureBoxFooter.Size = new System.Drawing.Size(484, 44);
            this.pictureBoxFooter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxFooter.TabIndex = 1;
            this.pictureBoxFooter.TabStop = false;
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.panelReleaseNotes);
            this.panelMain.Controls.Add(this.labelAllRightsReserved);
            this.panelMain.Controls.Add(this.labelCopyright);
            this.panelMain.Controls.Add(this.labelVersion);
            this.panelMain.Controls.Add(this.labelTitle);
            this.panelMain.Controls.Add(this.labelDescription);
            this.panelMain.Controls.Add(this.labelCompany);
            this.panelMain.Controls.Add(this.pictureBox1);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 44);
            this.panelMain.Margin = new System.Windows.Forms.Padding(4);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(484, 322);
            this.panelMain.TabIndex = 2;
            // 
            // panelReleaseNotes
            // 
            this.panelReleaseNotes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelReleaseNotes.Controls.Add(this.textBoxDescription);
            this.panelReleaseNotes.Location = new System.Drawing.Point(186, 28);
            this.panelReleaseNotes.Name = "panelReleaseNotes";
            this.panelReleaseNotes.Size = new System.Drawing.Size(286, 188);
            this.panelReleaseNotes.TabIndex = 1;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBoxDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxDescription.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDescription.Location = new System.Drawing.Point(0, 0);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ReadOnly = true;
            this.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDescription.Size = new System.Drawing.Size(282, 184);
            this.textBoxDescription.TabIndex = 2;
            this.textBoxDescription.TabStop = false;
            this.textBoxDescription.Text = resources.GetString("textBoxDescription.Text");
            // 
            // labelAllRightsReserved
            // 
            this.labelAllRightsReserved.AutoSize = true;
            this.labelAllRightsReserved.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAllRightsReserved.Location = new System.Drawing.Point(13, 91);
            this.labelAllRightsReserved.Margin = new System.Windows.Forms.Padding(4);
            this.labelAllRightsReserved.Name = "labelAllRightsReserved";
            this.labelAllRightsReserved.Size = new System.Drawing.Size(99, 14);
            this.labelAllRightsReserved.TabIndex = 0;
            this.labelAllRightsReserved.Text = "All rights reserved.";
            // 
            // labelCopyright
            // 
            this.labelCopyright.AutoSize = true;
            this.labelCopyright.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCopyright.Location = new System.Drawing.Point(13, 70);
            this.labelCopyright.Margin = new System.Windows.Forms.Padding(4);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(139, 14);
            this.labelCopyright.TabIndex = 0;
            this.labelCopyright.Text = "Copyright © Metalcom 2013";
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVersion.Location = new System.Drawing.Point(13, 49);
            this.labelVersion.Margin = new System.Windows.Forms.Padding(4);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(72, 14);
            this.labelVersion.TabIndex = 0;
            this.labelVersion.Text = "Version 1.0.0";
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Location = new System.Drawing.Point(13, 28);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(4);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(90, 14);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Genome Notepad";
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDescription.Location = new System.Drawing.Point(183, 7);
            this.labelDescription.Margin = new System.Windows.Forms.Padding(4);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(86, 14);
            this.labelDescription.TabIndex = 0;
            this.labelDescription.Text = "Release Notes";
            // 
            // labelCompany
            // 
            this.labelCompany.AutoSize = true;
            this.labelCompany.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCompany.Location = new System.Drawing.Point(13, 7);
            this.labelCompany.Margin = new System.Windows.Forms.Padding(4);
            this.labelCompany.Name = "labelCompany";
            this.labelCompany.Size = new System.Drawing.Size(121, 14);
            this.labelCompany.TabIndex = 0;
            this.labelCompany.Text = "Metalcom Industries";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(47, 102);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(89, 126);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // panelButtonBar
            // 
            this.panelButtonBar.Controls.Add(this.buttonOK);
            this.panelButtonBar.Controls.Add(this.panelSeperator);
            this.panelButtonBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtonBar.Location = new System.Drawing.Point(0, 278);
            this.panelButtonBar.Name = "panelButtonBar";
            this.panelButtonBar.Size = new System.Drawing.Size(484, 44);
            this.panelButtonBar.TabIndex = 4;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(205, 12);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 6;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click_1);
            // 
            // panelSeperator
            // 
            this.panelSeperator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelSeperator.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSeperator.Location = new System.Drawing.Point(0, 0);
            this.panelSeperator.Name = "panelSeperator";
            this.panelSeperator.Size = new System.Drawing.Size(484, 4);
            this.panelSeperator.TabIndex = 5;
            // 
            // AboutBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 366);
            this.Controls.Add(this.panelButtonBar);
            this.Controls.Add(this.pictureBoxFooter);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.pictureBoxHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About Genome Notepad";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFooter)).EndInit();
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelReleaseNotes.ResumeLayout(false);
            this.panelReleaseNotes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelButtonBar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxHeader;
        private System.Windows.Forms.PictureBox pictureBoxFooter;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelReleaseNotes;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label labelAllRightsReserved;
        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Label labelCompany;
        private System.Windows.Forms.Panel panelButtonBar;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Panel panelSeperator;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}