using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace EOM_v3_M
{
    public partial class EOForm : MetroFramework.Forms.MetroForm
    {
        public static DataGridView datagridview01;

        ToolTip toolTip = new ToolTip();

        private string[] DatabaseColumnData(int _data1, string _data2)
        {
            string[] returnData;

            switch (_data1)
            {
                case 1:
                    // _data2 : string.Empty
                    returnData = new string[]
                    {
                        "line", MainForm.cbbMetroLine01.Text,
                        "car_name", dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString(),
                        "customer_eo_number", dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString(),
                        "mobis_eo_number", dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value.ToString(),
                        "eo_contents", dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value.ToString(),
                        "registrant", dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[7].Value.ToString(),
                        "print_type", dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[8].Value.ToString(),
                        "shipment", dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[9].Value.ToString(),
                        "eo_type", dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[11].Value.ToString(),
                    };
                    break;
                case 2:
                    // _data2 : mobisNumber
                    string customerNumber = txtCustomerEO.Text.Equals("-") || txtCustomerEO.Text.Equals(string.Empty) ? "-" : txtCustomerEO.Text;
                    string stickerColorData = string.Empty;
                    string stickerTextData = string.Empty;
                    string printType = rdoFirst.Checked ? "초도품" : "샘플";
                    string eoTypeData = string.Empty;

                    if (!rdoNone.Checked)
                    {
                        if (rdoOrange.Checked)
                        {
                            stickerColorData = "Orange";
                            //stickerTextData = "주황";
                        }
                        else if (rdoBlue.Checked)
                        {
                            stickerColorData = "Blue";
                            //stickerTextData = "파랑";
                        }
                        else if (rdoLimeGreen.Checked)
                        {
                            stickerColorData = "LimeGreen";
                            //stickerTextData = "초록";
                        }
                        else if (rdoWhite.Checked)
                        {
                            stickerColorData = "White";
                            stickerTextData = "흰색";
                        }
                        else if (rdoYellow.Checked)
                        {
                            stickerColorData = "Yellow";
                            stickerTextData = comboBox1.SelectedIndex != -1 ? stickerTextData = comboBox1.SelectedItem.ToString() : string.Empty;
                        }
                    }
                    else
                    {
                        //stickerColorData = "ScrollBar";
                    }

                    /*
                    // 2020.06.22
                    // D-오디오 수삽
                    if (MainForm.cbbMetroLine01.Text.Equals(MainForm.lineData[1]))
                    {
                        printType = "초도품";
                    }
                    */

                    // 2021.01.21
                    // EO 구분
                    if (groupBox3.Enabled)
                    {
                        if (rdoMain.Checked)
                        {
                            eoTypeData = rdoMain.Text;
                        }
                        else if (rdoSub.Checked)
                        {
                            eoTypeData = rdoSub.Text;
                        }
                        else if (rdoException.Checked)
                        {
                            eoTypeData = rdoException.Text;
                        }
                        else
                        {
                            MainForm.dc.Msg("경고", "'EO 구분' 체크 오류 발생", true);
                            Close();
                        }
                    }
                    else
                    {
                        eoTypeData = "-";
                    }

                    returnData = new string[]
                    {
                        MainForm.cbbMetroLine01.Text,
                        txtCarName.Text,
                        customerNumber,
                        _data2,
                        MainForm.BackSlashConvert(txtEOContents.Text),
                        stickerColorData,
                        stickerTextData,
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        MainForm.SearchRegistrant(NetworkInterface.GetAllNetworkInterfaces()[0].GetPhysicalAddress().ToString()),
                        printType,
                        //ShipmentData(),
                        "없음",
                        MainForm.mhtData,
                        eoTypeData
                    };
                    break;
                case 3:
                    // _data2 : i
                    returnData = new string[]
                    {
                        "line",
                        MainForm.cbbMetroLine01.Text,
                        "car_name",
                        dataGridView1.Rows[Convert.ToInt32(_data2)].Cells[1].Value.ToString(),
                        "customer_eo_number",
                        dataGridView1.Rows[Convert.ToInt32(_data2)].Cells[2].Value.ToString(),
                        "mobis_eo_number",
                        dataGridView1.Rows[Convert.ToInt32(_data2)].Cells[3].Value.ToString(),
                        "eo_contents",
                        dataGridView1.Rows[Convert.ToInt32(_data2)].Cells[4].Value.ToString(),
                        "date",
                        dataGridView1.Rows[Convert.ToInt32(_data2)].Cells[6].Value.ToString(),
                        // 보여지는 것과 데이터가 틀림
                        // "registrant",
                        // dataGridView1.Rows[Convert.ToInt32(_data2)].Cells[7].Value.ToString(),
                        // "print_type",
                        // dataGridView1.Rows[Convert.ToInt32(_data2)].Cells[8].Value.ToString()
                        "shipment",
                        dataGridView1.Rows[Convert.ToInt32(_data2)].Cells[9].Value.ToString(),
                    };
                    break;
                case 4:
                    // _data2 : stickerColor
                    returnData = new string[]
                    {
                        "line", MainForm.cbbMetroLine01.Text,
                        "car_name", dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString(),
                        "customer_eo_number", dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString(),
                        "mobis_eo_number", dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value.ToString(),
                        "eo_contents", dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value.ToString(),
                        "sticker_color", _data2,
                        "registrant", dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[7].Value.ToString(),
                        "print_type", dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[8].Value.ToString(),
                        "shipment", dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[9].Value.ToString(),
                    };
                    break;
                default:
                    returnData = new string[]
                    {
                        ""
                    };
                    break;
            }

            return returnData;
        }

        private string ShipmentData()
        {
            if (MainForm.cbbMetroLine01.Text.Equals(MainForm.lineData[1]) || MainForm.cbbMetroLine01.Text.Equals(MainForm.lineData[3]))
            {
                return "-";
            }

            return rdoOEM.Checked ? "OEM" : rdoCKD.Checked ? "CKD" : "KD";
        }

        public static void ModelSelect(string _data, int _type)
        {
            try
            {
                // dataGridView 초기화
                datagridview01.Rows.Clear();

                // 쿼리 변수 선언
                string query = string.Empty;

                // 차종 조회
                if (_type == 1)
                {
                    string[] selectData = new string[] { "line", MainForm.cbbMetroLine01.Text, "car_name", _data };
                    query = MainForm.dc.SelectDeleteQueryANDConvert(MainForm.settingData[0], MainForm.settingData[2], selectData, "SELECT");
                }
                // 고객사 EO 조회
                else if (_type == 2)
                {
                    string[] selectData = new string[] { "line", MainForm.cbbMetroLine01.Text, "customer_eo_number", _data };
                    query = MainForm.dc.SelectDeleteQueryANDConvert(MainForm.settingData[0], MainForm.settingData[2], selectData, "SELECT");
                }
                // 모비스 EO 조회
                else if (_type == 3)
                {
                    string[] selectData = new string[] { "line", MainForm.cbbMetroLine01.Text, "mobis_eo_number", _data };
                    query = MainForm.dc.SelectDeleteQueryANDConvert(MainForm.settingData[0], MainForm.settingData[2], selectData, "SELECT");
                }
                // EO 내용 조회
                else if (_type == 4)
                {
                    string[] selectData = new string[] { "line", MainForm.cbbMetroLine01.Text, "eo_contents", _data };
                    query = MainForm.dc.SelectDeleteQueryANDConvert(MainForm.settingData[0], MainForm.settingData[2], selectData, "SELECT");
                }
                // 2020.12.18
                // MAIN PCB 조회
                else if (_type == 5)
                {
                    string[] selectData = new string[] { "line", MainForm.cbbMetroLine01.Text, "main_pcb_name", _data };
                    query = MainForm.dc.SelectDeleteQueryANDConvert(MainForm.settingData[0], MainForm.settingData[2], selectData, "SELECT");
                }
                // 2020.12.18
                // SUB PCB 조회
                else if (_type == 6)
                {
                    string[] selectData = new string[] { "line", MainForm.cbbMetroLine01.Text, "sub_pcb_name", _data };
                    query = MainForm.dc.SelectDeleteQueryANDConvert(MainForm.settingData[0], MainForm.settingData[2], selectData, "SELECT");
                }
                else
                {
                    MainForm.dc.Msg("경고", "치명적인 오류");
                    return;
                }

                query += " ORDER BY date";

                string[,] eoData = MainForm.mariaDB.SelectQuery2(query);

                DGVViewDataUpdate(eoData);
            }
            catch (Exception ex)
            {
                MainForm.dc.LogFileSave(ex.Message);
            }
        }

        private static void DGVViewDataUpdate(string[,] _data)
        {
            // datagridview 초기화
            datagridview01.Rows.Clear();

            string registrantData = string.Empty;

            for (int i = 0; i < _data.GetLength(0); i++)
            {
                string eoMhtData = string.Empty;

                if (!_data[i, 11].Equals(string.Empty))
                {
                    eoMhtData = "보기";
                }

                // 등록자 불러오기
                registrantData = MainForm.SearchRegistrant(_data[i, 8]);
                
                datagridview01.Rows.Add(
                    false,                                                                 // 체크
                    _data[i, 1],                                                           // 차종
                    _data[i, 2],                                                           // 고객사 EO
                    _data[i, 3],                                                           // 모비스 EO
                    _data[i, 4],                                                           // EO 내용
                    _data[i, 6],                                                           // 스티커 내용
                    Convert.ToDateTime(_data[i, 7]).ToString("yyyy-MM-dd HH:mm:ss"),       // 등록일
                    registrantData,                                                        // 등록자
                    _data[i, 9],                                                           // 타입
                    _data[i, 10],                                                          // 출하지
                    eoMhtData,                                                             // *.mht
                    _data[i, 12]
                );

                switch (_data[i, 5])
                {
                    case "Orange":
                        datagridview01.Rows[i].Cells[5].Style.BackColor = Color.Orange;
                        break;
                    case "Blue":
                        datagridview01.Rows[i].Cells[5].Style.BackColor = Color.Blue;
                        break;
                    case "LimeGreen":
                        datagridview01.Rows[i].Cells[5].Style.BackColor = Color.LimeGreen;
                        break;
                    case "White":
                        datagridview01.Rows[i].Cells[5].Style.BackColor = Color.White;
                        break;
                    case "Yellow":
                        datagridview01.Rows[i].Cells[5].Style.BackColor = Color.Yellow;
                        break;
                    default:
                        datagridview01.Rows[i].Cells[5].Style.BackColor = SystemColors.ScrollBar;
                        datagridview01.Rows[i].Cells[5].Value = "없음";
                        break;
                }

            }

            // 등록일 정렬
            //datagridview01.Sort(datagridview01.Columns[5], ListSortDirection.Ascending);
            // 2022.01.11
            // 출하지 관리 안함
            //MainForm.ShipmentBackColor(datagridview01, 9);
        }

        private void EOReloadView(string _data)
        {
            string query;
            string[] selectData;
            string[,] eoData = new string[0, 0];

            if (_data != null)
            {
                string[] y = { string.Empty };

                query = "SELECT * FROM `" + MainForm.settingData[0] + "`.`" + MainForm.settingData[2] + "` WHERE line = '" + MainForm.cbbMetroLine01.Text + "' AND car_name LIKE '" + _data + "%'";

                // 2021.04.12
                // 조회 기간 추가
                if (radioButton4.Checked)
                {
                    y = radioButton4.Text.Split('년');
                }
                else if (radioButton5.Checked)
                {
                    y = radioButton5.Text.Split('년');
                }
                else if (radioButton6.Checked)
                {
                    y = radioButton6.Text.Split('년');
                }

                if (y[0] != string.Empty)
                {
                    query += " AND date >= '" + y[0] + "-01-01 00:00:00' AND date < '" + y[0] + "-12-31 23:59:59'";
                }
            }
            else
            {
                selectData = new string[] { "line", MainForm.cbbMetroLine01.Text };

                query = MainForm.dc.SelectDeleteQueryANDConvert(MainForm.settingData[0], MainForm.settingData[2], selectData, "SELECT");
            }

            query += " ORDER BY date";

            eoData = MainForm.mariaDB.SelectQuery2(query);

            // SUB 고객사 EO FALSE
            if (MainForm.cbbMetroLine01.Text.Equals(MainForm.lineData[3]))
            {
                txtCustomerEO.Enabled = false;
                txtCustomerEO.Text = "-";
            }

            DGVViewDataUpdate(eoData);

            /*
            // datagridview 초기화
            dataGridView1.Rows.Clear();

            string registrantData = string.Empty;

            for (int i = 0; i < eoData.GetLength(0); i++)
            {
                string eoMhtData = string.Empty;

                if (!eoData[i, 11].Equals(string.Empty))
                {
                    eoMhtData = "보기";
                }

                // 등록자 불러오기
                registrantData = MainForm.SearchRegistrant(eoData[i, 8]);

                dataGridView1.Rows.Add(
                    false,                                                                  // 체크
                    eoData[i, 1],                                                           // 차종
                    eoData[i, 2],                                                           // 고객사 EO
                    eoData[i, 3],                                                           // 모비스 EO
                    eoData[i, 4],                                                           // EO 내용
                    eoData[i, 6],                                                           // 스티커 내용
                    Convert.ToDateTime(eoData[i, 7]).ToString("yyyy-MM-dd HH:mm:ss"),       // 등록일
                    registrantData,                                                         // 등록자
                    eoData[i, 9],                                                           // 타입
                    eoData[i, 10],                                                          // 출하지
                    eoMhtData,                                                              // *.mht
                    eoData[i, 12]
                );

                switch (eoData[i, 5])
                {
                    case "Orange":
                        dataGridView1.Rows[i].Cells[5].Style.BackColor = Color.Orange;
                        break;
                    case "Blue":
                        dataGridView1.Rows[i].Cells[5].Style.BackColor = Color.Blue;
                        break;
                    case "LimeGreen":
                        dataGridView1.Rows[i].Cells[5].Style.BackColor = Color.LimeGreen;
                        break;
                    case "White":
                        dataGridView1.Rows[i].Cells[5].Style.BackColor = Color.White;
                        break;
                    case "Yellow":
                        dataGridView1.Rows[i].Cells[5].Style.BackColor = Color.Yellow;
                        break;
                    default:
                        dataGridView1.Rows[i].Cells[5].Style.BackColor = SystemColors.ScrollBar;
                        dataGridView1.Rows[i].Cells[5].Value = "없음";
                        break;
                }

            }

            // 등록일 정렬
            dataGridView1.Sort(dataGridView1.Columns[5], ListSortDirection.Ascending);
            //MainForm.SetDoNotSort(dataGridView1);
            MainForm.ShipmentBackColor(dataGridView1, 9);
            */
        }

        private bool EODuplicationCheck(string _data, string _column)
        {
            string[] selectData = new string[] { "line", MainForm.cbbMetroLine01.Text, "car_name", txtCarName.Text };
            string query = MainForm.dc.SelectDeleteQueryANDConvert(MainForm.settingData[0], MainForm.settingData[2], selectData, "SELECT");
            string[] columnData = MainForm.mariaDB.SelectQueryCount(query, _column);

            for (int i = 0; i < columnData.Length; i++)
            {
                // 조건 1. '-' 하이픈이 아니고
                // 조건 2. 중복된 이름이 있다면
                if (!columnData[i].Equals("-") && columnData[i].Equals(_data))
                {
                    return true;
                }
            }

            return false;
        }

        private bool EONumberShipmentCheck(string _data)
        {
            string shipmentData = string.Empty;

            if (rdoOEM.Checked)
                shipmentData = "OEM";
            else if (rdoCKD.Checked)
                shipmentData = "CKD";
            else if (rdoKD.Checked)
                shipmentData = "KD";
            else
                shipmentData = "-";

            string[] selectData;

            if (_data.Equals("-") || _data.Equals("4M"))
            {
                selectData = new string[] { "line", MainForm.cbbMetroLine01.Text, "car_name", txtCarName.Text, "mobis_eo_number", _data, "shipment", shipmentData, "eo_contents", txtEOContents.Text };
            }
            else
            {
                selectData = new string[] { "line", MainForm.cbbMetroLine01.Text, "car_name", txtCarName.Text, "mobis_eo_number", _data, "shipment", shipmentData };
            }

            string query = MainForm.dc.SelectDeleteQueryANDConvert(MainForm.settingData[0], MainForm.settingData[2], selectData, "SELECT");
            string[,] columnData = MainForm.mariaDB.SelectQuery2(query);

            if (columnData.GetLength(0) > 0)
                return true;
            else
                return false;
        }
    
        private bool EONumberContentsCheck(string _data)
        {
            string[] tmpData = new string[] { "4M", "-", "" };

            for(int i = 0; i < tmpData.Length; i++)
            {
                if(_data.Equals(tmpData[i]))
                {
                    return true;
                }
            }

            return false;
        }

        private bool FirstCharCheck(char _data)
        {
            char[] value = { 'H', 'K', 'R', 'F' }; 

            // 2020.09.21
            // 고객사 EO 앞 글자 인터락 추가
            foreach(char c in value)
            {
                if (c == _data)
                {
                    return true;
                }
            }

            return false;
        }

        public EOForm()
        {
            InitializeComponent();
        }

        private void EOForm_Load(object sender, EventArgs e)
        {
            Text = MainForm.programName + " - " + MainForm.dc.Version();

            datagridview01 = dataGridView1;
            datagridview01.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            datagridview01.Columns[4].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            MainForm.dc.DataGridViewDoubleBuffered(dataGridView1, true);

            comboBox1.Enabled = false;

            toolTip.SetToolTip(txtCarName, "차종은 예로 AE, AEa, AE HEV, AE HEV 명확히 구분해야 합니다");

            // 2021.04.12
            // 조회 기간 추가
            radioButton4.Text = DateTime.Now.Year + "년";
            radioButton5.Text = (DateTime.Now.Year - 1) + "년";
            radioButton6.Text = (DateTime.Now.Year - 2) + "년";

            /*
            // D-오디오 수삽
            if (MainForm.cbbMetroLine01.Text.Equals(MainForm.lineData[1]))
            {
                rdoFirst.Enabled = false;
                rdoSample.Enabled = false;

                rdoFirst.Checked = false;
                rdoSample.Checked = false;
            }
            */

            // D-오디오 수삽, D-오디오 SUB
            if (MainForm.cbbMetroLine01.Text.Equals(MainForm.lineData[1]) || MainForm.cbbMetroLine01.Text.Equals(MainForm.lineData[3]))
            {
                // 출하지
                rdoOEM.Enabled = false;
                rdoCKD.Enabled = false;
                rdoKD.Enabled = false;
                rdoOEM.Checked = false;
                rdoCKD.Checked = false;
                rdoKD.Checked = false;

                // EO 구분
                groupBox3.Enabled = false;
            }
            // 클러스터, 허드
            else if (MainForm.cbbMetroLine01.Text.Equals(MainForm.lineData[4]) || MainForm.cbbMetroLine01.Text.Equals(MainForm.lineData[5]))
            {
                // 2021.03.08
                // 판넬 활성화하고 이름은 변경
                //rdoPanel.Enabled = false;
                //rdoException.Text = "SAMPLE";
            }

            // 2022.01.11
            // 출하지 비활성화
            groupBox2.Enabled = false;

            // 식별 스티커
            // D-오디오 조립
            if (MainForm.cbbMetroLine01.Text.Equals(MainForm.lineData[2]))
            {
                //groupBox4.Enabled = true;
            }
            else
            {
                //groupBox4.Enabled = false;
            }

            EOReloadView(MainForm.eoCarData);

            TopMost = true;
            TopMost = false;
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // 예외 설정
            if (dataGridView1.Rows.Count > 0 && dataGridView1.CurrentCell.ColumnIndex != 0)
            {
                if (MainForm.carSelectData.Equals(string.Empty) || MainForm.carSelectData == null)
                {
                    MainForm.mariaDB.InsertLogDB("모비스 EO 창에서 DataGridView1 잘못 선택하여 오류 발생", false);
                }
                else if (!MainForm.carSelectData.Equals(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString()))
                // 2020.04.27
                // 2020.04.09 차종 앞자리만 체크해서 인터락 해제했다가 EO 번호 불량 유출
                //else if (!MainForm.carSelectData.Substring(0, 2).Equals(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString().Substring(0, 2)))
                {
                    MainForm.dc.Msg("경고", "다른 차종은 선택할 수 없습니다");
                    return;
                }

                for (int i = 0; i < MainForm.eoSelectData.Length; i++)
                {
                    MainForm.eoSelectData[i] = string.Empty;
                }

                // 2020.05.06
                // mht 데이터 저장
                string[] selectData = DatabaseColumnData(1, string.Empty);
                string query = MainForm.dc.SelectDeleteQueryANDConvert(MainForm.settingData[0], MainForm.settingData[2], selectData, "SELECT");
                string[] columnData = MainForm.mariaDB.SelectQueryCount(query, "mht_data");

                // 2021.02.10
                // mht_data 없을 경우
                if (columnData.Length == 0)
                {
                    columnData = new string[] { string.Empty };
                }

                // 2021.02.24
                // 없음 제외
                string stickerColor = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Style.BackColor.Name.ToString();
                string stickerText = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Value.ToString();

                if (stickerColor == "ScrollBar")
                {
                    stickerColor = string.Empty;
                }
                if (stickerText == "없음")
                {
                    stickerText = string.Empty;
                }

                MainForm.eoSelectData[0] = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[8].Value.ToString();                    // 타입
                MainForm.eoSelectData[1] = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString();                    // 고객사 EO
                MainForm.eoSelectData[2] = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value.ToString();                    // 모비스 EO
                MainForm.eoSelectData[3] = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value.ToString();                    // EO 내용
                MainForm.eoSelectData[4] = stickerColor;                                                                                    // 스티커 색상
                MainForm.eoSelectData[5] = stickerText;                                                                                     // 차수
                MainForm.eoSelectData[6] = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[9].Value.ToString();                    // 출하지
                MainForm.eoSelectData[7] = columnData[0];                                                                                   // *.mht
                MainForm.eoSelectData[8] = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[11].Value.ToString();                   // EO 구분

                //MainForm.dc.DatFileSave("eoSelectData.log", MainForm.eoSelectData);

                MainForm.carSelectData = string.Empty;                                                                                      // 초기화
                Close();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                txtMobisEO2.Enabled = true;
            }
            else
            {
                txtMobisEO2.Enabled = false;
                txtMobisEO2.Text = string.Empty;
            }
        }

        private void txtCarName_KeyUp(object sender, KeyEventArgs e)
        {
            MainForm.dc.CarNameUpperConvert(txtCarName);
        }

        private void txtCarName_TextChanged(object sender, EventArgs e)
        {
            // 2021.09.04
            MainForm.dc.NumAlphaSpaceString(txtCarName);
        }

        private void rdoFirst_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoFirst.Checked)
            {
                txtCustomerEO.Text = string.Empty;
                txtCustomerEO.ReadOnly = false;
                checkBox1.Enabled = true;
                rdoMain.Checked = true;
                rdoMain.Enabled = true;
                rdoSub.Enabled = true;
                rdoException.Enabled = true;

                if (!txtCustomerEO.Enabled)
                {
                    txtCustomerEO.Text = "-";
                }

                txtCarName.Focus();
            }
        }

        private void rdoSample_CheckedChanged(object sender, EventArgs e)
        {
            if(rdoSample.Checked)
            {
                txtCustomerEO.Text = "-";
                txtMobisEO1.Text = string.Empty;
                txtCustomerEO.ReadOnly = true;
                checkBox1.Enabled = false;
                rdoMain.Enabled = false;
                rdoSub.Enabled = false;
                rdoException.Enabled = false;
                rdoException.Checked = true;

                txtCarName.Focus();
            }
        }

        // 추가
        private void AddMetroBtn_Click(object sender, EventArgs e)
        {
            string query, mobisNumber = string.Empty;
            //string shipmentType = "-";

            // 2020.04.28
            // *.mht 인터락
            if (!checkBox2.Checked && MainForm.mhtData.Equals(string.Empty))
            {
                MainForm.dc.Msg("경고", "'작업변경지시서' 첨부해야 합니다");
                return;
            }

            // 차종 인터락
            if (txtCarName.Text.Length < 2)
            {
                MainForm.dc.Msg("경고", "차종을 입력해주세요 [2글자 이상]");
                return;
            }

            // 2020.05.06
            // 차종 연계 인터락
            // 2022.11.15
            // 차종 프로젝트명 조건 수정
            int v = 0;
            string carProjectName = (txtCarName.Text.Length > 2 && int.TryParse(txtCarName.Text.Substring(2, 1), out v)) ? txtCarName.Text.Substring(0, 3).Replace(" ", "") : txtCarName.Text.Substring(0, 2).Replace(" ", "");

            WebBrowser webBrowser = new WebBrowser();
            webBrowser.Navigate(txtMhtFile.Text);

            //MainForm.dc.LogFileSave(webBrowser.DocumentText);

            //MessageBox.Show("차종 [" + carProjectName + "]");
            //MessageBox.Show(!webBrowser.DocumentText.Contains(carProjectName) + "");
            //MessageBox.Show(!MainForm.mhtData.Contains(" " + carProjectName) + "");
            //MessageBox.Show(!MainForm.mhtData.Contains("/" + carProjectName) + "");
            //MessageBox.Show(!MainForm.mhtData.Contains("]" + carProjectName) + "");

            if (!checkBox2.Checked && 
                !webBrowser.DocumentText.Contains(carProjectName)           // 'US4'  <- 2022.11.15 추가
                //!MainForm.mhtData.Contains(" " + carProjectName) &&       // ' US4'
                //!MainForm.mhtData.Contains("/" + carProjectName) &&       // '/US4'
                //!MainForm.mhtData.Contains("]" + carProjectName)          // '[US4' <- 2021.05.11 추가
               )
            {
                //Clipboard.SetText(MainForm.mhtData);

                MainForm.dc.Msg("경고", "작업변경지시서 차종과 입력한 차종이 일치 하지 않습니다");
                txtCarName.SelectAll();
                return;
            }

            // 2022.03.16
            if (!rdoFirst.Checked && !rdoSample.Checked)
            {
                MainForm.dc.Msg("경고", "'초도품' 또는 '샘플'을 선택해야 합니다");
                return;
            }

            // 출하지
            // shipmentType = ShipmentData();
            // 2022.01.11
            // 삭제 EO는 출하지 관리 안함
            //shipmentType = "-";

            //// 고객사 EO 인터락
            // 1자리, - 아니거나
            // 2~7자리라면
            if (!EONumberContentsCheck(txtCustomerEO.Text) && txtCustomerEO.Text.Length != 8)
            {
                MainForm.dc.Msg("경고", "고객사 EO 번호는 8자리 입니다");
                return;
            }
            else if (MainForm.IsSpaceCheck(txtCustomerEO.Text))
            {
                MainForm.dc.Msg("경고", "고객사 EO 번호에 띄어쓰기가 있습니다");
                return;
            }
            //else if (txtCustomerEO.Text.Length == 8 && !(txtCustomerEO.Text.Substring(0, 1).Equals("K") || txtCustomerEO.Text.Substring(0, 1).Equals("H")))
            else if (txtCustomerEO.Text.Length == 8 && !FirstCharCheck(Convert.ToChar(txtCustomerEO.Text.Substring(0, 1))))
            {
                MainForm.dc.Msg("경고", "고객사 EO 번호의 첫 글자는 H, K, R로 시작해야합니다");
                return;
            }

            // 모비스 EO 인터락
            if (!checkBox1.Checked)
            {
                mobisNumber = txtMobisEO1.Text;

                // 모비스 EO 확인
                if (txtMobisEO1.Text.Equals(string.Empty))
                {
                    MainForm.dc.Msg("경고", "모비스 EO 번호를 입력해주세요 [1번 입력칸]");
                    return;
                }
                // 8자리 입력 확인
                else if (!EONumberContentsCheck(txtMobisEO1.Text) && txtMobisEO1.Text.Length != 8)
                {
                    MainForm.dc.Msg("경고", "모비스 EO 번호는 8자리 입니다 [1번 입력칸]");
                    return;
                }
                // 공백 확인
                else if (MainForm.IsSpaceCheck(txtMobisEO1.Text))
                {
                    MainForm.dc.Msg("경고", "모비스 EO 번호에 띄어쓰기가 있습니다");
                    return;
                }
                else if (EODuplicationCheck(txtCustomerEO.Text, "customer_eo_number"))
                {
                    MainForm.dc.Msg("경고", "고객사 EO 번호 : " + txtCustomerEO.Text + "\n\n이미 등록되어 있습니다");
                    return;
                }
                else if (EODuplicationCheck(txtMobisEO1.Text, "mobis_eo_number"))
                {
                    MainForm.dc.Msg("경고", "모비스 EO 번호 : " + mobisNumber + "\n\n이미 등록되어 있습니다");
                    return;
                }
                /*
                // 출하지 확인
                else if (EONumberShipmentCheck(txtMobisEO1.Text))
                {
                    if (!shipmentType.Equals(string.Empty))
                    {
                        // 2021.01.26
                        // "-" EO 내용 출력
                        if (mobisNumber == "-")
                        {
                            MainForm.dc.Msg("경고", "모비스 EO 번호 [" + mobisNumber + "](이)가 EO 내용 [" + txtEOContents.Text + "] (으)로 등록되어 있습니다");
                        }
                        else
                        {
                            MainForm.dc.Msg("경고", "모비스 EO 번호 [" + mobisNumber + "](이)가 출하지 [" + shipmentType + "](으)로 등록되어 있습니다");
                        }
                    }
                    // D-오디오 SUB
                    else
                    {
                        MainForm.dc.Msg("경고", "모비스 EO가 등록되어 있습니다");
                    }
                    return;
                }
                */
                else if (EODuplicationCheck(txtEOContents.Text, "eo_contents") && (txtMobisEO1.Text.Equals("-") || txtMobisEO1.Text.Equals("4M")))
                {
                    MainForm.dc.Msg("경고", "모비스 EO 내용이 중복입니다");
                    return;
                }
            }
            // 모비스 EO - 2 체크
            else
            {
                mobisNumber = txtMobisEO1.Text + "/" + txtMobisEO2.Text;

                // 모비스 EO (1) 체크섬
                if (txtMobisEO1.Text.Equals(string.Empty))
                {
                    MainForm.dc.Msg("경고", "모비스 EO 번호를 입력해주세요 [1번 입력칸]");
                    return;
                }
                // 8자리 입력 확인
                else if (!EONumberContentsCheck(txtMobisEO1.Text) && txtMobisEO1.Text.Length != 8)
                {
                    MainForm.dc.Msg("경고", "모비스 EO 번호는 8자리 입니다 [1번 입력칸]");
                    return;
                }
                else if (MainForm.IsSpaceCheck(txtMobisEO1.Text))
                {
                    MainForm.dc.Msg("경고", "고객사 EO 번호에 띄어쓰기가 있습니다 [1번 입력칸]");
                    return;
                }

                // 모비스 EO (2) 체크섬
                if (txtMobisEO2.Text.Equals(string.Empty))
                {
                    MainForm.dc.Msg("경고", "모비스 EO 번호를 입력해주세요 [2번 입력칸]");
                    return;
                }
                // 8자리 입력 확인
                else if (txtMobisEO2.TextLength != 8)
                {
                    MainForm.dc.Msg("경고", "모비스 EO 번호는 8자리 입니다 [2번 입력칸]");
                    return;
                }
                else if (MainForm.IsSpaceCheck(txtMobisEO2.Text))
                {
                    MainForm.dc.Msg("경고", "모비스 EO 번호에 띄어쓰기가 있습니다 [2번 입력칸]");
                    return;
                }
                // 2개가 같은지 검사
                else if (txtMobisEO1.Text == txtMobisEO2.Text)
                {
                    MainForm.dc.Msg("경고", "모비스 EO 번호가 똑같습니다. [1번, 2번 입력칸]");
                    return;
                }
                else if (EODuplicationCheck(txtCustomerEO.Text, "customer_eo_number"))
                {
                    MainForm.dc.Msg("경고", "고객사 EO 번호 : " + txtCustomerEO.Text + "\n\n고객사 EO 번호가 이미 등록되어 있습니다");
                    return;
                }
                else if (EODuplicationCheck(mobisNumber, "mobis_eo_number"))
                {
                    MainForm.dc.Msg("경고", "모비스 EO 번호 : " + mobisNumber + "\n\n이미 등록되어 있습니다");
                    return;
                }
                /*
                else if (EONumberShipmentCheck(mobisNumber) && txtCustomerEO.Enabled)
                {
                    if (!shipmentType.Equals(string.Empty))
                    {
                        // 2021.01.26
                        // "-" EO 내용 출력
                        if (mobisNumber == "-")
                        {
                            MainForm.dc.Msg("경고", "모비스 EO 번호 [" + mobisNumber + "](이)가 EO 내용 [" + txtEOContents.Text + "] (으)로 등록되어 있습니다");
                        }
                        else
                        {
                            MainForm.dc.Msg("경고", "모비스 EO 번호 [" + mobisNumber + "](이)가 출하지 [" + shipmentType + "](으)로 등록되어 있습니다");
                        }
                    }
                    // D-오디오 SUB
                    else
                    {
                        MainForm.dc.Msg("경고", "모비스 EO가 등록되어 있습니다");
                    }

                    return;
                }
                // 두번째 EO 중복 검사
                else if (EODuplicationCheck(mobisNumber, "mobis_eo_number"))
                {
                    MainForm.dc.Msg("경고", "모비스 EO 번호가 중복입니다");
                    return;
                }
                */
                else if (EODuplicationCheck(txtEOContents.Text, "eo_contents") && (txtMobisEO1.Text.Equals("-") || txtMobisEO1.Text.Equals("4M")))
                {
                    MainForm.dc.Msg("경고", "모비스 EO 내용이 중복입니다");
                    return;
                }
            }

            // EO 내용 체크섬
            if (txtEOContents.Text.Equals(string.Empty))
            {
                MainForm.dc.Msg("경고", "EO 내용을 입력해주세요");
                return;
            }
            else if (txtEOContents.Lines.Length > 3)
            {
                MainForm.dc.Msg("경고", "EO 내용은 3줄 이하로 입력해주세요");
                return;
            }

            /*
             * EONumberShipmentCheck 항목때문에 안걸림
             * 앞에서 EO번호와 출하지로 검출하기 때문에 불필요함
            else if (EODuplicationCheck(txtEOContents.Text, "eo_contents"))
            {
                MainForm.dc.Msg("경고", "모비스 EO 내용이 중복입니다");
                return;
            }
            */

            string[] insertData = DatabaseColumnData(2, mobisNumber);
            query = MainForm.dc.InsertQueryArrayConvert(MainForm.settingData[0], MainForm.settingData[2], insertData);
            MainForm.mariaDB.EtcQuery(query);
            
            // 로그
            MainForm.dc.LogFileSave("' " + txtCarName.Text.Substring(0, 2) + "' 차종 *.mht 내용 검색 결과 -> TRUE");

            txtMhtFile.Text = string.Empty;
            txtCarName.Text = string.Empty;
            txtCustomerEO.Text = string.Empty;
            txtMobisEO1.Text = string.Empty;
            txtEOContents.Text = string.Empty;
            txtMobisEO2.Text = string.Empty;
            MainForm.mhtData = string.Empty;

            // 2020.04.27
            // 로그 데이터가 너무 길어 있음 유무만
            if (!insertData[11].Equals(string.Empty))
                insertData[11] = "O";
            else
                insertData[11] = "X";

            // 로그
            MainForm.mariaDB.InsertLogDB(MainForm.SplitConvert(insertData) + " EO 추가", false);

            EOReloadView(insertData[1]);
        }

        // 삭제
        private void DelMetroBtn_Click(object sender, EventArgs e)
        {
            int count = 0;

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value.ToString().Equals("True"))
                {
                    string[] deleteData = DatabaseColumnData(3, i.ToString());
                    string query = MainForm.dc.SelectDeleteQueryANDConvert(MainForm.settingData[0], MainForm.settingData[2], deleteData, "DELETE");
                    MainForm.mariaDB.EtcQuery(query);

                    // 로그
                    string[] logData = new string[dataGridView1.ColumnCount];

                    // 라인
                    logData[0] = MainForm.cbbMetroLine01.Text;

                    for (int j = 1; j < logData.Length - 1; j++)
                    {
                        logData[j] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }

                    MainForm.mariaDB.InsertLogDB(MainForm.SplitConvert(logData) + " EO 삭제", false);

                    count++;
                }
            }

            if (count > 0)
            {
                MainForm.dc.Msg("경고", count.ToString() + "건 삭제되었습니다");

                EOReloadView(MainForm.eoCarData);

                /*
                if (listBox1.SelectedIndex == -1)
                {
                    //MainForm.msg("삭제할 EO 내용을 선택해주세요.");
                }
                else
                {
                    listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                    textBox1.Text = "";
                }
                */
            }
        }

        // 검색
        private void SearchMetroBtn_Click(object sender, EventArgs e)
        {
            if (MainForm.cbbMetroLine01.SelectedIndex != -1)
            {
                SearchForm showForm = new SearchForm();

                MainForm.searchType = "EO";
                showForm.ShowDialog();
            }
            else
            {
                MainForm.dc.Msg("경고", "라인을 선택해주세요");
            }
        }

        // 초기화
        private void ResetMetroBtn_Click(object sender, EventArgs e)
        {
            txtCarName.Text = string.Empty;
            txtCustomerEO.Text = string.Empty;
            txtEOContents.Text = string.Empty;
            rdoFirst.Checked = true;
            checkBox1.Checked = false;

            if (!txtCustomerEO.Enabled)
            {
                txtCustomerEO.Text = "-";
            }
            else
            {
                txtMobisEO1.Text = string.Empty;
            }

            EOReloadView(null);
        }

        private void OpenMetroBtn_Click(object sender, EventArgs e)
        {
            txtMobisEO1.ReadOnly = false;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // 오픈 설정
                //openFileDialog.Filter = "mht files (*.mht)|*.mht|All files (*.*)|*.*";
                openFileDialog.Filter = "mht files (*.mht)|*.mht";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                // 열기 누르면
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // 파일 경로 복사
                    txtMhtFile.Text = openFileDialog.FileName;
                    MainForm.eoViewData = openFileDialog.FileName;

                    string[] mhtSearchData = MainForm.dc.MhtFileSearch(txtMhtFile.Text);

                    //2022.11.15
                    if (mhtSearchData[0] != null)
                    {
                        // mht 데이터 저장
                        // 데이터가 있는지 검사함
                        MainForm.mhtData = File.ReadAllText(txtMhtFile.Text, Encoding.Default);

                        // 제목 저장
                        //MainForm.subjectData = mhtSearchData[0];

                        // EO 번호 복사
                        txtMobisEO1.Text = mhtSearchData[1];

                        // 커서 이동
                        txtCarName.Focus();

                        ViewForm viewForm = new ViewForm();
                        viewForm.Show();

                        //txtMobisEO1.ReadOnly = true;
                    }
                }
                else
                {
                    txtMhtFile.Text = string.Empty;
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                if (MessageBox.Show("'작업변경지시서' 없이 강제 입력을 하게 되면 추후에 문제가 될 수도 있고 책임은 본인에게 있습니다. 그래도 진행하시겠습니까?\n\n[이 기능은 작변이 누락된 경우 긴급으로 사용하는 기능입니다]", "경고", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    //checkBox2.Checked = true;
                    txtMhtFile.Text = "--------------------------------";
                    txtMhtFile.Enabled = false;
                    OpenMetroBtn.Enabled = false;

                    txtCarName.Focus();
                }
                else
                {
                    checkBox2.Checked = false;
                    return;
                }
            }
            else
            {
                txtMhtFile.Text = string.Empty;
                txtMhtFile.Enabled = true;
                OpenMetroBtn.Enabled = true;
            }
        }

        private void txtCarName_Enter(object sender, EventArgs e)
        {
            txtCarName.BackColor = Color.LemonChiffon;
            txtCarName.ImeMode = ImeMode.Alpha;
        }

        private void txtCustomerEO_Enter(object sender, EventArgs e)
        {
            txtCustomerEO.BackColor = Color.LemonChiffon;
            txtCustomerEO.ImeMode = ImeMode.Alpha;
        }

        private void txtMobisEO1_Enter(object sender, EventArgs e)
        {
            txtMobisEO1.BackColor = Color.LemonChiffon;
            txtMobisEO1.ImeMode = ImeMode.Alpha;
        }

        private void txtMobisEO2_Enter(object sender, EventArgs e)
        {
            txtMobisEO2.BackColor = Color.LemonChiffon;
            txtMobisEO2.ImeMode = ImeMode.Alpha;
        }

        private void txtEOContents_Enter(object sender, EventArgs e)
        {
            txtEOContents.BackColor = Color.LemonChiffon;
            txtEOContents.ImeMode = ImeMode.Alpha;
        }

        private void txtCarName_Leave(object sender, EventArgs e)
        {
            txtCarName.BackColor = Color.White;
        }

        private void txtCustomerEO_Leave(object sender, EventArgs e)
        {
            txtCustomerEO.BackColor = Color.White;
        }

        private void txtMobisEO1_Leave(object sender, EventArgs e)
        {
            txtMobisEO1.BackColor = Color.White;
        }

        private void txtMobisEO2_Leave(object sender, EventArgs e)
        {
            txtMobisEO2.BackColor = Color.White;
        }

        private void txtEOContents_Leave(object sender, EventArgs e)
        {
            txtEOContents.BackColor = Color.White;

            // 2022.08.30
            // ' 오류 제거
            txtEOContents.Text = txtEOContents.Text.Replace("'", "");
        }

        private void txtCarName_KeyDown(object sender, KeyEventArgs e)
        {
            // 한영 키 버튼
            if (e.KeyValue == 229)
            {
                txtCarName.ImeMode = ImeMode.Alpha;
            }
        }

        private void txtCustomerEO_KeyDown(object sender, KeyEventArgs e)
        {
            // 한영 키 버튼
            if (e.KeyValue == 229)
            {
                txtCustomerEO.ImeMode = ImeMode.Alpha;
            }
        }

        private void txtMobisEO1_KeyDown(object sender, KeyEventArgs e)
        {
            // 한영 키 버튼
            if (e.KeyValue == 229)
            {
                txtMobisEO1.ImeMode = ImeMode.Alpha;
            }
        }

        private void txtMobisEO2_KeyDown(object sender, KeyEventArgs e)
        {
            // 한영 키 버튼
            if (e.KeyValue == 229)
            {
                txtMobisEO2.ImeMode = ImeMode.Alpha;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string stickerColor = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Value.ToString();

            // 2021.03.03
            // 오류 수정
            stickerColor = stickerColor == "없음" ? string.Empty : stickerColor;

            // 보기
            if (e.ColumnIndex == 10)
            {
                string[] selectData = DatabaseColumnData(4, stickerColor);
                string query = MainForm.dc.SelectDeleteQueryANDConvert(MainForm.settingData[0], MainForm.settingData[2], selectData, "SELECT");
                string[] columnData = MainForm.mariaDB.SelectQueryCount(query, "mht_data");

                // 공백이 아니라면 파일 생성
                if (!columnData[0].Equals(string.Empty))
                {
                    string tmpPath = @"C:\" + Application.ProductName + @"\mht_data.mht";

                    FileInfo fi = new FileInfo(tmpPath);

                    // 파일 삭제
                    if (fi.Exists)
                    {
                        fi.Delete();
                    }

                    // 파일 쓰기
                    File.WriteAllText(tmpPath, columnData[0], Encoding.Default);

                    string[] mhtSearchData = MainForm.dc.MhtFileSearch(tmpPath);

                    // 제목
                    //MainForm.subjectData = mhtSearchData[0];

                    // mht 경로
                    MainForm.eoViewData = tmpPath;

                    ViewForm viewForm = new ViewForm();
                    viewForm.ShowDialog();
                }
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // 2020.05.06
            // 값 초기화
            txtMhtFile.Text = string.Empty;
            MainForm.mhtData = string.Empty;

            // 예외 설정
            if (dataGridView1.Rows.Count > 0)
            {
                // 초도품 or 샘플
                if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[8].Value.ToString().Equals("초도품"))
                {
                    rdoFirst.Checked = true;
                }
                else if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[8].Value.ToString().Equals("샘플"))
                {
                    rdoSample.Checked = true;
                }
                else
                {
                    //return;
                }

                // 출하지
                if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[9].Value.ToString().Equals("OEM"))
                {
                    rdoOEM.Checked = true;
                }
                else if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[9].Value.ToString().Equals("CKD"))
                {
                    rdoCKD.Checked = true;
                }
                else if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[9].Value.ToString().Equals("KD"))
                {
                    rdoKD.Checked = true;
                }
                else
                {
                    rdoOEM.Checked = false;
                    rdoCKD.Checked = false;
                    rdoKD.Checked = false;
                }

                txtCarName.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
                txtCustomerEO.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString();

                // 값이 2개인지 검사
                string[] tmpData = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value.ToString().Split('/');

                // 2개 이상이라면
                if (tmpData.Length > 1)
                {
                    txtMobisEO1.Text = tmpData[0];
                    txtMobisEO2.Text = tmpData[1];
                    checkBox1.Checked = true;
                }
                else
                {
                    txtMobisEO1.Text = tmpData[0];
                    txtMobisEO2.Text = string.Empty;
                    checkBox1.Checked = false;
                }

                txtEOContents.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value.ToString();

                // 2021.02.09
                // EO 색상
                if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Style.BackColor.Name.ToString() == "Orange")
                {
                    rdoOrange.Checked = true;
                }
                else if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Style.BackColor.Name.ToString() == "Blue")
                {
                    rdoBlue.Checked = true;
                }
                else if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Style.BackColor.Name.ToString() == "LimeGreen")
                {
                    rdoLimeGreen.Checked = true;
                }
                else if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Style.BackColor.Name.ToString() == "White")
                {
                    rdoWhite.Checked = true;
                }
                else if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Style.BackColor.Name.ToString() == "Yellow")
                {
                    rdoYellow.Checked = true;

                    if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Value.ToString() != string.Empty)
                    {
                        comboBox1.SelectedItem = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Value.ToString();
                    }
                }
                else
                {
                    rdoNone.Checked = true;
                }

                // 2021.01.26
                // EO 구분
                if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[11].Value.ToString() == "MAIN PCB")
                {
                    rdoMain.Checked = true;
                }
                else if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[11].Value.ToString() == "SUB PCB")
                {
                    rdoSub.Checked = true;
                }
                else if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[11].Value.ToString() == "PANEL")
                {
                    rdoException.Checked = true;
                }
            }
        }

        private void comboBox1_EnabledChanged(object sender, EventArgs e)
        {
            if (!comboBox1.Enabled)
            {
                comboBox1.SelectedIndex = -1;
            }
        }

        private void RadioButtonCheckedBackColor(RadioButton _radioButton)
        {
            _radioButton.BackColor = _radioButton.Checked ? SystemColors.ControlLight : Color.White;
        }

        private void rdoNone_CheckedChanged(object sender, EventArgs e)
        {
            RadioButtonCheckedBackColor(rdoNone);
        }

        private void rdoOrange_CheckedChanged(object sender, EventArgs e)
        {
            RadioButtonCheckedBackColor(rdoOrange);
        }

        private void rdoBlue_CheckedChanged(object sender, EventArgs e)
        {
            RadioButtonCheckedBackColor(rdoBlue);
        }

        private void rdoLimeGreen_CheckedChanged(object sender, EventArgs e)
        {
            RadioButtonCheckedBackColor(rdoLimeGreen);
        }

        private void rdoWhite_CheckedChanged(object sender, EventArgs e)
        {
            RadioButtonCheckedBackColor(rdoWhite);
        }

        private void rdoYellow_CheckedChanged(object sender, EventArgs e)
        {
            RadioButtonCheckedBackColor(rdoYellow);

            if (rdoYellow.Checked)
            {
                comboBox1.Enabled = true;
            }
            else
            {
                comboBox1.Enabled = false;
            }
        }

        private void lblOrange_Click(object sender, EventArgs e)
        {
            rdoOrange.Checked = true;
        }

        private void lblBlue_Click(object sender, EventArgs e)
        {
            rdoBlue.Checked = true;
        }

        private void lblLimeGreen_Click(object sender, EventArgs e)
        {
            rdoLimeGreen.Checked = true;
        }

        private void lblWhite_Click(object sender, EventArgs e)
        {
            rdoWhite.Checked = true;
        }

        private void lblYellow_Click(object sender, EventArgs e)
        {
            rdoYellow.Checked = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked) EOReloadView(MainForm.eoCarData);
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked) EOReloadView(MainForm.eoCarData);
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked) EOReloadView(MainForm.eoCarData);
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked) EOReloadView(MainForm.eoCarData);
        }

        private void rdoException_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
