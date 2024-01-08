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
    public partial class OrderMasterForm_E : Form
    {
        const int OEM_DAY = 7;
        const int CKD_MONTH = 1;
        const int KD_MONTH = 1;
        const string END_TIME = "08:30:00";

        string[] modelData;

        private void ComboBoxItemCheck()
        {
            for (int i = 0; i < MainForm.dc.Shipment.Length; i++)
            {
                // true or false
                //if (MainForm.ShipmentData[i] != modelData[2])
                {
                    if (MainForm.dc.Shipment[i] == modelData[2])
                    {
                        metroComboBox1.Items.Add(MainForm.dc.Shipment[i] + " - [기존]");
                        metroComboBox1.SelectedIndex = i;
                    }
                    else if (MainForm.dc.Shipment[i] == modelData[3])
                    {
                        metroComboBox1.Items.Add(MainForm.dc.Shipment[i] + " - [적용]");
                    }
                    else
                    {
                        metroComboBox1.Items.Add(MainForm.dc.Shipment[i]);
                        //metroComboBox1.SelectedIndex = i;
                    }
                }
            }
        }

        private void EndDateTimeValue(int _data, string _type)
        {
            string[] splitData = metroComboBox1.Text.Split('-');

            if (modelData[2] != splitData[0].Trim())
            {
                if (_type == "D")
                {
                    dtpEndDate.Value = dtpStartDate.Value.AddDays(_data);
                }
                else if (_type == "M")
                {
                    dtpEndDate.Value = dtpStartDate.Value.AddMonths(_data);
                }
            }
            else
            {
                dtpEndDate.Value = DateTime.Now;
            }

            //MessageBox.Show(metroDateTime2.Value.ToString());
        }

        public OrderMasterForm_E(string[] _data)
        {
            modelData = _data;
            InitializeComponent();
        }

        private void OrderMasterForm_E_Load(object sender, EventArgs e)
        {
            Text = MainForm.programName + " - " + MainForm.dc.Version();

            txtModelName.Text = modelData[1];

            metroComboBox1.Items.Clear();
            ComboBoxItemCheck();

            //metroDateTime2.Value = metroDateTime2.Value.AddDays(TEMP_DAY);
        }

        /// <summary>
        /// 마지막 
        /// </summary>
        /// <param name="_data">출하지</param>
        /// <returns></returns>
        private void ExceptShipmentData(string _data)
        {
            string[] shipmentDataNot;

            switch (_data)
            {
                case "OEM":
                    shipmentDataNot = new string[] { "CKD", "KD" };
                    break;
                case "CKD":
                    shipmentDataNot = new string[] { "OEM", "KD" };
                    break;
                case "KD":
                    shipmentDataNot = new string[] { "OEM", "CKD" };
                    break;
            }

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string query = string.Empty;
                string oldShilmentData = string.Empty;
                string newShilmentData = string.Empty;
                // 시작일 날짜
                string startDatetimeData = dtpStartDate.Value.Date < DateTime.Now.Date ? dtpStartDate.Value.ToString("yyyy-MM-dd 00:00:00") : DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string endDatetimeData = dtpEndDate.Value.ToString("yyyy-MM-dd " + END_TIME);
                //string[] insertData, selectData, splitData;
                string[] selectData, splitData;
                string[] ComboBoxTextSplit = metroComboBox1.Text.Split('-');
                string[,] resultData; //, resultData2;
                
                splitData = metroComboBox1.Text.Split('-');

                if (splitData.Length > 1 && splitData[1].Trim() == "[기존]")
                {
                    oldShilmentData = splitData[0].Trim();
                }

                // SELECT QUERY [조회]
                // shipment_history_data TABLE
                selectData = new string[] { "model_name", txtModelName.Text };
                query = MainForm.dc.SelectDeleteQueryANDConvert(MainForm.settingData[0], "shipment_history_data", selectData, "SELECT", "start_date", "ASC");
                resultData = MainForm.mariaDB.SelectQuery2(query);

                // 2022.01.17
                // 등록 인터락
                if ((resultData.GetLength(0) > 0 && resultData[resultData.GetLength(0) - 1, 1] == oldShilmentData))
                {
                    MainForm.Guna2Msg("오류", "마스터 기준 출하지[" + oldShilmentData + "]와 마지막 등록된 출하지[" + resultData[resultData.GetLength(0) - 1, 1] + "]가 동일합니다\n\n ※ '" + txtModelName.Text + "' 품번의 출하지 변경 이력을 확인하세요");
                    return;
                }
                else if (resultData.GetLength(0) == 0 && splitData.Length > 1 && splitData[1].Trim() == "[기존]")
                {
                    MainForm.Guna2Msg("오류", "첫 등록은 마스터 기준 출하지[" + oldShilmentData + "]와 동일하게 등록할 수 없습니다");
                    return;
                }
#if !DEBUG
                else if (txtContents.Text.Length < 10)
                {
                    MainForm.Guna2Msg("오류", "등록 내용을 10자 이상 입력해주세요");
                    return;
                }
#endif
                if (dtpStartDate.Value.ToString("yyyy-MM-dd") == dtpEndDate.Value.ToString("yyyy-MM-dd"))
                {
                    startDatetimeData = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    endDatetimeData = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }

                // INSERT QUERY [추가]
                // shipment_history_data TABLE
                string[] insertData = new string[] { txtModelName.Text, ComboBoxTextSplit[0].Trim(), txtContents.Text, startDatetimeData, endDatetimeData, MainForm.userNameData };
                query = MainForm.dc.InsertQueryArrayConvert(MainForm.settingData[0], "shipment_history_data", insertData);
                MainForm.historyInsertComplete = true;

                MainForm.mariaDB.EtcQuery(query);
                Close();

                /*
                // 품번의 마지막 출하지 조회
                // 초도품 등록 내용 조회 [현재 출하지 제외]
                query = "SELECT * FROM ( SELECT * FROM `" + MainForm.settingData[0] + "`.`" + MainForm.settingData[1] + "` WHERE model_name = '" + txtModelName.Text + "' AND NOT shipment = '" + ComboBoxTextSplit[0].Trim() + "' ORDER BY start_date ) AS sort_data GROUP BY sort_data.eo_contents";
                resultData = MainForm.mariaDB.SelectQuery4(query, 17);

                OrderMasterForm_A orderMasterForm_A = new OrderMasterForm_A(txtModelName.Text, ComboBoxTextSplit[0].Trim());
                orderMasterForm_A.ShowDialog();
                orderMasterForm_A.Owner = this;

                // INSERT QUERY [추가]
                // shipment_history_data TABLE
                string[] insertData = new string[] { txtModelName.Text, ComboBoxTextSplit[0].Trim(), txtContents.Text, startDatetimeData, endDatetimeData, MainForm.userNameData };
                query = MainForm.dc.InsertQueryArrayConvert(MainForm.settingData[0], "shipment_history_data", insertData);
                MainForm.historyInsertComplete = true;

                MainForm.mariaDB.EtcQuery(query);
                Close();
                */
            }
            catch (Exception ex)
            {
                MainForm.dc.LogFileSave(ex.Message);
            }
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainForm.dc.ShipmentDays(dtpStartDate, dtpEndDate, metroComboBox1.Text);
            MainForm.dc.Delay(10);
            txtContents.Focus();
        }

        private void metroDateTime1_ValueChanged(object sender, EventArgs e)
        {
            MainForm.dc.ShipmentDays(dtpStartDate, dtpEndDate, metroComboBox1.Text);
            MainForm.dc.Delay(10);
            txtContents.Focus();
        }

        private void txtContents_TextChanged(object sender, EventArgs e)
        {
            label5.Text = txtContents.Text.Length + " / " + txtContents.MaxLength + " byte";
        }
    }
}
