namespace EOM_v3_M
{
    partial class MessageForm_Receive
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
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.chkBoxMessageRetry = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 23);
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(8, 67);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessage.Size = new System.Drawing.Size(419, 213);
            this.txtMessage.TabIndex = 0;
            this.txtMessage.Text = "테스트로 작성하는 글씨1\r\n2\r\n3\r\n4\r\n5\r\n6\r\n7\r\n8\r\n9\r\n10\r\n11\r\n12\r\n13\r\n14\r\n15\r\n16\r\n17\r\n18\r\n19\r\n20" +
    "\r\n";
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(7, 35);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 26);
            this.button1.TabIndex = 1;
            this.button1.Text = "보낸사람";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(342, 288);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(86, 26);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "확인";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // lblName
            // 
            this.lblName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblName.Location = new System.Drawing.Point(99, 36);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(328, 24);
            this.lblName.TabIndex = 5;
            this.lblName.Text = "label1";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkBoxMessageRetry
            // 
            this.chkBoxMessageRetry.AutoSize = true;
            this.chkBoxMessageRetry.Location = new System.Drawing.Point(168, 294);
            this.chkBoxMessageRetry.Name = "chkBoxMessageRetry";
            this.chkBoxMessageRetry.Size = new System.Drawing.Size(168, 16);
            this.chkBoxMessageRetry.TabIndex = 7;
            this.chkBoxMessageRetry.Text = "쪽지 다음에 다시 확인하기";
            this.chkBoxMessageRetry.UseVisualStyleBackColor = true;
            this.chkBoxMessageRetry.Visible = false;
            // 
            // MessageForm_Receive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 322);
            this.Controls.Add(this.chkBoxMessageRetry);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtMessage);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MessageForm_Receive";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.MessageForm_Send_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.CheckBox chkBoxMessageRetry;
    }
}