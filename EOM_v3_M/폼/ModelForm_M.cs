using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using Guna.UI2.WinForms;

namespace EOM_v3_M
{
    public partial class ModelForm_M : Form
    {
        const int OEM_DAY = 7;
        const int CKD_MONTH = 1;
        const string END_TIME = "08:30:00";

        MessageFormClass messageFormClass = new MessageFormClass();

        private void MainPCBInitialValue(Guna2TextBox _textBox)
        {
            if (MainForm.cbbLineName01.Text != "HUD")
            {
                _textBox.Text = "M15";
            }
        }

        private void InitialSetControl(bool[] _data)
        {
            txtProductName.Enabled = _data[0];
            txtCarName.Enabled = _data[1];
            txtMainPCB.Enabled = _data[2];
            txtSubPCB.Enabled = _data[3];
            chkNonInput.Enabled = _data[4];
            txtTagType.Enabled = _data[5];
            txtCustomerEO.Enabled = _data[6];
            txtMobisEO.Enabled = _data[7];
            txtEOContents.Enabled = _data[8];
            chkPrivate.Enabled = _data[9];
            cbbShipment.Enabled = _data[10];
            btnEOSelect.Enabled = _data[11];
            btnProductAdd.Enabled = _data[12];
            dtpStartDate.Enabled = _data[13];
            dtpEndDate.Enabled = _data[14];
            btnReservation.Enabled = _data[15];
        }

