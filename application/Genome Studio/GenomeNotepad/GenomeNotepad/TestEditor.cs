using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GenomeNotepad
{
    //----------------------------------------------------------------------------------------------------
    // LineSegment
    //----------------------------------------------------------------------------------------------------

    struct LineSegment
    {
        public int x0;
        public int x1;
    }

    //----------------------------------------------------------------------------------------------------
    // Test Editor
    //----------------------------------------------------------------------------------------------------

    public partial class TestEditor : UserControl
    {
        //----------------------------------------------------------------------------------------------------
        // Private memebers
        //----------------------------------------------------------------------------------------------------

        private Sequence    sequence;             // data sequence object.
        private long        visibleLineOffset;    // Offset into data of top visible line.  
        private int         visibleLineCount;     // Number of lines that are visible on the screen.
        private long        lineCount;            // Current number of lines.
        private int         currentRow;           // Current cursor row.
        private int         currentCol;           // Current cursor column. 
        private LineSegment baseGroup;            // Base editor column.
        private LineSegment codonGroup;         // Peptide (Amino Acid) editor column.

        #region Public Properties

        public Sequence Sequence
        {
            get { return this.sequence; }
            set { this.sequence = value; }
        }

        public long VisibleLineOffset
        {
            get { return this.visibleLineOffset; }
            //set { this.visibleLineOffset = value; }
        }

        public long VisibleLineCount
        {
            get { return this.visibleLineCount; }
            //set { this.visibleLineCount = value; }
        }

        public long LineCount
        {
            get { return this.lineCount; }
            //set { this.lineCount = value; }
        }

        #endregion

        //----------------------------------------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------------------------------------

        public TestEditor ()
        {
            InitializeComponent ();

            this.visibleLineOffset = 0;
            this.visibleLineCount  = 0;
            this.lineCount         = 0;

            this.currentRow = 0;
            this.currentCol = 0;
        }

        //----------------------------------------------------------------------------------------------------
        // LoadSettings
        //----------------------------------------------------------------------------------------------------      

        public void LoadSettings ()
        {
        }

        //----------------------------------------------------------------------------------------------------
        // SaveSettings
        //----------------------------------------------------------------------------------------------------      

        public void SaveSettings ()
        {
        }

        #region Event Handlers

        //----------------------------------------------------------------------------------------------------
        // vScrollBar_ValueChanged
        //----------------------------------------------------------------------------------------------------

        private void vScrollBar_ValueChanged ( object sender, EventArgs e )
        {               
            this.visibleLineOffset = ( this.lineCount * this.vScrollBar.Value ) / this.vScrollBar.Maximum;
            RefreshData ();
        }

        //----------------------------------------------------------------------------------------------------
        // TestEditor_Resize
        //----------------------------------------------------------------------------------------------------

        private void TestEditor_Resize ( object sender, EventArgs e )
        {               
            // Refresh sequence data.

            RefreshData ();     
    
            // Calculate new tab incement for the vertical scroll bar.

            if ( sequence != null )
            {
                this.vScrollBar.LargeChange = (int) ( this.lineCount / this.visibleLineCount );
            }
        }

        //----------------------------------------------------------------------------------------------------
        // richTextTestEditor_KeyPress
        //----------------------------------------------------------------------------------------------------

        private void richTextTestEditor_KeyPress ( object sender, KeyPressEventArgs e )
        {
            switch ( e.KeyChar )
            {
                // ESC

                case (char) 27:
                    //this.richTextTestEditor.Clear ();
                    break;
            }
        }

        //----------------------------------------------------------------------------------------------------
        // richTextTestEditor_KeyDown
        //----------------------------------------------------------------------------------------------------

        private void richTextTestEditor_KeyDown ( object sender, KeyEventArgs e )
        {
            // F1

            if ( e.KeyCode == Keys.F1 )
            {   
            }

            // Page Up

            if ( e.KeyCode == Keys.PageUp )
            {
                e.Handled = true;
                PageUp ();    
            }

            // Page Down

            if ( e.KeyCode == Keys.PageDown )
            {
                e.Handled = true;
                PageDown ();
            }

            // Up Arrow

            if ( e.KeyCode == Keys.Up )
            {
                LineUp ( e );           // Move one line up.
            }

            // Down Arrow

            if ( e.KeyCode == Keys.Down )
            {
                LineDown ( e );         // Move one line down.
            }

            // Right Arrow

            if ( e.KeyCode == Keys.Right )
            {
                CursorRight ( e );      // Move cursor to the right.
            }

            // Left Arrow

            if ( e.KeyCode == Keys.Left )
            {
                CursorLeft ( e );       // Move cursor to the left.
            }


            // Ctrl + Home

            if ( e.KeyCode == Keys.Home )
            {
                e.Handled = true;

                // Move to beginning of sequence.

                this.vScrollBar.Value = 0;

                //this.visibleLineOffset = 0;
            }

            // Ctrl + End

            if ( e.KeyCode == Keys.End )
            {
                e.Handled = true;

                // Move to end of sequence.

                this.vScrollBar.Value = this.vScrollBar.Maximum - this.visibleLineCount;

                //this.visibleLineOffset = this.lineCount - this.VisibleLineCount;
            }
        }

        #endregion

        //----------------------------------------------------------------------------------------------------
        // CursorRight
        //----------------------------------------------------------------------------------------------------

        public void CursorRight ( KeyEventArgs e )
        {   
            if ( this.sequence != null )
            {
                // Save the cursor position.

                int charIndex   = this.richTextTestEditor.SelectionStart;                 
                this.currentRow = this.richTextTestEditor.GetLineFromCharIndex ( charIndex );
                this.currentCol = charIndex - this.richTextTestEditor.GetFirstCharIndexFromLine ( this.currentRow );

                // Calculate line group baoundaries.

                int x0 = 0;
                int x1 = 0;

                switch ( this.sequence.Unit )
                {
                    case GenomeNotepad.Sequence.SequenceUnit.Base:
                        x0 = this.baseGroup.x0;
                        x1 = this.baseGroup.x1;
                        break;

                    case GenomeNotepad.Sequence.SequenceUnit.Codon:
                        x0 = this.codonGroup.x0;
                        x1 = this.codonGroup.x1;                        
                        break;
                }

                // Move Cursor Right

                if ( this.currentCol < x1 )
                {
                    this.richTextTestEditor.Select ( this.currentCol + 1, this.currentRow );
                }

                // Disable default behavior.

                e.Handled = true;
            }
        }

        //----------------------------------------------------------------------------------------------------
        // CursorLeft
        //----------------------------------------------------------------------------------------------------

        public void CursorLeft ( KeyEventArgs e )
        {
            if ( this.sequence != null )
            {
                // Save the cursor position.

                this.currentCol = this.richTextTestEditor.SelectionStart;
                this.currentRow = this.richTextTestEditor.GetLineFromCharIndex ( currentCol );

                // Move Cursor Right

                this.richTextTestEditor.Select ( this.currentCol - 1, 0 );

                // Disable default behavior.

                e.Handled = true;
            }
        }

        //----------------------------------------------------------------------------------------------------
        // LineUp
        //----------------------------------------------------------------------------------------------------

        public void LineUp ( KeyEventArgs e )
        {
            if ( this.sequence != null )
            {
                this.currentCol = this.richTextTestEditor.SelectionStart;
                this.currentRow = this.richTextTestEditor.GetLineFromCharIndex ( currentCol );

                // Increment visible window by one line, if the cursor is at the bottom of the page.

                int headerRowHeight = ( this.sequence.Unit == GenomeNotepad.Sequence.SequenceUnit.Base ) ? 1 : 2;

                if ( this.currentRow <= headerRowHeight )
                {
                    e.Handled = true;

                    if ( this.vScrollBar.Value > 0 )
                    {
                        this.vScrollBar.Value--;
                    }
                }

                // Restore the cursor position

                this.richTextTestEditor.Select ( this.currentCol, 0 );
            }
        }

        //----------------------------------------------------------------------------------------------------
        // LineDown
        //----------------------------------------------------------------------------------------------------

        public void LineDown ( KeyEventArgs e )
        {
            if ( this.sequence != null )
            {                
                // Save the cursor position.

                this.currentCol = this.richTextTestEditor.SelectionStart;
                this.currentRow = this.richTextTestEditor.GetLineFromCharIndex ( currentCol );

                int headerRowAdjustment = ( this.sequence.Unit == GenomeNotepad.Sequence.SequenceUnit.Base ) ? 1 : 0;

                // Increment visible window by one line, if the cursor is at the bottom of the page.

                if ( this.currentRow > this.visibleLineCount - headerRowAdjustment )
                {
                    e.Handled = true;

                    if ( this.vScrollBar.Value < this.vScrollBar.Maximum - this.VisibleLineCount )
                    {
                        this.vScrollBar.Value++;
                    }
                }                

                // Restore the cursor position

                this.richTextTestEditor.Select ( this.currentCol, 0 );
            }
        }

        //----------------------------------------------------------------------------------------------------
        // PageUp
        //----------------------------------------------------------------------------------------------------

        public void PageUp ()
        {
            if ( this.sequence != null )
            {   
                // Save the cursor position.

                this.currentCol = this.richTextTestEditor.SelectionStart;

                // Decrement the visible windo by one page.

                if ( this.visibleLineOffset - this.visibleLineCount < 0 )
                {
                    this.vScrollBar.Value = 0;
                }
                else
                {
                    this.vScrollBar.Value -= this.visibleLineCount;
                }

                // Restore the cursor position

                this.richTextTestEditor.Select ( this.currentCol, 0 );
            }
        }

        //----------------------------------------------------------------------------------------------------
        // PageDown
        //----------------------------------------------------------------------------------------------------

        public void PageDown ()
        {
            if ( this.sequence != null )
            {
                // Save the cursor position.

                this.currentCol = this.richTextTestEditor.SelectionStart;
                
                // Increment visible window by one page.

                if ( this.visibleLineOffset + this.visibleLineCount > this.lineCount )                
                {
                    this.vScrollBar.Value = this.vScrollBar.Maximum;
                }
                else
                {
                    this.vScrollBar.Value += this.visibleLineCount;
                }

                // Restore the cursor position

                this.richTextTestEditor.Select ( this.currentCol, 0 );
            }
        }
                
        //----------------------------------------------------------------------------------------------------
        // RefreshData
        //----------------------------------------------------------------------------------------------------

        public void RefreshData ()
        {
            // We refresh only if this.sequence has been asigned a value.

            if ( this.sequence != null )
            {
                // Refresh unit specific data.

                switch ( this.sequence.Unit )
                {
                    case Sequence.SequenceUnit.Base:
                        RefreshBaseData ( this.sequence );
                        this.lineCount = 1 + this.sequence.BaseCount / ( this.sequence.GroupCountBase * this.sequence.GroupWidthBase );
                        break;

                    case Sequence.SequenceUnit.Codon:
                        RefreshCodonData ( this.sequence );
                        this.lineCount = 1 + this.sequence.BaseCount / ( 3 * this.sequence.GroupCountCodon * this.sequence.GroupWidthCodon );
                        break;
                }

                // Set vertical scroll bar visibility.

                if ( this.lineCount > this.visibleLineCount )
                {
                    this.vScrollBar.Enabled = true;
                }
                else
                {
                    this.vScrollBar.Enabled = false;
                }

                // Set vertical scroll bar range.

                this.vScrollBar.Maximum = (int) this.lineCount;
            }
        }

        //----------------------------------------------------------------------------------------------------
        // RefreshBaseData
        //----------------------------------------------------------------------------------------------------

        private void RefreshBaseData ( Sequence sequence )
        {
            // Calculate the number of visible lines in the RichText control.

            this.visibleLineCount = GetVisibleLineCount ();

            // Clear the editor control.

            this.richTextTestEditor.Clear ();
            
            // Add the Header row/s.
            
            AddHeaderRowBase ( sequence );
            
            // Load block of sequence data into control.
                        
            string dataRow        = "";
            long   dataIndex      = this.visibleLineOffset * sequence.GroupCountBase * sequence.GroupWidthBase;
            long   dataRowAddress = dataIndex;
            char   ch             = (char) 0;

            // Set left boundary of base editor group.

            this.baseGroup.x0 =  dataRowAddress.ToString ( sequence.AddressFormatBase ).Length;
            this.baseGroup.x0 += StringTable.AddressStringTerminator.Length;
            this.baseGroup.x1 = ( sequence.GroupCountBase + 1 ) * ( sequence.GroupWidthBase + 1 );

            // Add data lines to the editor control.

            for ( int lineIndex = 0; lineIndex < this.visibleLineCount; lineIndex++ )
            {  
                // Add data address column.

                dataRow += dataRowAddress.ToString ( sequence.AddressFormatBase );
                dataRow += StringTable.AddressStringTerminator;

                // Add sequence data.

                for ( int groupIndex = 0; groupIndex < sequence.GroupCountBase; groupIndex++ )
                {
                    // Add group column spacer.

                    if ( groupIndex > 0 )
                    {
                        dataRow += " ";
                    }

                    // Add group of data to the row.

                    for ( int groupDataIndex = 0; groupDataIndex < sequence.GroupWidthBase; groupDataIndex++ )
                    {
                        // Add one unit of data.

                        if ( dataIndex < sequence.BaseCount )
                        {
                            // Get data unit.

                            ch = (char) sequence.Data [ dataIndex ];

                            // Apply sequence opperators.

                            ch = sequence.ApplyEncoding ( sequence, ch );    // Apply sequence encoding. e.g. DNA, RNA, etc
                            ch = sequence.ApplyStrand   ( sequence, ch );    // Apply strand. e.g. sense, anti-sense, etc.
                            ch = sequence.ApplyCase     ( sequence, ch );    // Apply case. e.g. upper case or lower case.
                        }
                        else
                        {
                            ch = '.';
                        }

                        // Add the character to the data row

                        dataRow += ch;

                        // Get next data unit.

                        dataIndex++;
                    }
                }

                // Add cariage return.

                if ( lineIndex < this.visibleLineCount - 1 ) dataRow += (char) 13;

                // Add row to RichText control.

                this.richTextTestEditor.AppendText ( dataRow );                
                dataRow = "";

                // Update data address.

                dataRowAddress = dataIndex;
            }

            ScrollToTop ();
        }

        //----------------------------------------------------------------------------------------------------
        // RefreshCodonData
        //----------------------------------------------------------------------------------------------------

        private void RefreshCodonData ( Sequence sequence )
        {
            // Calculate the number of visible lines in the RichText control.
            // For the codon view mode we subtract 1 in order to make room for the 2 row codon header.

            this.visibleLineCount = GetVisibleLineCount () - 1;            

            // Clear the editor control.

            this.richTextTestEditor.Clear ();

            // Add the Header row code codon view mode.

            AddHeaderRowCodon ( sequence );            
            
            // Load block of sequence data into control.

            string dataRow             = "";
            long   dataIndex           = 3 * this.visibleLineOffset * sequence.GroupCountCodon * sequence.GroupWidthCodon;
            long   dataRowBaseAddress  = dataIndex;
            long   dataRowCodonAddress = dataIndex / 3;            
            long   aminoAcidDataIndex  = 0;
            int    aminoAcidWidth      = sequence.GroupWidthCodon * sequence.GroupCountCodon;
            char[] codon               = new char [ sequence.CodonWidth ];
            char   ch                  = (char) 0;

            // Set left boundary of base editor group.

            this.codonGroup.x0 =  dataRowBaseAddress.ToString ( sequence.AddressFormatBase ).Length;
            this.codonGroup.x0 += StringTable.AddressStringSeparator.Length;
            this.codonGroup.x0 += dataRowCodonAddress.ToString ( sequence.AddressFormatCodon ).Length;
            this.codonGroup.x0 += StringTable.AddressStringTerminator.Length;
            this.codonGroup.x1 = sequence.GroupCountCodon * ( sequence.GroupWidthCodon + 4 );

            // Add data lines to the editor control.

            for ( int lineIndex = 0; lineIndex < visibleLineCount; lineIndex++ )
            {
                // Add data address column.

                dataRow += dataRowBaseAddress.ToString ( sequence.AddressFormatBase );
                dataRow += StringTable.AddressStringSeparator;
                dataRow += dataRowCodonAddress.ToString ( sequence.AddressFormatCodon );
                dataRow += StringTable.AddressStringTerminator;

                // Store the amino acid index for use later when adding the amino acid column.

                aminoAcidDataIndex = dataIndex;

                // Add sequence data.

                for ( int groupIndex = 0; groupIndex < sequence.GroupCountCodon; groupIndex++ )
                {
                    // Add group column spacer.

                    if ( groupIndex > 0 )
                    {
                        dataRow += "-";
                    }

                    // Add group of codon data to the row.

                    for ( int groupDataIndex = 0; groupDataIndex < sequence.GroupWidthCodon; groupDataIndex++ )
                    {
                        // Add one codon.

                        for ( int codonCharacterIndex = 0; codonCharacterIndex < sequence.CodonWidth; codonCharacterIndex++ )
                        {
                            // Add one base.

                            if ( dataIndex < sequence.BaseCount )
                            {
                                // Get data unit.

                                ch = (char) sequence.Data [ dataIndex ];

                                // Apply sequence opperators.

                                ch = sequence.ApplyEncoding ( sequence, ch );    // Apply sequence encoding. e.g. DNA, RNA, etc
                                ch = sequence.ApplyStrand   ( sequence, ch );    // Apply strand. e.g. sense, anti-sense, etc.
                                ch = sequence.ApplyCase     ( sequence, ch );    // Apply case. e.g. upper case or lower case.
                            }
                            else
                            {
                                ch = '.';
                            }

                            // Add the character to the data row

                            dataRow += ch;

                            // Increment base address.

                            dataIndex++;
                        }

                        // Add a spacer after the codon

                        if ( groupDataIndex < sequence.GroupWidthCodon - 1 )
                        {
                            dataRow += " ";
                        }  
                      
                        // Increment codon address

                        dataRowCodonAddress++;
                    }
                }

                // Add amino acid column.

                dataRow += "   ";

                for ( int aminoAcidIndex = 0; aminoAcidIndex < aminoAcidWidth; aminoAcidIndex++ )
                {
                    // Get codon.

                    for ( int codonCharacterIndex = 0; codonCharacterIndex < sequence.CodonWidth; codonCharacterIndex++ )
                    {
                        codon [ codonCharacterIndex ] = (char) sequence.Data [ aminoAcidDataIndex ];
                        aminoAcidDataIndex++;
                    }

                    // Add the single character symbol to the data row.

                    dataRow += sequence.GetAminoAcid1LetterSymbol ( new string ( codon ) );
                }


                // Add cariage return.

                if ( lineIndex < visibleLineCount - 1 ) dataRow += (char) 13;

                // Add row to RichText control.

                this.richTextTestEditor.AppendText ( dataRow );
                dataRow = "";

                // Update data address.

                dataRowBaseAddress  = dataIndex;
                dataRowCodonAddress = dataIndex / 3;
            }

            ScrollToTop ();
        }

        //----------------------------------------------------------------------------------------------------
        // AddHeaderRowBase
        //----------------------------------------------------------------------------------------------------

        private void AddHeaderRowBase ( Sequence sequence )
        {   
            // Calculate the width of the address column.

            int rowAddress    = 0;            
            int addressLength = rowAddress.ToString ( sequence.AddressFormatBase ).Count ();
                addressLength += StringTable.AddressStringTerminator.Count ();

            // Initialize the data row parameters.

            string dataRow       = new String ( ' ', addressLength );            
            long   dataIndex     = 0;

            // Compile the header row.

            for ( long groupIndex = 0; groupIndex < sequence.GroupCountBase; groupIndex++ )
            {
                dataRow   += dataIndex.ToString ().PadRight ( sequence.GroupWidthBase + 1 );
                dataIndex += sequence.GroupWidthBase;
            }
            dataRow += (char) 13;

            // Add the row to the edditor control.

            this.richTextTestEditor.AppendText ( dataRow );            
        }

        //----------------------------------------------------------------------------------------------------
        // AddHeaderRowCodon
        //----------------------------------------------------------------------------------------------------

        private void AddHeaderRowCodon ( Sequence sequence )
        {
            // Calculate the width of the address column.

            int colCount        = sequence.GroupWidthCodon * sequence.GroupCountCodon;
            int rowAddressBase  = 0;
            int rowAddressCodon = 0;
            int addressLength   = 0;

            addressLength += rowAddressBase.ToString ( sequence.AddressFormatBase ).Count ();
            addressLength += StringTable.AddressStringSeparator.Count ();
            addressLength += rowAddressCodon.ToString ( sequence.AddressFormatCodon ).Count ();
            addressLength += StringTable.AddressStringTerminator.Count ();

            // Initialize the data row parameters for the base addresses.

            string dataRow   = new String ( ' ', addressLength );
            long   dataIndex = 0;

            // Compile the header row.

            for ( long colIndex = 0; colIndex < colCount; colIndex++ )
            {
                dataRow   += dataIndex.ToString ().PadRight ( sequence.CodonWidth + 1 );
                dataIndex += sequence.CodonWidth;
            }
            dataRow += (char) 13;

            // Add the codon header row to the editor control.

            this.richTextTestEditor.AppendText ( dataRow );

            // Initialize the data row parameters for the codon addresses.

            dataRow   = new String ( ' ', addressLength );
            dataIndex = 0;

            // Compile the header row.

            for ( long colIndex = 0; colIndex < colCount; colIndex++ )
            {
                dataRow   += dataIndex.ToString ().PadRight ( sequence.CodonWidth + 1 );
                dataIndex++;
            }
            dataRow += (char) 13;

            // Add the codon header row to the editor control.

            this.richTextTestEditor.AppendText ( dataRow );
        }

        //----------------------------------------------------------------------------------------------------
        // GetVisibleLineCount
        // 
        // Description:
        //   Returns the number of visible lines in a richtext control.
        //----------------------------------------------------------------------------------------------------

        private int GetVisibleLineCount ()
        {
            // Calculate number of visible lines in RichText control.
            //
            // Note:
            //   The result of this calculation is an aproximation. For the life of me, I just can't seem to 
            //   work out why the calculation is not returning an accurate result.
            //   In theory it should be simple.
            //     e.g. Visible Lines = RichText.Height / RichText.Font.Height
            //   How ever, that calculation does not work accuratly and only produces an aproximate result 
            //   with a variance that ranges from +13% for tall control heights, ro more then +50% for short 
            //   control heights. It may have something to do with the font asscent and descent values.

            float lineHeight       = (float) this.richTextTestEditor.Font.GetHeight ();
            int   controlHeight    = this.richTextTestEditor.Height;
            int   visibleLineCount = (int) ( (float) controlHeight / lineHeight );

            // Create a mesuring column to calculate the exact number of visible lines.
            //
            // Note:
            //   Once we get the visible line count calculation right, we will be able to omit this
            //   mesuring column step.

            char ch             = ' ';                                  // Just an arbetary character to use for the measuring column.
            int  totalLineCount = this.richTextTestEditor.Lines.Length; // Total lines currently loaded inthe RichText control.     
            
            for ( int lineIndex = totalLineCount-1; lineIndex < visibleLineCount; lineIndex++ )
            {
                this.richTextTestEditor.AppendText ( ch + "\n" ); 
            }

            // Scroll up to the first line.

            ScrollToTop ();

            // Calculate the number of visible lines.

            int topIndex    = this.richTextTestEditor.GetCharIndexFromPosition ( new Point ( 0, 0 ) );
            int bottomIndex = this.richTextTestEditor.GetCharIndexFromPosition ( new Point ( 0, controlHeight ) );
            int topLine     = this.richTextTestEditor.GetLineFromCharIndex ( topIndex );
            int bottomLine  = this.richTextTestEditor.GetLineFromCharIndex ( bottomIndex );

            visibleLineCount = bottomLine - topLine - 2;    // -1 to acomodate partial lines at the bottom, and -1 to acomodate the scroll bar.

            return visibleLineCount;
        }

        //----------------------------------------------------------------------------------------------------
        // ScrollToTop        
        //----------------------------------------------------------------------------------------------------

        private void ScrollToTop ()
        {
            this.richTextTestEditor.SelectionStart = 0;
            this.richTextTestEditor.SelectionLength = 1;
            this.richTextTestEditor.ScrollToCaret ();
            this.richTextTestEditor.SelectionLength = 0;
        }






    } // class

} // Name space
