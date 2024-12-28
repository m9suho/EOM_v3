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
using Guna.UI2.WinForms;

namespace EOM_v3_M
{
    public partial class EOCheckPCBForm_M : Form
    {
        string strRadioButtonName = string.Empty;
        string strColumnName = string.Empty;
        string[] selectData;

        Stopwatch sw = new Stopwatch();

        private void initialSetDataGridView()
        {
            MainForm.dc.DataGridViewDoubleBuffered(dgvMainSub, true);
            MainForm.dc.DataGridViewDoubleBuffered(dgvExclude, true);
        }

        private void DGVDataAdd(DataGridView _dgv, string[,] _data)
        {
            dgvMainSub.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvExclude.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            _dgv.Rows.Clear();

            for (int i = 0; i < _data.GetLength(0); i++)
            {
                // *.mht
                string eoMhtData = string.Empty;

                if (_data[i, 8] != "-") _data[i, 8] = Convert.ToDateTime(_data[i, 8]).ToString("yyyy-MM-dd HH:mm:ss");
                if (_data[i, 9] != "-") _data[i, 9] = Convert.ToDateTime(_data[i, 9]).ToString("yyyy-MM-dd HH:mm:ss");
                //if (_data[i, 13] != "-") eoMhtData = "보기";

                _dgv.Rows.Add(
                    false,
                    _data[i, 1],                                                            // 품번
                    _data[i, 2],                                                            // 차종
                    _data[i, 3],                                                            // 고객사 EO
                    _data[i, 4],                                                            // 모비스 EO
                    _data[i, 5],                                                            // EO 내용
                    _data[i, 7],                                                            // 스티커 내용
                    _data[i, 8],                                                            // 적용일
                    _data[i, 9],                                                            // 종료일
                    _data[i, 10],                                                           // 등록자
                    _data[i, 11],                                                           // 타입
                    _data[i, 12],                                                           // 출하지
                    string.Empty,                                                           // mht
                    _data[i, 14],                                                           // MAIN PCB
                    _data[i, 15],                                                           // SUB PCB
                    _data[i, 16]                                                            // EO 구분
                );

                if (_data[i, 6] != "-") MainForm.dc.SetDGVCellsBackColor(_dgv, i, 6, _data[i, 6]);
            }

            dgvMainSub.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvExclude.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        public EOCheckPCBForm_M()
        {
            InitializeComponent();
        }

        public EOCheckPCBForm_M(string[] _data)
        {
            selectData = _data;
            InitializeComponent();
        }

        private void EOCheckPCBForm_M_Load(object sender, EventArgs e)
        {
            Text = lblFormTitle.Text;

#if !DEBUG
            txtPCB.Text = string.Empty;
#endif

            if (selectData != null)
            {
                if (selectData[1] == "MAIN PCB") rdoMain.Checked = true;
                else rdoSub.Checked = true;

                txtPCB.Text = selectData[0];

                btnSearch.PerformClick();
            }

            txtPCB.Enabled = true;
            grpEOType.Enabled = true;
        }

        private void EOCheckPCBForm_M_Resize(object sender, EventArgs e)
        {
            //dgvMainSub.Size = new Size(Width - 22, Height - 360);
            dgvMainSub.Size = new Size(Width - 22, Height - 150);
            dgvExclude.Size = new Size(Width - 22, Height - 470);
        }

        private void cbbShipment_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblFormTitle_DoubleClick(object sender, EventArgs e)
        {
            guna2ControlBox2.PerformClick();
        }

        private void dgvSelect_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    string[] gridData = new string[17];

                    for (int i = 2; i < gridData.Length; i++)
                    {
                        // 스티커 컬러, 차수
                        if (i == 6)
                        {
                            gridData[6] = dgvMainSub.Rows[e.RowIndex].Cells[6].Style.BackColor.Name.ToString();
                            gridData[7] = dgvMainSub.Rows[e.RowIndex].Cells[6].Value.ToString();
                            i++;
                        }
                        else if (i >= 8)
                        {
                            gridData[i] = dgvMainSub.Rows[e.RowIndex].Cells[i - 1].Value.ToString();
                        }
                        else
                        {
                            gridData[i] = dgvMainSub.Rows[e.RowIndex].Cells[i].Value.ToString();
                        }
                    }

                    Hide();

                    EOCheckContentsForm_M eOCheckContentsForm_M = new EOCheckContentsForm_M(gridData);
                    eOCheckContentsForm_M.ShowDialog();

                    Show();
                }
            }
            catch (Exception ex)
            {
                MainForm.dc.LogFileSave(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            sw.Reset();
            sw.Start();

            if (txtPCB.MaxLength != txtPCB.TextLength)
            {
                MainForm.Guna2Msg(this, "오류", "PCB 품번을 12자리 입력해주세요");
                txtPCB.Focus();
                return;
            }

            if (rdoMain.Checked)
            {
                strColumnName = "main_pcb_name";
                strRadioButtonName = rdoMain.Text;
            }
            else if (rdoSub.Checked)
            {
                strColumnName = "sub_pcb_name";
                strRadioButtonName = rdoSub.Text;
            }
            /*
            else
            {
                strRadioButtonName = rdoExclude.Text;
            }
            */

            PackingAndEOM pae = new PackingAndEOM();

            // 1. MAIN PCB 또는 SUB PCB
            //string query2 = "SELECT * FROM `" + MainForm.DATABASE_NAME + "`.`model_data` WHERE line = '" + MainForm.cbbLineName01.Text + "' AND " + strColumnName + " LIKE '" + txtPCB.Text.Substring(0, 11) + "%' AND eo_type = '" + strRadioButtonName + "' AND NOT eo_contents LIKE '%첫 투입 품번 등록%' GROUP BY eo_contents ORDER BY start_date";
            //string[,] resultData = MainForm.mariaDB.SelectQuery2(query);
            string query = pae.PCBTypeMasterEO(MainForm.DATABASE_NAME, strColumnName, MainForm.cbbLineName01.Text, txtPCB.Text.Substring(0, 11), strRadioButtonName);

                /*
                "SELECT * FROM ( " +
                "	SELECT * FROM `" + MainForm.DATABASE_NAME + "`.`model_data` " +
                "   WHERE (eo_contents, start_date, " + strColumnName + ") IN ( " +
                "		SELECT eo_contents, MIN(start_date), " + strColumnName + " FROM `" + MainForm.DATABASE_NAME + "`.`model_data` " +
                "       WHERE line = '" + MainForm.cbbLineName01.Text + "' AND " + strColumnName + " LIKE '" + txtPCB.Text.Substring(0, 11) + "%' AND eo_type = '" + strRadioButtonName + "' AND NOT eo_contents LIKE '%첫 투입 품번 등록%' " +
                "       GROUP BY eo_contents " +
                "	) " +
                ") t ORDER BY t.start_date";
                */

            string[,] resultData = MainForm.mariaDB.SelectQuery4(query, 18);

            DGVDataAdd(dgvMainSub, resultData);

            /*
            // 2. 제외
            query = "SELECT * FROM `" + MainForm.DATABASE_NAME + "`.`model_data` WHERE line = '" + MainForm.cbbLineName01.Text + "' AND " + strColumnName + " LIKE '" + txtPCB.Text.Substring(0, 11) + "%' AND eo_type = '" + rdoExclude.Text + "' AND NOT eo_contents LIKE '%첫 투입 품번 등록%' GROUP BY eo_contents ORDER BY start_date";
            resultData = MainForm.mariaDB.SelectQuery2(query);

            DGVDataAdd(dgvExclude, resultData);
            */

            MainForm.mariaDB.InsertLogDB(MainForm.DATABASE_NAME, "EO 적용 여부 점검 (" + selectData[0] + "/" + selectData[1] + "), " + sw.ElapsedMilliseconds + "ms", "user_data", false);

            lblPrint.Text = "총 " + resultData.GetLength(0) + "건 [" + sw.ElapsedMilliseconds + "ms]";
            sw.Stop();
        }

        private void txtFillColor_Enter(object sender, EventArgs e)
        {
            ((Guna2TextBox)sender).FillColor = Color.LemonChiffon;

            if (((Guna2TextBox)sender).Name == "txtEOContents")
            {
                MainForm.dc.ImeModeAllSet(this, "Hangul");
            }
            else
            {
                MainForm.dc.ImeModeAllSet(this, "");
            }
        }

        private void txtFillColor_Leave(object sender, EventArgs e)
        {
            ((Guna2TextBox)sender).FillColor = Color.White;
        }

        private void txtPCB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                btnSearch.PerformClick();
            }
        }

        private void txtPCB_KeyPress(object sender, KeyPressEventArgs e)
        {
            MainForm.dc.NumAlphaBetString((Guna2TextBox)sender, e);
        }

        // 2023.03.28
        // 텍스트박스 이벤트 통합
        private void txtAddHyphenCheckFirstChar_KeyUp(object sender, KeyEventArgs e)
        {
            MainForm.dc.AddHyphenCheckFirstChar((Guna2TextBox)sender, e);
        }

        private void rdoMain_CheckedChanged(object sender, EventArgs e)
        {
            txtPCB.Text = string.Empty;
            txtPCB.Select();
        }
    }
}
