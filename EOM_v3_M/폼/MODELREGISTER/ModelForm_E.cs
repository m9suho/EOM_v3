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
using System.Runtime.InteropServices;
using Guna.UI2.WinForms;

namespace EOM_v3_M
{
    public partial class ModelForm_E : Form
    {
        const int OEM_DAY = 7;
        const int CKD_MONTH = 1;
        const string END_TIME = "08:30:00";

        MessageFormClass messageFormClass = new MessageFormClass();

        public static string subPCBData;
        public static string[,] originalData;

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
            btnProductModify.Enabled = _data[12];
            dtpStartDate.Enabled = _data[13];
            dtpEndDate.Enabled = _data[14];
        }

        private string ConvertUpdateColumnData(string[] _data)
        {
            string returnData =
                "car_name = '" +                _data[2]        + "', " +
                "customer_eo_number = '" +      _data[3]        + "', " +
                "mobis_eo_number = '" +         _data[4]        + "', " +
                "eo_contents = '" +             _data[5]        + "', " +
                "sticker_color = '" +           _data[6]        + "', " +
                "sticker_text = '" +            _data[7]        + "', " +
                "start_date = '" +              _data[8]        + "', " +
                "end_date = '" +                _data[9]        + "', " +
                //"registrant = '" +              _data[10]       + "', " +
                "print_type = '" +              _data[11]       + "', " +
                "shipment = '" +                _data[12]       + "', " +
                "mht_data = '" +                _data[13]       + "', " +
                "main_pcb_name = '" +           _data[14]       + "', " +
                "sub_pcb_name = '" +            _data[15]       + "', " +
                "eo_type = '" +                 _data[16]       + "'";

            return returnData;
        }