        /// <summary>
        /// 1 : 미입력 (출하지, M/PCB, S/PCB, EO 구분, 적용 오더)
        /// 2 : 미입력 (적용 오더)
        /// 3 : 미입력 (시작일, 종료일, 적용 오더) - 예약 기능
        /// </summary>
        /// <param name="_data1"></param>
        /// <returns></returns>
        private string[] DatabaseColumnData(int _data)
        {
            string startDatetimeData, endDatetimeData = string.Empty;
            string[] returnData;

            TimeSpan startTime = new TimeSpan(00, 00, 00);
            TimeSpan endTime = new TimeSpan(08, 30, 00);
            TimeSpan realTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            // 시작일 날짜
            startDatetimeData = dtpStartDate.Value.Date < DateTime.Now.Date ? dtpStartDate.Value.ToString("yyyy-MM-dd 00:00:00") : DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            /*
            // 종료일 날짜
            if (!(startTime <= realTime && endTime >= realTime))
            {
                // 새벽이 아니라면 +1일
                dateTimePicker2.Value = dateTimePicker2.Value.AddDays(1);
            }
            */

            // 2021.04.02
            // 전표 날짜 기준으로 변경한다고 가정
            dtpEndDate.Value = dtpEndDate.Value.AddDays(1);

            endDatetimeData = dtpEndDate.Value.ToString("yyyy-MM-dd " + END_TIME);

            switch (_data)
            {
                case 1:
                    returnData = new string[]
                    {
                        MainForm.cbbLineName01.Text,                                                                                        // [0] line
                        txtProductName.Text,                                                                                                // [1] model_name
                        txtCarName.Text,                                                                                                    // [2] car_name
                        txtCustomerEO.Text,                                                                                                 // [3] customer_eo_number
                        txtMobisEO.Text,                                                                                                    // [4] mobis_eo_number
                        txtEOContents.Text,                                                                                                 // [5] eo_contents
                        MainForm.eoSelectData[4],                                                                                           // [6] sticker_color
                        MainForm.eoSelectData[5],                                                                                           // [7] sticker_text
                        startDatetimeData,                                                                                                  // [8] start_date
                        endDatetimeData,                                                                                                    // [9] end_date
                        MainForm.SearchRegistrant(NetworkInterface.GetAllNetworkInterfaces()[0].GetPhysicalAddress().ToString()),           // [10] registrant
                        txtTagType.Text,                                                                                                    // [11] print_type
                        "-",                                                                                                                // [12] shipment        (X)
                        MainForm.eoSelectData[7],                                                                                           // [13] mht_data
                        "-",                                                                                                                // [14] main_pcb_name   (X)
                        "-",                                                                                                                // [15] sub_pcb_name    (X)
                        "-",                                                                                                                // [16] eo_type         (X)
                        "",                                                                                                                 // [17] start_order     (X)
                    };
                    break;
                case 2:
                    returnData = new string[]
                    {
                        MainForm.cbbLineName01.Text,                                                                                        // [0] line
                        txtProductName.Text,                                                                                                // [1] model_name
                        txtCarName.Text,                                                                                                    // [2] car_name
                        txtCustomerEO.Text,                                                                                                 // [3] customer_eo_number
                        txtMobisEO.Text,                                                                                                    // [4] mobis_eo_number
                        txtEOContents.Text,                                                                                                 // [5] eo_contents
                        MainForm.eoSelectData[4],                                                                                           // [6] sticker_color
                        MainForm.eoSelectData[5],                                                                                           // [7] sticker_text
                        startDatetimeData,                                                                                                  // [8] start_date
                        endDatetimeData,                                                                                                    // [9] end_date
                        MainForm.SearchRegistrant(NetworkInterface.GetAllNetworkInterfaces()[0].GetPhysicalAddress().ToString()),           // [10] registrant
                        txtTagType.Text,                                                                                                    // [11] print_type
                        cbbShipment.Text,                                                                                                   // [12] shipment
                        MainForm.eoSelectData[7],                                                                                           // [13] mht_data
                        txtMainPCB.Text,                                                                                                    // [14] main_pcb_name
                        txtSubPCB.Text,                                                                                                     // [15] sub_pcb_name
                        txtEOType.Text,                                                                                                     // [16] eo_type
                        "",                                                                                                                 // [17] start_order     (X)
                    };
                    break;
                case 3:
                    returnData = new string[]
                    {
                        MainForm.cbbLineName01.Text,                                                                                        // [0] line
                        txtProductName.Text,                                                                                                // [1] model_name
                        txtCarName.Text,                                                                                                    // [2] car_name
                        txtCustomerEO.Text,                                                                                                 // [3] customer_eo_number
                        txtMobisEO.Text,                                                                                                    // [4] mobis_eo_number
                        txtEOContents.Text,                                                                                                 // [5] eo_contents
                        MainForm.eoSelectData[4],                                                                                           // [6] sticker_color
                        MainForm.eoSelectData[5],                                                                                           // [7] sticker_text
                        startDatetimeData,                                                                                                  // [8] start_date
                        MainForm.RESERVATION_DEFAULT_DATE,                                                                                  // [9] end_date
                        MainForm.SearchRegistrant(NetworkInterface.GetAllNetworkInterfaces()[0].GetPhysicalAddress().ToString()),           // [10] registrant
                        txtTagType.Text,                                                                                                    // [11] print_type
                        cbbShipment.Text,                                                                                                   // [12] shipment
                        MainForm.eoSelectData[7],                                                                                           // [13] mht_data
                        txtMainPCB.Text,                                                                                                    // [14] main_pcb_name
                        txtSubPCB.Text,                                                                                                     // [15] sub_pcb_name
                        txtEOType.Text,                                                                                                     // [16] eo_type
                        "",                                                                                                                 // [17] start_order     (X)
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

        private bool HyphenCheckSum(Guna2TextBox textBox)
        {
            int count = 0;

            for (int i = 0; i < textBox.TextLength; i++)
            {
                if (textBox.Text.Substring(i, 1).Equals("-"))
                {
                    count++;
                }
            }

            // 하이픈 2개 이상, 0개 이하
            if (count > 1 || count <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void DGVSearchSelect(string _data0, string _data1, int _cell1, string _data2, int _cell2)
        {
            MainForm.cbbProductName01.SelectedItem = _data0;

            for (int i = 0; i < MainForm.datagridview01.RowCount; i++)
            {
                if ( MainForm.datagridview01.Rows[i].Cells[_cell1].Value.ToString().Equals(_data1) &&
                     MainForm.datagridview01.Rows[i].Cells[_cell2].Value.ToString().Equals(_data2) )
                {
                    MainForm.datagridview01.CurrentCell = MainForm.datagridview01.Rows[i].Cells[0];
                    break;
                }
            }

            Close();
        }

        private bool ModelEODuplicationCheck(string _column, string _data01, string _data02, string _data03)
        {
            string[] selectData = new string[] { "line", MainForm.cbbLineName01.Text, _column, _data01, "model_name", _data02, "shipment", _data03 };
            string query = MainForm.dc.SelectDeleteQueryANDConvert(MainForm.DATABASE_NAME, "model_data", selectData, "SELECT");
            string[,] columnData = MainForm.mariaDB.SelectQuery2(query);

            if (columnData.GetLength(0) > 0)
                return true;
            else
                return false;
        }

        private bool ShipmentCheck()
        {
            // 2022.01.10
            // 출하지 마스터 검사
            string query = string.Empty;
            string shipmentConditionData = string.Empty;
            string[,] shipmentData;

            query = "SELECT * FROM `" + MainForm.DATABASE_NAME + "`.`shipment_history_data` WHERE model_name = '" + txtProductName.Text + "' AND end_date > '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ORDER BY start_date";
            shipmentData = MainForm.mariaDB.SelectQuery2(query);

            // 1. 출하지 마스터 검사 [등록 데이터]
            if (shipmentData.GetLength(0) > 0)
            {
                shipmentConditionData = shipmentData[shipmentData.GetLength(0) - 1, 1];
            }
            // 2. 설변 등록 유무
            else
            {
                
            }

            if (shipmentConditionData != string.Empty && shipmentConditionData != cbbShipment.Text)
            {
                return false;
            }

            return true;
        }

        private bool EOContentsExceptionHandling(string _data)
        {
            string query = "SELECT * FROM `" + MainForm.DATABASE_NAME + "`.`eo_contents_duplication`";
            string[,] selectData = MainForm.mariaDB.SelectQuery2(query);

            for (int i = 0; i < selectData.GetLength(0); i++)
            {
                // eo_duplication_contents
                if (_data.Contains(selectData[i, 1]))
                {
                    MainForm.dc.LogFileSave(_data + ".Contains(" + selectData[i, 1] + ") => true");

                    return true;
                }

                MainForm.dc.LogFileSave(selectData[i, 1]);
            }

            return false;
        }

        private bool ModelAddCheckSum()
        {
            if (txtProductName.Text.Equals(string.Empty))
            {
                MainForm.Guna2Msg(this, "오류", "모델명을 정확히 입력해주세요 [공란 확인]");
                txtProductName.Select();
                return false;
            }
            if (HyphenCheckSum(txtProductName) && txtProductName.Text.Substring(0, 1) != "A")
            {
                MainForm.Guna2Msg(this, "오류", "모델명을 정확히 입력해주세요 [하이픈 1개만 입력 가능]");
                txtProductName.SelectAll();
                return false;
            }
            // 2020.05.25
            // 하이픈 위치 검사 조건 변경
            // 앞에 9가 올때만 가운데 검사
            if (!txtProductName.Text.Substring(5, 1).Equals("-") && txtProductName.Text.Substring(0, 1).Equals("9"))
            {
                MainForm.Guna2Msg(this, "오류", "모델명을 정확히 입력해주세요 [하이픈 위치 확인]");
                txtProductName.SelectAll();
                return false;
            }

            if (txtCarName.Text.Equals(string.Empty))
            {
                MainForm.Guna2Msg(this, "오류", "차종을 입력해주세요 [공란 확인]");
                return false;
            }
            // 2020.04.09
            // 클러스터 제외
            //if (txtMobisEO.Text.Equals(string.Empty) && !MainForm.cbbLineName01.Text.Equals(MainForm.LINE_NAME_LIST[3]))
            // 2021.12.12
            // 클러스터 포함
            if (txtMobisEO.Text.Equals(string.Empty))
            {
                MainForm.Guna2Msg(this, "오류", "EO 번호를 선택해주세요 [공란 확인]");
                return false;
            }

            /*
            if (comboBox2.Enabled && comboBox2.SelectedIndex == -1)
            {
                MainForm.Guna2Msg(this, "오류", "생산지를 선택해주세요");
                comboBox2.Select();
                return false;
            }
            */

            if (cbbShipment.Text == string.Empty)
            {
                MainForm.Guna2Msg(this, "오류", "출하지를 선택해주세요");
                return false;
            }

            /*
            if (ChangeHistoryFirstShipmentCheck())
            {
                DialogResult dialogResult = MessageBox.Show("출하지를 '" + cbbShipment.Text + "'로 첫 등록하시겠습니까?", "공지사항 등록", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.No)
                {
                    cbbShipment.SelectedIndex = -1;
                    return false;
                }
            }
            */

            if (dtpStartDate.Enabled && dtpStartDate.Value > DateTime.Now)
            {
                MainForm.Guna2Msg(this, "오류", "현재 날짜보다 크게 설정할 수 없습니다");
                dtpStartDate.Select();
                return false;
            }
            // 공백, 하이픈, 4M 예외 처리
            if(txtMobisEO.Text.Equals(string.Empty) || txtMobisEO.Text.Equals("-") || txtMobisEO.Text.Equals("4M"))
            {
                // EO 내용 중복 검사 && 출하지 검사
                // 찾을 컬럼, 컬럼 값, 모델 값, 출하지
                if (ModelEODuplicationCheck("eo_contents", txtEOContents.Text, txtProductName.Text, cbbShipment.Text))
                {
                    // 2025.06.05
                    // EO 내용 (예외처리)
                    if (!EOContentsExceptionHandling(txtEOContents.Text))
                    { 
                        if (!cbbShipment.Text.Equals("-"))
                        {
                            MainForm.Guna2Msg(this, "오류", "EO 내용, 출하지 '" + cbbShipment.Text + "' 등록되어 있습니다");
                        }
                        else
                        {
                            MainForm.Guna2Msg(this, "오류", "EO 내용이 등록되어 있습니다");
                        }
                        DGVSearchSelect(txtProductName.Text, txtEOContents.Text, 5, cbbShipment.Text, 10);
                        return false;
                    }
                }
            }
            else
            {
                // EO 번호 중복 검사 && 출하지 검사
                // 찾을 컬럼, 컬럼 값, 모델 값, 출하지
                if (ModelEODuplicationCheck("mobis_eo_number", txtMobisEO.Text, txtProductName.Text, cbbShipment.Text))
                {
                    if (!cbbShipment.Text.Equals("-"))
                    {
                        MainForm.Guna2Msg(this, "오류", "EO 번호, 출하지 '" + cbbShipment.Text + "' 등록되어 있습니다");
                    }
                    else
                    {
                        MainForm.Guna2Msg(this, "오류", "EO 번호가 등록되어 있습니다");
                    }
                    DGVSearchSelect(txtProductName.Text, txtMobisEO.Text, 4, cbbShipment.Text, 10);
                    return false;
                }
            }
            /*
            // 왜지?
            if (dt01 > dt02)
            {
                MainForm.Guna2Msg(this, "오류", "마감일이 종료일보다 작을 수 없습니다");
                return false;
            }
            */
            return true;
        }

        public ModelForm_M()
        {
            InitializeComponent();
        }

        private void ModelForm_M_Load(object sender, EventArgs e)
        {
            bool[] controlBox;

            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 00, 00, 00);

            Text = MainForm.programName + " - " + MainForm.dc.Version();

            dtpStartDate.Value = dt;
            dtpEndDate.Value = dt.AddDays(OEM_DAY);

            cbbShipment.Items.Clear();

            // 2023.03.28
            for (int i = 0; i < MainForm.dc.Shipment.Length; i++)
            {
                cbbShipment.Items.Add(MainForm.dc.Shipment[i]);
            }

            // 2023.03.28
            MainForm.dc.InitialSetTextBox(this);

            // D-오디오 수삽, D-오디오 SUB
            if (MainForm.cbbLineName01.Text.Equals(MainForm.LINE_NAME_LIST[0]) || MainForm.cbbLineName01.Text.Equals(MainForm.LINE_NAME_LIST[2]))
            {
                txtProductName.Text = "M";
                txtProductName.Select(txtProductName.MaxLength + 1, 0);
                //dateTimePicker2.Value = dt;
                controlBox = new bool[] { true, true, false, false, false,
                                          false, false, false, false, true,
                                          false, true, true, true, false, false };
                InitialSetControl(controlBox);
            }
            // D-오디오 조립, 클러스터
            else
            {
                txtProductName.Text = string.Empty;
                //dateTimePicker2.Value = dt.AddDays(7);
                controlBox = new bool[] { true, true, true, true, true,
                                          false, false, false, false, true,
                                          true, true, true, false, false, true };
                InitialSetControl(controlBox);
            }

            //dtpStartDate.Enabled = false;
            //dtpEndDate.Enabled = false;

            // 2022.07.01
            // 모델이 선택됬을 경우 빠른 입력
            if (MainForm.datagridview01.Visible)
            {
                txtProductName.Text = MainForm.cbbProductName01.Text;
            }

#if DEBUG
            txtProductName.Text = "96560-S1000";
            txtCarName.Text = "TMa";
            txtMainPCB.Text = "M1568-100100";
            chkNonInput.Checked = true;
            btnEOSelect.Select();
#endif

            // 2022.01.11
            // 중앙 정렬
            // MainForm.dc.ComboBoxDrawAlignCenter(cbbShipment);

            // 2023.03.24
            // Guna UI 강제 위치 적용
            //Location = MainForm.dc.CenterLocation(Width, Height);
        }

        private void ModelForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 2022.07.01
            // 메세지 폼 잔상 강제삭제
            messageFormClass.EndForm();

            // 2020.09.25
            // EO 등록 데이터 무조건 초기화
            for (int i = 0; i < MainForm.eoSelectData.Length; i++)
            {
                MainForm.eoSelectData[i] = string.Empty;
            }
        }

        private void TextHyphenConvert(TextBox _textBox)
        {
            // 2020.05.25
            // 앞자리 체크 후 하이픈 추가
            // 2020.06.22
            // M, 9 체크
            //if (_textBox.Text.Length == 5 && (_textBox.Text.Substring(0, 1).Equals("M") || _textBox.Text.Substring(0, 1).Equals("9")))
            if (_textBox.Text.Length == 5)
            {
                _textBox.Text = _textBox.Text + "-";
                _textBox.Select(_textBox.Text.Length, 0);
            }
        }

        private void txtProductName_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC 키 폼 종료
            if (e.KeyValue == 27)
            {
                Close();
            }
        }

        private void NumAlphaBetString_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 2024.10.29
            MainForm.dc.NumAlphaBetString((Guna2TextBox)sender, e);
        }

        private void NumAlphaSpaceString_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 2024.10.29
            MainForm.dc.NumAlphaSpaceString((Guna2TextBox)sender, e);
        }

