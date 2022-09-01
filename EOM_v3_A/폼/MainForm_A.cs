using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EOM_v3_A
{
    public partial class MainForm : MetroFramework.Forms.MetroForm
    {
        string programName = "Engineer Order Manager v3.0 (멀티 통합)";

        DefaultClass dc = new DefaultClass();

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = programName + " - " + dc.Version();

            
        }
    }
}
