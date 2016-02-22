namespace CodeGeneration
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnOpenPath = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbFolderDetail = new System.Windows.Forms.Label();
            this.lstInterfaceList = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtInterfaceCode = new System.Windows.Forms.TextBox();
            this.btnGenerateCode = new System.Windows.Forms.Button();
            this.cmbFileList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(14, 24);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(355, 21);
            this.txtFilePath.TabIndex = 0;
            // 
            // btnOpenPath
            // 
            this.btnOpenPath.Location = new System.Drawing.Point(393, 24);
            this.btnOpenPath.Name = "btnOpenPath";
            this.btnOpenPath.Size = new System.Drawing.Size(113, 23);
            this.btnOpenPath.TabIndex = 1;
            this.btnOpenPath.Text = "打开接口文件路径";
            this.btnOpenPath.UseVisualStyleBackColor = true;
            this.btnOpenPath.Click += new System.EventHandler(this.btnOpenPath_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "文件夹一栏表:";
            // 
            // lbFolderDetail
            // 
            this.lbFolderDetail.AutoSize = true;
            this.lbFolderDetail.Location = new System.Drawing.Point(47, 82);
            this.lbFolderDetail.Name = "lbFolderDetail";
            this.lbFolderDetail.Size = new System.Drawing.Size(0, 12);
            this.lbFolderDetail.TabIndex = 3;
            // 
            // lstInterfaceList
            // 
            this.lstInterfaceList.FormattingEnabled = true;
            this.lstInterfaceList.ItemHeight = 12;
            this.lstInterfaceList.Location = new System.Drawing.Point(12, 157);
            this.lstInterfaceList.Name = "lstInterfaceList";
            this.lstInterfaceList.Size = new System.Drawing.Size(270, 244);
            this.lstInterfaceList.TabIndex = 5;
            this.lstInterfaceList.SelectedIndexChanged += new System.EventHandler(this.lstInterfaceList_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(299, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "接口代码:";
            // 
            // txtInterfaceCode
            // 
            this.txtInterfaceCode.Location = new System.Drawing.Point(301, 73);
            this.txtInterfaceCode.Multiline = true;
            this.txtInterfaceCode.Name = "txtInterfaceCode";
            this.txtInterfaceCode.Size = new System.Drawing.Size(400, 404);
            this.txtInterfaceCode.TabIndex = 7;
            // 
            // btnGenerateCode
            // 
            this.btnGenerateCode.Location = new System.Drawing.Point(26, 438);
            this.btnGenerateCode.Name = "btnGenerateCode";
            this.btnGenerateCode.Size = new System.Drawing.Size(81, 23);
            this.btnGenerateCode.TabIndex = 8;
            this.btnGenerateCode.Text = "生成代码";
            this.btnGenerateCode.UseVisualStyleBackColor = true;
            this.btnGenerateCode.Click += new System.EventHandler(this.btnGenerateCode_Click);
            // 
            // cmbFileList
            // 
            this.cmbFileList.FormattingEnabled = true;
            this.cmbFileList.Items.AddRange(new object[] {
            "hotel",
            "flight",
            "user",
            "backstage",
            "ndPublic",
            "order",
            "travel",
            "activity",
            "pay"});
            this.cmbFileList.Location = new System.Drawing.Point(114, 131);
            this.cmbFileList.Name = "cmbFileList";
            this.cmbFileList.Size = new System.Drawing.Size(121, 20);
            this.cmbFileList.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "接口所属业务范畴:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(287, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "接口需是ND.Lib.Core中设计的以excutor:结尾的接口";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 489);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbFileList);
            this.Controls.Add(this.btnGenerateCode);
            this.Controls.Add(this.txtInterfaceCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstInterfaceList);
            this.Controls.Add(this.lbFolderDetail);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOpenPath);
            this.Controls.Add(this.txtFilePath);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnOpenPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbFolderDetail;
        private System.Windows.Forms.ListBox lstInterfaceList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtInterfaceCode;
        private System.Windows.Forms.Button btnGenerateCode;
        private System.Windows.Forms.ComboBox cmbFileList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;

    }
}

