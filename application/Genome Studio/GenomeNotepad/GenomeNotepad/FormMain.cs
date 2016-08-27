using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace GenomeNotepad
{
    public partial class FormMain : Form
    {
        private string fileName;
        private string filePath;
        private string fileFilter;

        private Sequence   sequence;
        private TestEditor testEditor;

        private Sequence.SequenceEncoding sequenceEncoding;
        private Sequence.SequenceUnit     sequenceUnit;
        private Sequence.SequenceStrand   sequenceStrand;
        private int                       sequenceGroupWidthBase;
        private int                       sequenceGroupCountBase;
        private int                       sequenceGroupWidthCodon;
        private int                       sequenceGroupCountCodon;
        private bool                      sequenceUpperCase;

        private string[] groupWidthPresetsBase    = { "1", "3", "6", "9", "10", "12" };
        private string[] groupWidthPresetsCodon   = { "1", "2", "3", "4", "6", "9", "10", "12", "24" };
        private string[] groupWidthPresetsGeneric = { "1", "2", "3", "4", "6", "9", "10", "12", "24" };

        private string[] groupCountPresetsBase    = { "1", "6", "8", "9", "10", "12" };
        private string[] groupCountPresetsCodon   = { "1", "2", "3", "4", "8", "9", "10" };
        private string[] groupCountPresetsGeneric = { "1", "2", "3", "4", "6", "8", "9", "10", "12" };
        
        
        //----------------------------------------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------------------------------------

        public FormMain ()
        {
            // Visual Studio managed initialization code.

            InitializeComponent ();

            // Initialization main application form.

            this.Text = Application.ProductName;
            this.splitContainerMain.Panel1Collapsed = true;

            // Load application settings.

            LoadSettings ();

            // Initialize tool bar.

            this.toolStripComboBoxViewEncoding.SelectedIndex = (int) this.sequenceEncoding;
            this.toolStripComboBoxViewUnit.SelectedIndex     = (int) this.sequenceUnit;
            this.toolStripComboBoxViewStrand.SelectedIndex   = (int) this.sequenceStrand;
            this.toolStripButtonViewCase.Checked             = this.sequenceUpperCase;

            // Update the tool strip combo boxes for group width and group count.

            UpdateToolStripViewGroupParameters ();            

            // Initialize default sequence data grouping parameters.

            this.toolStripComboBoxViewGroupWidth.Items.AddRange ( this.groupWidthPresetsGeneric );
            this.toolStripComboBoxViewGroupCount.Items.AddRange ( this.groupCountPresetsGeneric );

            // Initialize file manamgnet data.

            this.fileName   = "";
            this.filePath   = "";
            this.fileFilter = "";

            CompileFileFilter ();

            // Initialise test editor.

            this.testEditor      = new TestEditor ();
            this.testEditor.Dock = DockStyle.Fill;
            this.splitContainerMain.Panel2.Controls.Add ( testEditor );
        }

        //----------------------------------------------------------------------------------------------------
        // LoadSettings
        //----------------------------------------------------------------------------------------------------      

        public void LoadSettings ()
        {
            this.sequenceEncoding        = (Sequence.SequenceEncoding) Properties.Settings.Default.SequenceEncoding;
            this.sequenceUnit            = (Sequence.SequenceUnit)     Properties.Settings.Default.SequenceUnit;
            this.sequenceStrand          = (Sequence.SequenceStrand)   Properties.Settings.Default.SequenceStrand;
            this.sequenceGroupWidthBase  = Properties.Settings.Default.SequenceGroupWidthBase;
            this.sequenceGroupCountBase  = Properties.Settings.Default.SequenceGroupCountBase;
            this.sequenceGroupWidthCodon = Properties.Settings.Default.SequenceGroupWidthCodon;
            this.sequenceGroupCountCodon = Properties.Settings.Default.SequenceGroupCountCodon;
            this.sequenceUpperCase       = Properties.Settings.Default.SequenceUpperCase;
        }

        //----------------------------------------------------------------------------------------------------
        // SaveSettings
        //----------------------------------------------------------------------------------------------------      

        public void SaveSettings ()
        {
        }

        #region Event handlers

        //----------------------------------------------------------------------------------------------------
        // menuItemHelpAbout_Click
        //----------------------------------------------------------------------------------------------------

        private void menuItemHelpAbout_Click ( object sender, EventArgs e )
        {
            AboutBox aboutBox = new AboutBox ();

            aboutBox.ShowDialog ();
        }

        //----------------------------------------------------------------------------------------------------
        // menuItemFileOpen_Click
        //----------------------------------------------------------------------------------------------------

        private void menuItemFileOpen_Click ( object sender, EventArgs e )
        {
            OpenFile ();
        }

        //----------------------------------------------------------------------------------------------------
        // menuItemFileClose_Click
        //----------------------------------------------------------------------------------------------------

        private void menuItemFileClose_Click ( object sender, EventArgs e )
        {
            CloseFile ();
        }

        //----------------------------------------------------------------------------------------------------
        // menuItemFileSaveAs_Click
        //----------------------------------------------------------------------------------------------------

        private void menuItemFileSaveAs_Click ( object sender, EventArgs e )
        {
            SaveFileAs ();
        }

        //----------------------------------------------------------------------------------------------------
        // toolStripButtonViewCase_Click
        //----------------------------------------------------------------------------------------------------

        private void toolStripButtonViewCase_Click ( object sender, EventArgs e )
        {
            if ( this.toolStripButtonViewCase.Checked )
            {
                this.toolStripButtonViewCase.Checked = false;
                this.sequence.UpperCase              = false;
                this.testEditor.RefreshData ();
            }
            else
            {
                this.toolStripButtonViewCase.Checked = true;
                this.sequence.UpperCase              = true;
                this.testEditor.RefreshData ();
            }
        }

        //----------------------------------------------------------------------------------------------------
        // toolStripComboBoxViewGroupWidth_TextChanged
        //----------------------------------------------------------------------------------------------------

        private void toolStripComboBoxViewGroupWidth_TextChanged ( object sender, EventArgs e )
        {
            try
            {
                // Select group width.

                if ( this.sequence != null )
                {
                    switch ( this.sequence.Unit )
                    {
                        case Sequence.SequenceUnit.Base:
                            this.sequence.GroupWidthBase = Int16.Parse ( this.toolStripComboBoxViewGroupWidth.Text );
                            this.testEditor.RefreshData ();
                            break;

                        case Sequence.SequenceUnit.Codon:
                            this.sequence.GroupWidthCodon = Int16.Parse ( this.toolStripComboBoxViewGroupWidth.Text );
                            this.testEditor.RefreshData ();
                            break;
                    }
                }

                // Update the status bar.

                RefreshStstusBar ( this.sequence );
            }
            catch ( Exception exception )
            {
                Debug.Write ( exception.Message + "\n" );
            }
        }

        //----------------------------------------------------------------------------------------------------
        // toolStripComboBoxViewGroupCount_TextChanged
        //----------------------------------------------------------------------------------------------------

        private void toolStripComboBoxViewGroupCount_TextChanged ( object sender, EventArgs e )
        {
            try
            {
                // Select group count.

                if ( this.sequence != null )
                {   
                    // Refresh data.

                    switch ( this.sequence.Unit )
                    {
                        case Sequence.SequenceUnit.Base:
                            this.sequence.GroupCountBase = Int16.Parse ( this.toolStripComboBoxViewGroupCount.Text );
                            this.testEditor.RefreshData ();
                            break;

                        case Sequence.SequenceUnit.Codon:
                            this.sequence.GroupCountCodon = Int16.Parse ( this.toolStripComboBoxViewGroupCount.Text );
                            this.testEditor.RefreshData ();
                            break;
                    }
                }

                // Update the status bar.

                RefreshStstusBar ( this.sequence );
            }
            catch ( Exception exception )
            {
                Debug.Write ( exception.Message + "\n" );
            }
        }

        //----------------------------------------------------------------------------------------------------
        // toolStripComboBoxViewEncoding_SelectedIndexChanged
        //----------------------------------------------------------------------------------------------------

        private void toolStripComboBoxViewEncoding_SelectedIndexChanged ( object sender, EventArgs e )
        {
            try
            {
                // Select encoding.

                if ( this.sequence != null )
                {
                    this.sequence.Encoding = (Sequence.SequenceEncoding) this.toolStripComboBoxViewEncoding.SelectedIndex;
                    this.testEditor.RefreshData ();
                }

                // Update the status bar.

                RefreshStstusBar ( this.sequence );
            }
            catch ( Exception exception )
            {
                Debug.Write ( exception.Message + "\n" );
            }
        }

        //----------------------------------------------------------------------------------------------------
        // toolStripComboBoxViewStrand_SelectedIndexChanged
        //----------------------------------------------------------------------------------------------------

        private void toolStripComboBoxViewStrand_SelectedIndexChanged ( object sender, EventArgs e )
        {
            try
            {
                // Select Strand.

                if ( this.sequence != null )
                {
                    this.sequence.Strand = (Sequence.SequenceStrand) this.toolStripComboBoxViewStrand.SelectedIndex;
                    this.testEditor.RefreshData ();
                }

                // Update the status bar.

                RefreshStstusBar ( this.sequence );
            }
            catch ( Exception exception )
            {
                Debug.Write ( exception.Message + "\n" );
            }
        }

        //----------------------------------------------------------------------------------------------------
        // toolStripComboBoxViewUnit_SelectedIndexChanged
        //----------------------------------------------------------------------------------------------------

        private void toolStripComboBoxViewUnit_SelectedIndexChanged ( object sender, EventArgs e )
        {
            try
            {                
                // Select unit.

                if ( this.sequence != null )
                {
                    this.sequence.Unit = (Sequence.SequenceUnit) this.toolStripComboBoxViewUnit.SelectedIndex;
                    this.testEditor.RefreshData ();

                    switch ( this.sequence.Unit )
                    {
                        case Sequence.SequenceUnit.Base:
                            this.toolStripComboBoxViewGroupWidth.Text = Properties.Settings.Default.SequenceGroupWidthBase.ToString();
                            this.toolStripComboBoxViewGroupCount.Text = Properties.Settings.Default.SequenceGroupCountBase.ToString ();
                            break;

                        case Sequence.SequenceUnit.Codon:
                            this.toolStripComboBoxViewGroupWidth.Text = Properties.Settings.Default.SequenceGroupWidthCodon.ToString();
                            this.toolStripComboBoxViewGroupCount.Text = Properties.Settings.Default.SequenceGroupCountCodon.ToString ();
                            break;
                    }
                }

                // Update the status bar.

                RefreshStstusBar ( this.sequence );
            }
            catch ( Exception exception )
            {
                Debug.Write ( exception.Message + "\n" );    
            }
        }

        #endregion

        //----------------------------------------------------------------------------------------------------
        // OpenFile
        //----------------------------------------------------------------------------------------------------

        public void OpenFile ()
        {   
            // Initialize OpenFileDialog.

            OpenFileDialog openFileDialog   = new OpenFileDialog ();
            openFileDialog.Filter           = this.fileFilter;
            openFileDialog.FilterIndex      = 3;
            openFileDialog.DefaultExt       = StringTable.FileExtentionPlainSequence;
            openFileDialog.AddExtension     = true;
            openFileDialog.InitialDirectory = Properties.Settings.Default.StartDirectory;            
                
            // Show OpenFileDialog.

            if ( openFileDialog.ShowDialog () == DialogResult.OK )
            {
                // Add file name to the main application form caption.

                this.filePath = openFileDialog.FileName;
                this.fileName = Path.GetFileName ( this.filePath );
                this.Text     = Application.ProductName + " ( " + this.fileName + " )";

                // Load the sequence.

                this.sequence = new Sequence ();
                this.testEditor.Sequence = this.sequence;

                //this.sequence.Encoding       = (Sequence.SequenceEncoding) this.toolStripComboBoxViewEncoding.SelectedIndex;
                //this.sequence.GroupCountBase = Int16.Parse ( this.toolStripComboBoxViewGroupCount.Text );
                //this.sequence.GroupWidthBase = Int16.Parse ( this.toolStripComboBoxViewGroupWidth.Text );                

                this.toolStripComboBoxViewEncoding.SelectedIndex = (int) this.sequenceEncoding;
                this.toolStripComboBoxViewUnit.SelectedIndex     = (int) this.sequenceUnit;
                UpdateToolStripViewGroupParameters (); 

                this.sequence.LoadSequence  ( this.filePath, Sequence.SequenceFormat.PlainSequence );
                this.testEditor.RefreshData ();
                RefreshStstusBar            ( this.sequence );
            }
        }

        //----------------------------------------------------------------------------------------------------
        // CloseFile
        //----------------------------------------------------------------------------------------------------

        public void CloseFile ()
        {
            this.filePath = "";
            this.fileName = "";
            this.Text     = Application.ProductName;
        }

        //----------------------------------------------------------------------------------------------------
        // SaveFileAs
        //----------------------------------------------------------------------------------------------------

        public void SaveFileAs ()
        {
            // Initialize SaveFileDialog.

            SaveFileDialog saveFileDialog   = new SaveFileDialog ();
            saveFileDialog.Filter           = this.fileFilter;
            saveFileDialog.FilterIndex      = 3;
            saveFileDialog.DefaultExt       = StringTable.FileExtentionPlainSequence;
            saveFileDialog.AddExtension     = true;
            saveFileDialog.InitialDirectory = Properties.Settings.Default.StartDirectory;   

            // Show the SaveFileDialog.

            saveFileDialog.ShowDialog ();
            
            // Save the file.

            this.filePath = saveFileDialog.FileName;
            this.fileName = Path.GetFileName ( this.filePath );
            this.Text     = Application.ProductName + " ( " + this.fileName + " )";
        }

        //----------------------------------------------------------------------------------------------------
        // CompileFileFilter
        //----------------------------------------------------------------------------------------------------

        public void CompileFileFilter ()
        {
            // Load individual file filters from string table.

            string   fileFilterSeperator = "|";
            string[] fileFilters =
            {
                StringTable.FileFilterAllFiles,
                StringTable.FileFilterGenomeStudio,
                StringTable.FileFilterPlainSequence,
                StringTable.FileFilterExtendedPlainSequence
            };

            // Initialize file filter

            this.fileFilter = "";
            int filterCount = fileFilters.Count ();

            // Compile file filter string for use in OpenFileDialog and SaveFileDialog forms.
            // e.g. int N = filterCount;
            //      filter = fileFilters[0] + "|" + fileFilters[1] + "|" + fileFilters[N];

            for ( int index = 0; index < filterCount; index++ )
            {
                if ( index < filterCount - 1 )
                {
                    // Append '|' seperator between filters.

                    fileFilter += fileFilters [ index ] + fileFilterSeperator;
                }
                else
                {
                    // For the last filter, we do not append the '|' seperator.

                    fileFilter += fileFilters [ index ];
                }
            }
        }

        //----------------------------------------------------------------------------------------------------
        // UpdateToolStripGroupParameters
        //----------------------------------------------------------------------------------------------------

        public void UpdateToolStripViewGroupParameters ()
        {
            switch ( this.sequenceUnit )
            {
                case Sequence.SequenceUnit.Base:
                    this.toolStripComboBoxViewGroupWidth.Text = this.sequenceGroupWidthBase.ToString ();
                    this.toolStripComboBoxViewGroupCount.Text = this.sequenceGroupCountBase.ToString ();
                    break;

                case Sequence.SequenceUnit.Codon:
                    this.toolStripComboBoxViewGroupWidth.Text = this.sequenceGroupWidthCodon.ToString ();
                    this.toolStripComboBoxViewGroupCount.Text = this.sequenceGroupCountCodon.ToString ();
                    break;
            }
        }

        //----------------------------------------------------------------------------------------------------
        // UpdateStatusBar
        //----------------------------------------------------------------------------------------------------

        public void RefreshStstusBar ( Sequence sequence )
        {
            // Get sequence length.

            long baseCount = sequence.BaseCount;

            // Add sequence length in bp's.

            string statusBarString = "Sequence Length: ";
            statusBarString += baseCount.ToString ( "###,###,###,###" );

            // Add sequence length  in kbp's.

            double kbp = baseCount / 1000.0;
            statusBarString += " bp ( " + kbp.ToString ( "###,###,##0.###" ) + " kbp )";

            // Update the status bar.

            this.statusStripMain.Items [ 0 ].Text = statusBarString;
            
            // Update debug data.

            statusBarString =  "Line Count: ";
            statusBarString += this.testEditor.LineCount.ToString ( "###,###,###,##0" );
            this.statusStripMain.Items [ 2 ].Text = statusBarString;

            statusBarString = "Visible Line Count: ";
            statusBarString += this.testEditor.VisibleLineCount.ToString ( "#,##0" );
            this.statusStripMain.Items [ 4 ].Text = statusBarString;
        }



    } // Class

} // Namespace
