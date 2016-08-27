namespace GenomeNotepad
{
    partial class TestEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ()
        {
            this.richTextTestEditor = new System.Windows.Forms.RichTextBox();
            this.panelVerticalScrollBar = new System.Windows.Forms.Panel();
            this.vScrollBar = new System.Windows.Forms.VScrollBar();
            this.panelBottomRightCorner = new System.Windows.Forms.Panel();
            this.panelVerticalScrollBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextTestEditor
            // 
            this.richTextTestEditor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextTestEditor.DetectUrls = false;
            this.richTextTestEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextTestEditor.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.richTextTestEditor.Location = new System.Drawing.Point(0, 0);
            this.richTextTestEditor.Name = "richTextTestEditor";
            this.richTextTestEditor.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedHorizontal;
            this.richTextTestEditor.ShowSelectionMargin = true;
            this.richTextTestEditor.Size = new System.Drawing.Size(379, 396);
            this.richTextTestEditor.TabIndex = 0;
            this.richTextTestEditor.Text = "";
            this.richTextTestEditor.WordWrap = false;
            this.richTextTestEditor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextTestEditor_KeyDown);
            this.richTextTestEditor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.richTextTestEditor_KeyPress);
            // 
            // panelVerticalScrollBar
            // 
            this.panelVerticalScrollBar.Controls.Add(this.vScrollBar);
            this.panelVerticalScrollBar.Controls.Add(this.panelBottomRightCorner);
            this.panelVerticalScrollBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelVerticalScrollBar.Location = new System.Drawing.Point(379, 0);
            this.panelVerticalScrollBar.Name = "panelVerticalScrollBar";
            this.panelVerticalScrollBar.Size = new System.Drawing.Size(17, 396);
            this.panelVerticalScrollBar.TabIndex = 1;
            // 
            // vScrollBar
            // 
            this.vScrollBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vScrollBar.Enabled = false;
            this.vScrollBar.Location = new System.Drawing.Point(0, 0);
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(17, 379);
            this.vScrollBar.TabIndex = 2;
            this.vScrollBar.ValueChanged += new System.EventHandler(this.vScrollBar_ValueChanged);
            // 
            // panelBottomRightCorner
            // 
            this.panelBottomRightCorner.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottomRightCorner.Location = new System.Drawing.Point(0, 379);
            this.panelBottomRightCorner.Name = "panelBottomRightCorner";
            this.panelBottomRightCorner.Size = new System.Drawing.Size(17, 17);
            this.panelBottomRightCorner.TabIndex = 3;
            // 
            // TestEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.richTextTestEditor);
            this.Controls.Add(this.panelVerticalScrollBar);
            this.DoubleBuffered = true;
            this.Name = "TestEditor";
            this.Size = new System.Drawing.Size(396, 396);            
            this.Resize += new System.EventHandler(this.TestEditor_Resize);
            this.panelVerticalScrollBar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextTestEditor;
        private System.Windows.Forms.Panel panelVerticalScrollBar;
        private System.Windows.Forms.VScrollBar vScrollBar;
        private System.Windows.Forms.Panel panelBottomRightCorner;
    }
}
