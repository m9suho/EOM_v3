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
    public partial class EOCheckForm_M : Form
    {
        string eoTypeData = string.Empty;
        string[] selectData;

        private void DGVDataAdd(string[,] _data)
        {
            Stopwatch sw = new Stopwatch();
            sw.Reset();
            sw.Start();

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
            Stopwatch sw = new Stopwatch();
            sw.Reset();
            sw.Start();

            try
            {
                string[,] modelData = new string[_data.Length, 17];

                for (int i = 0; i < _data.Length; i++)
                {
                    string query = string.Empty;

                    // 1. 조건 검사 [고객사 EO, 모비스 EO]
                    if (txtCustomerEO.Text.Length == 6 && txtMobisEO.Text.Length == 6)
                    {
                        //query = "SELECT model_name, start_date, end_date, registrant, print_type, shipment, main_pcb_name, sub_pcb_name FROM `" + MainForm.strDbName + "`.`model_data` WHERE line = '" + MainForm.cbbLineName01.Text + "' AND model_name = '" + _data[i] + "' AND customer_eo_number = '" + txtCustomerEO.Text + "' AND mobis_eo_number = '" + txtMobisEO.Text + "'";
                        query = "SELECT * FROM `" + MainForm.strDbName + "`.`model_data` WHERE line = '" + MainForm.cbbLineName01.Text + "' AND model_name = '" + _data[i] + "' AND customer_eo_number = '" + txtCustomerEO.Text + "' AND mobis_eo_number = '" + txtMobisEO.Text + "'";
                    }
                    else
                    {
                        //query = "SELECT model_name, start_date, end_date, registrant, print_type, shipment, main_pcb_name, sub_pcb_name FROM `" + MainForm.strDbName + "`.`model_data` WHERE line = '" + MainForm.cbbLineName01.Text + "' AND model_name = '" + _data[i] + "' AND eo_contents = '" + txtEOContents.Text + "'";
                        query = "SELECT * FROM `" + MainForm.strDbName + "`.`model_data` WHERE line = '" + MainForm.cbbLineName01.Text + "' AND model_name = '" + _data[i] + "' AND eo_contents = '" + txtEOContents.Text + "'";
                    }
                    //string[,] resultData = MainForm.mariaDB.SelectQuery4(query, 8);
                    string[,] resultData = MainForm.mariaDB.SelectQuery2(query);

                    // 2. 품번 입력
                    modelData[i, 1] = _data[i];                     // 품번

                    // 3. 검색된 데이터 - OK
                    if (resultData.GetLength(0) > 0)
                    {
                        /*
                        modelData[i, 8] = resultData[0, 1];         // 시작일
                        modelData[i, 9] = resultData[0, 2];         // 종료일
                        modelData[i, 10] = resultData[0, 3];        // 등록자
                        modelData[i, 11] = resultData[0, 4];        // 타입
                        modelData[i, 12] = resultData[0, 5];        // 출하지
                        modelData[i, 14] = resultData[0, 6];        // M/PCB
                        modelData[i, 15] = resultData[0, 7];        // S/PCB

                        for (int j = 2; j < modelData.GetLength(1); j++)
                        {
                            // [2 ~ 7], [14 ~ 16] 입력
                            if (j <= 7 || j >= 14)
                            {
                                modelData[i, j] = selectData[j];
                            }
                        }
                        */
                        for (int j = 2; j < modelData.GetLength(1); j++)
                        {
                            if (j != 13)
                            {
                                modelData[i, j] = resultData[0, j];
                            }
                        }
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

                DGVDataAdd(modelData);

                lblDelayTimeView.Text = sw.ElapsedMilliseconds + "ms 지연";

                MainForm.mariaDB.InsertLogDB("EO 적용 여부 점검 시작 (" + txtPCB.Text + "/" + txtCustomerEO.Text + "/" + txtMobisEO.Text + "/" + txtEOContents.Text +"), " + sw.ElapsedMilliseconds + "ms", false);
            }
            catch (Exception ex)
            {
                MainForm.mariaDB.InsertLogDB(ex.Message, true);
            }
        }

        public EOCheckForm_M()
        {
            InitializeComponent();
        }

        public EOCheckForm_M(string[] _data)
        {
            selectData = _data;
            InitializeComponent();
        }

        private void EOCheckForm_M_Load(object sender, EventArgs e)
        {
            Text = MainForm.programName + " - " + MainForm.dc.Version();

            eoTypeData = selectData[16];

            if (eoTypeData == "MAIN PCB")
            {
                dgvSelect.Columns[13].Visible = true;
                //dgvSelect.Columns[14].Visible = true;
                txtPCB.Text = selectData[14].Substring(0, 11) + "%";
                eoTypeData = "main_pcb_name";
            }
            else if (eoTypeData == "SUB PCB")
            {
                //dgvSelect.Columns[13].Visible = true;
                dgvSelect.Columns[14].Visible = true;
                txtPCB.Text = selectData[15].Substring(0, 11) + "%";
                eoTypeData = "sub_pcb_name";
            }
            else
            {
                MainForm.dc.Msg("경고", "EO 구분이 메인 PCB 또는 서브 PCB만 조회할 수 있습니다");
                Close();
                return;
            }

            txtCustomerEO.Text = selectData[3];
            txtMobisEO.Text = selectData[4];
            txtEOContents.Text = selectData[5];

            // 1. M/PCB 또는 S/PCB 품번 리스트 조회
            string query = "SELECT * FROM `" + MainForm.strDbName + "`.`model_data` WHERE line = '" + MainForm.cbbLineName01.Text + "' AND " + eoTypeData + " LIKE '" + txtPCB.Text + "' GROUP BY model_name ORDER BY model_name";
            string[,] resultData = MainForm.mariaDB.SelectQuery2(query);

            // 2. 마지막 품번 기준으로 데이터 산출
            string[] searchProductNameList = new string[resultData.GetLength(0)];

            /*
            //int count = 0;

            for (int i = 0; i < resultData.GetLength(0); i++)
            {
                query = "SELECT * FROM `" + MainForm.strDbName + "`.`model_data` WHERE line = '" + MainForm.cbbLineName01.Text + "' AND model_name = '" + resultData[i, 1] + "' ORDER BY start_date ASC";
                string[] endSelectData = MainForm.mariaDB.ModelEndSelectData(query);

                // 현재 PCB == 모델 중 마지막 데이터 PCB
                //if (resultData[i, 14] == endSelectData[14])
                {
                    searchProductNameList[count++] = endSelectData[1];
                }
                
            }

            //Array.Resize(ref searchProductNameList, count);
            */

            for (int i = 0; i < resultData.GetLength(0); i++)
            {
                searchProductNameList[i] = resultData[i, 1];
            }

            EOCompareSearch(searchProductNameList);

            MainForm.dc.Delay(100);
        }

        private void dgvSelect_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MainForm.DGVMHTDataView(dgvSelect, e, this);
        }

        private void cbbShipment_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 메인 출하지 설변태그 조회
            //string query = "SELECT * FROM `" + MainForm.strDbName + "`.`model_data` WHERE model_name = '" + selectData + "' AND end_date > '" + DateTime.Now.ToString("yyyy") + "-01-01' AND shipment = '" + shipmentData + "'";
            //string[,] selectData = MainForm.mariaDB.SelectQuery2(query);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            /*
            int count = 0;
            string[,] checkData = new string[1000, 3];

            // 체크 데이터 불러오기
            for (int i = 0; i < dgvSelect.RowCount; i++)
            {
                if (dgvSelect.Rows[i].Cells[0].Value.ToString() == "True")
                {
                    // 2022.01.25
                    // 고객사 EO & 모비스 EO & EO 내용 검사
                    checkData[count, 0] = dgvSelect.Rows[i].Cells[3].Value.ToString();
                    checkData[count, 1] = dgvSelect.Rows[i].Cells[4].Value.ToString();
                    checkData[count, 2] = dgvSelect.Rows[i].Cells[5].Value.ToString();
                    count++;
                }
            }

            checkData = MainForm.dc.TwoDimensionResize(checkData, count, 3);
            selectDataInsert = new string[checkData.GetLength(0), 17];
            count = 0;

            // 불러온 데이터 검사
            for (int i = 0; i < selectData.GetLength(0); i++)
            {
                for (int j = 0; j < checkData.GetLength(0); j++)
                {
                    if (selectData[i, 3] == checkData[j, 0] &&   // 고객사 EO
                        selectData[i, 4] == checkData[j, 1] &&   // 모비스 EO
                        selectData[i, 5] == checkData[j, 2])     // EO 내용
                    {
                        for (int k = 0; k < 17; k++)
                        {
                            selectDataInsert[count, k] = selectData[i, k];
                        }

                        count++;
                    }
                }
                //count++;
            }

            // 값 추가
            for (int i = 0; i < selectDataInsert.GetLength(0); i++)
            {
                string query = string.Empty;
                string[] insertData = new string[17];

                for (int j = 0; j < insertData.Length; j++)
                {
                    insertData[j] = selectDataInsert[i, j];
                }

                query = MainForm.dc.InsertQueryArrayConvert(MainForm.strDbName, "model_data", insertData);

                MainForm.mariaDB.EtcQuery(query);
            }
            */
        }

        private void EOCheckForm_M_Resize(object sender, EventArgs e)
        {
            dgvSelect.Size = new Size(Width - 22, Height - 247 - 11);
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
    }
}
