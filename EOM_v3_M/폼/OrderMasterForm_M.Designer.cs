namespace EOM_v3_M
{
    partial class OrderMasterForm_M
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvOrderMaster = new System.Windows.Forms.DataGridView();
            this.Column0 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rdoYear3 = new System.Windows.Forms.RadioButton();
            this.rdoYear2 = new System.Windows.Forms.RadioButton();
            this.rdoYear1 = new System.Windows.Forms.RadioButton();
            this.rdoYearAll = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoShipmentKD = new System.Windows.Forms.RadioButton();
            this.rdoShipmentCKD = new System.Windows.Forms.RadioButton();
            this.rdoShipmentOEM = new System.Windows.Forms.RadioButton();
            this.rdoShipmentAll = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdoProcessEnd = new System.Windows.Forms.RadioButton();
            this.rdoProcessIng = new System.Windows.Forms.RadioButton();
            this.rdoContentsAll = new System.Windows.Forms.RadioButton();
            this.pnTitle = new System.Windows.Forms.Panel();
            this.guna2ControlBox3 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.lblTopDeco = new System.Windows.Forms.Label();
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.lblChangeHistory = new System.Windows.Forms.Label();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnShipmentMaster = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderMaster)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.pnTitle.SuspendLayout();
            this.guna2Panel1.SuspendLayout();
            this.guna2Panel2.SuspendLayout();
            this.guna2Panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvOrderMaster
            // 
            this.dgvOrderMaster.AllowUserToAddRows = false;
            this.dgvOrderMaster.AllowUserToDeleteRows = false;
            this.dgvOrderMaster.AllowUserToResizeColumns = false;
            this.dgvOrderMaster.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvOrderMaster.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvOrderMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrderMaster.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column0,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column7,
            this.Column4,
            this.Column5,
            this.Column6});
            this.dgvOrderMaster.Location = new System.Drawing.Point(9, 167);
            this.dgvOrderMaster.MultiSelect = false;
            this.dgvOrderMaster.Name = "dgvOrderMaster";
            this.dgvOrderMaster.RowHeadersVisible = false;
            this.dgvOrderMaster.RowTemplate.Height = 23;
            this.dgvOrderMaster.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrderMaster.Size = new System.Drawing.Size(1182, 445);
            this.dgvOrderMaster.TabIndex = 65;
            this.dgvOrderMaster.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvOrderMaster_CellMouseClick);
            // 
            // Column0
            // 
            this.Column0.HeaderText = "";
            this.Column0.Name = "Column0";
            this.Column0.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column0.Width = 30;
            // 
            // Column1
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column1.HeaderText = "품번";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 130;
            // 
            // Column2
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column2.HeaderText = "기준 출하지";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column3
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Blue;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column3.HeaderText = "변경 출하지";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column7
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column7.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column7.HeaderText = "변경 내역";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column7.Width = 320;
            // 
            // Column4
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle6;
            this.Column4.HeaderText = "변경 시작일";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 190;
            // 
            // Column5
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column5.DefaultCellStyle = dataGridViewCellStyle7;
            this.Column5.HeaderText = "변경 종료일";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column5.Width = 190;
            // 
            // Column6
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column6.DefaultCellStyle = dataGridViewCellStyle8;
            this.Column6.HeaderText = "변경 사용자";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column6.Width = 109;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rdoYear3);
            this.groupBox5.Controls.Add(this.rdoYear2);
            this.groupBox5.Controls.Add(this.rdoYear1);
            this.groupBox5.Controls.Add(this.rdoYearAll);
            this.groupBox5.Location = new System.Drawing.Point(113, 123);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(300, 37);
            this.groupBox5.TabIndex = 70;
            this.groupBox5.TabStop = false;
            // 
            // rdoYear3
            // 
            this.rdoYear3.AutoSize = true;
            this.rdoYear3.Location = new System.Drawing.Point(200, 14);
            this.rdoYear3.Name = "rdoYear3";
            this.rdoYear3.Size = new System.Drawing.Size(59, 16);
            this.rdoYear3.TabIndex = 4;
            this.rdoYear3.Text = "2019년";
            this.rdoYear3.UseVisualStyleBackColor = true;
            this.rdoYear3.CheckedChanged += new System.EventHandler(this.UpdateDataGridView_CheckedChanged);
            // 
            // rdoYear2
            // 
            this.rdoYear2.AutoSize = true;
            this.rdoYear2.Location = new System.Drawing.Point(135, 14);
            this.rdoYear2.Name = "rdoYear2";
            this.rdoYear2.Size = new System.Drawing.Size(59, 16);
            this.rdoYear2.TabIndex = 3;
            this.rdoYear2.Text = "2019년";
            this.rdoYear2.UseVisualStyleBackColor = true;
            this.rdoYear2.CheckedChanged += new System.EventHandler(this.UpdateDataGridView_CheckedChanged);
            // 
            // rdoYear1
            // 
            this.rdoYear1.AutoSize = true;
            this.rdoYear1.Location = new System.Drawing.Point(70, 14);
            this.rdoYear1.Name = "rdoYear1";
            this.rdoYear1.Size = new System.Drawing.Size(59, 16);
            this.rdoYear1.TabIndex = 2;
            this.rdoYear1.Text = "2021년";
            this.rdoYear1.UseVisualStyleBackColor = true;
            this.rdoYear1.CheckedChanged += new System.EventHandler(this.UpdateDataGridView_CheckedChanged);
            // 
            // rdoYearAll
            // 
            this.rdoYearAll.AutoSize = true;
            this.rdoYearAll.Checked = true;
            this.rdoYearAll.Location = new System.Drawing.Point(12, 14);
            this.rdoYearAll.Name = "rdoYearAll";
            this.rdoYearAll.Size = new System.Drawing.Size(47, 16);
            this.rdoYearAll.TabIndex = 1;
            this.rdoYearAll.TabStop = true;
            this.rdoYearAll.Text = "전체";
            this.rdoYearAll.UseVisualStyleBackColor = true;
            this.rdoYearAll.CheckedChanged += new System.EventHandler(this.UpdateDataGridView_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoShipmentKD);
            this.groupBox1.Controls.Add(this.rdoShipmentCKD);
            this.groupBox1.Controls.Add(this.rdoShipmentOEM);
            this.groupBox1.Controls.Add(this.rdoShipmentAll);
            this.groupBox1.Location = new System.Drawing.Point(113, 86);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 37);
            this.groupBox1.TabIndex = 71;
            this.groupBox1.TabStop = false;
            // 
            // rdoShipmentKD
            // 
            this.rdoShipmentKD.AutoSize = true;
            this.rdoShipmentKD.Location = new System.Drawing.Point(200, 14);
            this.rdoShipmentKD.Name = "rdoShipmentKD";
            this.rdoShipmentKD.Size = new System.Drawing.Size(39, 16);
            this.rdoShipmentKD.TabIndex = 4;
            this.rdoShipmentKD.Text = "KD";
            this.rdoShipmentKD.UseVisualStyleBackColor = true;
            this.rdoShipmentKD.CheckedChanged += new System.EventHandler(this.UpdateDataGridView_CheckedChanged);
            // 
            // rdoShipmentCKD
            // 
            this.rdoShipmentCKD.AutoSize = true;
            this.rdoShipmentCKD.Location = new System.Drawing.Point(136, 14);
            this.rdoShipmentCKD.Name = "rdoShipmentCKD";
            this.rdoShipmentCKD.Size = new System.Drawing.Size(48, 16);
            this.rdoShipmentCKD.TabIndex = 3;
            this.rdoShipmentCKD.Text = "CKD";
            this.rdoShipmentCKD.UseVisualStyleBackColor = true;
            this.rdoShipmentCKD.CheckedChanged += new System.EventHandler(this.UpdateDataGridView_CheckedChanged);
            // 
            // rdoShipmentOEM
            // 
            this.rdoShipmentOEM.AutoSize = true;
            this.rdoShipmentOEM.Location = new System.Drawing.Point(70, 14);
            this.rdoShipmentOEM.Name = "rdoShipmentOEM";
            this.rdoShipmentOEM.Size = new System.Drawing.Size(51, 16);
            this.rdoShipmentOEM.TabIndex = 2;
            this.rdoShipmentOEM.Text = "OEM";
            this.rdoShipmentOEM.UseVisualStyleBackColor = true;
            this.rdoShipmentOEM.CheckedChanged += new System.EventHandler(this.UpdateDataGridView_CheckedChanged);
            // 
            // rdoShipmentAll
            // 
            this.rdoShipmentAll.AutoSize = true;
            this.rdoShipmentAll.Checked = true;
            this.rdoShipmentAll.Location = new System.Drawing.Point(12, 14);
            this.rdoShipmentAll.Name = "rdoShipmentAll";
            this.rdoShipmentAll.Size = new System.Drawing.Size(47, 16);
            this.rdoShipmentAll.TabIndex = 1;
            this.rdoShipmentAll.TabStop = true;
            this.rdoShipmentAll.Text = "전체";
            this.rdoShipmentAll.UseVisualStyleBackColor = true;
            this.rdoShipmentAll.CheckedChanged += new System.EventHandler(this.UpdateDataGridView_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdoProcessEnd);
            this.groupBox2.Controls.Add(this.rdoProcessIng);
            this.groupBox2.Controls.Add(this.rdoContentsAll);
            this.groupBox2.Location = new System.Drawing.Point(113, 49);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(300, 37);
            this.groupBox2.TabIndex = 71;
            this.groupBox2.TabStop = false;
            // 
            // rdoProcessEnd
            // 
            this.rdoProcessEnd.AutoSize = true;
            this.rdoProcessEnd.Location = new System.Drawing.Point(137, 14);
            this.rdoProcessEnd.Name = "rdoProcessEnd";
            this.rdoProcessEnd.Size = new System.Drawing.Size(75, 16);
            this.rdoProcessEnd.TabIndex = 3;
            this.rdoProcessEnd.Text = "진행 종료";
            this.rdoProcessEnd.UseVisualStyleBackColor = true;
            this.rdoProcessEnd.CheckedChanged += new System.EventHandler(this.UpdateDataGridView_CheckedChanged);
            // 
            // rdoProcessIng
            // 
            this.rdoProcessIng.AutoSize = true;
            this.rdoProcessIng.Location = new System.Drawing.Point(70, 14);
            this.rdoProcessIng.Name = "rdoProcessIng";
            this.rdoProcessIng.Size = new System.Drawing.Size(63, 16);
            this.rdoProcessIng.TabIndex = 2;
            this.rdoProcessIng.Text = "진행 중";
            this.rdoProcessIng.UseVisualStyleBackColor = true;
            this.rdoProcessIng.CheckedChanged += new System.EventHandler(this.UpdateDataGridView_CheckedChanged);
            // 
            // rdoContentsAll
            // 
            this.rdoContentsAll.AutoSize = true;
            this.rdoContentsAll.Checked = true;
            this.rdoContentsAll.Location = new System.Drawing.Point(12, 14);
            this.rdoContentsAll.Name = "rdoContentsAll";
            this.rdoContentsAll.Size = new System.Drawing.Size(47, 16);
            this.rdoContentsAll.TabIndex = 1;
            this.rdoContentsAll.TabStop = true;
            this.rdoContentsAll.Text = "전체";
            this.rdoContentsAll.UseVisualStyleBackColor = true;
            this.rdoContentsAll.CheckedChanged += new System.EventHandler(this.UpdateDataGridView_CheckedChanged);
            // 
            // pnTitle
            // 
            this.pnTitle.Controls.Add(this.guna2ControlBox3);
            this.pnTitle.Controls.Add(this.lblFormTitle);
            this.pnTitle.Controls.Add(this.lblTopDeco);
            this.pnTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.pnTitle.Location = new System.Drawing.Point(0, 0);
            this.pnTitle.Name = "pnTitle";
            this.pnTitle.Size = new System.Drawing.Size(1200, 50);
            this.pnTitle.TabIndex = 84;
            // 
            // guna2ControlBox3
            // 
            this.guna2ControlBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox3.ControlBoxStyle = Guna.UI2.WinForms.Enums.ControlBoxStyle.Custom;
            this.guna2ControlBox3.FillColor = System.Drawing.Color.White;
            this.guna2ControlBox3.IconColor = System.Drawing.SystemColors.ControlDarkDark;
            this.guna2ControlBox3.Location = new System.Drawing.Point(1170, 6);
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
            this.lblFormTitle.Size = new System.Drawing.Size(1200, 45);
            this.lblFormTitle.TabIndex = 79;
            this.lblFormTitle.Text = "출하지 변경";
            this.lblFormTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTopDeco
            // 
            this.lblTopDeco.BackColor = System.Drawing.Color.DodgerBlue;
            this.lblTopDeco.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTopDeco.Location = new System.Drawing.Point(0, 0);
            this.lblTopDeco.Name = "lblTopDeco";
            this.lblTopDeco.Size = new System.Drawing.Size(1200, 5);
            this.lblTopDeco.TabIndex = 1;
            this.lblTopDeco.Text = " ";
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2DragControl1.TargetControl = this.lblFormTitle;
            this.guna2DragControl1.UseTransparentDrag = true;
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.guna2Panel1.BorderRadius = 8;
            this.guna2Panel1.BorderThickness = 1;
            this.guna2Panel1.Controls.Add(this.lblChangeHistory);
            this.guna2Panel1.CustomizableEdges.BottomLeft = false;
            this.guna2Panel1.CustomizableEdges.TopLeft = false;
            this.guna2Panel1.FillColor = System.Drawing.SystemColors.ControlDarkDark;
            this.guna2Panel1.Location = new System.Drawing.Point(9, 56);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(97, 29);
            this.guna2Panel1.TabIndex = 135;
            // 
            // lblChangeHistory
            // 
            this.lblChangeHistory.BackColor = System.Drawing.Color.Transparent;
            this.lblChangeHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblChangeHistory.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChangeHistory.ForeColor = System.Drawing.Color.White;
            this.lblChangeHistory.Location = new System.Drawing.Point(0, 0);
            this.lblChangeHistory.Name = "lblChangeHistory";
            this.lblChangeHistory.Size = new System.Drawing.Size(97, 29);
            this.lblChangeHistory.TabIndex = 0;
            this.lblChangeHistory.Text = "변경사항";
            this.lblChangeHistory.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.guna2Panel2.BorderRadius = 8;
            this.guna2Panel2.BorderThickness = 1;
            this.guna2Panel2.Controls.Add(this.label1);
            this.guna2Panel2.CustomizableEdges.BottomLeft = false;
            this.guna2Panel2.CustomizableEdges.TopLeft = false;
            this.guna2Panel2.FillColor = System.Drawing.SystemColors.ControlDarkDark;
            this.guna2Panel2.Location = new System.Drawing.Point(9, 93);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(97, 29);
            this.guna2Panel2.TabIndex = 136;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "출하지";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // guna2Panel3
            // 
            this.guna2Panel3.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.guna2Panel3.BorderRadius = 8;
            this.guna2Panel3.BorderThickness = 1;
            this.guna2Panel3.Controls.Add(this.label2);
            this.guna2Panel3.CustomizableEdges.BottomLeft = false;
            this.guna2Panel3.CustomizableEdges.TopLeft = false;
            this.guna2Panel3.FillColor = System.Drawing.SystemColors.ControlDarkDark;
            this.guna2Panel3.Location = new System.Drawing.Point(9, 130);
            this.guna2Panel3.Name = "guna2Panel3";
            this.guna2Panel3.Size = new System.Drawing.Size(97, 29);
            this.guna2Panel3.TabIndex = 137;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 29);
            this.label2.TabIndex = 0;
            this.label2.Text = "변경 기간";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnShipmentMaster
            // 
            this.btnShipmentMaster.BackColor = System.Drawing.Color.Transparent;
            this.btnShipmentMaster.BorderColor = System.Drawing.Color.Silver;
            this.btnShipmentMaster.BorderRadius = 8;
            this.btnShipmentMaster.BorderThickness = 1;
            this.btnShipmentMaster.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnShipmentMaster.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnShipmentMaster.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnShipmentMaster.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnShipmentMaster.FillColor = System.Drawing.SystemColors.ControlLight;
            this.btnShipmentMaster.FocusedColor = System.Drawing.SystemColors.ControlDark;
            this.btnShipmentMaster.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnShipmentMaster.ForeColor = System.Drawing.Color.Black;
            this.btnShipmentMaster.Location = new System.Drawing.Point(1066, 130);
            this.btnShipmentMaster.Name = "btnShipmentMaster";
            this.btnShipmentMaster.Size = new System.Drawing.Size(125, 29);
            this.btnShipmentMaster.TabIndex = 138;
            this.btnShipmentMaster.Text = "검색";
            this.btnShipmentMaster.Click += new System.EventHandler(this.btnShipmentSearch_Click);
            // 
            // OrderMasterForm_M
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 620);
            this.Controls.Add(this.btnShipmentMaster);
            this.Controls.Add(this.guna2Panel3);
            this.Controls.Add(this.guna2Panel2);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.pnTitle);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.dgvOrderMaster);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OrderMasterForm_M";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OrderMasterForm";
            this.Load += new System.EventHandler(this.OrderMasterForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderMaster)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.pnTitle.ResumeLayout(false);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel2.ResumeLayout(false);
            this.guna2Panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvOrderMaster;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton rdoYear3;
        private System.Windows.Forms.RadioButton rdoYear2;
        private System.Windows.Forms.RadioButton rdoYear1;
        private System.Windows.Forms.RadioButton rdoYearAll;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoShipmentKD;
        private System.Windows.Forms.RadioButton rdoShipmentCKD;
        private System.Windows.Forms.RadioButton rdoShipmentOEM;
        private System.Windows.Forms.RadioButton rdoShipmentAll;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdoProcessIng;
        private System.Windows.Forms.RadioButton rdoContentsAll;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column0;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.RadioButton rdoProcessEnd;
        private System.Windows.Forms.Panel pnTitle;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox3;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Label lblTopDeco;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label lblChangeHistory;
        private Guna.UI2.WinForms.Guna2Button btnShipmentMaster;
    }
}