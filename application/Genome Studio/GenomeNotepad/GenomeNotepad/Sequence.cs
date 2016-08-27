using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GenomeNotepad
{
    public class Sequence
    {
        //----------------------------------------------------------------------------------------------------
        // Enumerations
        //----------------------------------------------------------------------------------------------------

        // Sequence file format.

        public enum SequenceFormat
        {
            None,
            GenomeStudio,
            PlainSequence,
            PlainSequenceExtended,
            EMBL,
            FASTA,
            GCG,
            GCGRSF,
            Genbank,
            IG,
            Genomatics
        }

        // Sequence address mode.
        // e.g. Absolute = counting from origin.
        //      Relative = counting from first unit in segment or feature.

        public enum SequenceAddressMode
        {            
            Absolute,
            Relative
        }

        // Sequence unit. either individual bases, or codons of a specific leangth.

        public enum SequenceUnit
        {
            Base,
            Codon
        }

        // Sequence Encoding.

        public enum SequenceEncoding
        {
            DNA,
            mRNA
        }

        // Sequence Strand.

        public enum SequenceStrand
        {
            Sense,
            Antisense,
            Double
        }

        //----------------------------------------------------------------------------------------------------
        // Private memebers
        //----------------------------------------------------------------------------------------------------

        private byte[]              data;                   // Sequence data block.
        private long                blockSize;              // Sequence data block size.
        private long                baseCount;              // Sequence length in bases.
        private long                origin;                 // Address of sequence origin. Default = 0.        
        private SequenceEncoding    encoding;               // DNA or mRNA.
        private SequenceStrand      strand;                 // Sense, Antisense, or double stranded.
        private SequenceUnit        unit;                   // Sequence units are either bases or codons.
        private SequenceAddressMode addressModeBase;        // Absolute address offset from origin, or relative address offest into sequence feature. 
        private SequenceAddressMode addressModeCodon;       // Absolute address offset from origin, or relative address offest into sequence feature. 
        private bool                addressGroupDigits;     // Seperate thousands group with a comma. e.g. 1,000,000.
        private int                 columnCountBase;        // * No longer used. Use groupCountBase instead.
        private int                 columnCountCodon;       // * No longer used. Use groupCountCodon instead.
        private int                 groupWidthBase;         // Width of a base sequence group. e.g. acgtacgtac acgtacgtac acgtacgtac = 3 columns of group width 10.
        private int                 groupWidthCodon;        // Width of a codon sequence group. e.g. att att att-att att att = 2 columns of codon group width 3.
        private int                 groupCountBase;         // Number of sequence columns. e.g. acgt acgt acgt = 3 columns of base group width 4.
        private int                 groupCountCodon;        // Number of sequence columns. e.g. atg atg-atg atg-atg atg = 3 columns of codon group width 2.
        private string              groupSeperatorBase;     // Seperator string/characater. e.g. acgt acgt acgt = " ".
        private string              groupSeperatorCodon;    // Seperator string/characater. e.g. att att-att att = "-".
        private int                 codonWidth;             // Width of a codon. Default for natural DNA = 3.
        private int                 columnDividerWidth;     // * Not yet used.
        private string              addressFormatBase;      // Base address format. e.g. "000,000,000,000".
        private string              addressFormatCodon;     // Codon address format. e.g. "00,000".
        private bool                upperCase;              // Upper case or lower case sequence code.

        //----------------------------------------------------------------------------------------------------
        // Properties
        //----------------------------------------------------------------------------------------------------

        #region

        public long BaseCount
        {
            get { return this.baseCount; }            
        }

        public SequenceUnit Unit
        {
            get { return this.unit; }
            set { this.unit = value; }
        }

        public int CodonWidth
        {
            get { return this.codonWidth; }
            set { this.codonWidth = value; }
        }

        public string AddressFormatBase
        {
            get { return this.addressFormatBase; }
            set { this.addressFormatBase = value; }
        }

        public string AddressFormatCodon
        {
            get { return this.addressFormatCodon; }
            set { this.addressFormatCodon = value; }
        }

        public byte [] Data
        {
            get { return this.data; }
            set { this.data = value; }
        }

        public SequenceEncoding Encoding
        {
            get { return this.encoding; }
            set { this.encoding = value; }
        }

        public SequenceStrand Strand
        {
            get { return this.strand; }
            set { this.strand = value; }
        }

        public int ColumnCountBase
        {
            get { return this.columnCountBase; }
            set { this.columnCountBase = value; }
        }

        public int GroupWidthBase
        {
            get { return this.groupWidthBase; }
            set { this.groupWidthBase = value; }
        }

        public int GroupWidthCodon
        {
            get { return this.groupWidthCodon; }
            set { this.groupWidthCodon = value; }
        }

        public int GroupCountBase
        {
            get { return this.groupCountBase; }
            set { this.groupCountBase = value; }
        }

        public int GroupCountCodon
        {
            get { return this.groupCountCodon; }
            set { this.groupCountCodon = value; }
        }

        public bool UpperCase
        {
            get { return this.upperCase; }
            set { this.upperCase = value; }
        }

        #endregion

        //----------------------------------------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------------------------------------

        public Sequence ()
        {
            LoadSettings ();
        }

        //----------------------------------------------------------------------------------------------------
        // LoadSettings
        //----------------------------------------------------------------------------------------------------      

        public void LoadSettings ()
        {
            this.blockSize           = Properties.Settings.Default.SequenceBlockSize;
            this.data                = new byte [ this.blockSize ];
            this.baseCount           = 0;
            this.origin              = Properties.Settings.Default.SequenceOrigin;
            this.encoding            = (SequenceEncoding) Properties.Settings.Default.SequenceEncoding;
            this.strand              = (SequenceStrand) Properties.Settings.Default.SequenceStrand;
            this.unit                = (SequenceUnit) Properties.Settings.Default.SequenceUnit;
            this.addressModeBase     = SequenceAddressMode.Absolute;
            this.addressModeCodon    = SequenceAddressMode.Relative;
            this.addressGroupDigits  = Properties.Settings.Default.SequenceAddressGroupDigits;
            this.columnCountBase     = Properties.Settings.Default.SequenceColumnCountBase;
            this.columnCountCodon    = Properties.Settings.Default.SequenceColumnCountCodon;
            this.groupWidthBase      = Properties.Settings.Default.SequenceGroupWidthBase;
            this.groupWidthCodon     = Properties.Settings.Default.SequenceGroupWidthCodon;
            this.groupCountBase      = Properties.Settings.Default.SequenceGroupCountBase;
            this.groupCountCodon     = Properties.Settings.Default.SequenceGroupCountCodon;
            this.groupSeperatorBase  = " ";
            this.groupSeperatorCodon = "-";
            this.codonWidth          = Properties.Settings.Default.SequenceCodonWidth;
            this.columnDividerWidth  = Properties.Settings.Default.SequenceColumnDividerWidth;
            this.addressFormatBase   = Properties.Settings.Default.AddressFormatBase;
            this.addressFormatCodon  = Properties.Settings.Default.AddressFormatCodon;
            this.upperCase           = Properties.Settings.Default.SequenceUpperCase;
        }

        //----------------------------------------------------------------------------------------------------
        // SaveSettings
        //----------------------------------------------------------------------------------------------------      

        public void SaveSettings ()
        {
        }

        //----------------------------------------------------------------------------------------------------
        // LoadSequence
        //----------------------------------------------------------------------------------------------------

        public void LoadSequence ( string filePath, SequenceFormat sequenceFormat )
        {
            switch ( sequenceFormat )
            {
                case SequenceFormat.GenomeStudio:
                    break;

                case SequenceFormat.PlainSequence:
                    LoadSequencePlainSequence ( filePath );
                    break;

                case SequenceFormat.PlainSequenceExtended:
                    break;
            }
        }

        //----------------------------------------------------------------------------------------------------
        // LoadSequencePlainSequence
        //----------------------------------------------------------------------------------------------------

        public void LoadSequencePlainSequence ( string filePath )
        {
            StreamReader streamReader = new StreamReader ( filePath );
            char ch    = (char) 0;
            long index = 0;

            while ( !streamReader.EndOfStream )
            {
                ch = (char) streamReader.Read ();
                if
                (
                    ( ( ch == 'a' ) || ( ch == 'A' ) )      // Adenine.     ( DNA / RNA )
                    ||
                    ( ( ch == 'c' ) || ( ch == 'C' ) )      // Cytosine.    ( DNA / RNA )
                    ||
                    ( ( ch == 'g' ) || ( ch == 'G' ) )      // Guanine.     ( DNA / RNA )
                    ||
                    ( ( ch == 't' ) || ( ch == 'T' ) )      // Thymine.     ( DNA )
                    ||
                    ( ( ch == 'u' ) || ( ch == 'U' ) )      // Uracil.      ( RNA )
                )
                {
                    // Convert to upper case.

                    ch = (ch.ToString ().ToUpper ())[0];

                    // Convert all sequence bases to DNA encoding.

                    if ( ch == 'u' ) ch = 't';
                    if ( ch == 'U' ) ch = 'T';
                    
                    // Load sequence data buffer.

                    this.data [ index ] = (byte) ch;
                    index++;
                }
            }
            streamReader.Close ();

            this.baseCount = index;
        }



        //----------------------------------------------------------------------------------------------------
        // ApplyCase        
        //----------------------------------------------------------------------------------------------------

        public char ApplyCase ( Sequence sequence, char chIn )
        {
            char chOut = chIn;

            if ( sequence.UpperCase )
            {
                chOut = ( chIn.ToString ().ToUpper () ) [ 0 ];
            }
            else
            {
                chOut = ( chIn.ToString ().ToLower () ) [ 0 ];
            }

            return chOut;
        }

        //----------------------------------------------------------------------------------------------------
        // ApplyEncoding        
        //----------------------------------------------------------------------------------------------------

        public char ApplyEncoding ( Sequence sequence, char chIn )
        {
            char chOut = chIn;

            switch ( sequence.Encoding )
            {
                case Sequence.SequenceEncoding.DNA:

                    if ( chIn == 'u' ) chOut = 't';
                    if ( chIn == 'U' ) chOut = 'T';

                    break;

                case Sequence.SequenceEncoding.mRNA:

                    if ( chIn == 't' ) chOut = 'u';
                    if ( chIn == 'T' ) chOut = 'U';

                    break;
            }

            return chOut;
        }

        //----------------------------------------------------------------------------------------------------
        // ApplyStrand        
        //----------------------------------------------------------------------------------------------------

        public char ApplyStrand ( Sequence sequence, char chIn )
        {
            char chOut = chIn;

            switch ( sequence.Strand )
            {
                case Sequence.SequenceStrand.Sense:
                    break;

                case Sequence.SequenceStrand.Antisense:

                    switch ( sequence.Encoding )
                    {
                        case Sequence.SequenceEncoding.DNA:

                            if      ( chIn == 'a' ) chOut = 't';
                            else if ( chIn == 'A' ) chOut = 'T';
                            else if ( chIn == 't' ) chOut = 'a';
                            else if ( chIn == 'T' ) chOut = 'A';                            
                            else if ( chIn == 'u' ) chOut = 'a';
                            else if ( chIn == 'U' ) chOut = 'A';

                            break;

                        case Sequence.SequenceEncoding.mRNA:

                            if      ( chIn == 'a' ) chOut = 'u';
                            else if ( chIn == 'A' ) chOut = 'U';
                            else if ( chIn == 'u' ) chOut = 'a';
                            else if ( chIn == 'U' ) chOut = 'A';
                            else if ( chIn == 't' ) chOut = 'a';
                            else if ( chIn == 'T' ) chOut = 'A';

                            break;
                    }

                    if      ( chIn == 'c' ) chOut = 'g';
                    else if ( chIn == 'C' ) chOut = 'G';
                    else if ( chIn == 'g' ) chOut = 'c';
                    else if ( chIn == 'G' ) chOut = 'C';

                    break;

                case Sequence.SequenceStrand.Double:
                    break;
            }

            return chOut;
        }

        //----------------------------------------------------------------------------------------------------
        // GetAminoAcid1LetterSymbol        
        //
        // Preconditions:
        //   All codons must be in upper case DNA format.
        //----------------------------------------------------------------------------------------------------

        public char GetAminoAcid1LetterSymbol ( string codon )
        {
            char symbol = '.';

            // Alanine, Ala, A

            if ( codon == "GCT" ) symbol = 'A';
            if ( codon == "GCC" ) symbol = 'A';
            if ( codon == "GCA" ) symbol = 'A';
            if ( codon == "GCG" ) symbol = 'A';

            // Arginine, Arg, R

            if ( codon == "CGT" ) symbol = 'R';
            if ( codon == "CGC" ) symbol = 'R';
            if ( codon == "CGA" ) symbol = 'R';
            if ( codon == "CGG" ) symbol = 'R';
            if ( codon == "AGA" ) symbol = 'R';
            if ( codon == "AGG" ) symbol = 'R';

            // Asparagine, Asn, N

            if ( codon == "AAT" ) symbol = 'N';
            if ( codon == "AAC" ) symbol = 'N';

            // Aspartic acid, Asp, D

            if ( codon == "GAT" ) symbol = 'D';
            if ( codon == "GAC" ) symbol = 'D';

            // Cysteine, Cys, C

            if ( codon == "TGT" ) symbol = 'C';
            if ( codon == "TGC" ) symbol = 'C';

            // Glutamine, Gln, Q

            if ( codon == "CAA" ) symbol = 'Q';
            if ( codon == "CAG" ) symbol = 'Q';

            // Glutamic acid, Glu, E

            if ( codon == "GAA" ) symbol = 'E';
            if ( codon == "GAG" ) symbol = 'E';

            // Glycine, Gly, G

            if ( codon == "GGT" ) symbol = 'G';
            if ( codon == "GGC" ) symbol = 'G';
            if ( codon == "GGA" ) symbol = 'G';
            if ( codon == "GGG" ) symbol = 'G';

            // Histidine, His, H

            if ( codon == "CAT" ) symbol = 'H';
            if ( codon == "CAC" ) symbol = 'H';

            // Isoleucine, Ile, I

            if ( codon == "ATT" ) symbol = 'I';
            if ( codon == "ATC" ) symbol = 'I';
            if ( codon == "ATA" ) symbol = 'I';

            // (START): Methionine, Met, M ( ► )

            if ( codon == "ATG" ) symbol = '►';

            // Leucine, Leu, L

            if ( codon == "TTA" ) symbol = 'L';
            if ( codon == "TTG" ) symbol = 'L';
            if ( codon == "CTT" ) symbol = 'L';
            if ( codon == "CTC" ) symbol = 'L';
            if ( codon == "CTA" ) symbol = 'L';
            if ( codon == "CTG" ) symbol = 'L';

            // Lysine, Lys, K

            if ( codon == "AAA" ) symbol = 'K';
            if ( codon == "AAG" ) symbol = 'K';

            // Phenylalanine, Phe, F

            if ( codon == "TTT" ) symbol = 'F';
            if ( codon == "TTC" ) symbol = 'F';

            // Proline, Pro, P

            if ( codon == "CCT" ) symbol = 'P';
            if ( codon == "CCC" ) symbol = 'P';
            if ( codon == "CCA" ) symbol = 'P';
            if ( codon == "CCG" ) symbol = 'P';

            // Serine, Ser, S

            if ( codon == "TCT" ) symbol = 'S';
            if ( codon == "TCC" ) symbol = 'S';
            if ( codon == "TCA" ) symbol = 'S';
            if ( codon == "TCG" ) symbol = 'S';
            if ( codon == "AGT" ) symbol = 'S';
            if ( codon == "AGC" ) symbol = 'S';

            // Threonine, Thr, T

            if ( codon == "ACT" ) symbol = 'T';
            if ( codon == "ACC" ) symbol = 'T';
            if ( codon == "ACA" ) symbol = 'T';
            if ( codon == "ACG" ) symbol = 'T';

            // Tryptophan, Trp, W

            if ( codon == "TGG" ) symbol = 'W';

            // Tyrosine, Tyr, Y

            if ( codon == "TAT" ) symbol = 'Y';
            if ( codon == "TAC" ) symbol = 'Y';

            // Valine, Val, V

            if ( codon == "GTT" ) symbol = 'V';
            if ( codon == "GTC" ) symbol = 'V';
            if ( codon == "GTA" ) symbol = 'V';
            if ( codon == "GTG" ) symbol = 'V';

            // (STOP):

            if ( codon == "TAA" ) symbol = '●';

            // (STOP): Selenocysteine, Sec, U

            if ( codon == "TGA" ) symbol = '●';

            // (STOP): Pyrrolysine, Pyl, O

            if ( codon == "TAG" ) symbol = '●';
            
            return symbol;
        }

        //----------------------------------------------------------------------------------------------------
        // GetAminoAcid3LetterSymbol        
        //----------------------------------------------------------------------------------------------------

        public char GetAminoAcid3LetterSymbol ( string codon )
        {
            return (char) 0;
        }

        //----------------------------------------------------------------------------------------------------
        // GenerateTestSequence
        //----------------------------------------------------------------------------------------------------

        public void GenerateTestSequence ()
        {
        }
    }
}
