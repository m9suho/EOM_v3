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

namespace EOM_v3_M
{
    public partial class ModelForm_E : MetroFramework.Forms.MetroForm
    {
        const int OEM_DAY = 7;
        const int CKD_MONTH = 1;
        const string END_TIME = "08:30:00";

        public static string subPCBData;
        public static string[,] editData;

        private void MainPCBInitialValue(TextBox _textBox)
        {
            if (MainForm.cbbMetroLine01.Text != "HUD")
            {
                _textBox.Text = "M15";
            }
        }

        private void InitialSetControl(bool[] _data)
        {
            txtModelName.Enabled = _data[0];
            txtCarName.Enabled = _data[1];
            txtMainPCB.Enabled = _data[2];
            txtSubPCB.Enabled = _data[3];
            chkNonInput.Enabled = _data[4];
            txtTagType.Enabled = _data[5];
            txtCustomerEO.Enabled = _data[6];
            txtMobisEO.Enabled = _data[7];
            txtEOContents.Enabled = _data[8];
            chkPrivate.Enabled = _data[9];
            comboBox1.Enabled = _data[10];
            SelectMetroBtn.Enabled = _data[11];
            AddMetroBtn.Enabled = _data[12];
            dateTimePicker1.Enabled = _data[13];
            dateTimePicker2.Enabled = _data[14];
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
            startDatetimeData = dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:" + dateTimePicker1.Value.ToString("ss"));

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
            dateTimePicker2.Value = dateTimePicker2.Value.AddDays(1);

            endDatetimeData = dateTimePicker2.Value.ToString("yyyy-MM-dd " + END_TIME);

            switch (_data1)
            {
                case 1:
                    returnData = new string[]
                    {
                        MainForm.cbbMetroLine01.Text,                                                                                       // [0] line
                        txtModelName.Text,                                                                                                  // [1] model_name
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
                        MainForm.cbbMetroLine01.Text,                                                                                       // [0] line
                        txtModelName.Text,                                                                                                  // [1] model_name
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
                        comboBox1.Text,                                                                                                   // [12] shipment
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

        private bool HyphenCheckSum(TextBox textBox)
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
            MainForm.cbbMetroProduct01.SelectedItem = _data0;

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
            string[] selectData = new string[] { "line", MainForm.cbbMetroLine01.Text, _column, _data01, "model_name", _data02, "shipment", _data03 };
            string query = MainForm.dc.SelectDeleteQueryANDConvert(MainForm.settingData[0], MainForm.settingData[1], selectData, "SELECT");
            string[,] columnData = MainForm.mariaDB.SelectQuery2(query);

            if (columnData.GetLength(0) > 0)
                return true;
            else
                return false;
        }

        private bool ModelAddCheckSum()
        {
            if (txtModelName.Text.Equals(string.Empty))
            {
                MainForm.dc.Msg("경고", "모델명을 정확히 입력해주세요 [공란 확인]");
                txtModelName.Select();
                return false;
            }
            if (HyphenCheckSum(txtModelName))
            {
                MainForm.dc.Msg("경고", "모델명을 정확히 입력해주세요 [하이픈 1개만 입력 가능]");
                txtModelName.SelectAll();
                return false;
            }
            // 2020.05.25
            // 하이픈 위치 검사 조건 변경
            // 앞에 9가 올때만 가운데 검사
            if (!txtModelName.Text.Substring(5, 1).Equals("-") && txtModelName.Text.Substring(0, 1).Equals("9"))
            {
                MainForm.dc.Msg("경고", "모델명을 정확히 입력해주세요 [하이픈 위치 확인]");
                txtModelName.SelectAll();
                return false;
            }
            if (txtCarName.Text.Equals(string.Empty))
            {
                MainForm.dc.Msg("경고", "차종을 입력해주세요 [공란 확인]");
                return false;
            }
            // 2020.04.09
            // 클러스터 제외
            if (txtMobisEO.Text.Equals(string.Empty) && !MainForm.cbbMetroLine01.Text.Equals(MainForm.lineData[4]))
            {
                MainForm.dc.Msg("경고", "EO 번호를 선택해주세요 [공란 확인]");
                return false;
            }
            /*
            if (comboBox2.Enabled && comboBox2.SelectedIndex == -1)
            {
                MainForm.dc.Msg("경고", "생산지를 선택해주세요");
                comboBox2.Select();
                return false;
            }
            */
            if (dateTimePicker1.Enabled && dateTimePicker1.Value > DateTime.Now)
            {
                MainForm.dc.Msg("경고", "현재 날짜보다 크게 설정할 수 없습니다");
                dateTimePicker1.Select();
                return false;
            }
            /*
            // 공백, 하이픈, 4M 예외 처리
            if(txtMobisEO.Text.Equals(string.Empty) || txtMobisEO.Text.Equals("-") || txtMobisEO.Text.Equals("4M"))
            {
                // EO 내용 중복 검사 && 출하지 검사
                // 찾을 컬럼, 컬럼 값, 모델 값, 출하지
                if (ModelEODuplicationCheck("eo_contents", txtEOContents.Text, txtModelName.Text, txtShipment.Text))
                {
                    if (!txtShipment.Text.Equals("-"))
                    {
                        MainForm.dc.Msg("경고", "EO 내용, 출하지 '" + txtShipment.Text + "' 등록되어 있습니다");
                    }
                    else
                    {
                        MainForm.dc.Msg("경고", "EO 내용이 등록되어 있습니다");
                    }
                    DGVSearchSelect(txtModelName.Text, txtEOContents.Text, 5, txtShipment.Text, 10);
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
                        MainForm.dc.Msg("경고", "EO 번호, 출하지 '" + txtShipment.Text + "' 등록되어 있습니다");
                    }
                    else
                    {
                        MainForm.dc.Msg("경고", "EO 번호가 등록되어 있습니다");
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
                MainForm.dc.Msg("경고", "마감일이 종료일보다 작을 수 없습니다");
                return false;
            }
            */
            return true;
        }

        public ModelForm_E(string[,] _data)
        {
            editData = _data;
            InitializeComponent();
        }

        private void ModelForm_E_Load(object sender, EventArgs e)
        {
            bool[] controlBox;

            //DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 00, 00, 00);

            Text = MainForm.programName + " - " + MainForm.dc.Version();

            //dateTimePicker1.Value = dt;
            //dateTimePicker2.Value = dt.AddDays(OEM_DAY);

            // D-오디오 수삽, D-오디오 SUB
            if (MainForm.cbbMetroLine01.Text.Equals(MainForm.lineData[1]) || MainForm.cbbMetroLine01.Text.Equals(MainForm.lineData[3]))
            {
                txtModelName.Text = "M";
                txtModelName.Select(txtModelName.MaxLength + 1, 0);
                //dateTimePicker2.Value = dt;
                controlBox = new bool[] { //false, false, false, false, false,
                                          false, true, false, true, false,
                                          false, false, false, false, true,
                                          false, true, true, true, true };
                InitialSetControl(controlBox);
            }
            // D-오디오 조립
            else if (MainForm.cbbMetroLine01.Text.Equals(MainForm.lineData[2]))
            {
                txtModelName.Text = string.Empty;
                //dateTimePicker2.Value = dt.AddDays(7);
                controlBox = new bool[] { false, true, true, true, true,
                                          false, false, false, false, true,
                                          true, true, true, true, true };
                InitialSetControl(controlBox);
            }
            // 클러스터, HUD
            else
            {
                txtModelName.Text = string.Empty;
                //dateTimePicker2.Value = dt.AddDays(7);
                controlBox = new bool[] { false, true, true, true, true,
                                          false, false, false, false, true,
                                          true, true, true, true, true };
                InitialSetControl(controlBox);
            }

            // 2021.09.03
            // 기존 값 삽입
            txtModelName.Text = editData[0, 1];
            txtCarName.Text = editData[0, 2];

            txtMainPCB.Text = editData[0, 14];

            if (editData[0, 15] == "-")
            {
                chkNonInput.Checked = true;
            }
            else
            {
                chkNonInput.Checked = false;
                txtSubPCB.Text = editData[0, 15];
            }

            txtTagType.Text = editData[0, 11];
            txtCustomerEO.Text = editData[0, 3];
            txtMobisEO.Text = editData[0, 4];

            if (editData[0, 13] == string.Empty)
            {
                ViewMetroBtn.Enabled = false;
            }
            else
            {
                ViewMetroBtn.Enabled = true;
            }

            MainForm.eoSelectData[7] = editData[0, 13];

            if (editData[0, 5].Contains("내부관리") || editData[0, 5].Contains("내부 관리") || editData[0, 5].Contains("내부공유") || editData[0, 5].Contains("내부 공유"))
            {
                chkPrivate.Checked = true;
            }
            else
            {
                chkPrivate.Checked = false;
            }

            txtEOContents.Text = editData[0, 5];

            lblSticker.ForeColor = Color.FromName(editData[0, 6]);
            lblSticker.Text = "● " + editData[0, 7];

            comboBox1.Text = editData[0, 12];
            txtEOType.Text = editData[0, 16];

            DateTime dt = Convert.ToDateTime(editData[0, 8]);

            dateTimePicker1.Value = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
            dateTimePicker2.Value = Convert.ToDateTime(editData[0, 9]);

            txtCarName.Select(txtCarName.TextLength, 0);

            // 2021.01.12
            MainForm.dc.ComboBoxDrawAlignCenter(comboBox1);
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


        private void txtCarName_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 2022.02.10
            MainForm.dc.NumAlphaSpaceString((TextBox)sender);
        }

        private void txtMainPCB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (MainForm.cbbMetroLine01.Text != "HUD")
            {
                // 2022.02.10
                MainForm.dc.NumAlphaString((TextBox)sender, e);
            }
        }

        private void txtSubPCB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (MainForm.cbbMetroLine01.Text != "HUD")
            {
                // 2022.02.10
                MainForm.dc.NumAlphaString((TextBox)sender, e);
            }
        }