        // 2023.03.28
        // 텍스트박스 이벤트 통합
        private void txtAddHyphenCheckFirstChar_KeyUp(object sender, KeyEventArgs e)
        {
            MainForm.dc.AddHyphenCheckFirstChar((Guna2TextBox)sender, e);
        }

        private void txtCarName_KeyUp(object sender, KeyEventArgs e)
        {
            MainForm.dc.CarNameUpperConvert(txtCarName);
        }

        private void txtSubPCB_TextChanged(object sender, EventArgs e)
        {
            if (((Guna2TextBox)sender).Enabled)
            {
                if (((Guna2TextBox)sender).TextLength < 3 || ((Guna2TextBox)sender).Text.Substring(0, 1) != "M")
                {
                    // 2020.12.14
                    // D-오디오 조립만 M17
                    // 그 외 M15
                    if (MainForm.cbbLineName01.Text == MainForm.LINE_NAME_LIST[1])
                    {
                        //((Guna2TextBox)sender).Text = "M17";
                    }
                    else
                    {
                        //txtSubPCB.Text = "M15";
                    }

                    ((Guna2TextBox)sender).Select(((Guna2TextBox)sender).MaxLength + 1, 0);
                }
            }
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            if (!chkPrivate.Checked)
            {
                MainForm.dc.ShipmentDays(dtpStartDate, dtpEndDate, cbbShipment.Text);
            }
            else
            {
                dtpEndDate.Value = dtpStartDate.Value;
            }
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            // 2020.12.29
            // 내부관리 항목 추가로 조건 추가
            // 2021.09.04
            // 공백이 아닐 경우
            if (!chkPrivate.Checked && txtProductName.Text != string.Empty)
            {
                if (dtpStartDate.Value > dtpEndDate.Value)
                {
                    MainForm.Guna2Msg(this, "오류", "적용일보다 작을 수 없습니다");
                    MainForm.dc.ShipmentDays(dtpStartDate, dtpEndDate, cbbShipment.Text);
                    return;
                }

                if (cbbShipment.Text == "CKD" || cbbShipment.Text == "KD")
                {
                    DateTime firstDateTime = dtpStartDate.Value.AddMonths(1);

                    if (firstDateTime > dtpEndDate.Value)
                    {
                        MainForm.Guna2Msg(this, "오류", "적용일보다 작을 수 없습니다 [CKD, KD : 최소 " + CKD_MONTH + "달]");
                        MainForm.dc.ShipmentDays(dtpStartDate, dtpEndDate, cbbShipment.Text);
                        return;
                    }
                }

                if (cbbShipment.Text == "OEM")
                {
                    DateTime firstDateTime = dtpStartDate.Value.AddDays(OEM_DAY);

                    if (firstDateTime > dtpEndDate.Value)
                    {
                        MainForm.Guna2Msg(this, "오류", "적용일보다 작을 수 없습니다 [OEM : 최소 " + OEM_DAY + "일]");
                        MainForm.dc.ShipmentDays(dtpStartDate, dtpEndDate, cbbShipment.Text);
                        return;
                    }
                }
            }
        }

