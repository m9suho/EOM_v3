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
        string[] modelData;

        private void dgvDataAdd(string[,] _data)
        {
            dgvSelect.Rows.Clear();

            for (int i = 0; i < _data.GetLength(0); i++)
            {
                // *.mht
                string eoMhtData = string.Empty;

                if (_data[i, 8] != "-") _data[i, 8] = Convert.ToDateTime(_data[i, 8]).ToString("yyyy-MM-dd HH:mm:ss");
                if (_data[i, 9] != "-") _data[i, 9] = Convert.ToDateTime(_data[i, 9]).ToString("yyyy-MM-dd HH:mm:ss");
                if (_data[i, 13] != "-") eoMhtData = "보기";

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
                    eoMhtData                                                               // mht
                    //_data[i, 14],                                                           // MAIN PCB
                    //_data[i, 15],                                                           // SUB PCB
                    //_data[i, 16]                                                            // EO 구분
                );
            }
        }

        private void DataSearchInput(string[,] _data)
        {
            Stopwatch sw = new Stopwatch();
            sw.Reset();
            sw.Start();

            try
            {
                string[,] gridViewData = new string[_data.GetLength(0), 14];

                for (int i = 0; i < _data.GetLength(0); i++)
                {
                    string query = string.Empty;

                    // 고객사 EO, 모비스 EO 검사
                    if (_data[i, 3].Length == 6 && _data[i, 4].Length == 6)
                    {
                        query = "SELECT * FROM `" + MainForm.settingData[0] + "`.`model_data` WHERE line = '" + MainForm.cbbLineName01.Text + "' AND model_name = '" + _data[i, 1] + "' AND customer_eo_number = '" + txtCustomerEO.Text + "' AND mobis_eo_number = '" + txtMobisEO.Text + "'";
                    }
                    else
                    {
                        query = "SELECT * FROM `" + MainForm.settingData[0] + "`.`model_data` WHERE line = '" + MainForm.cbbLineName01.Text + "' AND model_name = '" + _data[i, 1] + "' AND eo_contents = '" + txtEOContents.Text + "'";
                    }
                    string[,] resultData = MainForm.mariaDB.SelectQuery2(query);

                    // 품번만 무조건 입력
                    gridViewData[i, 1] = _data[i, 1];

                    if (resultData.GetLength(0) > 0)
                    {
                        for (int j = 2; j < gridViewData.GetLength(1); j++)
                        {
                            gridViewData[i, j] = resultData[0, j];
                        }
                    }
                    else
                    {
                        for (int j = 2; j < gridViewData.GetLength(1); j++)
                        {
                            gridViewData[i, j] = "-";
                        }
                    }
                }

                dgvDataAdd(gridViewData);

                lblDelayTimeView.Text = sw.ElapsedMilliseconds + "ms 지연";

                MainForm.mariaDB.InsertLogDB(txtPCB.Text + " / " + txtCustomerEO.Text + " / " + txtMobisEO.Text + " / " + txtEOContents.Text + " / " + MainForm.userNameData + " 검색, " + sw.ElapsedMilliseconds + "ms", false);
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
            modelData = _data;
            InitializeComponent();
        }

        private void EOCheckForm_M_Load(object sender, EventArgs e)
        {
            Text = MainForm.programName + " - " + MainForm.dc.Version();

            eoTypeData = modelData[15];

            if (eoTypeData == "MAIN PCB")
            {
                txtPCB.Text = modelData[13];
                eoTypeData = "main_pcb_name";
            }
            else if (eoTypeData == "SUB PCB")
            {
                txtPCB.Text = modelData[14];
                eoTypeData = "sub_pcb_name";
            }
            else
            {
                MainForm.dc.Msg("경고", "EO 구분이 메인 PCB 또는 서브 PCB만 조회할 수 있습니다");
                Close();
                return;
            }

            txtCustomerEO.Text = modelData[3];
            txtMobisEO.Text = modelData[4];
            txtEOContents.Text = modelData[5];

            string query = "SELECT * FROM `" + MainForm.settingData[0] + "`.`model_data` WHERE line = '" + MainForm.cbbLineName01.Text + "' AND " + eoTypeData + " = '" + txtPCB.Text + "' GROUP BY model_name ORDER BY model_name";
            string[,] resultData = MainForm.mariaDB.SelectQuery2(query);

            DataSearchInput(resultData);

            //dgvDataAdd(resultData);

            /*
            MainForm.dc.LogFileSave("1-1. " + query);

            for (int i = 0; i < selectData.GetLength(0); i++)
            {
                selectData[i, 13] = "-";
            }

            MainForm.dc.LogFileSave("1-2. " + shipmentData + " 출하지 외 등록된 설변태그 조회");
            MainForm.dc.ArrayLogFileSave(selectData, true);

            // 
            for (int i = 0; i < selectData.GetLength(0); i++)
            {
                query = "SELECT * FROM `" + MainForm.settingData[0] + "`.`model_data` WHERE model_name = '" + selectData[i, 1] + "' ";
                query += "AND customer_eo_number = '" + selectData[i, 3] + "' ";
                query += "AND mobis_eo_number = '" + selectData[i, 4] + "' ";
                query += "AND eo_contents = '" + selectData[i, 5] + "' ";
                //query += "AND NOT eo_contents = '' ";
                query += "AND shipment = '" + shipmentData + "' ";
                query += "ORDER BY start_date";

                string[,] selectData2 = MainForm.mariaDB.SelectQuery2(query);

                MainForm.dc.LogFileSave("2-1. " + query);

                for (int q = 0; q < selectData2.GetLength(0); q++)
                {
                    selectData2[q, 13] = "-";
                }

                if (selectData2.GetLength(0) > 0)
                {
                    MainForm.dc.LogFileSave("2-2. " + shipmentData + " 출하지 외 등록된 설변태그");
                    MainForm.dc.ArrayLogFileSave(selectData2, true);
                }

                //MessageBox.Show("TEST");
            }

            /*
            for (int i = 0; i < modelData.GetLength(0); i++)
            {
                string query = "SELECT * FROM `" + MainForm.settingData[0] + "`.`model_data` WHERE model_name = '" + modelData[i, 1] + "' ";
                string[,] selectData;

                query += "AND customer_eo_number = '" + modelData[i, 3] + "' ";
                query += "AND mobis_eo_number = '" + modelData[i, 4] + "' ";
                query += "AND eo_contents = '" + modelData[i, 5] + "' ";
                //query += "AND NOT eo_contents = '' ";
                query += "AND shipment = '" + shipmentData + "' ";
                query += "ORDER BY start_date";

                selectData = MainForm.mariaDB.SelectQuery2(query);

                if (selectData.GetLength(0) > 0)
                {
                    for (int j = 0; j < modelData.GetLength(1); j++)
                    {
                        totalData[count, j] = modelData[i, j];
                    }

                    count++;
                }

                totalData = MainForm.dc.TwoDimensionResize(totalData, count, totalData.GetLength(1));
            }

            /*
            string query = "SELECT * FROM  `" + MainForm.settingData[0] + "`.`shipment_history_data` WHERE model_name = '" + modelData[1] + "'";
            query += "ORDER BY start_date";
            totalData = MainForm.mariaDB.SelectQuery2(query);

            if (totalData.GetLength(0) <= 0)
            {
                MainForm.Guna2Msg("에러", "등록된 데이터가 없습니다");
                Close();
            }

            dgvDataAdd(totalData);
            */
        }

        private void dgvSelect_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MainForm.DGVMHTDataView(dgvSelect, e, this);
        }

        private void cbbShipment_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 메인 출하지 설변태그 조회
            //string query = "SELECT * FROM `" + MainForm.settingData[0] + "`.`model_data` WHERE model_name = '" + modelData + "' AND end_date > '" + DateTime.Now.ToString("yyyy") + "-01-01' AND shipment = '" + shipmentData + "'";
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
            modelDataInsert = new string[checkData.GetLength(0), 17];
            count = 0;

            // 불러온 데이터 검사
            for (int i = 0; i < modelData.GetLength(0); i++)
            {
                for (int j = 0; j < checkData.GetLength(0); j++)
                {
                    if (modelData[i, 3] == checkData[j, 0] &&   // 고객사 EO
                        modelData[i, 4] == checkData[j, 1] &&   // 모비스 EO
                        modelData[i, 5] == checkData[j, 2])     // EO 내용
                    {
                        for (int k = 0; k < 17; k++)
                        {
                            modelDataInsert[count, k] = modelData[i, k];
                        }

                        count++;
                    }
                }
                //count++;
            }

            // 값 추가
            for (int i = 0; i < modelDataInsert.GetLength(0); i++)
            {
                string query = string.Empty;
                string[] insertData = new string[17];

                for (int j = 0; j < insertData.Length; j++)
                {
                    insertData[j] = modelDataInsert[i, j];
                }

                query = MainForm.dc.InsertQueryArrayConvert(MainForm.settingData[0], MainForm.settingData[1], insertData);

                MainForm.mariaDB.EtcQuery(query);
            }
            */
        }

        private void EOCheckForm_M_Resize(object sender, EventArgs e)
        {
            dgvSelect.Size = new Size(Width - 22, Height - 247 - 11);
        }

        private void lblFormTitle_DoubleClick(object sender, EventArgs e)
        {
            guna2ControlBox2.PerformClick();
        }
    }
}
