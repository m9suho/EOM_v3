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
    public partial class MessageForm_List : Form
    {
        string[,] userData;

        public MessageForm_List()
        {
            InitializeComponent();
        }

        private void MessageForm_List_Load(object sender, EventArgs e)
        {
            userData = MainForm.mariaDB.SelectQuery2("SELECT * FROM `" + MainForm.DATABASE_NAME + "`.`registrant_data` ORDER BY name");

            if (userData.GetLength(0) <= 0)
            {
                MainForm.Guna2Msg(this, "오류", "사용자 목록 조회 오류입니다 [윤민규 사원 문의]");
                Close();
            }

            treeViewList.Nodes[0].Nodes.Clear();

            for (int i = 0; i < userData.GetLength(0); i++)
            {
                if (userData[i, 1] == "윤민규") treeViewList.Nodes[0].Nodes.Add(userData[i, 1] + " [관리자 및 개발자]");
                else treeViewList.Nodes[0].Nodes.Add(userData[i, 1] + " [" + userData[i, 2] + "]");

            }

            treeViewList.ExpandAll();
        }

        private void treeViewList_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (userData[e.Node.Index, 1] == MainForm.strUserAddressData[0])
            {
                MainForm.Guna2Msg(this, "오류", "자신한테는 보낼 수 없습니다");
                return;
            }

            MessageForm_Send messageForm_Send = new MessageForm_Send(userData[e.Node.Index, 1]);

            Hide();
            messageForm_Send.Owner = this;
            messageForm_Send.ShowDialog();

            Close();
        }
    }
}