        private void btnEOSelect_Click(object sender, EventArgs e)
        {
            // 2022.07.01
            // 메세지 폼 잔상 강제삭제
            messageFormClass.EndForm();

            string[,] s = new string[,]
            {
                { "오디오 플랫폼" , "11" },
                { "D-오디오 수삽" , "11" },
                { "D-오디오 조립" , "11" },
                { "D-오디오 SUB" , "11" },
                { "클러스터" , "11" },
                { "HUD" , "10" },
            };

            if (txtProductName.Text.Equals(string.Empty))
            {
                MainForm.Guna2Msg(this, "오류", "품번을 정확히 입력해주세요[공란 확인]");

                txtProductName.Select();
                return;
            }

            for (int i = 0; i < s.GetLength(0); i++)
            {
                if (MainForm.cbbLineName01.Text == s[i, 0] && txtProductName.Text.Length < Convert.ToInt32(s[i, 1]) && txtProductName.Text.Substring(0, 1) != "A")
                {
                    MainForm.Guna2Msg(this, "오류", "품번을 정확히 입력해주세요 [" + s[i, 1] + "자 이상]");

                    txtProductName.Select();
                    return;
                }
            }

            // 2022.03.10
            // 수삽, SUB 제외
            if (MainForm.cbbLineName01.Text != "D-오디오 수삽" && MainForm.cbbLineName01.Text != "D-오디오 SUB")
            {
                if (MainForm.dc.CarNameCheck(txtCarName) || MainForm.dc.PCBProductNameCheck(txtMainPCB, "메인 PCB") || (!chkNonInput.Checked && MainForm.dc.PCBProductNameCheck(txtSubPCB, "서브 PCB")))
                {
                    return;
                }
            }

            // 2021.01.14
            // 체크박스 해제
            //chkPrivate.Checked = false;

            Opacity = 0;

            // 잘못된 차종의 EO를 선택을 막기 위한 인터락
            MainForm.carSelectData = txtCarName.Text;
            MainForm.eoCarData = txtCarName.Text;

            EOForm showForm = new EOForm();
            showForm.Owner = this;
            showForm.ShowDialog();

            if (MainForm.eoSelectData[0] != null)
            {
                txtTagType.Text = MainForm.eoSelectData[0];                     // 타입
                txtCustomerEO.Text = MainForm.eoSelectData[1];                  // 고객사 EO
                txtMobisEO.Text = MainForm.eoSelectData[2];                     // 모비스 EO
                txtEOContents.Text = MainForm.eoSelectData[3];                  // EO 내용

                // 2021.02.10
                // 스티커 색상
                if (MainForm.eoSelectData[4] != "ScrollBar")
                {
                    txtStickerColor.ForeColor = Color.FromName(MainForm.eoSelectData[4]);
                    txtStickerColor.Text = "● " + MainForm.eoSelectData[5];
                }
                else
                {
                    txtStickerColor.ForeColor = Color.Black;
                    txtStickerColor.Text = string.Empty;
                }

                // 2021.01.12
                if (txtEOContents.Text != string.Empty)
                {
                    chkPrivate.Enabled = true;
                }

                // D-오디오 SUB 필요 없음으로 공란
                if (MainForm.cbbLineName01.Text != "D-오디오 수삽" && MainForm.cbbLineName01.Text != "D-오디오 SUB")     // 출하지
                {
                    cbbShipment.Text = MainForm.eoSelectData[6];
                }
                else
                {
                    cbbShipment.Items.Clear();
                    cbbShipment.Items.Add("-");
                    cbbShipment.Enabled = false;
                    cbbShipment.Text = "-";
                }

                // 작업지시변경서 확인
                if (MainForm.eoSelectData[7] != null && !MainForm.eoSelectData[7].Equals(string.Empty))
                {
                    btnMthView.Enabled = true;
                }
                else
                {
                    btnMthView.Enabled = false;
                }

                if (!txtEOContents.Text.Equals(string.Empty))
                {
                    txtProductName.Enabled = false;
                    txtCarName.Enabled = false;

                    dtpStartDate.Enabled = true;
                    dtpEndDate.Enabled = true;
                }

                // 2021.01.21
                // EO 구분
                txtEOType.Text = MainForm.eoSelectData[8];

                /*
                // 2020.08.28
                // EO 등록 데이터 무조건 초기화
                for (int i = 0; i < MainForm.eoSelectData.Length; i++)
                {
                    MainForm.eoSelectData[i] = string.Empty;
                }
                */

                MainForm.dc.ShipmentDays(dtpStartDate, dtpEndDate, cbbShipment.Text);
            }

            Opacity = 1;
        }

