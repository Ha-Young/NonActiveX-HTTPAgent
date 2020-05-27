namespace TestScan
{
    partial class TestScanForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnl_left = new System.Windows.Forms.Panel();
            this.label_filePath = new System.Windows.Forms.Label();
            this.linkLabel_git = new System.Windows.Forms.LinkLabel();
            this.label_filePath_title = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label_jumin = new System.Windows.Forms.Label();
            this.label_user = new System.Windows.Forms.Label();
            this.label_inputDate = new System.Windows.Forms.Label();
            this.label_systemDate = new System.Windows.Forms.Label();
            this.btn_Import = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnl_left.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pnl_left, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(443, 262);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // pnl_left
            // 
            this.pnl_left.Controls.Add(this.label_filePath);
            this.pnl_left.Controls.Add(this.linkLabel_git);
            this.pnl_left.Controls.Add(this.label_filePath_title);
            this.pnl_left.Controls.Add(this.groupBox1);
            this.pnl_left.Controls.Add(this.btn_Import);
            this.pnl_left.Controls.Add(this.btn_Save);
            this.pnl_left.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_left.Location = new System.Drawing.Point(3, 3);
            this.pnl_left.Name = "pnl_left";
            this.pnl_left.Size = new System.Drawing.Size(437, 256);
            this.pnl_left.TabIndex = 1;
            // 
            // label_filePath
            // 
            this.label_filePath.Location = new System.Drawing.Point(190, 75);
            this.label_filePath.Name = "label_filePath";
            this.label_filePath.Size = new System.Drawing.Size(238, 87);
            this.label_filePath.TabIndex = 8;
            // 
            // linkLabel_git
            // 
            this.linkLabel_git.AutoSize = true;
            this.linkLabel_git.Location = new System.Drawing.Point(26, 221);
            this.linkLabel_git.Name = "linkLabel_git";
            this.linkLabel_git.Size = new System.Drawing.Size(315, 12);
            this.linkLabel_git.TabIndex = 7;
            this.linkLabel_git.TabStop = true;
            this.linkLabel_git.Text = "https://github.com/Ha-Young/NonActiveX-HTTPAgent";
            // 
            // label_filePath_title
            // 
            this.label_filePath_title.AutoSize = true;
            this.label_filePath_title.Location = new System.Drawing.Point(192, 56);
            this.label_filePath_title.Name = "label_filePath_title";
            this.label_filePath_title.Size = new System.Drawing.Size(58, 12);
            this.label_filePath_title.TabIndex = 6;
            this.label_filePath_title.Text = "filePath : ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label_jumin);
            this.groupBox1.Controls.Add(this.label_user);
            this.groupBox1.Controls.Add(this.label_inputDate);
            this.groupBox1.Controls.Add(this.label_systemDate);
            this.groupBox1.Location = new System.Drawing.Point(9, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(166, 188);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "전달받은 정보";
            // 
            // label_jumin
            // 
            this.label_jumin.AutoSize = true;
            this.label_jumin.Location = new System.Drawing.Point(17, 145);
            this.label_jumin.Name = "label_jumin";
            this.label_jumin.Size = new System.Drawing.Size(51, 12);
            this.label_jumin.TabIndex = 0;
            this.label_jumin.Text = "Jumin : ";
            // 
            // label_user
            // 
            this.label_user.AutoSize = true;
            this.label_user.Location = new System.Drawing.Point(17, 109);
            this.label_user.Name = "label_user";
            this.label_user.Size = new System.Drawing.Size(81, 12);
            this.label_user.TabIndex = 0;
            this.label_user.Text = "User Name : ";
            // 
            // label_inputDate
            // 
            this.label_inputDate.AutoSize = true;
            this.label_inputDate.Location = new System.Drawing.Point(17, 74);
            this.label_inputDate.Name = "label_inputDate";
            this.label_inputDate.Size = new System.Drawing.Size(69, 12);
            this.label_inputDate.TabIndex = 0;
            this.label_inputDate.Text = "InputDate : ";
            // 
            // label_systemDate
            // 
            this.label_systemDate.AutoSize = true;
            this.label_systemDate.Location = new System.Drawing.Point(17, 39);
            this.label_systemDate.Name = "label_systemDate";
            this.label_systemDate.Size = new System.Drawing.Size(89, 12);
            this.label_systemDate.TabIndex = 0;
            this.label_systemDate.Text = "System Date : ";
            // 
            // btn_Import
            // 
            this.btn_Import.Font = new System.Drawing.Font("굴림", 8F);
            this.btn_Import.Location = new System.Drawing.Point(190, 15);
            this.btn_Import.Name = "btn_Import";
            this.btn_Import.Size = new System.Drawing.Size(80, 31);
            this.btn_Import.TabIndex = 4;
            this.btn_Import.Text = "가져오기";
            this.btn_Import.UseVisualStyleBackColor = true;
            this.btn_Import.Click += new System.EventHandler(this.btn_Import_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Font = new System.Drawing.Font("굴림", 8F);
            this.btn_Save.Location = new System.Drawing.Point(190, 165);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(80, 31);
            this.btn_Save.TabIndex = 0;
            this.btn_Save.Text = "저장";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // TestScanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 262);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "TestScanForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ThisIsTestProgram";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TestScanForm_FormClosed);
            this.Load += new System.EventHandler(this.TestScanForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnl_left.ResumeLayout(false);
            this.pnl_left.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnl_left;
        private System.Windows.Forms.Button btn_Import;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label_jumin;
        private System.Windows.Forms.Label label_user;
        private System.Windows.Forms.Label label_inputDate;
        private System.Windows.Forms.Label label_systemDate;
        private System.Windows.Forms.Label label_filePath;
        private System.Windows.Forms.LinkLabel linkLabel_git;
        private System.Windows.Forms.Label label_filePath_title;
    }
}

