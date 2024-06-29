namespace EOM_v3_M
{
    partial class MessageForm_List
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("노드3");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("노드4");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("노드5");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("노드6");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("노드7");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("멀티파트", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.treeViewList = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 23);
            // 
            // treeViewList
            // 
            this.treeViewList.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.treeViewList.Location = new System.Drawing.Point(10, 35);
            this.treeViewList.Name = "treeViewList";
            treeNode1.Name = "노드3";
            treeNode1.Text = "노드3";
            treeNode2.Name = "노드4";
            treeNode2.Text = "노드4";
            treeNode3.Name = "노드5";
            treeNode3.Text = "노드5";
            treeNode4.Name = "노드6";
            treeNode4.Text = "노드6";
            treeNode5.Name = "노드7";
            treeNode5.Text = "노드7";
            treeNode6.Name = "노드0";
            treeNode6.Text = "멀티파트";
            this.treeViewList.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode6});
            this.treeViewList.Size = new System.Drawing.Size(301, 265);
            this.treeViewList.TabIndex = 5;
            this.treeViewList.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewList_NodeMouseDoubleClick);
            // 
            // MessageForm_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 311);
            this.Controls.Add(this.treeViewList);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MessageForm_List";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.MessageForm_List_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.TreeView treeViewList;
    }
}