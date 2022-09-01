using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EOM_v3_M
{
    public partial class EOCheckForm_M : MetroFramework.Forms.MetroForm
    {
        string modelData, shipmentData;
        string[,] modelDataInsert;

        private void dgvDataAdd(string[,] _data)
        {
            dgvSelect.Rows.Clear();

            for (int i = 0; i < _data.GetLength(0); i++)
            {
                // *.mht
                string eoMhtData = string.Empty;

                if (!_data[i, 13].Equals(string.Empty)) eoMhtData = "보기";

                dgvSelect.Rows.Add(
                    false,
                    _data[i, 1],                                                            // -품번
                    _data[i, 2],                                                            // 차종
                    _data[i, 3],                                                            // 고객사 EO
                    _data[i, 4],                                                            // 모비스 EO
                    _data[i, 5],                                                            // EO 내용
                    _data[i, 7],                                                            // 스티커 내용
                    Convert.ToDateTime(_data[i, 8]).ToString("yyyy-MM-dd HH:mm:ss"),        // 적용일
                    Convert.ToDateTime(_data[i, 9]).ToString("yyyy-MM-dd HH:mm:ss"),        // 종료일
                    _data[i, 10],                                                           // 등록자
                    _data[i, 11],                                                           // 타입
                    _data[i, 12],                                                           // 출하지
                    eoMhtData,                                                              // mht
                    _data[i, 14],                                                           // MAIN PCB
                    _data[i, 15],                                                           // SUB PCB
                    _data[i, 16]                                                            // EO 구분
                );
            }
        }

        public EOCheckForm_M()
        {
            InitializeComponent();
        }

        public EOCheckForm_M(string _data)
        {
            

            modelData = _data;
            InitializeComponent();
        }

        /// <summary>
        /// 품번, 출하지
        /// </summary>
        /// <param name="_data1"></param>
        /// <param name="_data2"></param>
        public EOCheckForm_M(string _data1, string _data2)
        {
            modelData = _data1;
            shipmentData = _data2;
            InitializeComponent();
        }

        private void EOCheckForm_M_Load(object sender, EventArgs e)
        {
            //int count = 0;
            //string[,] totalData = new string[1000, modelData.GetLength(1)];

            Text = MainForm.programName + " - " + MainForm.dc.Version();

            txtModelName.Text = modelData;

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
                MainForm.dc.Msg("에러", "등록된 데이터가 없습니다");
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
            string query = "SELECT * FROM `" + MainForm.settingData[0] + "`.`model_data` WHERE model_name = '" + modelData + "' AND end_date > '" + DateTime.Now.ToString("yyyy") + "-01-01' AND shipment = '" + shipmentData + "'";
            string[,] selectData = MainForm.mariaDB.SelectQuery2(query);
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
    }
}
