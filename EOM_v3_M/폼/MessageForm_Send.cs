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
    public partial class MessageForm_Send : Form
    {
        string strName = string.Empty;

        public MessageForm_Send(string _data)
        {
            strName = _data;
            InitializeComponent();
        }

        private void MessageForm_Send_Load(object sender, EventArgs e)
        {
            lblName.Text = strName;
            ActiveControl = txtMessage;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txtMessage.Text == string.Empty)
            {
                MainForm.Guna2Msg(this, "오류", "내용을 입력해주세요");
                txtMessage.Select();
                return;
            }

            // write_time, message, send_name, receive_name
            MainForm.mariaDB.EtcQuery("INSERT INTO `" + MainForm.strDbName + "`.`message_data` VALUES (NOW(),'" + txtMessage.Text + "','" + MainForm.userNameData + "','" + strName + "','C')");

            /*
            MessageFormClass messageFormClass = new MessageFormClass();
            messageFormClass.FormMsg = "메세지 전송이 완료되었습니다";
            messageFormClass.ShowDelay = 2000;
            messageFormClass.InstanceForm = this;
            messageFormClass.StartForm();
            */

            Close();
        }
    }
}