        private void txtCarName_KeyUp(object sender, KeyEventArgs e)
        {
            // 2021.09.04
            MainForm.dc.CarNameUpperConvert((TextBox)sender);
        }

        private void txtMainPCB_KeyUp(object sender, KeyEventArgs e)
        {
            // 2022.02.10
            MainForm.dc.AddHyphenCheckFirstChar((TextBox)sender, e);
        }

        private void txtSubPCB_KeyUp(object sender, KeyEventArgs e)
        {
            // 2022.02.10
            MainForm.dc.AddHyphenCheckFirstChar((TextBox)sender, e);
        }


        private void txtCarName_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text != editData[0, 2])
            {
                ((TextBox)sender).ForeColor = Color.Green;
            }
            else
            {
                ((TextBox)sender).ForeColor = SystemColors.WindowText;
            }
        }

        private void txtMainPCB_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Enabled)
            {
                /*
                if (((TextBox)sender).TextLength < 3 || ((TextBox)sender).Text.Substring(0, 1) != "M")
                {
                    ((TextBox)sender).Text = "M15";
                    ((TextBox)sender).Select(((TextBox)sender).MaxLength + 1, 0);
                }
                */

                if (MainForm.cbbMetroLine01.Text != "HUD" && (((TextBox)sender).TextLength < 3 || ((TextBox)sender).Text.Substring(0, 1) != "M"))
                {
                    MainPCBInitialValue(((TextBox)sender));

                    ((TextBox)sender).Select(((TextBox)sender).MaxLength + 1, 0);
                }

                // 다를 경우 색깔 변경
                if (((TextBox)sender).Text != editData[0, 14])
                {
                    ((TextBox)sender).ForeColor = Color.Green;
                }
                else
                {
                    ((TextBox)sender).ForeColor = SystemColors.WindowText;
                }
            }
        }

        private void txtSubPCB_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Enabled && (((TextBox)sender).TextLength < 3 || ((TextBox)sender).Text.Substring(0, 1) != "M"))
            {
                // 2020.12.14
                // D-오디오 조립만 M17
                // 그 외 M15
                if (MainForm.cbbMetroLine01.Text == MainForm.lineData[2])
                {
                    ((TextBox)sender).Text = "M17";
                }
                else
                {
                    //((TextBox)sender).Text = "M15";
                }

                ((TextBox)sender).Select(((TextBox)sender).MaxLength + 1, 0);
            }

            if (((TextBox)sender).Text != editData[0, 15])
            {
                ((TextBox)sender).ForeColor = Color.Green;
            }
            else
            {
                ((TextBox)sender).ForeColor = SystemColors.WindowText;
            }
        }


        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (!chkPrivate.Checked)
            {
                MainForm.dc.ShipmentDays(dateTimePicker1, dateTimePicker2, comboBox1.Text);
            }
            else
            {
                dateTimePicker2.Value = dateTimePicker1.Value;
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            // 2020.12.29
            // 내부관리 항목 추가로 조건 추가
            // 2021.09.04
            // 공백이 아닐 경우
            if (!chkPrivate.Checked && txtModelName.Text != string.Empty)
            {
                if (dateTimePicker1.Value > dateTimePicker2.Value)
                {
                    MainForm.dc.Msg("경고", "적용일보다 작을 수 없습니다");
                    MainForm.dc.ShipmentDays(dateTimePicker1, dateTimePicker2, comboBox1.Text);
                    return;
                }

                if (comboBox1.Text == "CKD")
                {
                    DateTime firstDateTime = dateTimePicker1.Value.AddMonths(1);

                    if (firstDateTime > dateTimePicker2.Value)
                    {
                        MainForm.dc.Msg("경고", "적용일보다 작을 수 없습니다 [CKD, 최소 " + CKD_MONTH + "달]");
                        MainForm.dc.ShipmentDays(dateTimePicker1, dateTimePicker2, comboBox1.Text);
                        return;
                    }
                }

                if (comboBox1.Text == "OEM")
                {
                    DateTime firstDateTime = dateTimePicker1.Value.AddDays(OEM_DAY);

                    if (firstDateTime > dateTimePicker2.Value)
                    {
                        MainForm.dc.Msg("경고", "적용일보다 작을 수 없습니다 [OEM, 최소 " + OEM_DAY + "일]");
                        MainForm.dc.ShipmentDays(dateTimePicker1, dateTimePicker2, comboBox1.Text);
                        return;
                    }
                }
            }
        }

        private void SelectMetroBtn_Click(object sender, EventArgs e)
        {
            string[,] s = new string[,]
            {
                { "오디오 플랫폼" , "11" },
                { "D-오디오 수삽" , "11" },
                { "D-오디오 조립" , "11" },
                { "D-오디오 SUB" , "11" },
                { "클러스터" , "11" },
                { "HUD" , "10" },
            };

            if (txtModelName.Text.Equals(string.Empty))
            {
                MainForm.dc.Msg("경고", "품번을 정확히 입력해주세요 [공란 확인]");
                txtModelName.Select();
                return;
            }

            for (int i = 0; i < s.GetLength(0); i++)
            {
                if (MainForm.cbbMetroLine01.Text == s[i, 0] && txtModelName.Text.Length < Convert.ToInt32(s[i, 1]))
                {
                    MainForm.dc.Msg("경고", "품번을 정확히 입력해주세요 [" + s[i, 1] + "자 이상]");
                    txtModelName.Select();
                    return;
                }
            }

            // 2022.03.10
            // 수삽, SUB 제외
            if (MainForm.cbbMetroLine01.Text != "D-오디오 수삽" && MainForm.cbbMetroLine01.Text != "D-오디오 SUB")
            {
                if (MainForm.dc.PCBProductNameCheck(txtMainPCB, "메인 PCB") || (!chkNonInput.Checked && MainForm.dc.PCBProductNameCheck(txtSubPCB, "서브 PCB")) || MainForm.dc.CarNameCheck(txtCarName))
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
                    lblSticker.ForeColor = Color.FromName(MainForm.eoSelectData[4]);
                    lblSticker.Text = "● " + MainForm.eoSelectData[5];
                }
                else
                {
                    lblSticker.ForeColor = Color.Black;
                    lblSticker.Text = string.Empty;
                }

                // 2021.01.12
                if (txtEOContents.Text != string.Empty)
                {
                    chkPrivate.Enabled = true;
                }

                // D-오디오 SUB 필요 없음으로 공란
                if (!MainForm.cbbMetroLine01.Text.Equals(MainForm.lineData[3]))     // 출하지
                {
                    comboBox1.Text = MainForm.eoSelectData[6];
                }
                else
                {
                    comboBox1.Text = "-";
                }

                // 작업지시변경서 확인
                if (MainForm.eoSelectData[7] != null && !MainForm.eoSelectData[7].Equals(string.Empty))
                {
                    ViewMetroBtn.Enabled = true;
                }
                else
                {
                    ViewMetroBtn.Enabled = false;
                }

                if (!txtEOContents.Text.Equals(string.Empty))
                {
                    txtModelName.Enabled = false;
                    txtCarName.Enabled = false;

                    dateTimePicker1.Enabled = true;
                    dateTimePicker2.Enabled = true;
                }

                // 2021.01.21
                // EO 구분
                txtEOType.Text = MainForm.eoSelectData[8];

                MainForm.dc.ShipmentDays(dateTimePicker1, dateTimePicker2, comboBox1.Text);
            }

            // 2021.09.04
            // 시간 보정
            //dateTimePicker2.Value = new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day, 8, 30, 0);

            Opacity = 1;
        }

        private void AddMetroBtn_Click(object sender, EventArgs e)
        {
            if (ModelAddCheckSum())
            {
                string query, printData = string.Empty;
                string[] updateData;

                // D-오디오 수삽
                // D-오디오 SUB
                if (MainForm.cbbMetroLine01.Text.Equals(MainForm.lineData[1]) || MainForm.cbbMetroLine01.Text.Equals(MainForm.lineData[3]))
                {
                    updateData = DatabaseColumnData(1);
                }
                // 오디오 플랫폼, D-오디오 조립, 클러스터, HUD
                else
                {
                    updateData = DatabaseColumnData(2);
                }

                //query = MainForm.dc.InsertQueryArrayConvert(MainForm.settingData[0], MainForm.settingData[1], insertData);
                query = @"UPDATE `" + MainForm.settingData[0] + "`.`" + MainForm.settingData[1] + "` SET " + ConvertUpdateColumnData(updateData) + " WHERE line = '" + editData[0, 0] + "' AND model_name = '" + editData[0, 1] + "' AND car_name = '" + editData[0, 2] + "' AND customer_eo_number = '" + editData[0, 3] + "' AND mobis_eo_number = '" + editData[0, 4] + "' AND eo_contents = '" + editData[0, 5] + "'";
                MainForm.mariaDB.EtcQuery(query);
                //Clipboard.SetText(query);

                Opacity = 0;
                MainForm.modelSelectData = txtModelName.Text;

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

        private void ViewMetroBtn_Click(object sender, EventArgs e)
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
            MainForm.subjectData = mhtSearchData[0];

            // mht 경로
            MainForm.eoViewData = tmpPath;

            ViewForm viewForm = new ViewForm();
            viewForm.ShowDialog();
        }

        private void txtCarName_Enter(object sender, EventArgs e)
        {
            txtCarName.BackColor = Color.LemonChiffon;
        }

        private void txtMainPCB_Enter(object sender, EventArgs e)
        {
            //txtMainPCB.Text = "M15";
            //txtMainPCB.Select(txtMainPCB.TextLength, 0);
            txtMainPCB.SelectAll();
            txtMainPCB.BackColor = Color.LemonChiffon;
        }

        private void txtSubPCB_Enter(object sender, EventArgs e)
        {
            /*
            // 2020.12.14
            // D-오디오 조립만 M17
            // 그 외 M15
            if (MainForm.cbbMetroLine01.Text == MainForm.lineData[2])
            {
                txtSubPCB.Text = "M17";
            }
            else
            {
                txtSubPCB.Text = "M15";
            }
            */
            //txtSubPCB.Select(txtSubPCB.TextLength, txtSubPCB.TextLength);
            txtSubPCB.SelectAll();
            txtSubPCB.BackColor = Color.LemonChiffon;
        }

        private void txtTagType_Enter(object sender, EventArgs e)
        {
            txtTagType.BackColor = Color.LemonChiffon;
        }

        private void txtCustomerEO_Enter(object sender, EventArgs e)
        {
            txtEOContents.BackColor = Color.LemonChiffon;
        }

        private void txtMobisEO_Enter(object sender, EventArgs e)
        {
            txtMobisEO.BackColor = Color.LemonChiffon;
        }

        private void txtCarName_Leave(object sender, EventArgs e)
        {
            txtCarName.BackColor = Color.White;
        }

        private void txtTagType_Leave(object sender, EventArgs e)
        {
            txtTagType.BackColor = Color.White;
        }

        private void txtCustomerEO_Leave(object sender, EventArgs e)
        {
            txtCustomerEO.BackColor = Color.White;
        }

        private void txtMobisEO_Leave(object sender, EventArgs e)
        {
            txtMobisEO.BackColor = Color.White;
        }

        private void txtMainPCB_Leave(object sender, EventArgs e)
        {
            MainForm.dc.TextBoxLeaveHyphenConvert(txtMainPCB);

            txtMainPCB.BackColor = Color.White;
        }

        private void txtSubPCB_Leave(object sender, EventArgs e)
        {
            MainForm.dc.TextBoxLeaveHyphenConvert(txtSubPCB);

            txtSubPCB.BackColor = Color.White;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
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

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPrivate.Checked)
            {
                if (txtEOContents.Text != string.Empty)
                {
                    txtEOContents.Text += " (내부관리)";
                    
                    dateTimePicker2.Value = dateTimePicker1.Value;
                    dateTimePicker2.Enabled = false;
                }
            }
            else
            {
                if (txtEOContents.Text != string.Empty)
                {
                    txtEOContents.Text = MainForm.dc.PrivateTextDeleteConvert(txtEOContents.Text);

                    MainForm.dc.ShipmentDays(dateTimePicker1, dateTimePicker2, comboBox1.Text);
                    dateTimePicker2.Enabled = true;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
            MainForm.dc.ShipmentDays(dateTimePicker1, dateTimePicker2, comboBox1.Text);
        }
    }
}
