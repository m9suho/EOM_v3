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
    public partial class OrderMasterForm_H : MetroFramework.Forms.MetroForm
    {
        string[] modelData;
        string[,] shipmentHistoryData;

        private void dgvDataAdd(string[,] _data)
        {
            dgvOrderMasterHistory.Rows.Clear();

            for (int i = 0; i < _data.GetLength(0); i++)
            {
                dgvOrderMasterHistory.Rows.Add(false, _data[i, 1], _data[i, 2], _data[i, 3], _data[i, 4], _data[i, 5]);
            }
        }

        public OrderMasterForm_H(string[] _data)
        {
            modelData = _data;
            InitializeComponent();
        }

        private void OrderMasterForm_H_Load(object sender, EventArgs e)
        {
            Text = MainForm.programName + " - " + MainForm.dc.Version();

            txtModelName.Text = modelData[1];

            string query = "SELECT * FROM  `" + MainForm.settingData[0] + "`.`shipment_history_data` WHERE model_name = '" + modelData[1] + "'";
            query += "ORDER BY start_date";
            shipmentHistoryData = MainForm.mariaDB.SelectQuery4(query, 6);

            if (shipmentHistoryData.GetLength(0) <= 0)
            {
                MainForm.dc.Msg("에러", "등록된 데이터가 없습니다");
                Close();
            }

            dgvDataAdd(shipmentHistoryData);
        }
    }
}
