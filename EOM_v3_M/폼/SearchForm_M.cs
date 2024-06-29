using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace EOM_v3_M
{
    public partial class SearchForm : Form
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

            txtSearch.Select(txtSearch.Text.Length, 0);

            // 메인 폼
            if (MainForm.searchType == "MAIN")
            {
                rdoProduct.Enabled = true;
                rdoCarName.Enabled = true;
                rdoCustomer.Enabled = true;
                rdoMobis.Enabled = true;
                rdoContents.Enabled = true;
                rdoMainPCB.Enabled = true;
                rdoSubPCB.Enabled = true;

                rdoProduct.Checked = true;
            }
            // EO 폼
            else if (MainForm.searchType.Equals("EO"))
            {
                rdoProduct.Enabled = true;
                rdoCarName.Enabled = true;
                rdoCustomer.Enabled = true;
                rdoMobis.Enabled = true;
                rdoContents.Enabled = true;
                rdoMainPCB.Enabled = false;
                rdoSubPCB.Enabled = false;

                rdoCarName.Checked = true;
            }
            // 출하지 폼
            else if (MainForm.searchType.Equals("SHIPMENT"))
            {
                rdoProduct.Enabled = true;
                rdoCarName.Enabled = false;
                rdoCustomer.Enabled = false;
                rdoMobis.Enabled = false;
                rdoContents.Enabled = false;
                rdoMainPCB.Enabled = false;
                rdoSubPCB.Enabled = false;

                rdoProduct.Checked = true;
            }
            else
            {
                rdoProduct.Enabled = false;
                rdoCarName.Enabled = false;
                rdoCustomer.Enabled = false;
                rdoMobis.Enabled = false;
                rdoContents.Enabled = false;
                rdoMainPCB.Enabled = false;
                rdoSubPCB.Enabled = false;

                rdoProduct.Checked = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            btnProductSearch.Enabled = true;
            timer1.Stop();
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(listBox1.SelectedIndex != -1)
            {
                if (MainForm.searchType.Equals("MAIN"))
                {
                    if (rdoProduct.Checked)
                    {
                        MainForm.cbbProductName01.SelectedItem = listBox1.SelectedItem;
                    }
                    // 2020.09.07
                    // 차종 검색 기능 추가
                    else if(rdoCarName.Checked)
                    {
                        MainForm.searchData[0] = listBox1.SelectedItem.ToString();
                        MainForm.searchData[1] = "1";
                    }
                    else if (rdoCustomer.Checked)
                    {
                        MainForm.searchData[0] = listBox1.SelectedItem.ToString();
                        MainForm.searchData[1] = "2";
                    }
                    else if (rdoMobis.Checked)
                    {
                        MainForm.searchData[0] = listBox1.SelectedItem.ToString();
                        MainForm.searchData[1] = "3";
                    }
                    else if (rdoContents.Checked)
                    {
                        MainForm.searchData[0] = listBox1.SelectedItem.ToString();
                        MainForm.searchData[1] = "4";
                    }
                    else if (rdoMainPCB.Checked)
                    {
                        MainForm.searchData[0] = listBox1.SelectedItem.ToString();
                        MainForm.searchData[1] = "5";
                    }
                    else if (rdoSubPCB.Checked)
                    {
                        MainForm.searchData[0] = listBox1.SelectedItem.ToString();
                        MainForm.searchData[1] = "6";
                    }
                    else
                    {
                        MainForm.Guna2Msg(this, "오류", "치명적인 오류");
                        return;
                    }
                }
                else if (MainForm.searchType.Equals("EO"))
                {
                    if (rdoCarName.Checked)
                    {
                        EOForm.ModelSelect(listBox1.SelectedItem.ToString(), 1);
                    }
                    else if (rdoCustomer.Checked)
                    {
                        EOForm.ModelSelect(listBox1.SelectedItem.ToString(), 2);
                    }

                    else if (rdoMobis.Checked)
                    {
                        EOForm.ModelSelect(listBox1.SelectedItem.ToString(), 3);
                    }
                    else if (rdoContents.Checked)
                    {
                        EOForm.ModelSelect(listBox1.SelectedItem.ToString(), 4);
                    }
                    else if (rdoMainPCB.Checked)
                    {
                        EOForm.ModelSelect(listBox1.SelectedItem.ToString(), 5);
                    }
                    else if (rdoSubPCB.Checked)
                    {
                        EOForm.ModelSelect(listBox1.SelectedItem.ToString(), 6);
                    }
                    else
                    {
                        MainForm.Guna2Msg(this, "오류", "치명적인 오류");
                        return;
                    }
                }
                else
                {
                    if (rdoProduct.Checked)
                    {
                        MainForm.searchData[0] = listBox1.SelectedItem.ToString();
                        MainForm.searchData[1] = "";
                    }
                    else
                    {
                        MainForm.Guna2Msg(this, "오류", "치명적인 오류");
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

        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            try
            {
                guna2ProgressIndicator1.Visible = true;
                guna2ProgressIndicator1.AutoStart = true;
                btnProductSearch.Enabled = false;

                

                // 공백 검사
                if (txtSearch.Text.Equals(string.Empty))
                {
                    MainForm.Guna2Msg(this, "오류", "검색어를 입력해주세요");
                    txtSearch.Select();
                    return;
                }

                // 조건 검색
                string SearchTableName = string.Empty;
                string query = string.Empty;
                string[] searchData;

                MainForm.dc.Delay(100);

                // 메인 폼
                if (MainForm.searchType == "MAIN")
                {
                    // 모델
                    if (rdoProduct.Checked)
                    {
                        SearchTableName = "model_name";
                    }
                    // 차종
                    else if (rdoCarName.Checked)
                    {
                        SearchTableName = "car_name";
                    }
                    // 고객사 EO
                    else if (rdoCustomer.Checked)
                    {
                        SearchTableName = "customer_eo_number";
                    }
                    // 모비스 EO
                    else if (rdoMobis.Checked)
                    {
                        SearchTableName = "mobis_eo_number";
                    }
                    // EO 내용
                    else if (rdoContents.Checked)
                    {
                        SearchTableName = "eo_contents";
                    }
                    // MAIN PCB
                    else if (rdoMainPCB.Checked)
                    {
                        SearchTableName = "main_pcb_name";
                    }
                    // SUB PCB
                    else if (rdoSubPCB.Checked)
                    {
                        SearchTableName = "sub_pcb_name";
                    }
                    // 예외 경우
                    else
                    {
                        MainForm.Guna2Msg(this, "오류", "잘못 된 오류 발생");
                        return;
                    }

                    query = "SELECT * FROM `" + MainForm.strDbName + "`.`model_data` WHERE line = '" + MainForm.cbbLineName01.Text + "' AND " + SearchTableName + " LIKE '%" + txtSearch.Text + "%'";
                    searchData = MainForm.mariaDB.SelectQueryCount(query, SearchTableName);
                }
                // EO 폼
                else if (MainForm.searchType == "EO")
                {
                    // 모델
                    if (rdoProduct.Checked)
                    {
                        SearchTableName = "model_name";
                    }
                    // 차종
                    else if (rdoCarName.Checked)
                    {
                        SearchTableName = "car_name";
                    }
                    // 고객사 EO
                    else if (rdoCustomer.Checked)
                    {
                        SearchTableName = "customer_eo_number";
                    }
                    // 모비스 EO
                    else if (rdoMobis.Checked)
                    {
                        SearchTableName = "mobis_eo_number";
                    }
                    // EO 내용
                    else if (rdoContents.Checked)
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
                        MainForm.Guna2Msg(this, "오류", "잘못 된 오류 발생");
                        return;
                    }

                    query = "SELECT * FROM `" + MainForm.strDbName + "`.`" + "eo_data" + "` WHERE line = '" + MainForm.cbbLineName01.Text + "' AND " + SearchTableName + " LIKE '%" + txtSearch.Text + "%'";
                    searchData = MainForm.mariaDB.SelectQueryCount(query, SearchTableName);
                }
                else
                {
                    // 모델
                    SearchTableName = "model_name";

                    query = "SELECT * FROM `" + MainForm.strDbName + "`.`model_data` WHERE " + SearchTableName + " LIKE '%" + txtSearch.Text + "%' AND NOT shipment = '-' GROUP BY " + SearchTableName;
                    searchData = MainForm.mariaDB.SelectQueryCount(query, SearchTableName);
                }

                listBox1.Items.Clear();

                //MainForm.dc.Delay(500);

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

                //txtSearch.BackColor = Color.White;
                //btnProductSearch.Enabled = true;

                // 2023.04.01
                // 커서 유지
                //if (listBox1.Items.Count == 0)
                {
                    txtSearch.Text = string.Empty;
                    txtSearch.Select();
                }

                txtSearch.Text = string.Empty;
                MessageForm(count + "건 검색 완료");
                timer1.Interval = 3000;
                timer1.Start();
            }
            catch (Exception ex)
            {
                MainForm.Guna2Msg(this, "오류", ex.Message);
            }
            finally
            {
                guna2ProgressIndicator1.AutoStart = false;
                guna2ProgressIndicator1.Visible = false;
            }
        }

        private void txtFillColor_Enter(object sender, EventArgs e)
        {
            ((Guna2TextBox)sender).FillColor = Color.LemonChiffon;
        }

        private void txtFillColor_Leave(object sender, EventArgs e)
        {
            ((Guna2TextBox)sender).FillColor = Color.White;
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            // 엔터 키 버튼
            if (e.KeyValue == 13)
            {
                btnProductSearch.PerformClick();
            }

            // ESC 키 폼 종료
            if (e.KeyValue == 27)
            {
                Close();
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            // [숫자, 영어, 하이픈] 품번, MAIN PCB, SUB PCB
            if (rdoProduct.Checked || rdoMainPCB.Checked || rdoSubPCB.Checked)
            {
                MainForm.dc.NumAlphaHypenString((Guna2TextBox)sender, e);
            }
            // [숫자, 영어, 공백] 차종
            else if (rdoCarName.Checked)
            {
                MainForm.dc.NumAlphaSpaceString((Guna2TextBox)sender, e);
            }
            // [숫자, 영어] 고객사 EO, 모비스 EO
            else if (rdoCustomer.Checked || rdoMobis.Checked)
            {
                MainForm.dc.NumAlphaBetString((Guna2TextBox)sender, e);
            }
            // [숫자, 한글, 영어, 공백] EO 내용
            else if (rdoContents.Checked)
            {
                MainForm.dc.NumKoreanAlphaSpaceString((Guna2TextBox)sender, e);
            }
            else
            {
                
            }
        }

        private void SetRadioButtonCheck()
        {
            if (rdoContents.Checked)
            {
                MainForm.dc.ImeModeAllSet(this, "Hangul");
            }
            else
            {
                MainForm.dc.ImeModeAllSet(this, "");
            }

            // 2024.02.21
            // 품번, 고객사 EO, 모비스 EO, M/PCB, S/PCB
            if (rdoProduct.Checked || rdoCustomer.Checked || rdoMobis.Checked || rdoMainPCB.Checked || rdoSubPCB.Checked)
            {
                txtSearch.CharacterCasing = CharacterCasing.Upper;
            }
            // 차종, EO 내용
            else
            {
                txtSearch.CharacterCasing = CharacterCasing.Normal;
            }

            txtSearch.Text = string.Empty;
            txtSearch.Select();
        }

        private void SetRadioButtonCheck_CheckedChanged(object sender, EventArgs e)
        {
            SetRadioButtonCheck();
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            // 2024.02.21
            // M/PCB, S/PCB
            if (rdoMainPCB.Checked || rdoSubPCB.Checked)
            {
                MainForm.dc.AddHyphenCheckFirstChar((Guna2TextBox)sender, e);
            }
            // 차종
            else if (rdoCarName.Checked)
            {
                MainForm.dc.CarNameUpperConvert((Guna2TextBox)sender);
            }
        }
    }
}
