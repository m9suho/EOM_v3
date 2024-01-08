namespace EOM_v3_M
{
    partial class SearchForm
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
            this.components = new System.ComponentModel.Container();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pnTitle = new System.Windows.Forms.Panel();
            this.txtAllSearchBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2ControlBox3 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.lblTopDeco = new System.Windows.Forms.Label();
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.txtSearch = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnProductSearch = new Guna.UI2.WinForms.Guna2Button();
            this.guna2TaskBarProgress1 = new Guna.UI2.WinForms.Guna2TaskBarProgress(this.components);
            this.guna2ProgressIndicator1 = new Guna.UI2.WinForms.Guna2ProgressIndicator();
            this.guna2GroupBox1 = new Guna.UI2.WinForms.Guna2GroupBox();
            this.radioButton7 = new Guna.UI2.WinForms.Guna2RadioButton();
            this.radioButton6 = new Guna.UI2.WinForms.Guna2RadioButton();
            this.radioButton5 = new Guna.UI2.WinForms.Guna2RadioButton();
            this.radioButton4 = new Guna.UI2.WinForms.Guna2RadioButton();
            this.radioButton3 = new Guna.UI2.WinForms.Guna2RadioButton();
            this.radioButton2 = new Guna.UI2.WinForms.Guna2RadioButton();
            this.radioButton1 = new Guna.UI2.WinForms.Guna2RadioButton();
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.pnTitle.SuspendLayout();
            this.guna2GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 21;
            this.listBox1.Location = new System.Drawing.Point(13, 168);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(692, 319);
            this.listBox1.Sorted = true;
            this.listBox1.TabIndex = 5;
            this.listBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDoubleClick);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
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
            this.pnTitle.Size = new System.Drawing.Size(717, 50);
            this.pnTitle.TabIndex = 85;
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
            this.guna2ControlBox3.Location = new System.Drawing.Point(687, 6);
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
            this.lblFormTitle.Size = new System.Drawing.Size(717, 45);
            this.lblFormTitle.TabIndex = 79;
            this.lblFormTitle.Text = "품번 검색";
            this.lblFormTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTopDeco
            // 
            this.lblTopDeco.BackColor = System.Drawing.Color.DodgerBlue;
            this.lblTopDeco.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTopDeco.Location = new System.Drawing.Point(0, 0);
            this.lblTopDeco.Name = "lblTopDeco";
            this.lblTopDeco.Size = new System.Drawing.Size(717, 5);
            this.lblTopDeco.TabIndex = 1;
            this.lblTopDeco.Text = " ";
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2DragControl1.TargetControl = this.lblFormTitle;
            this.guna2DragControl1.UseTransparentDrag = true;
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.White;
            this.txtSearch.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.txtSearch.BorderRadius = 8;
            this.txtSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSearch.DefaultText = "";
            this.txtSearch.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSearch.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSearch.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSearch.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.txtSearch.Location = new System.Drawing.Point(12, 130);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PasswordChar = '\0';
            this.txtSearch.PlaceholderText = "";
            this.txtSearch.SelectedText = "";
            this.txtSearch.Size = new System.Drawing.Size(592, 29);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSearch.Enter += new System.EventHandler(this.txtFillColor_Enter);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            this.txtSearch.Leave += new System.EventHandler(this.txtFillColor_Leave);
            // 
            // btnProductSearch
            // 
            this.btnProductSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnProductSearch.BorderColor = System.Drawing.Color.Silver;
            this.btnProductSearch.BorderRadius = 8;
            this.btnProductSearch.BorderThickness = 1;
            this.btnProductSearch.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnProductSearch.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnProductSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnProductSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnProductSearch.FillColor = System.Drawing.SystemColors.ControlLight;
            this.btnProductSearch.FocusedColor = System.Drawing.SystemColors.ControlDark;
            this.btnProductSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnProductSearch.ForeColor = System.Drawing.Color.Black;
            this.btnProductSearch.Location = new System.Drawing.Point(611, 130);
            this.btnProductSearch.Name = "btnProductSearch";
            this.btnProductSearch.Size = new System.Drawing.Size(93, 29);
            this.btnProductSearch.TabIndex = 87;
            this.btnProductSearch.Text = "검색";
            this.btnProductSearch.Click += new System.EventHandler(this.btnProductSearch_Click);
            // 
            // guna2TaskBarProgress1
            // 
            this.guna2TaskBarProgress1.TargetForm = null;
            // 
            // guna2ProgressIndicator1
            // 
            this.guna2ProgressIndicator1.Location = new System.Drawing.Point(314, 285);
            this.guna2ProgressIndicator1.Name = "guna2ProgressIndicator1";
            this.guna2ProgressIndicator1.Size = new System.Drawing.Size(90, 90);
            this.guna2ProgressIndicator1.TabIndex = 90;
            this.guna2ProgressIndicator1.Visible = false;
            // 
            // guna2GroupBox1
            // 
            this.guna2GroupBox1.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.guna2GroupBox1.BorderRadius = 8;
            this.guna2GroupBox1.Controls.Add(this.radioButton7);
            this.guna2GroupBox1.Controls.Add(this.radioButton6);
            this.guna2GroupBox1.Controls.Add(this.radioButton5);
            this.guna2GroupBox1.Controls.Add(this.radioButton4);
            this.guna2GroupBox1.Controls.Add(this.radioButton3);
            this.guna2GroupBox1.Controls.Add(this.radioButton2);
            this.guna2GroupBox1.Controls.Add(this.radioButton1);
            this.guna2GroupBox1.CustomBorderThickness = new System.Windows.Forms.Padding(0);
            this.guna2GroupBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2GroupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.guna2GroupBox1.Location = new System.Drawing.Point(12, 56);
            this.guna2GroupBox1.Name = "guna2GroupBox1";
            this.guna2GroupBox1.Size = new System.Drawing.Size(692, 65);
            this.guna2GroupBox1.TabIndex = 91;
            this.guna2GroupBox1.Text = "검색 타입";
            // 
            // radioButton7
            // 
            this.radioButton7.AutoSize = true;
            this.radioButton7.BackColor = System.Drawing.Color.White;
            this.radioButton7.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.radioButton7.CheckedState.BorderThickness = 0;
            this.radioButton7.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.radioButton7.CheckedState.InnerColor = System.Drawing.Color.White;
            this.radioButton7.CheckedState.InnerOffset = -4;
            this.radioButton7.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioButton7.Location = new System.Drawing.Point(556, 33);
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.Size = new System.Drawing.Size(91, 25);
            this.radioButton7.TabIndex = 146;
            this.radioButton7.Text = "SUB PCB";
            this.radioButton7.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.radioButton7.UncheckedState.BorderThickness = 2;
            this.radioButton7.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.radioButton7.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.radioButton7.UseVisualStyleBackColor = false;
            this.radioButton7.CheckedChanged += new System.EventHandler(this.SetRadioButtonCheck_CheckedChanged);
            this.radioButton7.Click += new System.EventHandler(this.SetRadioButtonCheck_CheckedChanged);
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.BackColor = System.Drawing.Color.White;
            this.radioButton6.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.radioButton6.CheckedState.BorderThickness = 0;
            this.radioButton6.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.radioButton6.CheckedState.InnerColor = System.Drawing.Color.White;
            this.radioButton6.CheckedState.InnerOffset = -4;
            this.radioButton6.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioButton6.Location = new System.Drawing.Point(446, 33);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(104, 25);
            this.radioButton6.TabIndex = 145;
            this.radioButton6.Text = "MAIN PCB";
            this.radioButton6.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.radioButton6.UncheckedState.BorderThickness = 2;
            this.radioButton6.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.radioButton6.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.radioButton6.UseVisualStyleBackColor = false;
            this.radioButton6.CheckedChanged += new System.EventHandler(this.SetRadioButtonCheck_CheckedChanged);
            this.radioButton6.Click += new System.EventHandler(this.SetRadioButtonCheck_CheckedChanged);
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.BackColor = System.Drawing.Color.White;
            this.radioButton5.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.radioButton5.CheckedState.BorderThickness = 0;
            this.radioButton5.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.radioButton5.CheckedState.InnerColor = System.Drawing.Color.White;
            this.radioButton5.CheckedState.InnerOffset = -4;
            this.radioButton5.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioButton5.Location = new System.Drawing.Point(356, 33);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(84, 25);
            this.radioButton5.TabIndex = 144;
            this.radioButton5.Text = "EO 내용";
            this.radioButton5.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.radioButton5.UncheckedState.BorderThickness = 2;
            this.radioButton5.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.radioButton5.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.radioButton5.UseVisualStyleBackColor = false;
            this.radioButton5.CheckedChanged += new System.EventHandler(this.SetRadioButtonCheck_CheckedChanged);
            this.radioButton5.Click += new System.EventHandler(this.SetRadioButtonCheck_CheckedChanged);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.BackColor = System.Drawing.Color.White;
            this.radioButton4.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.radioButton4.CheckedState.BorderThickness = 0;
            this.radioButton4.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.radioButton4.CheckedState.InnerColor = System.Drawing.Color.White;
            this.radioButton4.CheckedState.InnerOffset = -4;
            this.radioButton4.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioButton4.Location = new System.Drawing.Point(250, 33);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(100, 25);
            this.radioButton4.TabIndex = 143;
            this.radioButton4.Text = "모비스 EO";
            this.radioButton4.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.radioButton4.UncheckedState.BorderThickness = 2;
            this.radioButton4.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.radioButton4.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.radioButton4.UseVisualStyleBackColor = false;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.SetRadioButtonCheck_CheckedChanged);
            this.radioButton4.Click += new System.EventHandler(this.SetRadioButtonCheck_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.BackColor = System.Drawing.Color.White;
            this.radioButton3.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.radioButton3.CheckedState.BorderThickness = 0;
            this.radioButton3.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.radioButton3.CheckedState.InnerColor = System.Drawing.Color.White;
            this.radioButton3.CheckedState.InnerOffset = -4;
            this.radioButton3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioButton3.Location = new System.Drawing.Point(144, 33);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(100, 25);
            this.radioButton3.TabIndex = 142;
            this.radioButton3.Text = "고객사 EO";
            this.radioButton3.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.radioButton3.UncheckedState.BorderThickness = 2;
            this.radioButton3.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.radioButton3.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.radioButton3.UseVisualStyleBackColor = false;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.SetRadioButtonCheck_CheckedChanged);
            this.radioButton3.Click += new System.EventHandler(this.SetRadioButtonCheck_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.BackColor = System.Drawing.Color.White;
            this.radioButton2.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.radioButton2.CheckedState.BorderThickness = 0;
            this.radioButton2.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.radioButton2.CheckedState.InnerColor = System.Drawing.Color.White;
            this.radioButton2.CheckedState.InnerOffset = -4;
            this.radioButton2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioButton2.Location = new System.Drawing.Point(78, 33);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(60, 25);
            this.radioButton2.TabIndex = 141;
            this.radioButton2.Text = "차종";
            this.radioButton2.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.radioButton2.UncheckedState.BorderThickness = 2;
            this.radioButton2.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.radioButton2.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.radioButton2.UseVisualStyleBackColor = false;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.SetRadioButtonCheck_CheckedChanged);
            this.radioButton2.Click += new System.EventHandler(this.SetRadioButtonCheck_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.BackColor = System.Drawing.Color.White;
            this.radioButton1.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.radioButton1.CheckedState.BorderThickness = 0;
            this.radioButton1.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.radioButton1.CheckedState.InnerColor = System.Drawing.Color.White;
            this.radioButton1.CheckedState.InnerOffset = -4;
            this.radioButton1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioButton1.Location = new System.Drawing.Point(12, 33);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(60, 25);
            this.radioButton1.TabIndex = 140;
            this.radioButton1.Text = "품번";
            this.radioButton1.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.radioButton1.UncheckedState.BorderThickness = 2;
            this.radioButton1.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.radioButton1.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.radioButton1.UseVisualStyleBackColor = false;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.SetRadioButtonCheck_CheckedChanged);
            this.radioButton1.Click += new System.EventHandler(this.SetRadioButtonCheck_CheckedChanged);
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // SearchForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(717, 500);
            this.Controls.Add(this.guna2GroupBox1);
            this.Controls.Add(this.guna2ProgressIndicator1);
            this.Controls.Add(this.btnProductSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.pnTitle);
            this.Controls.Add(this.listBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SearchForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SearchForm_FormClosing);
            this.Load += new System.EventHandler(this.SearchForm_Load);
            this.pnTitle.ResumeLayout(false);
            this.guna2GroupBox1.ResumeLayout(false);
            this.guna2GroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel pnTitle;
        private Guna.UI2.WinForms.Guna2TextBox txtAllSearchBox;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox3;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Label lblTopDeco;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        private Guna.UI2.WinForms.Guna2TextBox txtSearch;
        private Guna.UI2.WinForms.Guna2Button btnProductSearch;
        private Guna.UI2.WinForms.Guna2TaskBarProgress guna2TaskBarProgress1;
        private Guna.UI2.WinForms.Guna2ProgressIndicator guna2ProgressIndicator1;
        private Guna.UI2.WinForms.Guna2GroupBox guna2GroupBox1;
        private Guna.UI2.WinForms.Guna2RadioButton radioButton4;
        private Guna.UI2.WinForms.Guna2RadioButton radioButton3;
        private Guna.UI2.WinForms.Guna2RadioButton radioButton2;
        private Guna.UI2.WinForms.Guna2RadioButton radioButton1;
        private Guna.UI2.WinForms.Guna2RadioButton radioButton7;
        private Guna.UI2.WinForms.Guna2RadioButton radioButton6;
        private Guna.UI2.WinForms.Guna2RadioButton radioButton5;
        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
    }
}