        private void ProductAddCheck()
        {

        }

        private void btnReservation_Click(object sender, EventArgs e)
        {
            // 2025.04.06
            // 샘플은 제외
            if (txtTagType.Text == "샘플")
            {
                MainForm.Guna2Msg(this, "오류", "샘플은 예약할 수 없습니다.");
                return;
            }

            if (ModelAddCheckSum())
            {
                DialogResult dialogResult = MessageBox.Show("EO 예약 기능은 특정 품번이 포장 공정에 투입될 때 적용되므로 주의하셔야 합니다. 이해되셨으면 '예'를 눌러주세요.", "알림", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.No)
                {
                    return;
                }

                string query, printData = string.Empty;
                string[] insertData;

                // D-오디오 수삽 OR D-오디오 SUB
                //if (MainForm.cbbLineName01.Text.Equals(MainForm.LINE_NAME_LIST[1]) || MainForm.cbbLineName01.Text.Equals(MainForm.LINE_NAME_LIST[3]))
                // D-오디오 조립 OR 클러스터
                insertData = DatabaseColumnData(3);

                query = MainForm.dc.InsertQueryArrayConvert(MainForm.DATABASE_NAME, "model_data", insertData);

                MainForm.mariaDB.EtcQuery(query);

                Opacity = 0;
                MainForm.modelSelectData = txtProductName.Text;

                // 2020.05.06
                // mht 파일 여부
                if (!MainForm.eoSelectData[7].Equals(string.Empty))
                {
                    insertData[13] = "O";
                }
                else
                {
                    insertData[13] = "X";
                }

                MainForm.mariaDB.InsertLogDB(MainForm.SplitConvert(insertData) + " 모델 예약", false);
                Close();
            }
        }

