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
            this.label7 = new System.Windows.Forms.Label();
            this.metroDateTime2 = new MetroFramework.Controls.MetroDateTime();
            this.metroDateTime1 = new MetroFramework.Controls.MetroDateTime();
            this.label6 = new System.Windows.Forms.Label();
            this.txtContents = new MetroFramework.Controls.MetroTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtModelName = new System.Windows.Forms.TextBox();
            this.metroComboBox1 = new MetroFramework.Controls.MetroComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
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
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(326, 145);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 12);
            this.label7.TabIndex = 90;
            this.label7.Text = "~";
            // 
            // metroDateTime2
            // 
            this.metroDateTime2.Location = new System.Drawing.Point(345, 136);
            this.metroDateTime2.MinimumSize = new System.Drawing.Size(0, 29);
            this.metroDateTime2.Name = "metroDateTime2";
            this.metroDateTime2.Size = new System.Drawing.Size(200, 29);
            this.metroDateTime2.TabIndex = 89;
            // 
            // metroDateTime1
            // 
            this.metroDateTime1.Location = new System.Drawing.Point(120, 136);
            this.metroDateTime1.MinimumSize = new System.Drawing.Size(0, 29);
            this.metroDateTime1.Name = "metroDateTime1";
            this.metroDateTime1.Size = new System.Drawing.Size(200, 29);
            this.metroDateTime1.TabIndex = 88;
            this.metroDateTime1.ValueChanged += new System.EventHandler(this.metroDateTime1_ValueChanged);
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
            this.txtContents.CustomButton.Location = new System.Drawing.Point(296, 1);
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
            this.txtContents.Size = new System.Drawing.Size(494, 199);
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
            // OrderMasterForm_E
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 474);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.metroDateTime2);
            this.Controls.Add(this.metroDateTime1);
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
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OrderMasterForm_E";
            this.Resizable = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "OrderMasterForm_E";
            this.Load += new System.EventHandler(this.OrderMasterForm_E_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label4;
        private MetroFramework.Controls.MetroButton metroButton1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private MetroFramework.Controls.MetroDateTime metroDateTime2;
        private MetroFramework.Controls.MetroDateTime metroDateTime1;
        private System.Windows.Forms.Label label6;
        private MetroFramework.Controls.MetroTextBox txtContents;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtModelName;
        private MetroFramework.Controls.MetroComboBox metroComboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}