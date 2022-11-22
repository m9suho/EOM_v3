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
    public partial class ModelForm : MetroFramework.Forms.MetroForm
    {
        const int OEM_DAY = 7;
        const int CKD_MONTH = 1;
        const string END_TIME = "08:30:00";

        MessageFormClass messageFormClass = new MessageFormClass();

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
            cbbShipment.Enabled = _data[10];
            SelectMetroBtn.Enabled = _data[11];
            AddMetroBtn.Enabled = _data[12];
            dateTimePicker1.Enabled = _data[13];
            dateTimePicker2.Enabled = _data[14];
        }

        private string[] DatabaseColumnData(int _data1)
        {
            string startDatetimeData, endDatetimeData = string.Empty;
            string[] returnData;

            TimeSpan startTime = new TimeSpan(00, 00, 00);
            TimeSpan endTime = new TimeSpan(08, 30, 00);
            TimeSpan realTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            // 시작일 날짜
            startDatetimeData = dateTimePicker1.Value.Date < DateTime.Now.Date ? dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00") : DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

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
                        "",                                                                                                                 // [17] start_order
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
                        cbbShipment.Text,                                                                                                   // [12] shipment
                        MainForm.eoSelectData[7],                                                                                           // [13] mht_data
                        txtMainPCB.Text,                                                                                                    // [14] main_pcb_name
                        txtSubPCB.Text,                                                                                                     // [15] sub_pcb_name
                        txtEOType.Text,                                                                                                     // [16] eo_type
                        "",                                                                                                                 // [17] start_order
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

        private bool ShipmentCheck()
        {
            // 2022.01.10
            // 출하지 마스터 검사
            string query = string.Empty;
            string shipmentConditionData = string.Empty;
            string[,] shipmentData;

            query = "SELECT * FROM `" + MainForm.settingData[0] + "`.`shipment_history_data` WHERE model_name = '" + txtModelName.Text + "' AND end_date > '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ORDER BY start_date";
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

        /*
        private bool ChangeHistoryFirstShipmentCheck()
        {
            string[] selectData = { "model_name", txtModelName.Text };
            string query = MainForm.dc.SelectDeleteQueryANDConvert(MainForm.settingData[0], MainForm.settingData[1], selectData, "SELECT");
            string[,] shipmentData = MainForm.mariaDB.SelectQuery2(query);

            if (shipmentData.GetLength(0) > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        */

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
            //if (txtMobisEO.Text.Equals(string.Empty) && !MainForm.cbbMetroLine01.Text.Equals(MainForm.lineData[4]))
            // 2021.12.12
            // 클러스터 포함
            if (txtMobisEO.Text.Equals(string.Empty))
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

            if (cbbShipment.Text == string.Empty)
            {
                MainForm.dc.Msg("경고", "출하지를 선택해주세요");
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

            if (dateTimePicker1.Enabled && dateTimePicker1.Value > DateTime.Now)
            {
                MainForm.dc.Msg("경고", "현재 날짜보다 크게 설정할 수 없습니다");
                dateTimePicker1.Select();
                return false;
            }
            // 공백, 하이픈, 4M 예외 처리
            if(txtMobisEO.Text.Equals(string.Empty) || txtMobisEO.Text.Equals("-") || txtMobisEO.Text.Equals("4M"))
            {
                // EO 내용 중복 검사 && 출하지 검사
                // 찾을 컬럼, 컬럼 값, 모델 값, 출하지
                if (ModelEODuplicationCheck("eo_contents", txtEOContents.Text, txtModelName.Text, cbbShipment.Text))
                {
                    if (!cbbShipment.Text.Equals("-"))
                    {
                        MainForm.dc.Msg("경고", "EO 내용, 출하지 '" + cbbShipment.Text + "' 등록되어 있습니다");
                    }
                    else
                    {
                        MainForm.dc.Msg("경고", "EO 내용이 등록되어 있습니다");
                    }
                    DGVSearchSelect(txtModelName.Text, txtEOContents.Text, 5, cbbShipment.Text, 10);
                    return false;
                }
            }
            else
            {
                // EO 번호 중복 검사 && 출하지 검사
                // 찾을 컬럼, 컬럼 값, 모델 값, 출하지
                if (ModelEODuplicationCheck("mobis_eo_number", txtMobisEO.Text, txtModelName.Text, cbbShipment.Text))
                {
                    if (!cbbShipment.Text.Equals("-"))
                    {
                        MainForm.dc.Msg("경고", "EO 번호, 출하지 '" + cbbShipment.Text + "' 등록되어 있습니다");
                    }
                    else
                    {
                        MainForm.dc.Msg("경고", "EO 번호가 등록되어 있습니다");
                    }
                    DGVSearchSelect(txtModelName.Text, txtMobisEO.Text, 4, cbbShipment.Text, 10);
                    return false;
                }
            }
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

        public ModelForm()
        {
            InitializeComponent();
        }

        private void ModelForm_Load(object sender, EventArgs e)
        {
            bool[] controlBox;

            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 00, 00, 00);

            Text = MainForm.programName + " - " + MainForm.dc.Version();

            dateTimePicker1.Value = dt;
            dateTimePicker2.Value = dt.AddDays(OEM_DAY);

            // D-오디오 수삽, D-오디오 SUB
            if (MainForm.cbbMetroLine01.Text.Equals(MainForm.lineData[1]) || MainForm.cbbMetroLine01.Text.Equals(MainForm.lineData[3]))
            {
                txtModelName.Text = "M";
                txtModelName.Select(txtModelName.MaxLength + 1, 0);
                //dateTimePicker2.Value = dt;
                controlBox = new bool[] { true, true, false, false, false,
                                          false, false, false, true, true,
                                          false, true, true, true, false };
                InitialSetControl(controlBox);
            }
            // 오디오 조립, D-오디오 조립, 클러스터, HUD
            else
            {
                txtModelName.Text = string.Empty;
                //dateTimePicker2.Value = dt.AddDays(7);
                controlBox = new bool[] { true, true, true, true, true,
                                          false, false, false, true, true,
                                          true, true, true, true, true };
                InitialSetControl(controlBox);
            }

            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;

            // 2022.07.01
            // 모델이 선택됬을 경우 빠른 입력
            if (MainForm.datagridview01.Visible)
            {
                txtModelName.Text = MainForm.cbbMetroProduct01.Text;
            }

            // 2022.01.11
            // 중앙 정렬
            MainForm.dc.ComboBoxDrawAlignCenter(cbbShipment);
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


        private void txtModelName_KeyUp(object sender, KeyEventArgs e)
        {
            /*
            if (!(e.KeyValue == Convert.ToInt32(Keys.Back)))
            {
                TextHyphenConvert(txtModelName);
            }
            */

            // 2022.02.10
            MainForm.dc.AddHyphenCheckFirstChar((TextBox)sender, e);
        }

        private void txtCarName_KeyUp(object sender, KeyEventArgs e)
        {
            MainForm.dc.CarNameUpperConvert(txtCarName);
        }

        private void txtMainPCB_KeyUp(object sender, KeyEventArgs e)
        {
            // 2022.02.10
            MainForm.dc.AddHyphenCheckFirstChar((TextBox)sender, e);
        }

        private void txtSubPCB_KeyUp(object sender, KeyEventArgs e)
        {
            /*
            if (!(e.KeyValue == Convert.ToInt32(Keys.Back)))
            {
                TextHyphenConvert(txtSubPCB);
            }
            */

            // 2022.02.10
            MainForm.dc.AddHyphenCheckFirstChar((TextBox)sender, e);
        }


        private void txtModelName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (MainForm.cbbMetroLine01.Text.Equals(MainForm.lineData[1]) || MainForm.cbbMetroLine01.Text.Equals(MainForm.lineData[3]))
                {
                    if (((TextBox)sender).TextLength == 0)
                    {
                        ((TextBox)sender).Text = "M";
                        ((TextBox)sender).Select(((TextBox)sender).MaxLength + 1, 0);
                    }
                }

                // 2021.09.04
                MainForm.dc.NumAlphaSpaceString(((TextBox)sender));
            }
            catch (Exception ex)
            {
                MainForm.dc.Msg("오류", ex.Message);
            }
        }

        private void txtCarName_TextChanged(object sender, EventArgs e)
        {
            // 2021.09.04
            //MainForm.dc.NumAlphaSpaceString(((TextBox)sender));
        }

        private void txtMainPCB_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Enabled)
            {
                if (MainForm.cbbMetroLine01.Text != "HUD" && (((TextBox)sender).TextLength < 3 || ((TextBox)sender).Text.Substring(0, 1) != "M"))
                {
                    MainPCBInitialValue(((TextBox)sender));

                    ((TextBox)sender).Select(((TextBox)sender).MaxLength + 1, 0);
                }
            }
        }

        private void txtSubPCB_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Enabled)
            {
                if (((TextBox)sender).TextLength < 3 || ((TextBox)sender).Text.Substring(0, 1) != "M")
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
                        //txtSubPCB.Text = "M15";
                    }

                    ((TextBox)sender).Select(((TextBox)sender).MaxLength + 1, 0);
                }
            }
        }


        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (!chkPrivate.Checked)
            {
                MainForm.dc.ShipmentDays(dateTimePicker1, dateTimePicker2, cbbShipment.Text);
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
            if (!chkPrivate.Checked)
            {
                if (dateTimePicker1.Value > dateTimePicker2.Value)
                {
                    MainForm.dc.Msg("경고", "적용일보다 작을 수 없습니다");
                    MainForm.dc.ShipmentDays(dateTimePicker1, dateTimePicker2, cbbShipment.Text);
                    return;
                }

                if (cbbShipment.Text == "CKD" || cbbShipment.Text == "KD")
                {
                    DateTime firstDateTime = dateTimePicker1.Value.AddMonths(1);

                    if (firstDateTime > dateTimePicker2.Value)
                    {
                        MainForm.dc.Msg("경고", "적용일보다 작을 수 없습니다 [CKD, KD : 최소 " + CKD_MONTH + "달]");
                        MainForm.dc.ShipmentDays(dateTimePicker1, dateTimePicker2, cbbShipment.Text);
                        return;
                    }
                }

                if (cbbShipment.Text == "OEM")
                {
                    DateTime firstDateTime = dateTimePicker1.Value.AddDays(OEM_DAY);

                    if (firstDateTime > dateTimePicker2.Value)
                    {
                        MainForm.dc.Msg("경고", "적용일보다 작을 수 없습니다 [OEM : 최소 " + OEM_DAY + "일]");
                        MainForm.dc.ShipmentDays(dateTimePicker1, dateTimePicker2, cbbShipment.Text);
                        return;
                    }
                }
            }
        }

        private void SelectMetroBtn_Click(object sender, EventArgs e)
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
                if (MainForm.cbbMetroLine01.Text != "D-오디오 수삽" && MainForm.cbbMetroLine01.Text != "D-오디오 SUB")     // 출하지
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

                /*
                // 2020.08.28
                // EO 등록 데이터 무조건 초기화
                for (int i = 0; i < MainForm.eoSelectData.Length; i++)
                {
                    MainForm.eoSelectData[i] = string.Empty;
                }
                */

                MainForm.dc.ShipmentDays(dateTimePicker1, dateTimePicker2, cbbShipment.Text);
            }

            Opacity = 1;
        }

        private void AddMetroBtn_Click(object sender, EventArgs e)
        {
            if (ModelAddCheckSum())
            {
                string query, printData = string.Empty;
                string[] insertData;

                // D-오디오 수삽
                // D-오디오 SUB
                if (MainForm.cbbMetroLine01.Text.Equals(MainForm.lineData[1]) || MainForm.cbbMetroLine01.Text.Equals(MainForm.lineData[3]))
                {
                    insertData = DatabaseColumnData(1);
                }
                // 오디오 플랫폼, D-오디오 조립, 클러스터, HUD
                else
                {
                    insertData = DatabaseColumnData(2);
                }

                query = MainForm.dc.InsertQueryArrayConvert(MainForm.settingData[0], MainForm.settingData[1], insertData);
                MainForm.mariaDB.EtcQuery(query);

                Opacity = 0;
                MainForm.modelSelectData = txtModelName.Text;

                // 2020.05.06
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

            /*
            if (cbbShipment.Enabled && MessageBox.Show("출하지가 첫 등록하게 됩니다. 기준 출하지를 '" + cbbShipment.Text + "'로 등록하시겠습니까?", "알림", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

            }
            */
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
            //MainForm.subjectData = mhtSearchData[0];

            // mht 경로
            MainForm.eoViewData = tmpPath;

            ViewForm viewForm = new ViewForm();
            viewForm.ShowDialog();
        }

        private void txtModelName_Enter(object sender, EventArgs e)
        {
            txtModelName.BackColor = Color.LemonChiffon;
        }

        private void txtCarName_Enter(object sender, EventArgs e)
        {
            txtCarName.BackColor = Color.LemonChiffon;
        }

        private void txtMainPCB_Enter(object sender, EventArgs e)
        {
            MainPCBInitialValue(txtMainPCB);

            txtMainPCB.Select(txtMainPCB.MaxLength + 1, 0);
            txtMainPCB.BackColor = Color.LemonChiffon;
        }

        private void txtSubPCB_Enter(object sender, EventArgs e)
        {
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
            txtSubPCB.Select(txtSubPCB.MaxLength + 1, 0);
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

        private void txtModelName_Leave(object sender, EventArgs e)
        {
            if (txtModelName.TextLength > 10)
            {
                messageFormClass.FormMsg = "[" + txtModelName.Text + "] 품번 등록정보 조회 시도..";
                messageFormClass.ShowDelay = 2000;
                messageFormClass.StartForm();

                // 값이 있을 경우에만 체킹
                if (!txtModelName.Text.Equals(string.Empty))
                {
                    // 2020.11.09
                    // 품번 띄어쓰기 없애기
                    txtModelName.Text = txtModelName.Text.Replace(" ", String.Empty);

                    // 2022.01.14
                    // 라인의 전 모델을 가져와 프로그램 내에서 처리하므로 데이터베이스 조건부 처리 요청 
                    string[] selectData = new string[] { "line", MainForm.cbbMetroLine01.Text, "model_name", txtModelName.Text, "print_type", "초도품" };
                    //string[] selectData = new string[] { "line", MainForm.cbbMetroLine01.Text, "model_name", txtModelName.Text };
                    string query = MainForm.dc.SelectDeleteQueryANDConvert(MainForm.settingData[0], MainForm.settingData[1], selectData, "SELECT");
                    //query += " OR (model_name = '" + txtModelName.Text + "' AND eo_contents = '---------- 첫 투입 품번 등록 ----------')";
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
                        if (!(MainForm.cbbMetroLine01.Text == "HUD" || MainForm.cbbMetroLine01.Text == "클러스터"))
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
                        if (MainForm.cbbMetroLine01.Text != MainForm.lineData[1] && MainForm.cbbMetroLine01.Text != MainForm.lineData[3])
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
                        SelectMetroBtn.Focus();
                    }
                }

                // 2020.12.14
                if (txtModelName.TextLength > 0 && txtModelName.Text.Substring(0, 1) == "M")
                {
                    txtMainPCB.Enabled = false;
                    txtSubPCB.Enabled = false;

                    txtMainPCB.Text = "-";
                    txtSubPCB.Text = "-";
                }
            }

            txtModelName.BackColor = Color.White;
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
                txtSubPCB.Enabled = false;
                txtSubPCB.Text = "-";
            }
            else
            {
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

                    MainForm.dc.ShipmentDays(dateTimePicker1, dateTimePicker2, cbbShipment.Text);
                    dateTimePicker2.Enabled = true;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
            MainForm.dc.ShipmentDays(dateTimePicker1, dateTimePicker2, cbbShipment.Text);
        }
    }
}
