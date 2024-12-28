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
    public partial class EOCheckContentsForm_M : Form
    {
        string strColumnName = string.Empty;
        string[] selectData;

        Stopwatch sw = new Stopwatch();

        private void DGVDataAdd(string[,] _data)
        {
            dgvSelect.Visible = false;

            dgvSelect.Rows.Clear();

            for (int i = 0; i < _data.GetLength(0); i++)
            {
                // *.mht
                string eoMhtData = string.Empty;

                if (_data[i, 8] != "-") _data[i, 8] = Convert.ToDateTime(_data[i, 8]).ToString("yyyy-MM-dd HH:mm:ss");
                if (_data[i, 9] != "-") _data[i, 9] = Convert.ToDateTime(_data[i, 9]).ToString("yyyy-MM-dd HH:mm:ss");
                //if (_data[i, 13] != "-") eoMhtData = "보기";

                dgvSelect.Rows.Add(
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

                if (_data[i, 6] != "-") MainForm.dc.SetDGVCellsBackColor(dgvSelect, i, 6, _data[i, 6]);
            }

            dgvSelect.Visible = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_data"></param>
        private void EOCompareSearch(string[] _data)
        {
            int count = 0;

            try
            {
                string[,] modelData = new string[_data.Length, 17];

                for (int i = 0; i < _data.Length; i++)
                {
                    string query = string.Empty;

                    // 1. 조건 검사 [고객사 EO, 모비스 EO]
                    if (txtCustomerEO.Text.Length == 6 && txtMobisEO.Text.Length == 6)
                    {
                        query = "SELECT * FROM `" + MainForm.DATABASE_NAME + "`.`model_data` WHERE line = '" + MainForm.cbbLineName01.Text + "' AND model_name = '" + _data[i] + "' AND customer_eo_number = '" + txtCustomerEO.Text + "' AND mobis_eo_number = '" + txtMobisEO.Text + "' AND " + strColumnName + " LIKE '" + txtPCB.Text.Substring(0, 11) + "%'";
                    }
                    // 2. 그 외     [EO 내용]
                    else
                    {
                        query = "SELECT * FROM `" + MainForm.DATABASE_NAME + "`.`model_data` WHERE line = '" + MainForm.cbbLineName01.Text + "' AND model_name = '" + _data[i] + "' AND eo_contents = '" + txtEOContents.Text + "' AND " + strColumnName + " LIKE '" + txtPCB.Text.Substring(0, 11) + "%'";
                    }
                    string[,] resultData = MainForm.mariaDB.SelectQuery2(query);

                    Clipboard.SetText(query);

                    // 2. 품번 입력
                    modelData[i, 1] = _data[i];                     // 품번

                    // 3. 검색된 데이터 - OK
                    if (resultData.GetLength(0) > 0)
                    {
                        for (int j = 2; j < modelData.GetLength(1); j++)
                        {
                            if (j != 13)
                            {
                                modelData[i, j] = resultData[0, j];
                            }
                        }

                        count++;
                    }
                    // 3. 검색된 데이터 - NG
                    else
                    {
                        for (int j = 2; j < modelData.GetLength(1); j++)
                        {
                            modelData[i, j] = "-";
                        }
                    }
                }

                // 검색된 데이터가 있을 경우에만 리스트 출력
                if (count > 0) DGVDataAdd(modelData);

                MainForm.mariaDB.InsertLogDB(MainForm.DATABASE_NAME, "EO 적용 여부 점검 (" + txtPCB.Text + "/" + txtCustomerEO.Text + "/" + txtMobisEO.Text + "/" + txtEOContents.Text +"), " + sw.ElapsedMilliseconds + "ms", "user_data", false);

            }
            catch (Exception ex)
            {
                MainForm.mariaDB.InsertLogDB(ex.Message, true);
            }
        }

        public EOCheckContentsForm_M()
        {
            InitializeComponent();
        }

        public EOCheckContentsForm_M(string[] _data)
        {
            selectData = _data;
            InitializeComponent();
        }

        private void EOCheckContentsForm_M_Load(object sender, EventArgs e)
        {
            Text = lblFormTitle.Text;

            if (selectData != null)
            {
                if (selectData[16] == "MAIN PCB")
                {
                    //txtPCB.Text = selectData[14].Substring(0, 11) + "%";
                    txtPCB.Text = selectData[14];
                    rdoMain.Checked = true;
                }
                else
                {
                    //txtPCB.Text = selectData[15].Substring(0, 11) + "%";
                    txtPCB.Text = selectData[15];
                    rdoSub.Checked = true;
                }

                txtCustomerEO.Text = selectData[3];
                txtMobisEO.Text = selectData[4];
                txtEOContents.Text = selectData[5];

                btnSearch.PerformClick();
            }
            else
            {
                txtPCB.Enabled = true;
                txtCustomerEO.Enabled = true;
                txtMobisEO.Enabled = true;
                grpEOType.Enabled = true;
                txtEOContents.Enabled = true;
            }
        }

        private void EOCheckContentsForm_M_Resize(object sender, EventArgs e)
        {
            dgvSelect.Size = new Size(Width - 22, Height - 295);
            lblHelp.Left = Width - lblHelp.Width - 11;
        }

        private void lblFormTitle_DoubleClick(object sender, EventArgs e)
        {
            guna2ControlBox2.PerformClick();
        }

        private void dgvSelect_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                MainForm.cbbProductName01.Text = dgvSelect.Rows[e.RowIndex].Cells[1].Value.ToString();
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
            }

            if (rdoMain.Checked)
            {
                //dgvSelect.Columns[13].Visible = false;
                dgvSelect.Columns[14].Visible = false;
                strColumnName = "main_pcb_name";
            }
            else
            {
                dgvSelect.Columns[13].Visible = false;
                //dgvSelect.Columns[14].Visible = false;
                strColumnName = "sub_pcb_name";
            }

            // 1. M/PCB 또는 S/PCB 품번 리스트 조회
            string query = "SELECT * FROM `" + MainForm.DATABASE_NAME + "`.`model_data` WHERE line = '" + MainForm.cbbLineName01.Text + "' AND " + strColumnName + " LIKE '" + txtPCB.Text.Substring(0, 11) + "%' GROUP BY model_name ORDER BY model_name";
            string[,] resultData = MainForm.mariaDB.SelectQuery2(query);

#if DEBUG
            Clipboard.SetText(query);
#endif

            // 2. 마지막 품번 기준으로 데이터 산출
            string[] searchProductNameList = new string[resultData.GetLength(0)];

            for (int i = 0; i < resultData.GetLength(0); i++)
            {
                searchProductNameList[i] = resultData[i, 1];
            }

            EOCompareSearch(searchProductNameList);

            lblPrint.Text = "총 " + resultData.GetLength(0) + "건 [" + sw.ElapsedMilliseconds + "ms]";

            MainForm.dc.Delay(100);

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

        // 2023.03.28
        // 텍스트박스 이벤트 통합
        private void txtAddHyphenCheckFirstChar_KeyUp(object sender, KeyEventArgs e)
        {
            MainForm.dc.AddHyphenCheckFirstChar((Guna2TextBox)sender, e);
        }
    }
}
