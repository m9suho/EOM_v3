namespace EOM_v3_M
{
    partial class ModelForm_E
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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.txtCarName = new System.Windows.Forms.TextBox();
            this.txtModelName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCustomerEO = new System.Windows.Forms.TextBox();
            this.txtMobisEO = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTagType = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtEOContents = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.SelectMetroBtn = new MetroFramework.Controls.MetroButton();
            this.AddMetroBtn = new MetroFramework.Controls.MetroButton();
            this.ViewMetroBtn = new MetroFramework.Controls.MetroButton();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSubPCB = new System.Windows.Forms.TextBox();
            this.txtMainPCB = new System.Windows.Forms.TextBox();
            this.chkNonInput = new System.Windows.Forms.CheckBox();
            this.chkPrivate = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtEOType = new MetroFramework.Controls.MetroTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblSticker = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(9, 426);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 21);
            this.label4.TabIndex = 25;
            this.label4.Text = "적용일";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(9, 232);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 21);
            this.label3.TabIndex = 24;
            this.label3.Text = "모비스 EO";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(9, 204);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 21);
            this.label2.TabIndex = 23;
            this.label2.Text = "고객사 EO";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(9, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 21);
            this.label1.TabIndex = 22;
            this.label1.Text = "품번";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy년 M월 d일 dddd [ss초]";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(94, 426);
            this.dateTimePicker1.MinDate = new System.DateTime(2016, 8, 22, 0, 0, 0, 0);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(324, 21);
            this.dateTimePicker1.TabIndex = 11;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // txtCarName
            // 
            this.txtCarName.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.txtCarName.Location = new System.Drawing.Point(94, 92);
            this.txtCarName.MaxLength = 20;
            this.txtCarName.Name = "txtCarName";
            this.txtCarName.Size = new System.Drawing.Size(324, 21);
            this.txtCarName.TabIndex = 1;
            this.txtCarName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCarName.TextChanged += new System.EventHandler(this.txtCarName_TextChanged);
            this.txtCarName.Enter += new System.EventHandler(this.txtCarName_Enter);
            this.txtCarName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCarName_KeyPress);
            this.txtCarName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCarName_KeyUp);
            this.txtCarName.Leave += new System.EventHandler(this.txtCarName_Leave);
            // 
            // txtModelName
            // 
            this.txtModelName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtModelName.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.txtModelName.Location = new System.Drawing.Point(94, 64);
            this.txtModelName.MaxLength = 20;
            this.txtModelName.Name = "txtModelName";
            this.txtModelName.Size = new System.Drawing.Size(324, 21);
            this.txtModelName.TabIndex = 0;
            this.txtModelName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(9, 366);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 23);
            this.label5.TabIndex = 27;
            this.label5.Text = "출하지";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(9, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 21);
            this.label6.TabIndex = 29;
            this.label6.Text = "차종";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCustomerEO
            // 
            this.txtCustomerEO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCustomerEO.Enabled = false;
            this.txtCustomerEO.Location = new System.Drawing.Point(94, 204);
            this.txtCustomerEO.MaxLength = 8;
            this.txtCustomerEO.Name = "txtCustomerEO";
            this.txtCustomerEO.Size = new System.Drawing.Size(254, 21);
            this.txtCustomerEO.TabIndex = 5;
            this.txtCustomerEO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCustomerEO.Enter += new System.EventHandler(this.txtCustomerEO_Enter);
            this.txtCustomerEO.Leave += new System.EventHandler(this.txtCustomerEO_Leave);
            // 
            // txtMobisEO
            // 
            this.txtMobisEO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMobisEO.Enabled = false;
            this.txtMobisEO.Location = new System.Drawing.Point(94, 232);
            this.txtMobisEO.MaxLength = 8;
            this.txtMobisEO.Name = "txtMobisEO";
            this.txtMobisEO.Size = new System.Drawing.Size(254, 21);
            this.txtMobisEO.TabIndex = 6;
            this.txtMobisEO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMobisEO.Enter += new System.EventHandler(this.txtMobisEO_Enter);
            this.txtMobisEO.Leave += new System.EventHandler(this.txtMobisEO_Leave);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label7.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label7.Location = new System.Drawing.Point(9, 176);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 21);
            this.label7.TabIndex = 31;
            this.label7.Text = "태그 구분";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTagType
            // 
            this.txtTagType.Enabled = false;
            this.txtTagType.Location = new System.Drawing.Point(94, 176);
            this.txtTagType.MaxLength = 10;
            this.txtTagType.Name = "txtTagType";
            this.txtTagType.Size = new System.Drawing.Size(324, 21);
            this.txtTagType.TabIndex = 4;
            this.txtTagType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTagType.Enter += new System.EventHandler(this.txtTagType_Enter);
            this.txtTagType.Leave += new System.EventHandler(this.txtTagType_Leave);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label8.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label8.Location = new System.Drawing.Point(9, 260);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 42);
            this.label8.TabIndex = 33;
            this.label8.Text = "EO 내용";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtEOContents
            // 
            this.txtEOContents.Location = new System.Drawing.Point(94, 260);
            this.txtEOContents.MaxLength = 10;
            this.txtEOContents.Multiline = true;
            this.txtEOContents.Name = "txtEOContents";
            this.txtEOContents.ReadOnly = true;
            this.txtEOContents.Size = new System.Drawing.Size(254, 42);
            this.txtEOContents.TabIndex = 8;
            this.txtEOContents.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label9.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label9.Location = new System.Drawing.Point(9, 454);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 21);
            this.label9.TabIndex = 34;
            this.label9.Text = "마감일";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CustomFormat = "yyyy년 M월 d일 dddd";
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(94, 454);
            this.dateTimePicker2.MinDate = new System.DateTime(2016, 8, 22, 0, 0, 0, 0);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(324, 21);
            this.dateTimePicker2.TabIndex = 12;
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            // 
            // SelectMetroBtn
            // 
            this.SelectMetroBtn.FontWeight = MetroFramework.MetroButtonWeight.Light;
            this.SelectMetroBtn.Location = new System.Drawing.Point(354, 204);
            this.SelectMetroBtn.Name = "SelectMetroBtn";
            this.SelectMetroBtn.Size = new System.Drawing.Size(64, 49);
            this.SelectMetroBtn.TabIndex = 7;
            this.SelectMetroBtn.Text = "선택";
            this.SelectMetroBtn.UseSelectable = true;
            this.SelectMetroBtn.Click += new System.EventHandler(this.SelectMetroBtn_Click);
            // 
            // AddMetroBtn
            // 
            this.AddMetroBtn.FontWeight = MetroFramework.MetroButtonWeight.Light;
            this.AddMetroBtn.Location = new System.Drawing.Point(322, 491);
            this.AddMetroBtn.Name = "AddMetroBtn";
            this.AddMetroBtn.Size = new System.Drawing.Size(96, 31);
            this.AddMetroBtn.TabIndex = 13;
            this.AddMetroBtn.Text = "수정";
            this.AddMetroBtn.UseSelectable = true;
            this.AddMetroBtn.Click += new System.EventHandler(this.AddMetroBtn_Click);
            // 
            // ViewMetroBtn
            // 
            this.ViewMetroBtn.FontWeight = MetroFramework.MetroButtonWeight.Light;
            this.ViewMetroBtn.Location = new System.Drawing.Point(355, 260);
            this.ViewMetroBtn.Name = "ViewMetroBtn";
            this.ViewMetroBtn.Size = new System.Drawing.Size(63, 20);
            this.ViewMetroBtn.TabIndex = 9;
            this.ViewMetroBtn.Text = "작변 보기";
            this.ViewMetroBtn.UseSelectable = true;
            this.ViewMetroBtn.Click += new System.EventHandler(this.ViewMetroBtn_Click);
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label10.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label10.Location = new System.Drawing.Point(9, 148);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 21);
            this.label10.TabIndex = 50;
            this.label10.Text = "서브 PCB";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label11.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label11.Location = new System.Drawing.Point(9, 120);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 21);
            this.label11.TabIndex = 49;
            this.label11.Text = "메인 PCB";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtSubPCB
            // 
            this.txtSubPCB.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSubPCB.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.txtSubPCB.Location = new System.Drawing.Point(94, 148);
            this.txtSubPCB.MaxLength = 12;
            this.txtSubPCB.Name = "txtSubPCB";
            this.txtSubPCB.Size = new System.Drawing.Size(254, 21);
            this.txtSubPCB.TabIndex = 3;
            this.txtSubPCB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSubPCB.TextChanged += new System.EventHandler(this.txtSubPCB_TextChanged);
            this.txtSubPCB.Enter += new System.EventHandler(this.txtSubPCB_Enter);
            this.txtSubPCB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSubPCB_KeyPress);
            this.txtSubPCB.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSubPCB_KeyUp);
            this.txtSubPCB.Leave += new System.EventHandler(this.txtSubPCB_Leave);
            // 
            // txtMainPCB
            // 
            this.txtMainPCB.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMainPCB.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.txtMainPCB.Location = new System.Drawing.Point(94, 120);
            this.txtMainPCB.MaxLength = 12;
            this.txtMainPCB.Name = "txtMainPCB";
            this.txtMainPCB.Size = new System.Drawing.Size(324, 21);
            this.txtMainPCB.TabIndex = 2;
            this.txtMainPCB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMainPCB.TextChanged += new System.EventHandler(this.txtMainPCB_TextChanged);
            this.txtMainPCB.Enter += new System.EventHandler(this.txtMainPCB_Enter);
            this.txtMainPCB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMainPCB_KeyPress);
            this.txtMainPCB.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtMainPCB_KeyUp);
            this.txtMainPCB.Leave += new System.EventHandler(this.txtMainPCB_Leave);
            // 
            // chkNonInput
            // 
            this.chkNonInput.AutoSize = true;
            this.chkNonInput.Location = new System.Drawing.Point(356, 151);
            this.chkNonInput.Name = "chkNonInput";
            this.chkNonInput.Size = new System.Drawing.Size(64, 16);
            this.chkNonInput.TabIndex = 51;
            this.chkNonInput.Text = "미 입력";
            this.chkNonInput.UseVisualStyleBackColor = true;
            this.chkNonInput.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // chkPrivate
            // 
            this.chkPrivate.AutoSize = true;
            this.chkPrivate.Enabled = false;
            this.chkPrivate.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkPrivate.ForeColor = System.Drawing.Color.Red;
            this.chkPrivate.Location = new System.Drawing.Point(356, 287);
            this.chkPrivate.Name = "chkPrivate";
            this.chkPrivate.Size = new System.Drawing.Size(68, 15);
            this.chkPrivate.TabIndex = 52;
            this.chkPrivate.Text = "내부관리";
            this.chkPrivate.UseVisualStyleBackColor = true;
            this.chkPrivate.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label12.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label12.Location = new System.Drawing.Point(9, 396);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 23);
            this.label12.TabIndex = 54;
            this.label12.Text = "EO 구분";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtEOType
            // 
            // 
            // 
            // 
            this.txtEOType.CustomButton.Image = null;
            this.txtEOType.CustomButton.Location = new System.Drawing.Point(302, 1);
            this.txtEOType.CustomButton.Name = "";
            this.txtEOType.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtEOType.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtEOType.CustomButton.TabIndex = 1;
            this.txtEOType.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtEOType.CustomButton.UseSelectable = true;
            this.txtEOType.CustomButton.Visible = false;
            this.txtEOType.Enabled = false;
            this.txtEOType.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtEOType.Lines = new string[0];
            this.txtEOType.Location = new System.Drawing.Point(94, 396);
            this.txtEOType.MaxLength = 32767;
            this.txtEOType.Name = "txtEOType";
            this.txtEOType.PasswordChar = '\0';
            this.txtEOType.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtEOType.SelectedText = "";
            this.txtEOType.SelectionLength = 0;
            this.txtEOType.SelectionStart = 0;
            this.txtEOType.ShortcutsEnabled = true;
            this.txtEOType.Size = new System.Drawing.Size(324, 23);
            this.txtEOType.TabIndex = 55;
            this.txtEOType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtEOType.UseSelectable = true;
            this.txtEOType.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtEOType.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label13.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label13.Location = new System.Drawing.Point(9, 309);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 51);
            this.label13.TabIndex = 56;
            this.label13.Text = "스티커 색상";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblSticker);
            this.groupBox1.Location = new System.Drawing.Point(94, 302);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(324, 58);
            this.groupBox1.TabIndex = 57;
            this.groupBox1.TabStop = false;
            // 
            // lblSticker
            // 
            this.lblSticker.BackColor = System.Drawing.Color.Black;
            this.lblSticker.Font = new System.Drawing.Font("현대하모니 L", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblSticker.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblSticker.Location = new System.Drawing.Point(2, 9);
            this.lblSticker.Name = "lblSticker";
            this.lblSticker.Size = new System.Drawing.Size(320, 46);
            this.lblSticker.TabIndex = 0;
            this.lblSticker.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "OEM",
            "CKD",
            "KD"});
            this.comboBox1.Location = new System.Drawing.Point(94, 366);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(323, 23);
            this.comboBox1.TabIndex = 58;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // ModelForm_E
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(427, 538);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtEOType);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.chkPrivate);
            this.Controls.Add(this.chkNonInput);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtSubPCB);
            this.Controls.Add(this.txtMainPCB);
            this.Controls.Add(this.ViewMetroBtn);
            this.Controls.Add(this.AddMetroBtn);
            this.Controls.Add(this.SelectMetroBtn);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtEOContents);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtTagType);
            this.Controls.Add(this.txtMobisEO);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtCustomerEO);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.txtCarName);
            this.Controls.Add(this.txtModelName);
            this.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModelForm_E";
            this.Resizable = false;
            this.ShowInTaskbar = false;
            this.Text = "EditForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ModelForm_FormClosing);
            this.Load += new System.EventHandler(this.ModelForm_E_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox txtCarName;
        private System.Windows.Forms.TextBox txtModelName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCustomerEO;
        private System.Windows.Forms.TextBox txtMobisEO;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTagType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtEOContents;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private MetroFramework.Controls.MetroButton SelectMetroBtn;
        private MetroFramework.Controls.MetroButton AddMetroBtn;
        private MetroFramework.Controls.MetroButton ViewMetroBtn;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtSubPCB;
        private System.Windows.Forms.TextBox txtMainPCB;
        private System.Windows.Forms.CheckBox chkNonInput;
        private System.Windows.Forms.CheckBox chkPrivate;
        private System.Windows.Forms.Label label12;
        private MetroFramework.Controls.MetroTextBox txtEOType;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblSticker;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}