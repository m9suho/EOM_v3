using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EOM_v3_M
{
    public partial class FpiCheckForm : Form
    {
        public FpiCheckForm()
        {
            InitializeComponent();
        }

        private void FpiCheckForm_Load(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();

            sw.Reset();
            sw.Start();

            string query =
                "SELECT a.model_name, a.car_name" +
                "   FROM `" + MainForm.DATABASE_NAME + "`.`model_data` a" +
                "   LEFT OUTER JOIN `fpi`.`model_data` b" +
                "      ON a.model_name = b.model_name " +
                "WHERE a.line = 'D-오디오 조립' AND b.model_name IS NULL GROUP BY a.model_name;";

            string[,] s = MainForm.mariaDB.SelectQuery4(query, 2);

            dgvView.Rows.Clear();

            for (int i = 0; i < s.GetLength(0); i++)
            {
                dgvView.Rows.Add(false, s[i, 0], s[i, 1]);
            }

            MainForm.mariaDB.InsertLogDB("초물이미지 미등록 확인 시작, " + sw.ElapsedMilliseconds + "ms", false);
        }

        private void btnShipmentMaster_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            //if (txtProductName.Text == string.Empty)
            //{
            //    MainForm.dc.Msg("경고", "품번을 선택해주세요");
            //    return;
            //}

            //Close();

            //MainForm.cbbProductName01.Text = txtProductName.Text;
        }

        private void dgvView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                txtProductName.Text = dgvView.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
        }

        private void dgvView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (txtProductName.Text == string.Empty)
                {
                    MainForm.dc.Msg("경고", "품번을 선택해주세요");
                    return;
                }

                Close();

                MainForm.cbbProductName01.Text = txtProductName.Text;
            }
        }
    }
}
