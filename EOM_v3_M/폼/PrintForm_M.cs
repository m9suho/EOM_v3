using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace EOM_v3_M
{
    public partial class PrintForm : Form
    {
        private string SearchRegistrantPrintAddress(string _data)
        {
            string[] selectData = new string[] { };
            string query = MainForm.dc.SelectDeleteQueryANDConvert(MainForm.strDbName, "registrant_data", selectData, "SELECT");
            string[,] tmpData = MainForm.mariaDB.SelectQuery2(query);

            for (int i = 0; i < tmpData.GetLength(0); i++)
            {
                // 있을 경우
                if (tmpData[i, 0].Equals(_data))
                {
                    return tmpData[i, 2];
                }
            }

            return null;
        }

        private string TextGapConvert(string _data)
        {
            string tmpData = string.Empty;

            for (int i = 0; i < _data.Length; i++)
            {
                if (i == 0)
                {
                    tmpData += _data.Substring(0, 1);
                }
                else
                {
                    tmpData += " " + _data.Substring(i, 1);
                }
            }

            return tmpData;
        }

        public PrintForm()
        {
            InitializeComponent();
        }

        private void PrintForm_Load(object sender, EventArgs e)
        {
            try
            {
                Text = MainForm.programName + " - " + MainForm.dc.Version();

                //string carData, eoData = string.Empty;

                if (MainForm.printData[12].Equals("CKD"))
                {
                    lblProductName.BackColor = Color.FromArgb(255, 255, 128);
                    lblWriteDate.BackColor = Color.FromArgb(255, 255, 128);
                    lblEOContents.BackColor = Color.FromArgb(255, 255, 128);
                    lblEONumber.BackColor = Color.FromArgb(255, 255, 128);
                    lblManagerName.BackColor = Color.FromArgb(255, 255, 128);
                    lblPhoneNumber.BackColor = Color.FromArgb(255, 255, 128);
                    lblType.BackColor = Color.FromArgb(255, 255, 128);
                    lblType_eng.BackColor = Color.FromArgb(255, 255, 128);
                    label9.BackColor = Color.FromArgb(255, 255, 128);
                    label11.BackColor = Color.FromArgb(255, 255, 128);

                    for (int i = 0; i < 6; i++)
                    {
                        Label lbl = this.Controls.Find("lblSpace" + (i + 1).ToString(), true).FirstOrDefault() as Label;

                        lbl.BackColor = Color.FromArgb(255, 255, 128);
                    }

                    for (int i = 0; i < 8; i++)
                    {
                        Label lbl = this.Controls.Find("label" + (i + 1).ToString(), true).FirstOrDefault() as Label;

                        lbl.BackColor = Color.FromArgb(255, 255, 128);
                    }
                }

                string[] inputData =
                {
                    MainForm.printData[9],      // 샘플, 초도품
                    MainForm.printData[6],      // 작성일
                    string.Empty,               // 담당자
                    string.Empty,               // 연락처
                    MainForm.printData[1],      // 품번
                    MainForm.printData[2],      // 차종
                    MainForm.printData[3],      // 고객사 EO
                    MainForm.printData[4],      // 모비스 EO
                    MainForm.printData[5],      // 변경내역
                };
                string[] printFormData = MainForm.dc.PrintFormConditionConvert(MainForm.cbbLineName01.Text, inputData);

                // 2021.12.03
                // 품번 제외 (변경내역, 기존 형식, 차종만)
                printFormData[4] = MainForm.dc.ProductNameExclusion(printFormData[6], printFormData[4], inputData[5]);

                lblType.Text = printFormData[0];            // 샘플 or 초도품
                lblProductName.Text = printFormData[4];     // 차종
                lblEONumber.Text = printFormData[5];        // E/O No.
                lblEOContents.Text = printFormData[6];      // 변경 내역
                lblWriteDate.Text = printFormData[1];       // 작성일

                // 2022.04.29
                // 영어 표기

                if (printFormData[0] == "초 도 품")
                {
                    lblType_eng.Text = "(First Production)";
                }
                else if (printFormData[0] == "샘 플")
                {
                    lblType_eng.Text = "(Sample)";
                }
                else
                {
                    lblType_eng.Text = "";
                }

                // 어드민 모드
                if (MainForm.adminMode)
                {
                    cbbAdminTest.Enabled = true;
                    cbbAdminTest.SelectedIndex = 1;
                }

                numericUpDown1.Select();
                //numericUpDown1.Select(0, numericUpDown1.Value.ToString().Length);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPrintOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                {
                    MainForm.Guna2Msg(this, "오류", "인터넷 연결 상태를 확인해주세요.");
                    Close();
                }
                else
                {
                    if (!(numericUpDown1.Value > 0 && numericUpDown1.Value <= 100))
                    {
                        MainForm.Guna2Msg(this, "오류", "숫자를 입력하세요 [1 ~ 100]");
                        return;
                    }

                    btnPrintOK.Enabled = false;

                    /*
                    // eo_contents
                    if (tmpData[5].Length > 6 && tmpData[5].Substring(tmpData[5].Length - 6, 6).Equals("(내부관리)"))
                    {
                        // 마지막이 공백인지 확인
                        if (tmpData[5].Substring(tmpData[5].Length - 7, 1).Equals(" "))
                        {
                            tmpData[5] = tmpData[5].Substring(0, tmpData[5].Length - 7);
                        }
                        else
                        {
                            tmpData[5] = tmpData[5].Substring(0, tmpData[5].Length - 6);
                        }
                    }

                    // mobis_eo_number
                    if (MainForm.printData[4].Equals("-"))
                    {
                        MainForm.printData[4] = string.Empty;
                    }
                    */

                    MainForm.printData[10] = numericUpDown1.Value.ToString();

                    // print_address
                    string printAddressData = SearchRegistrantPrintAddress(NetworkInterface.GetAllNetworkInterfaces()[0].GetPhysicalAddress().ToString());

                    // 사용자 모드에는 PC 체크
                    if (!MainForm.adminMode)
                    {
                        if (printAddressData != null)
                        {
                            MainForm.printData[11] = printAddressData;
                        }
                        else
                        {
                            MainForm.Guna2Msg(this, "오류", "PC 등록이 되지 않았습니다 [윤민규 사원 문의]");
                            return;
                        }
                    }
                    // 마스터 모드에서는 지정
                    else
                    {
                        string[] addressData = MainForm.dc.LineMatchingData();

                        if (addressData != null && (addressData[0] == "A85" || addressData[0] == "A86"))
                        {
                            MainForm.printData[11] = "2F_CLU";
                        }
                        else
                        {
                            MainForm.Guna2Msg(this, "오류", "프린터 주소 설정이 없어 인쇄가 불가능합니다");
                            return;
                        }
                    }

                    // 리사이즈
                    Array.Resize(ref MainForm.printData, 12);

                    // 인쇄 쿼리 추가
                    string query = MainForm.dc.InsertQueryArrayConvert(MainForm.strDbName, "print_data", MainForm.printData);
                    
                    MainForm.mariaDB.EtcQuery(query);

                    MainForm.complete = true;

                    MainForm.Guna2Msg(this, "오류", "정상 처리 되었습니다!");

                    MainForm.mariaDB.InsertLogDB(lblProductName.Text + "/" + lblEONumber.Text + "/" + lblEOContents.Text + "/" + numericUpDown1.Value.ToString() + " 인쇄 요청", false);

                    // 리사이즈
                    Array.Resize(ref MainForm.printData, 13);

                    // 폼 닫기
                    Close();
                }
            }
            catch (Exception ex)
            {
                MainForm.mariaDB.InsertLogDB(ex.Message, false);
            }
        }

        private void numericUpDown1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == Convert.ToInt32(Keys.Enter))
            {
                btnPrintOK.PerformClick();
            }
        }

    }
}
