using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EOM_v3_M
{
    public partial class Guna2MessageBox : Form
    {
        string type, msg;

        public Guna2MessageBox(string _data01, string _data02)
        {
            type = _data01;
            msg = _data02;

            InitializeComponent();
        }

        private void MessageBoxForm_Load(object sender, EventArgs e)
        {
            if (type == "오류")
            {
                ptOK.Visible = false;
                ptWarning.Visible = true;

                MainForm.dc.SoundPlay(@"C:\Windows\media\Alarm04.wav");
            }
            else
            {
                ptOK.Visible = true;
                ptWarning.Visible = false;

                MainForm.dc.SoundPlay(@"C:\Windows\media\Alarm02.wav");
            }

            lblContents.Text = msg;

            TopMost = true;
            TopMost = false;


        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