        private void btnProductAdd_Click(object sender, EventArgs e)
        {
            if (ModelAddCheckSum())
            {
                string query, printData = string.Empty;
                string[] insertData;

                // D-오디오 수삽
                // OR
                // D-오디오 SUB
                if (MainForm.cbbLineName01.Text.Equals(MainForm.LINE_NAME_LIST[0]) || MainForm.cbbLineName01.Text.Equals(MainForm.LINE_NAME_LIST[2]))
                {
                    insertData = DatabaseColumnData(1);
                }
                // 오디오 플랫폼, D-오디오 조립, 클러스터, HUD
                else
                {
                    insertData = DatabaseColumnData(2);
                }

                query = MainForm.dc.InsertQueryArrayConvert(MainForm.DATABASE_NAME, "model_data", insertData);
                MainForm.mariaDB.EtcQuery(query);

                Clipboard.SetText(query);

                Opacity = 0;
                MainForm.modelSelectData = txtProductName.Text;

                // 2020.05.06
                // mht 파일 여부
                if (!MainForm.eoSelectData[7].Equals(string.Empty))
                {
                    insertData[13] = "O";
                }
                else
                {
                    insertData[13] = "X";
                }

                MainForm.mariaDB.InsertLogDB(MainForm.SplitConvert(insertData) + " 모델 추가", false);

                Close();
            }
            else
            {
                return;
            }
        }

