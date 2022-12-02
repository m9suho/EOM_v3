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
    public partial class SearchForm : MetroFramework.Forms.MetroForm
    {
        private void MessageForm(string _msg)
        {
            MessageFormClass messageFormClass = new MessageFormClass();

            messageFormClass.ShowDelay = 2000;
            messageFormClass.FormMsg = _msg;
            messageFormClass.InstanceForm = this;
            messageFormClass.StartForm();

            Focus();
        }

        public SearchForm()
        {
            InitializeComponent();
        }

        private void SearchForm_Load(object sender, EventArgs e)
        {
            Text = MainForm.programName + " - " + MainForm.dc.Version();

            textBox1.Select(textBox1.Text.Length, 0);

            // 메인 폼
            if (MainForm.searchType == "MAIN")
            {
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                radioButton3.Enabled = true;
                radioButton4.Enabled = true;
                radioButton5.Enabled = true;
                radioButton6.Enabled = true;
                radioButton7.Enabled = true;

                radioButton1.Checked = true;
            }
            // EO 폼
            else if (MainForm.searchType.Equals("EO"))
            {
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                radioButton3.Enabled = true;
                radioButton4.Enabled = true;
                radioButton5.Enabled = true;
                radioButton6.Enabled = false;
                radioButton7.Enabled = false;

                radioButton2.Checked = true;
            }
            // 출하지 폼
            else if (MainForm.searchType.Equals("SHIPMENT"))
            {
                radioButton1.Enabled = true;
                radioButton2.Enabled = false;
                radioButton3.Enabled = false;
                radioButton4.Enabled = false;
                radioButton5.Enabled = false;
                radioButton6.Enabled = false;
                radioButton7.Enabled = false;

                radioButton1.Checked = true;
            }
            else
            {
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                radioButton3.Enabled = false;
                radioButton4.Enabled = false;
                radioButton5.Enabled = false;
                radioButton6.Enabled = false;
                radioButton7.Enabled = false;

                radioButton1.Checked = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SearchMetroBtn.Enabled = true;
            progressBar1.Value = 0;
            timer1.Stop();
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(listBox1.SelectedIndex != -1)
            {
                if (MainForm.searchType.Equals("MAIN"))
                {
                    if (radioButton1.Checked)
                    {
                        MainForm.cbbMetroProduct01.SelectedItem = listBox1.SelectedItem;
                    }
                    // 2020.09.07
                    // 차종 검색 기능 추가
                    else if(radioButton2.Checked)
                    {
                        MainForm.searchData[0] = listBox1.SelectedItem.ToString();
                        MainForm.searchData[1] = "1";
                    }
                    else if (radioButton3.Checked)
                    {
                        MainForm.searchData[0] = listBox1.SelectedItem.ToString();
                        MainForm.searchData[1] = "2";
                    }
                    else if (radioButton4.Checked)
                    {
                        MainForm.searchData[0] = listBox1.SelectedItem.ToString();
                        MainForm.searchData[1] = "3";
                    }
                    else if (radioButton5.Checked)
                    {
                        MainForm.searchData[0] = listBox1.SelectedItem.ToString();
                        MainForm.searchData[1] = "4";
                    }
                    else if (radioButton6.Checked)
                    {
                        MainForm.searchData[0] = listBox1.SelectedItem.ToString();
                        MainForm.searchData[1] = "5";
                    }
                    else if (radioButton7.Checked)
                    {
                        MainForm.searchData[0] = listBox1.SelectedItem.ToString();
                        MainForm.searchData[1] = "6";
                    }
                    else
                    {
                        MainForm.dc.Msg("경고", "치명적인 오류");
                        return;
                    }
                }
                else if (MainForm.searchType.Equals("EO"))
                {
                    if (radioButton2.Checked)
                    {
                        EOForm.ModelSelect(listBox1.SelectedItem.ToString(), 1);
                    }
                    else if (radioButton3.Checked)
                    {
                        EOForm.ModelSelect(listBox1.SelectedItem.ToString(), 2);
                    }

                    else if (radioButton4.Checked)
                    {
                        EOForm.ModelSelect(listBox1.SelectedItem.ToString(), 3);
                    }
                    else if (radioButton5.Checked)
                    {
                        EOForm.ModelSelect(listBox1.SelectedItem.ToString(), 4);
                    }
                    else if (radioButton6.Checked)
                    {
                        EOForm.ModelSelect(listBox1.SelectedItem.ToString(), 5);
                    }
                    else if (radioButton7.Checked)
                    {
                        EOForm.ModelSelect(listBox1.SelectedItem.ToString(), 6);
                    }
                    else
                    {
                        MainForm.dc.Msg("경고", "치명적인 오류");
                        return;
                    }
                }
                else
                {
                    if (radioButton1.Checked)
                    {
                        MainForm.searchData[0] = listBox1.SelectedItem.ToString();
                        MainForm.searchData[1] = "";
                    }
                    else
                    {
                        MainForm.dc.Msg("경고", "치명적인 오류");
                        return;
                    }
                }

                Close();
            }
        }

        private void SearchForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 검색 타입 초기화
            MainForm.searchType = string.Empty;
        }

        private void SearchMetroBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // 공백 검사
                if (textBox1.Text.Equals(string.Empty))
                {
                    MainForm.dc.Msg("경고", "검색어를 입력해주세요");
                    textBox1.Select();
                    return;
                }

                // 조건 검색
                string SearchTableName = string.Empty;
                string query = string.Empty;
                string[] searchData;

                // 메인 폼
                if (MainForm.searchType == "MAIN")
                {
                    // 모델
                    if (radioButton1.Checked)
                    {
                        SearchTableName = "model_name";
                    }
                    // 차종
                    else if (radioButton2.Checked)
                    {
                        SearchTableName = "car_name";
                    }
                    // 고객사 EO
                    else if (radioButton3.Checked)
                    {
                        SearchTableName = "customer_eo_number";
                    }
                    // 모비스 EO
                    else if (radioButton4.Checked)
                    {
                        SearchTableName = "mobis_eo_number";
                    }
                    // EO 내용
                    else if (radioButton5.Checked)
                    {
                        SearchTableName = "eo_contents";
                    }
                    // MAIN PCB
                    else if (radioButton6.Checked)
                    {
                        SearchTableName = "main_pcb_name";
                    }
                    // SUB PCB
                    else if (radioButton7.Checked)
                    {
                        SearchTableName = "sub_pcb_name";
                    }
                    // 예외 경우
                    else
                    {
                        MainForm.dc.Msg("경고", "잘못 된 오류 발생");
                        return;
                    }

                    query = "SELECT * FROM `" + MainForm.settingData[0] + "`.`" + MainForm.settingData[1] + "` WHERE line = '" + MainForm.cbbMetroLine01.Text + "' AND " + SearchTableName + " LIKE '%" + textBox1.Text + "%'";
                    searchData = MainForm.mariaDB.SelectQueryCount(query, SearchTableName);
                }
                // EO 폼
                else if (MainForm.searchType == "EO")
                {
                    // 모델
                    if (radioButton1.Checked)
                    {
                        SearchTableName = "model_name";
                    }
                    // 차종
                    else if (radioButton2.Checked)
                    {
                        SearchTableName = "car_name";
                    }
                    // 고객사 EO
                    else if (radioButton3.Checked)
                    {
                        SearchTableName = "customer_eo_number";
                    }
                    // 모비스 EO
                    else if (radioButton4.Checked)
                    {
                        SearchTableName = "mobis_eo_number";
                    }
                    // EO 내용
                    else if (radioButton5.Checked)
                    {
                        SearchTableName = "eo_contents";
                    }
                    /*
                    // MAIN PCB
                    else if (radioButton6.Checked)
                    {
                        SearchTableName = "main_pcb_name";
                    }
                    // SUB PCB
                    else if (radioButton7.Checked)
                    {
                        SearchTableName = "sub_pcb_name";
                    }
                    */
                    // 예외 경우
                    else
                    {
                        MainForm.dc.Msg("경고", "잘못 된 오류 발생");
                        return;
                    }

                    query = "SELECT * FROM `" + MainForm.settingData[0] + "`.`" + MainForm.settingData[2] + "` WHERE line = '" + MainForm.cbbMetroLine01.Text + "' AND " + SearchTableName + " LIKE '%" + textBox1.Text + "%'";
                    searchData = MainForm.mariaDB.SelectQueryCount(query, SearchTableName);
                }
                else
                {
                    // 모델
                    SearchTableName = "model_name";

                    query = "SELECT * FROM `" + MainForm.settingData[0] + "`.`model_data` WHERE " + SearchTableName + " LIKE '%" + textBox1.Text + "%' AND NOT shipment = '-' GROUP BY " + SearchTableName;
                    searchData = MainForm.mariaDB.SelectQueryCount(query, SearchTableName);
                }

                listBox1.Items.Clear();

                // 검색 수량 카운트
                int count = 0;

                for (int i = 0; i < searchData.Length; i++)
                {
                    // 중복 제거
                    if (!listBox1.Items.Contains(searchData[i]) && !searchData[i].Equals(""))
                    {
                        listBox1.Items.Add(searchData[i]);
                        count++;
                    }
                }

                textBox1.BackColor = Color.White;

                SearchMetroBtn.Enabled = false;
                textBox1.Text = string.Empty;
                MessageForm(count + "건 검색 완료");
                progressBar1.Value = 100;
                timer1.Interval = 3000;
                timer1.Start();
            }
            catch (Exception ex)
            {
                MainForm.dc.Msg("에러", ex.Message);
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.LemonChiffon;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.White;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            // 엔터 키 버튼
            if (e.KeyValue == 13)
            {
                SearchMetroBtn.PerformClick();
            }

            // ESC 키 폼 종료
            if (e.KeyValue == 27)
            {
                Close();
            }
        }

        private void SetRadioButtonCheck()
        {
            textBox1.Text = string.Empty;
            textBox1.Select();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            SetRadioButtonCheck();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            SetRadioButtonCheck();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            SetRadioButtonCheck();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            SetRadioButtonCheck();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            SetRadioButtonCheck();
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            SetRadioButtonCheck();
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            SetRadioButtonCheck();
        }

        private void textBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (!radioButton5.Checked)
            {
                textBox1.ImeMode = ImeMode.Alpha;
            }
        }
    }
}
