using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EOM_v3_M
{
    public partial class MessageForm_Receive : Form
    {
        string[] strData;

        public MessageForm_Receive(string[] _data)
        {
            strData = _data;
            InitializeComponent();
        }

        private void MessageForm_Send_Load(object sender, EventArgs e)
        {
            lblName.Text = strData[2];
            txtMessage.Text = strData[1] + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + "***** 메세지 전송시간 : " + Convert.ToDateTime(strData[0]).ToString("yyyy-MM-dd HH:mm:ss");

            txtMessage.Select(txtMessage.Text.Length - 1, 0);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            MainForm.dc.LogFileSave(lblName.Text + "/" + txtMessage.Text);

            //if (!chkBoxMessageRetry.Checked)
            {
                MainForm.mariaDB.EtcQuery("UPDATE `" + MainForm.DATABASE_NAME + "`.`message_data` SET receive_check = '' WHERE write_time = '" + Convert.ToDateTime(strData[0]).ToString("yyyy-MM-dd HH:mm:ss") + "' AND send_name = '" + strData[2] + "' AND receive_name = '" + strData[3] + "'");
            }

            /*
            if (txtMessage.Text == string.Empty)
            {
                MainForm.Guna2Msg(this, "오류", "내용을 입력해주세요");
                return;
            }

            // write_time, message, send_name, receive_name
            MainForm.mariaDB.EtcQuery("INSERT INTO `" + DATABASE_NAME + "`.`message_data` VALUES (NOW(),'" + txtMessage.Text + "','" + MainForm.strUserAddressData[0] + "','" + strName + "','C')");

            Close();
            */
            Close();
        }
    }
}
