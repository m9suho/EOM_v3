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
    public partial class Form4 : Form
    {
        int errorCount;

        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            login();
        }

        public void login()
        {
            if (textBox1.Text.Equals(@"admin") && textBox2.Text.Equals(@"admin1!"))
            {
                MainForm.login = 1;
                Close();
            }
            else
            {
                errorCount++;
                label3.Text = @"아이디 또는 비밀번호가 틀렸습니다. (" + errorCount + ")";
            }
            if (errorCount >= 5)
            {
                Close();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                login();
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            Text = MainForm.programName + " - " + MainForm.dc.Version();
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Close();
        }
    }
}
