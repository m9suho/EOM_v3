using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EOM_v3_M
{
    public partial class SignForm : Form
    {
        public SignForm()
        {
            InitializeComponent();
        }

        private void SignForm_Load(object sender, EventArgs e)
        {
            Text = MainForm.programName + " - " + MainForm.dc.Version();
        }

        private void btnJoinEnter_Click(object sender, EventArgs e)
        {
            if (!txtUserName.Text.Equals(string.Empty))
            {
                DefaultClass dc = new DefaultClass();

#if DEBUG
                MainForm.mariaDB.InsertJoinDB(MainForm.DATABASE_NAME, "join_data", txtUserName.Text);
#else
                MainForm.mariaDB.InsertJoinDB(MainForm.DATABASE_NAME, "join_data", txtUserName.Text);
#endif

                MainForm.Guna2Msg(this, "알림", "사용자 등록 요청이 완료되었습니다\n3일 내에 등록이 되지 않을 경우 010-3363-0127 연락바랍니다.");
                Close();
            }
        }
    }
}
