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
    public partial class OrderMasterForm : MetroFramework.Forms.MetroForm
    {
        private void UpdateDataGridView()
        {
            string query = string.Empty;
            string[,] orderData = new string[0, 0];
            string[,] contentsData = new string[0, 0];
            string[,] totalData;

            // 2022.01.17
            // 라디오 버튼
            if (rdoContentsAll.Checked)
            {
                rdoShipmentAll.Checked = true;
                rdoYearAll.Checked = true;

                rdoShipmentAll.Enabled = false;
                rdoShipmentOEM.Enabled = false;
                rdoShipmentCKD.Enabled = false;
                rdoShipmentKD.Enabled = false;
                rdoYearAll.Enabled = false;
                rdoYear1.Enabled = false;
                rdoYear2.Enabled = false;
                rdoYear3.Enabled = false;
            }
            else
            {
                rdoShipmentAll.Enabled = true;
                rdoShipmentOEM.Enabled = true;
                rdoShipmentCKD.Enabled = true;
                rdoShipmentKD.Enabled = true;
                rdoYearAll.Enabled = true;
                rdoYear1.Enabled = true;
                rdoYear2.Enabled = true;
                rdoYear3.Enabled = true;

                //rdoShipmentAll.Checked = true;
                //rdoYearAll.Checked = true;
            }

            // 품번, 기준 출하지
            try
            {
                // 1. 시작일 기준 가장 낮은 값
                // 2. 중복 제거
                query = MainForm.pe.OriginalShipment(MainForm.settingData[0], MainForm.cbbMetroLine01.Text);

                orderData = MainForm.mariaDB.SelectQuery4(query, 3);
            }
            catch (Exception ex)
            {
                MainForm.dc.LogFileSave(ex.Message);
            }

            // 변경 출하지, 변경 시작일, 변경 종료일, 변경 사용자
            try
            {
                // 1. 시작일 기준 가장 높은 값
                // 2. 중복 제거
                query = "SELECT * FROM ( SELECT * FROM ( SELECT * FROM `" + MainForm.settingData[0] + "`.`shipment_history_data` ";

                if (rdoProcessIng.Checked)
                {
                    query += "WHERE end_date > '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                }
                else if (rdoProcessEnd.Checked)
                {
                    query += "WHERE end_date < '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                }

                query += "ORDER BY start_date DESC LIMIT 18446744073709551615 ) AS sort_data GROUP BY sort_data.model_name ) AS total_data ";

                //if (rdoProcessIng.Checked || rdoProcessEnd.Checked)
                {
                    // 기간 전체 False
                    if (!rdoYearAll.Checked)
                    {
                        string[] year = rdoYear1.Checked ? rdoYear1.Text.Split('년') : rdoYear2.Checked ? rdoYear2.Text.Split('년') : rdoYear3.Text.Split('년');
                        query += "WHERE start_date >= '" + year[0] + "-01-01 00:00:00' AND start_date < '" + year[0] + "-12-31 23:59:59' ";
                    }

                    // 출하지 전체 False
                    if (!rdoShipmentAll.Checked)
                    {
                        string shipment = rdoShipmentOEM.Checked ? "OEM" : rdoShipmentCKD.Checked ? "CKD" : "KD";
                        query += "WHERE order_type_variableness = '" + shipment + "'";

                        if (!rdoYearAll.Checked)
                        {
                            query = query.Replace("WHERE order_type", "AND order_type");
                        }
                    }
                }

                //Clipboard.SetText(query);

                contentsData = MainForm.mariaDB.SelectQuery4(query, 6);

                totalData = new string[orderData.GetLength(0), orderData.GetLength(1) + 5];

                // 데이터 병합
                // 추후 데이터베이스에서 취합
                for (int i = 0; i < orderData.GetLength(0); i++)
                {
                    totalData[i, 0] = orderData[i, 0];
                    totalData[i, 1] = orderData[i, 1];
                    totalData[i, 2] = orderData[i, 2];
                    totalData[i, 3] = string.Empty;         // order_type_variableness
                    totalData[i, 4] = string.Empty;         // contents
                    totalData[i, 5] = string.Empty;         // start_date
                    totalData[i, 6] = string.Empty;         // end_date
                    totalData[i, 7] = string.Empty;         // regstrant

                    for (int j = 0; j < contentsData.GetLength(0); j++)
                    {
                        //MainForm.dc.LogFileSave("[" + i + "] " + orderData[i, 1] + " == [" + j + "] " + contentsData[j, 0]);
                        //MainForm.dc.LogFileSave("&&");
                        //MainForm.dc.LogFileSave(orderData[i, 2] + " != " + contentsData[j, 1]);

                        // 태그 등록 '품번' == 출하지 등록 '품번' && 태그 등록 '출하지' != 출하지 등록 '출하지'
                        //if (orderData[i, 1] == contentsData[j, 0] && orderData[i, 2] != contentsData[j, 1])
                        // 2022.06.10
                        // 최종 출하지를 남기기 위한 조건 변경
                        if (orderData[i, 1] == contentsData[j, 0])
                        {
                            totalData[i, 3] = contentsData[j, 1];   // order_type_variableness
                            totalData[i, 4] = contentsData[j, 2];   // contents
                            totalData[i, 5] = contentsData[j, 3];   // start_date
                            totalData[i, 6] = contentsData[j, 4];   // end_date
                            totalData[i, 7] = contentsData[j, 5];   // regstrant

                            break;
                        }
                    }
                }

                dgvDataAdd(totalData);
            }
            catch (Exception ex)
            {
                MainForm.dc.LogFileSave(ex.Message);
            }
        }

        private void MouseRightMenuDGV(DataGridView _dgv, string[] _data)
        {
            ContextMenu m = new ContextMenu();

            MenuItem menuItem1 = new MenuItem("출하지 수정");
            m.MenuItems.Add(menuItem1);

            MenuItem menuItemLine1 = new MenuItem("-");
            m.MenuItems.Add(menuItemLine1);

            MenuItem menuItem2 = new MenuItem("출하지 변경이력");
            m.MenuItems.Add(menuItem2);

            menuItem1.Click += new EventHandler((sender, e) => ShipmentEdit(sender, e, _data));
            menuItem2.Click += new EventHandler((sender, e) => ShipmentHistory(sender, e, _data));
            //menuItem1.Click += new EventHandler(ShipmentEdit);
            //menuItem2.Click += new EventHandler(ShipmentHistory);

            m.Show(_dgv, _dgv.PointToClient(Cursor.Position));
        }

        private void MouseRightClickView(DataGridView _dataGridView, DataGridViewCellMouseEventArgs _e)
        {
            try
            {
                if (_e.RowIndex != -1)
                {
                    if (_e.Button == MouseButtons.Right)
                    {
                        _dataGridView.Rows[_e.RowIndex].Selected = true;                         // 마우스 우 클릭 선택
                        _dataGridView.CurrentCell = _dataGridView.Rows[_e.RowIndex].Cells[0];    // index 보정

                        string[] data = new string[_dataGridView.ColumnCount];

                        for (int i = 0; i < data.Length; i++)
                        {
                            data[i] = _dataGridView.Rows[_dataGridView.CurrentCell.RowIndex].Cells[i].Value.ToString();

                            MainForm.dc.LogFileSave(i + "/" + data[i]);
                        }

                        MouseRightMenuDGV(_dataGridView, data);
                    }
                }
            }
            catch (Exception ex)
            {
                MainForm.dc.Msg("경고", ex.Message, true);
            }
        }

        private void ShipmentEdit(object sender, EventArgs e, string[] _data)
        {
            OrderMasterForm_E orderMasterForm_E = new OrderMasterForm_E(_data);

            orderMasterForm_E.Owner = this;
            orderMasterForm_E.ShowDialog();

            if (MainForm.historyInsertComplete)
            {
                UpdateDataGridView();

                MainForm.historyInsertComplete = false;
            }

            MainForm.historyInsertComplete = false;
        }

        private void ShipmentHistory(object sender, EventArgs e, string[] _data)
        {
            OrderMasterForm_H orderMasterForm_H = new OrderMasterForm_H(_data);
            orderMasterForm_H.Owner = this;
            orderMasterForm_H.ShowDialog();
        }

        private void dgvDataAdd(string[,] _data)
        {
            dgvOrderMaster.Rows.Clear();

            for (int i = 0; i < _data.GetLength(0); i++)
            {
                // 전체
                if (rdoContentsAll.Checked)
                {
                    dgvOrderMaster.Rows.Add(false, _data[i, 1], _data[i, 2], _data[i, 3], _data[i, 4], _data[i, 5], _data[i, 6], _data[i, 7]);
                }
                // 진행 중, 진행 종료
                else if ((rdoProcessIng.Checked || rdoProcessEnd.Checked) && _data[i, 3] != "")
                {
                    dgvOrderMaster.Rows.Add(false, _data[i, 1], _data[i, 2], _data[i, 3], _data[i, 4], _data[i, 5], _data[i, 6], _data[i, 7]);
                }
            }

            if (dgvOrderMaster.RowCount <= 0)
            {
                //rdoContentsAll.Checked = true;
                return;
            }
        }

        public OrderMasterForm()
        {
            InitializeComponent();
        }

        private void OrderMasterForm_Load(object sender, EventArgs e)
        {
            Text = MainForm.programName + " - " + MainForm.dc.Version();

            // 2021.04.12
            // 조회 기간 추가
            rdoYear1.Text = DateTime.Now.Year + "년";
            rdoYear2.Text = (DateTime.Now.Year - 1) + "년";
            rdoYear3.Text = (DateTime.Now.Year - 2) + "년";

            UpdateDataGridView();
        }

        private void SearchMetroBtn_Click(object sender, EventArgs e)
        {
            SearchForm searchForm = new SearchForm();

            MainForm.searchType = "SHIPMENT";
            searchForm.Owner = this;
            searchForm.ShowDialog();

            if (MainForm.searchData[0] != string.Empty)
            {
                rdoContentsAll.Checked = true;

                SearchSelectDataGridView(MainForm.searchData[0]);

                MainForm.searchData[0] = string.Empty;
                MainForm.searchData[1] = string.Empty;
                //MessageBox.Show(MainForm.searchData[0]);
                //MessageBox.Show(MainForm.searchData[1]);
            }
        }

        private void SearchSelectDataGridView(string _data)
        {
            if (dgvOrderMaster.RowCount > 0)
            {
                dgvOrderMaster[0, dgvOrderMaster.RowCount - 1].Selected = true;
                dgvOrderMaster[0, dgvOrderMaster.RowCount - 1].Selected = false;
            }

            for (int i = 0; i < dgvOrderMaster.RowCount; i++)
            {
                if (dgvOrderMaster.Rows[i].Cells[1].Value.ToString() == _data)
                {
                    dgvOrderMaster.Enabled = false;
                    MainForm.dc.Delay(500);
                    dgvOrderMaster.Enabled = true;

                    dgvOrderMaster[0, i].Selected = true;
                    break;
                }
            }
        }

        private void AddMetroBtn_Click(object sender, EventArgs e)
        {

        }

        private void DelMetroBtn_Click(object sender, EventArgs e)
        {

        }

        private void rdoShipmentAll_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                UpdateDataGridView();
            }
        }

        private void rdoShipmentOEM_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                UpdateDataGridView();
            }
        }

        private void rdoShipmentCKD_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                UpdateDataGridView();
            }
        }

        private void rdoShipmentKD_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                UpdateDataGridView();
            }
        }

        private void rdoContentsAll_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                UpdateDataGridView();
            }
        }

        private void rdoProcessIng_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                UpdateDataGridView();
            }
        }

        private void rdoProcessEnd_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                UpdateDataGridView();
            }
        }

        private void rdoYearAll_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                UpdateDataGridView();
            }
        }

        private void rdoYear1_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                UpdateDataGridView();
            }
        }

        private void rdoYear2_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                UpdateDataGridView();
            }
        }

        private void rdoYear3_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                UpdateDataGridView();
            }
        }

        private void dgvOrderMaster_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            MouseRightClickView(dgvOrderMaster, e);
        }
    }
}