        private void btnMthView_Click(object sender, EventArgs e)
        {
            string tmpPath = @"C:\" + Application.ProductName + @"\mht_data.mht";

            FileInfo fi = new FileInfo(tmpPath);

            // 파일 삭제
            if (fi.Exists)
            {
                fi.Delete();
            }

            // 파일 쓰기
            File.WriteAllText(tmpPath, MainForm.eoSelectData[7], Encoding.Default);

            string[] mhtSearchData = MainForm.dc.MhtFileSearch(tmpPath);

            // 제목
            //MainForm.subjectData = mhtSearchData[0];

            // mht 경로
            //MainForm.eoViewData = tmpPath;

            ViewForm viewForm = new ViewForm(tmpPath);
            viewForm.ShowDialog();
        }

        private void txtFillColor_Enter(object sender, EventArgs e)
        {
            ((Guna2TextBox)sender).FillColor = Color.LemonChiffon;

            MainForm.dc.ImeModeAllSet(this, "");
        }

        private void txtMainPCB_Enter(object sender, EventArgs e)
        {
            MainPCBInitialValue(txtMainPCB);

            txtMainPCB.Select(txtMainPCB.MaxLength + 1, 0);
            txtMainPCB.FillColor = Color.LemonChiffon;
        }

        private void txtSubPCB_Enter(object sender, EventArgs e)
        {
            txtSubPCB.Select(txtSubPCB.MaxLength + 1, 0);
            txtSubPCB.FillColor = Color.LemonChiffon;
        }

        private void txtProductName_Leave(object sender, EventArgs e)
        {
            if (txtProductName.TextLength >= 10)
            {
                // ********* 아래 오류 수정해야함
                // 삭제된 개체에 액세스할 수 없습니다.
                // 개체 이름: 'Form'
                //messageFormClass.FormMsg = "[" + txtProductName.Text + "] 품번 등록정보 조회 시도..";
                //messageFormClass.ShowDelay = 2000;
                //messageFormClass.StartForm();

                // 값이 있을 경우에만 체킹
                if (!txtProductName.Text.Equals(string.Empty))
                {
                    // 2020.11.09
                    // 품번 띄어쓰기 없애기
                    txtProductName.Text = txtProductName.Text.Replace(" ", String.Empty);

                    // 2022.01.14
                    // 라인의 전 모델을 가져와 프로그램 내에서 처리하므로 데이터베이스 조건부 처리 요청 
                    string[] selectData = new string[] { "line", MainForm.cbbLineName01.Text, "model_name", txtProductName.Text, "print_type", "초도품" };
                    //string[] selectData = new string[] { "line", MainForm.cbbLineName01.Text, "model_name", txtProductName.Text };
                    string query = MainForm.dc.SelectDeleteQueryANDConvert(MainForm.DATABASE_NAME, "model_data", selectData, "SELECT");
                    //query += " OR (model_name = '" + txtProductName.Text + "' AND eo_contents = '---------- 첫 투입 품번 등록 ----------')";
                    string[,] modelData = MainForm.mariaDB.SelectQuery2(query);

                    txtCarName.Text = string.Empty;
                    txtCarName.ReadOnly = false;

                    // 2020.12.14
                    // 메인 PCB
                    txtMainPCB.Text = string.Empty;
                    txtMainPCB.ReadOnly = false;

                    // 2020.12.14
                    // 서브 PCB
                    txtSubPCB.Text = string.Empty;
                    txtSubPCB.ReadOnly = false;

                    // 2022.01.14
                    // 라인의 전 모델을 가져와 프로그램 내에서 처리하므로 데이터베이스 조건 처리 요청
                    if (modelData.GetLength(0) > 0)
                    {
                        // 2022.07.01
                        // 데이터가 있을 경우에만 입력 후 비활성화

                        // 차종
                        if (modelData[modelData.GetLength(0) - 1, 2] != string.Empty)
                        {
                            txtCarName.Text = modelData[modelData.GetLength(0) - 1, 2];
                            txtCarName.ReadOnly = true;
                        }

                        // 출하지
                        // 2022.05.16
                        // HUD, CLUSTER 라인은 제외
                        if (!(MainForm.cbbLineName01.Text == "HUD" || MainForm.cbbLineName01.Text == "클러스터"))
                        {
                            for (int i = 0; i < modelData.GetLength(0); i++)
                            {
                                if (modelData[i, 12] != "-")
                                {
                                    //cbbShipment.Enabled = false;
                                    cbbShipment.Text = modelData[i, 12];
                                }
                            }
                        }

                        // MAIN PCB
                        if (modelData[modelData.GetLength(0) - 1, 14] != string.Empty)
                        {
                            txtMainPCB.Text = modelData[modelData.GetLength(0) - 1, 14];
                            txtMainPCB.Enabled = false;
                        }

                        // SUB PCB
                        if (modelData[modelData.GetLength(0) - 1, 15] == "-")
                        {
                            // 없음
                            txtSubPCB.Text = modelData[modelData.GetLength(0) - 1, 15];
                            txtSubPCB.Enabled = false;
                            chkNonInput.Checked = true;
                        }
                        else if (modelData[modelData.GetLength(0) - 1, 15] != string.Empty)
                        {
                            // 있음
                            txtSubPCB.Text = modelData[modelData.GetLength(0) - 1, 15];
                            txtSubPCB.Enabled = false;
                            chkNonInput.Checked = false;
                        }
                    }

                    // 2020.12.14
                    // 차 모델이 안적혔을 경우
                    if (!txtCarName.ReadOnly)
                    {
                        txtMainPCB.Enabled = true;
                        txtSubPCB.Enabled = true;
                        cbbShipment.Enabled = true;
                        cbbShipment.SelectedIndex = -1;

                        // 2021.01.26
                        // D-오디오 수삽, D-오디오 조립 외만
                        if (MainForm.cbbLineName01.Text != MainForm.LINE_NAME_LIST[0] && MainForm.cbbLineName01.Text != MainForm.LINE_NAME_LIST[2])
                        {
                            chkNonInput.Enabled = true;
                        }
                    }
                    /*
                     * 불 필요한 것 같음
                     * 차후에 문제가 생기면 추가
                    // 메인 PCB 모델이 안적혔을 경우
                    else if (!txtMainPCB.ReadOnly)
                    {
                        txtMainPCB.Focus();
                    }
                    // 서브 PCB 모델이 안적혔을 경우
                    else if (!txtSubPCB.ReadOnly)
                    {
                        txtSubPCB.Focus();
                    }
                    */
                    else
                    {
                        chkNonInput.Enabled = false;
                        btnEOSelect.Focus();
                    }
                }

                // 2020.12.14
                if (txtProductName.TextLength > 0 && txtProductName.Text.Substring(0, 1) == "M")
                {
                    txtMainPCB.Enabled = false;
                    txtSubPCB.Enabled = false;

                    txtMainPCB.Text = "-";
                    txtSubPCB.Text = "-";
                }
            }

            ((Guna2TextBox)sender).FillColor = Color.White;
        }