        private string[] DatabaseColumnData(int _data1)
        {
            string startDatetimeData, endDatetimeData = string.Empty;
            string[] returnData;

            TimeSpan startTime = new TimeSpan(00, 00, 00);
            TimeSpan endTime = new TimeSpan(08, 30, 00);
            TimeSpan realTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            // 시작일 날짜
            //startDatetimeData = dateTimePicker1.Value.Date < DateTime.Now.Date ? dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00") : DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            startDatetimeData = dtpStartDate.Value.ToString("yyyy-MM-dd 00:00:" + dtpStartDate.Value.ToString("ss"));

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

            // 2023.01.02
            // 변경 값이 적용 오류 수정
            //string stickerColorCheck, stickerTextCheck = string.Empty;
            
            switch (_data1)
            {
                case 1:
                    returnData = new string[]
                    {
                        MainForm.cbbLineName01.Text,                                                                                       // [0] line
                        txtProductName.Text,                                                                                                  // [1] model_name
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
                        "-",                                                                                                                // [12] shipment
                        MainForm.eoSelectData[7],                                                                                           // [13] mht_data
                        "-",                                                                                                                // [14] main_pcb_name
                        "-",                                                                                                                // [15] sub_pcb_name
                        "-",                                                                                                                // [16] eo_type
                    };
                    break;
                case 2:
                    returnData = new string[]
                    {
                        MainForm.cbbLineName01.Text,                                                                                       // [0] line
                        txtProductName.Text,                                                                                                  // [1] model_name
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
                        cbbShipment.Text,                                                                                                     // [12] shipment
                        MainForm.eoSelectData[7],                                                                                           // [13] mht_data
                        txtMainPCB.Text,                                                                                                    // [14] main_pcb_name
                        txtSubPCB.Text,                                                                                                     // [15] sub_pcb_name
                        txtEOType.Text,                                                                                                     // [16] eo_type
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

        private bool HyphenCheckSum(Guna.UI2.WinForms.Guna2TextBox textBox)
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
            string query = MainForm.dc.SelectDeleteQueryANDConvert(MainForm.settingData[0], MainForm.settingData[1], selectData, "SELECT");
            string[,] columnData = MainForm.mariaDB.SelectQuery2(query);

            if (columnData.GetLength(0) > 0)
                return true;
            else
                return false;
        }

        private bool ModelAddCheckSum()
        {
            if (txtProductName.Text.Equals(string.Empty))
            {
                MainForm.Guna2Msg("오류", "모델명을 정확히 입력해주세요 [공란 확인]");
                txtProductName.Select();
                return false;
            }
            if (HyphenCheckSum(txtProductName))
            {
                MainForm.Guna2Msg("오류", "모델명을 정확히 입력해주세요 [하이픈 1개만 입력 가능]");
                txtProductName.SelectAll();
                return false;
            }
            // 2020.05.25
            // 하이픈 위치 검사 조건 변경
            // 앞에 9가 올때만 가운데 검사
            if (!txtProductName.Text.Substring(5, 1).Equals("-") && txtProductName.Text.Substring(0, 1).Equals("9"))
            {
                MainForm.Guna2Msg("오류", "모델명을 정확히 입력해주세요 [하이픈 위치 확인]");
                txtProductName.SelectAll();
                return false;
            }
            if (txtCarName.Text.Equals(string.Empty))
            {
                MainForm.Guna2Msg("오류", "차종을 입력해주세요 [공란 확인]");
                return false;
            }
            // 2020.04.09
            // 클러스터 제외
            if (txtMobisEO.Text.Equals(string.Empty) && !MainForm.cbbLineName01.Text.Equals(MainForm.LINE_NAME_LIST[4]))
            {
                MainForm.Guna2Msg("오류", "EO 번호를 선택해주세요 [공란 확인]");
                return false;
            }
            /*
            if (comboBox2.Enabled && comboBox2.SelectedIndex == -1)
            {
                MainForm.Guna2Msg("오류", "생산지를 선택해주세요");
                comboBox2.Select();
                return false;
            }
            */
            if (dtpStartDate.Enabled && dtpStartDate.Value > DateTime.Now)
            {
                MainForm.Guna2Msg("오류", "현재 날짜보다 크게 설정할 수 없습니다");
                dtpStartDate.Select();
                return false;
            }
            /*
            // 공백, 하이픈, 4M 예외 처리
            if(txtMobisEO.Text.Equals(string.Empty) || txtMobisEO.Text.Equals("-") || txtMobisEO.Text.Equals("4M"))
            {
                // EO 내용 중복 검사 && 출하지 검사
                // 찾을 컬럼, 컬럼 값, 모델 값, 출하지
                if (ModelEODuplicationCheck("eo_contents", txtEOContents.Text, txtProductName.Text, txtShipment.Text))
                {
                    if (!txtShipment.Text.Equals("-"))
                    {
                        MainForm.Guna2Msg("오류", "EO 내용, 출하지 '" + cbbShipmentShipment.Text + "' 등록되어 있습니다");
                    }
                    else
                    {
                        MainForm.Guna2Msg("오류", "EO 내용이 등록되어 있습니다");
                    }
                    DGVSearchSelect(txtProductName.Text, txtEOContents.Text, 5, txtShipment.Text, 10);
                    return false;
                }
            }
            else
            {
                // EO 번호 중복 검사 && 출하지 검사
                // 찾을 컬럼, 컬럼 값, 모델 값, 출하지
                if (ModelEODuplicationCheck("mobis_eo_number", txtMobisEO.Text, txtModelName.Text, txtShipment.Text))
                {
                    if (!txtShipment.Text.Equals("-"))
                    {
                        MainForm.Guna2Msg("오류", "EO 번호, 출하지 '" + txtShipment.Text + "' 등록되어 있습니다");
                    }
                    else
                    {
                        MainForm.Guna2Msg("오류", "EO 번호가 등록되어 있습니다");
                    }
                    DGVSearchSelect(txtModelName.Text, txtMobisEO.Text, 4, txtShipment.Text, 10);
                    return false;
                }
            }
            */
            /*
            // 왜지?
            if (dt01 > dt02)
            {
                MainForm.Guna2Msg("오류", "마감일이 종료일보다 작을 수 없습니다");
                return false;
            }
            */
            return true;
        }

        public ModelForm_E(string[,] _data)
        {
            originalData = _data;
            InitializeComponent();
        }

        private void ModelForm_E_Load(object sender, EventArgs e)
        {
            bool[] controlBox;

            //DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 00, 00, 00);

            Text = MainForm.programName + " - " + MainForm.dc.Version();

            //dateTimePicker1.Value = dt;
            //dateTimePicker2.Value = dt.AddDays(OEM_DAY);
            // 2023.03.28

            cbbShipment.Items.Clear();

            for (int i = 0; i < MainForm.dc.Shipment.Length; i++)
            {
                cbbShipment.Items.Add(MainForm.dc.Shipment[i]);
            }

            // 2023.03.28
            MainForm.dc.InitialSetTextBox(this);

            // D-오디오 수삽, D-오디오 SUB
            if (MainForm.cbbLineName01.Text.Equals(MainForm.LINE_NAME_LIST[1]) || MainForm.cbbLineName01.Text.Equals(MainForm.LINE_NAME_LIST[3]))
            {
                txtProductName.Text = "M";
                txtProductName.Select(txtProductName.MaxLength + 1, 0);
                //dateTimePicker2.Value = dt;
                controlBox = new bool[] { //false, false, false, false, false,
                                          false, true, false, true, false,
                                          false, false, false, false, true,
                                          false, true, true, true, true };
                InitialSetControl(controlBox);
            }
            // D-오디오 조립
            else if (MainForm.cbbLineName01.Text.Equals(MainForm.LINE_NAME_LIST[2]))
            {
                txtProductName.Text = string.Empty;
                //dateTimePicker2.Value = dt.AddDays(7);
                controlBox = new bool[] { false, true, true, true, true,
                                          false, false, false, false, true,
                                          true, true, true, true, true };
                InitialSetControl(controlBox);
            }
            // 클러스터, HUD
            else
            {
                txtProductName.Text = string.Empty;
                //dateTimePicker2.Value = dt.AddDays(7);
                controlBox = new bool[] { false, true, true, true, true,
                                          false, false, false, false, true,
                                          true, true, true, true, true };
                InitialSetControl(controlBox);
            }

            // [00] line
            // [01] model_name
            // [02] car_name
            // [03] customer_eo_number
            // [04] mobis_eo_number
            // [05] eo_contents
            // [06] sticker_color
            // [07] sticker_text
            // [08] start_date
            // [09] end_date
            // [10] registrant
            // [11] print_type
            // [12] shipment
            // [13] mht_data
            // [14] main_pcb_name
            // [15] sub_pcb_name
            // [16] eo_type
            // [17] start_order

            // 2021.09.03
            // 기존 값 삽입
            // [01] model_name
            // [02] car_name
            // [03] customer_eo_number
            // [04] mobis_eo_number
            // [05] eo_contents
            // [06] sticker_color
            // [07] sticker_text
            // [11] print_type
            // [13] mht_data
            // [14] main_pcb_name
            txtProductName.Text = originalData[0, 1];
            txtCarName.Text = originalData[0, 2];
            txtCustomerEO.Text = originalData[0, 3];
            txtMobisEO.Text = originalData[0, 4];
            txtEOContents.Text = originalData[0, 5];
            MainForm.eoSelectData[4] = originalData[0, 6];
            MainForm.eoSelectData[5] = originalData[0, 7];
            txtTagType.Text = originalData[0, 11];
            cbbShipment.Text = originalData[0, 12];
            MainForm.eoSelectData[7] = originalData[0, 13];
            txtMainPCB.Text = originalData[0, 14];
            txtEOType.Text = originalData[0, 16];

            // [02] car_name
            txtCarName.Select(txtCarName.TextLength, 0);

            // [05] eo_contents
            if (originalData[0, 5].Contains("내부관리") ||
                originalData[0, 5].Contains("내부 관리") ||
                originalData[0, 5].Contains("내부공유") ||
                originalData[0, 5].Contains("내부 공유"))
            {
                chkPrivate.Checked = true;
            }
            else
            {
                chkPrivate.Checked = false;
            }

            // [06] sticker_color
            // [07] sticker_text
            txtStickerColor.ForeColor = Color.FromName(MainForm.eoSelectData[4]);
            txtStickerColor.Text = "● " + MainForm.eoSelectData[5];

            // [08] start_date
            DateTime dt = Convert.ToDateTime(originalData[0, 8]);

            // [09] end_date
            dtpStartDate.Value = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
            dtpEndDate.Value = Convert.ToDateTime(originalData[0, 9]);

            // 2021.01.12
            // [12] shipment
            MainForm.dc.ComboBoxDrawAlignCenter(cbbShipment);

            // [13] mht_data
            if (originalData[0, 13] == string.Empty)
            {
                btnMthView.Enabled = false;
            }
            else
            {
                btnMthView.Enabled = true;
            }

            // [15] sub_pcb_name
            if (originalData[0, 15] == "-")
            {
                chkNonInput.Checked = true;
            }
            else
            {
                chkNonInput.Checked = false;
                txtSubPCB.Text = originalData[0, 15];
            }

            // 2023.03.24
            // Guna UI 강제 위치 적용
            Location = MainForm.dc.CenterLocation(Width, Height);
        }

        private void ModelForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 2020.09.25
            // EO 등록 데이터 무조건 초기화
            for (int i = 0; i < MainForm.eoSelectData.Length; i++)
            {
                MainForm.eoSelectData[i] = string.Empty;
            }
        }

        private void txtCarName_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC 키 폼 종료
            if (e.KeyValue == 27)
            {
                Close();
            }
        }

