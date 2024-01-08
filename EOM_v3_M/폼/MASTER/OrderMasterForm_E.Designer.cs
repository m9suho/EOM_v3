namespace EOM_v3_M
{
    partial class OrderMasterForm_E
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label4 = new System.Windows.Forms.Label();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtContents = new MetroFramework.Controls.MetroTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtModelName = new System.Windows.Forms.TextBox();
            this.metroComboBox1 = new MetroFramework.Controls.MetroComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnTitle = new System.Windows.Forms.Panel();
            this.txtAllSearchBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2ControlBox3 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.lblTopDeco = new System.Windows.Forms.Label();
            this.dtpStartDate = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.dtpEndDate = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.pnTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(23, 406);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(442, 53);
            this.label4.TabIndex = 72;
            this.label4.Text = "※ 출하지 변경사유를 정확하게 작성하지 않으면,\r\n　 차후에 사유서를 작성하오니 참고하시기 바랍니다.";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // metroButton1
            // 
            this.metroButton1.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroButton1.FontWeight = MetroFramework.MetroButtonWeight.Light;
            this.metroButton1.Location = new System.Drawing.Point(471, 406);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(144, 53);
            this.metroButton1.TabIndex = 73;
            this.metroButton1.Text = "변경 적용";
            this.metroButton1.UseSelectable = true;
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(516, 379);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 23);
            this.label5.TabIndex = 74;
            this.label5.Text = "0 / 500 byte";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label6.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(23, 136);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 29);
            this.label6.TabIndex = 87;
            this.label6.Text = "적용일";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtContents
            // 
            // 
            // 
            // 
            this.txtContents.CustomButton.Image = null;
            this.txtContents.CustomButton.Location = new System.Drawing.Point(304, 1);
            this.txtContents.CustomButton.Name = "";
            this.txtContents.CustomButton.Size = new System.Drawing.Size(197, 197);
            this.txtContents.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtContents.CustomButton.TabIndex = 1;
            this.txtContents.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtContents.CustomButton.UseSelectable = true;
            this.txtContents.CustomButton.Visible = false;
            this.txtContents.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.txtContents.Lines = new string[0];
            this.txtContents.Location = new System.Drawing.Point(120, 174);
            this.txtContents.MaxLength = 200;
            this.txtContents.Multiline = true;
            this.txtContents.Name = "txtContents";
            this.txtContents.PasswordChar = '\0';
            this.txtContents.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtContents.SelectedText = "";
            this.txtContents.SelectionLength = 0;
            this.txtContents.SelectionStart = 0;
            this.txtContents.ShortcutsEnabled = true;
            this.txtContents.Size = new System.Drawing.Size(502, 199);
            this.txtContents.TabIndex = 0;
            this.txtContents.UseSelectable = true;
            this.txtContents.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtContents.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtContents.TextChanged += new System.EventHandler(this.txtContents_TextChanged);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(23, 174);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 199);
            this.label3.TabIndex = 85;
            this.label3.Text = "변경사유";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtModelName
            // 
            this.txtModelName.Enabled = false;
            this.txtModelName.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtModelName.Location = new System.Drawing.Point(120, 60);
            this.txtModelName.Name = "txtModelName";
            this.txtModelName.Size = new System.Drawing.Size(200, 29);
            this.txtModelName.TabIndex = 84;
            this.txtModelName.Text = "96160-S1020CDD";
            // 
            // metroComboBox1
            // 
            this.metroComboBox1.FormattingEnabled = true;
            this.metroComboBox1.ItemHeight = 23;
            this.metroComboBox1.Items.AddRange(new object[] {
            "OEM",
            "CKD",
            "KD"});
            this.metroComboBox1.Location = new System.Drawing.Point(120, 98);
            this.metroComboBox1.Name = "metroComboBox1";
            this.metroComboBox1.Size = new System.Drawing.Size(200, 29);
            this.metroComboBox1.TabIndex = 83;
            this.metroComboBox1.UseSelectable = true;
            this.metroComboBox1.SelectedIndexChanged += new System.EventHandler(this.metroComboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(23, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 29);
            this.label2.TabIndex = 82;
            this.label2.Text = "출하지";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(23, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 29);
            this.label1.TabIndex = 81;
            this.label1.Text = "품번";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnTitle
            // 
            this.pnTitle.Controls.Add(this.txtAllSearchBox);
            this.pnTitle.Controls.Add(this.guna2ControlBox3);
            this.pnTitle.Controls.Add(this.lblFormTitle);
            this.pnTitle.Controls.Add(this.lblTopDeco);
            this.pnTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.pnTitle.Location = new System.Drawing.Point(0, 0);
            this.pnTitle.Name = "pnTitle";
            this.pnTitle.Size = new System.Drawing.Size(638, 50);
            this.pnTitle.TabIndex = 91;
            // 
            // txtAllSearchBox
            // 
            this.txtAllSearchBox.BackColor = System.Drawing.Color.White;
            this.txtAllSearchBox.BorderRadius = 8;
            this.txtAllSearchBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtAllSearchBox.DefaultText = "";
            this.txtAllSearchBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtAllSearchBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtAllSearchBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtAllSearchBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtAllSearchBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtAllSearchBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtAllSearchBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtAllSearchBox.Location = new System.Drawing.Point(971, 8);
            this.txtAllSearchBox.Name = "txtAllSearchBox";
            this.txtAllSearchBox.PasswordChar = '\0';
            this.txtAllSearchBox.PlaceholderText = "";
            this.txtAllSearchBox.SelectedText = "";
            this.txtAllSearchBox.Size = new System.Drawing.Size(200, 23);
            this.txtAllSearchBox.TabIndex = 82;
            // 
            // guna2ControlBox3
            // 
            this.guna2ControlBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox3.ControlBoxStyle = Guna.UI2.WinForms.Enums.ControlBoxStyle.Custom;
            this.guna2ControlBox3.FillColor = System.Drawing.Color.White;
            this.guna2ControlBox3.IconColor = System.Drawing.SystemColors.ControlDarkDark;
            this.guna2ControlBox3.Location = new System.Drawing.Point(608, 6);
            this.guna2ControlBox3.Name = "guna2ControlBox3";
            this.guna2ControlBox3.Size = new System.Drawing.Size(30, 25);
            this.guna2ControlBox3.TabIndex = 85;
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.BackColor = System.Drawing.Color.White;
            this.lblFormTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormTitle.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblFormTitle.Location = new System.Drawing.Point(0, 5);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblFormTitle.Size = new System.Drawing.Size(638, 45);
            this.lblFormTitle.TabIndex = 79;
            this.lblFormTitle.Text = "출하지 수정";
            this.lblFormTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTopDeco
            // 
            this.lblTopDeco.BackColor = System.Drawing.Color.DodgerBlue;
            this.lblTopDeco.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTopDeco.Location = new System.Drawing.Point(0, 0);
            this.lblTopDeco.Name = "lblTopDeco";
            this.lblTopDeco.Size = new System.Drawing.Size(638, 5);
            this.lblTopDeco.TabIndex = 1;
            this.lblTopDeco.Text = " ";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dtpStartDate.BorderRadius = 8;
            this.dtpStartDate.BorderThickness = 1;
            this.dtpStartDate.Checked = true;
            this.dtpStartDate.CustomFormat = "yyyy년 M월 d일 dddd";
            this.dtpStartDate.FillColor = System.Drawing.Color.White;
            this.dtpStartDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(120, 136);
            this.dtpStartDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpStartDate.MinDate = new System.DateTime(2016, 8, 22, 0, 0, 0, 0);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(248, 29);
            this.dtpStartDate.TabIndex = 128;
            this.dtpStartDate.Value = new System.DateTime(2023, 3, 25, 3, 55, 44, 811);
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dtpEndDate.BorderRadius = 8;
            this.dtpEndDate.BorderThickness = 1;
            this.dtpEndDate.Checked = true;
            this.dtpEndDate.CustomFormat = "yyyy년 M월 d일 dddd";
            this.dtpEndDate.FillColor = System.Drawing.Color.White;
            this.dtpEndDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(374, 136);
            this.dtpEndDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpEndDate.MinDate = new System.DateTime(2016, 8, 22, 0, 0, 0, 0);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(248, 29);
            this.dtpEndDate.TabIndex = 129;
            this.dtpEndDate.Value = new System.DateTime(2023, 3, 25, 3, 55, 44, 811);
            // 
            // OrderMasterForm_E
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(638, 474);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.pnTitle);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtContents);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtModelName);
            this.Controls.Add(this.metroComboBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OrderMasterForm_E";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "OrderMasterForm_E";
            this.Load += new System.EventHandler(this.OrderMasterForm_E_Load);
            this.pnTitle.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label4;
        private MetroFramework.Controls.MetroButton metroButton1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private MetroFramework.Controls.MetroTextBox txtContents;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtModelName;
        private MetroFramework.Controls.MetroComboBox metroComboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnTitle;
        private Guna.UI2.WinForms.Guna2TextBox txtAllSearchBox;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox3;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Label lblTopDeco;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpStartDate;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpEndDate;
    }
}