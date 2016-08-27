using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GenomeNotepad
{
    public partial class AboutBox : Form
    {
        public AboutBox ()
        {
            InitializeComponent ();
        }

        private void buttonOK_Click_1 ( object sender, EventArgs e )
        {
            Close ();
        }
    }
}