        private void txtFillColor_Leave(object sender, EventArgs e)
        {
            ((Guna2TextBox)sender).FillColor = Color.White;
        }

        private void txtMainPCB_Leave(object sender, EventArgs e)
        {
            MainForm.dc.TextBoxLeaveHyphenConvert(txtMainPCB);

            txtMainPCB.FillColor = Color.White;
        }

        private void txtSubPCB_Leave(object sender, EventArgs e)
        {
            MainForm.dc.TextBoxLeaveHyphenConvert(txtSubPCB);

            txtSubPCB.FillColor = Color.White;
        }

        private void chkNonInput_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNonInput.Checked)
            {
                txtSubPCB.Enabled = false;
                txtSubPCB.Text = "-";
            }
            else
            {
                txtSubPCB.Enabled = true;
                txtSubPCB.Focus();
            }
        }

        private void chkPrivate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPrivate.Checked)
            {
                if (txtEOContents.Text != string.Empty)
                {
                    txtEOContents.Text += " (내부관리)";
                    
                    dtpEndDate.Value = dtpStartDate.Value;
                    //dtpEndDate.Enabled = false;
                }
            }
            else
            {
                if (txtEOContents.Text != string.Empty)
                {
                    txtEOContents.Text = MainForm.dc.PrivateTextDeleteConvert(txtEOContents.Text);

                    MainForm.dc.ShipmentDays(dtpStartDate, dtpEndDate, cbbShipment.Text);
                    //dtpEndDate.Enabled = true;
                }
            }
        }

        private void cbbShipment_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 2023.06.20
            // 내부 관리 체크 확인
            if (!chkPrivate.Checked)
            {
                MainForm.dc.ShipmentDays(dtpStartDate, dtpEndDate, cbbShipment.Text);
            }
        }

        private void lblEOType_Click(object sender, EventArgs e)
        {
#if DEBUG
            txtEOType.Enabled = true;
            txtEOType.ReadOnly = !txtEOType.ReadOnly;
#endif
        }

        private void lblCarName_Click(object sender, EventArgs e)
        {
#if DEBUG
            MessageBox.Show(txtCarName.CharacterCasing.ToString());
#endif
        }

        private void lblEOContents_Click(object sender, EventArgs e)
        {
#if DEBUG
            MessageBox.Show(txtEOContents.CharacterCasing.ToString());
#endif
        }
    }
}
