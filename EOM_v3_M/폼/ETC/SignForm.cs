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
    public partial class SignForm : MetroFramework.Forms.MetroForm
    {
        public SignForm()
        {
            InitializeComponent();
        }

        private void SignForm_Load(object sender, EventArgs e)
        {
            Text = MainForm.programName + " - " + MainForm.dc.Version();
        }

        private void CreateMetroBtn_Click(object sender, EventArgs e)
        {
            if(!metroTextBox1.Text.Equals(string.Empty))
            {
                DefaultClass dc = new DefaultClass();

#if DEBUG
                MainForm.mariaDB.InsertJoinDB("eom_1floor_trunk", "join_data", metroTextBox1.Text);
#else
                MainForm.mariaDB.InsertJoinDB("eom_1floor", "join_data", metroTextBox1.Text);
#endif
                //dc.DatFileSave(@"\\10.239.14.12\xmsirius\D-AUDIO\D오디오 생산팀\D-AUDIO_TOOLS\EOM_v3\1F\SIGN", DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".dat", tmpData);

                MainForm.Guna2Msg("알림", "등록 요청이 완료되었습니다");
                Close();
            }
        }
    }
}