        private void txtCarName_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 2022.02.10
            MainForm.dc.NumAlphaSpaceString((Guna2TextBox)sender, e);
        }

        private void txtMainPCB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (MainForm.cbbLineName01.Text != "HUD")
            {
                // 2022.02.10
                MainForm.dc.NumAlphaString((Guna2TextBox)sender, e);
            }
        }

        private void txtSubPCB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (MainForm.cbbLineName01.Text != "HUD")
            {
                // 2022.02.10
                MainForm.dc.NumAlphaString((Guna2TextBox)sender, e);
            }
        }

        // 2023.03.28
        // 텍스트박스 이벤트 통합
        private void txtAddHyphenCheckFirstChar_KeyUp(object sender, KeyEventArgs e)
		{
            MainForm.dc.AddHyphenCheckFirstChar((Guna2TextBox)sender, e);
        }
        
        private void txtCarName_KeyUp(object sender, KeyEventArgs e)
        {
            // 2021.09.04
            MainForm.dc.CarNameUpperConvert((Guna2TextBox)sender);
        }

        private void txtMainPCB_KeyUp(object sender, KeyEventArgs e)
        {
            // 2022.02.10
            MainForm.dc.AddHyphenCheckFirstChar((Guna2TextBox)sender, e);
        }

        private void txtSubPCB_KeyUp(object sender, KeyEventArgs e)
        {
            // 2022.02.10
            MainForm.dc.AddHyphenCheckFirstChar((Guna2TextBox)sender, e);
        }


        private void txtCarName_TextChanged(object sender, EventArgs e)
        {
            if (((Guna2TextBox)sender).Text != originalData[0, 2])
            {
                ((Guna2TextBox)sender).ForeColor = Color.Green;
            }
            else
            {
                ((Guna2TextBox)sender).ForeColor = SystemColors.WindowText;
            }
        }

        private void txtMainPCB_TextChanged(object sender, EventArgs e)
        {
            if (((Guna2TextBox)sender).Enabled)
            {
                /*
                if (((Guna2TextBox)sender).TextLength < 3 || ((Guna2TextBox)sender).Text.Substring(0, 1) != "M")
                {
                    ((Guna2TextBox)sender).Text = "M15";
                    ((Guna2TextBox)sender).Select(((Guna2TextBox)sender).MaxLength + 1, 0);
                }
                */

                if (MainForm.cbbLineName01.Text != "HUD" && (((Guna2TextBox)sender).TextLength < 3 || ((Guna2TextBox)sender).Text.Substring(0, 1) != "M"))
                {
                    MainPCBInitialValue((Guna2TextBox)sender);

                    ((Guna2TextBox)sender).Select(((Guna2TextBox)sender).MaxLength + 1, 0);
                }

                // 다를 경우 색깔 변경
                if (((Guna2TextBox)sender).Text != originalData[0, 14])
                {
                    ((Guna2TextBox)sender).ForeColor = Color.Green;
                }
                else
                {
                    ((Guna2TextBox)sender).ForeColor = SystemColors.WindowText;
                }
            }
        }

        private void txtSubPCB_TextChanged(object sender, EventArgs e)
        {
            if (((Guna2TextBox)sender).Enabled && (((Guna2TextBox)sender).TextLength < 3 || ((Guna2TextBox)sender).Text.Substring(0, 1) != "M"))
            {
                // 2020.12.14
                // D-오디오 조립만 M17
                // 그 외 M15
                if (MainForm.cbbLineName01.Text == MainForm.LINE_NAME_LIST[2])
                {
                    //((Guna2TextBox)sender).Text = "M17";
                }
                else
                {
                    //((Guna2TextBox)sender).Text = "M15";
                }

                ((Guna2TextBox)sender).Select(((Guna2TextBox)sender).MaxLength + 1, 0);
            }

            if (((Guna2TextBox)sender).Text != originalData[0, 15])
            {
                ((Guna2TextBox)sender).ForeColor = Color.Green;
            }
            else
            {
                ((Guna2TextBox)sender).ForeColor = SystemColors.WindowText;
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

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            // 2020.12.29
            // 내부관리 항목 추가로 조건 추가
            // 2021.09.04
            // 공백이 아닐 경우
            if (!chkPrivate.Checked && txtProductName.Text != string.Empty)
            {
                if (dtpStartDate.Value > dtpEndDate.Value)
                {
                    MainForm.Guna2Msg("오류", "적용일보다 작을 수 없습니다");
                    MainForm.dc.ShipmentDays(dtpStartDate, dtpEndDate, cbbShipment.Text);
                    return;
                }

                if (cbbShipment.Text == "CKD" || cbbShipment.Text == "KD")
                {
                    DateTime firstDateTime = dtpStartDate.Value.AddMonths(1);

                    if (firstDateTime > dtpEndDate.Value)
                    {
						MainForm.Guna2Msg("오류", "적용일보다 작을 수 없습니다 [CKD, KD : 최소 " + CKD_MONTH + "달]");
                        MainForm.dc.ShipmentDays(dtpStartDate, dtpEndDate, cbbShipment.Text);
                        return;
                    }
                }

                if (cbbShipment.Text == "OEM")
                {
                    DateTime firstDateTime = dtpStartDate.Value.AddDays(OEM_DAY);

                    if (firstDateTime > dtpEndDate.Value)
                    {
                        MainForm.Guna2Msg("오류", "적용일보다 작을 수 없습니다 [OEM, 최소 " + OEM_DAY + "일]");
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
                MainForm.Guna2Msg("오류", "품번을 정확히 입력해주세요 [공란 확인]");
                txtProductName.Select();
                return;
            }

            for (int i = 0; i < s.GetLength(0); i++)
            {
                if (MainForm.cbbLineName01.Text == s[i, 0] && txtProductName.Text.Length < Convert.ToInt32(s[i, 1]))
                {
                    MainForm.Guna2Msg("오류", "품번을 정확히 입력해주세요 [" + s[i, 1] + "자 이상]");
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
            chkPrivate.Checked = false;

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

                MainForm.dc.ShipmentDays(dtpStartDate, dtpEndDate, cbbShipment.Text);
            }





            Opacity = 1;
        }

        private void btnProductModify_Click(object sender, EventArgs e)
        {
            if (ModelAddCheckSum())
            {
                string query, printData = string.Empty;
                string[] updateData;

                // D-오디오 수삽
                // D-오디오 SUB
                if (MainForm.cbbLineName01.Text.Equals(MainForm.LINE_NAME_LIST[1]) || MainForm.cbbLineName01.Text.Equals(MainForm.LINE_NAME_LIST[3]))
                {
                    updateData = DatabaseColumnData(1);
                }
                // 오디오 플랫폼, D-오디오 조립, 클러스터, HUD
                else
                {
                    updateData = DatabaseColumnData(2);
                }

                //query = MainForm.dc.InsertQueryArrayConvert(MainForm.settingData[0], MainForm.settingData[1], insertData);
                query = @"UPDATE `" + MainForm.settingData[0] + "`.`" + MainForm.settingData[1] + "` SET " + ConvertUpdateColumnData(updateData) + " WHERE " +
                    "line = '" + originalData[0, 0] + "' AND " +
                    "model_name = '" + originalData[0, 1] + "' AND " +
                    "car_name = '" + originalData[0, 2] + "' AND " +
                    "customer_eo_number = '" + originalData[0, 3] + "' AND " +
                    "mobis_eo_number = '" + originalData[0, 4] + "' AND " +
                    "eo_contents = '" + originalData[0, 5] + "' AND " +
                    "sticker_color = '" + originalData[0, 6] + "' AND " +
                    "sticker_text = '" + originalData[0, 7] + "' AND " +
                    "start_date = '" + Convert.ToDateTime(originalData[0, 8]).ToString("yyyy-MM-dd HH:mm:ss") + "' AND " +
                    "end_date = '" + Convert.ToDateTime(originalData[0, 9]).ToString("yyyy-MM-dd HH:mm:ss") + "' AND " +
                    "registrant = '" + originalData[0, 10] + "' AND " +
                    "print_type = '" + originalData[0, 11] + "' AND " +
                    "shipment = '" + originalData[0, 12] + "' AND " +
                    //"mht_data = '" + originalData[0, 13] + "' AND " +
                    "main_pcb_name = '" + originalData[0, 14] + "' AND " +
                    "sub_pcb_name = '" + originalData[0, 15] + "' AND " +
                    "eo_type = '" + originalData[0, 16] + "'";
                    //"start_order = '" + originalData[0, 17] + "'"; // NULL 값 인식 못함

                Clipboard.SetText(query);
                MainForm.mariaDB.EtcQuery(query);

                Opacity = 0;
                MainForm.modelSelectData = txtProductName.Text;

                // 2020.05.06
                if (!MainForm.eoSelectData[7].Equals(string.Empty))
                {
                    updateData[13] = "O";
                }
                else
                {
                    updateData[13] = "X";
                }

                MainForm.mariaDB.InsertLogDB(MainForm.SplitConvert(updateData) + " 모델 수정", false);

                Close();
            }
            else
            {
                return;
            }

            // 2021.09.03
            // EO 등록 데이터 무조건 초기화
            for (int i = 0; i < MainForm.eoSelectData.Length; i++)
            {
                MainForm.eoSelectData[i] = string.Empty;
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
            //MainPCBInitialValue(txtMainPCB);

            txtMainPCB.Select(txtMainPCB.MaxLength + 1, 0);
            txtMainPCB.FillColor = Color.LemonChiffon;
        }

        private void txtSubPCB_Enter(object sender, EventArgs e)
        {
            /*
            // 2020.12.14
            // D-오디오 조립만 M17
            // 그 외 M15
            if (MainForm.cbbLineName01.Text == MainForm.LINE_NAME_LIST[2])
            {
                txtSubPCB.Text = "M17";
            }
            else
            {
                txtSubPCB.Text = "M15";
            }
            */
            //txtSubPCB.Select(txtSubPCB.TextLength, txtSubPCB.TextLength);
            //txtSubPCB.SelectAll();
            txtSubPCB.Select(txtSubPCB.MaxLength + 1, 0);
            txtSubPCB.FillColor = Color.LemonChiffon;
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
                subPCBData = txtSubPCB.Text;
                txtSubPCB.Enabled = false;
                txtSubPCB.Text = "-";
            }
            else
            {
                txtSubPCB.Text = subPCBData;
                txtSubPCB.Enabled = true;
                txtSubPCB.Focus();
            }
        }

        private void chkPrivate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPrivate.Checked)
            {
                if (!txtEOContents.Text.Contains("내부관리") &&
                    !txtEOContents.Text.Contains("내부 관리") &&
                    !txtEOContents.Text.Contains("내부공유") &&
                    !txtEOContents.Text.Contains("내부 공유"))
                //if (txtEOContents.Text != string.Empty)
                {
                    txtEOContents.Text += " (내부관리)";
                    
                    dtpEndDate.Value = dtpStartDate.Value;
                    dtpEndDate.Enabled = false;
                }
            }
            else
            {
                if (txtEOContents.Text != string.Empty)
                {
                    txtEOContents.Text = MainForm.dc.PrivateTextDeleteConvert(txtEOContents.Text);

                    MainForm.dc.ShipmentDays(dtpStartDate, dtpEndDate, cbbShipment.Text);
                    dtpEndDate.Enabled = true;
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

        private void lblEOContents_Click(object sender, EventArgs e)
        {

        }
    }
}
