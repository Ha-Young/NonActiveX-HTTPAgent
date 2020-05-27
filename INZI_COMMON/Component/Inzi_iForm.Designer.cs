namespace INZI_COMMON.Component
{
    partial class Inzi_iForm
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inzi_iForm));
            this.Inzi_ImgViewer = new AxINZIIMGVIEWLib.AxInziImgView();
            this.gBox_OCX = new System.Windows.Forms.GroupBox();
            this.Inzi_iFile = new AxINZI_IFILELib.AxInzi_iFile();
            this.Inzi_Comp = new AxINZICOMPLib.AxInziComp();
            this.Inzi_ScanTwain = new AxINZISCANTWAINLib.AxInziScanTwain();
            this.Inzi_Preproc = new AxINZIPREPROCLib.AxInziPreproc();
            ((System.ComponentModel.ISupportInitialize)(this.Inzi_ImgViewer)).BeginInit();
            this.gBox_OCX.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Inzi_iFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Inzi_Comp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Inzi_ScanTwain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Inzi_Preproc)).BeginInit();
            this.SuspendLayout();
            // 
            // Inzi_ImgViewer
            // 
            this.Inzi_ImgViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Inzi_ImgViewer.Enabled = true;
            this.Inzi_ImgViewer.Location = new System.Drawing.Point(0, 0);
            this.Inzi_ImgViewer.Name = "Inzi_ImgViewer";
            this.Inzi_ImgViewer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("Inzi_ImgViewer.OcxState")));
            this.Inzi_ImgViewer.Size = new System.Drawing.Size(271, 267);
            this.Inzi_ImgViewer.TabIndex = 0;
            this.Inzi_ImgViewer.MouseSelectEnd += new AxINZIIMGVIEWLib._DInziImgViewEvents_MouseSelectEndEventHandler(this.Inzi_ImgViewer_MouseSelectEnd);
            // 
            // gBox_OCX
            // 
            this.gBox_OCX.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.gBox_OCX.Controls.Add(this.Inzi_iFile);
            this.gBox_OCX.Controls.Add(this.Inzi_Comp);
            this.gBox_OCX.Controls.Add(this.Inzi_ScanTwain);
            this.gBox_OCX.Controls.Add(this.Inzi_Preproc);
            this.gBox_OCX.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.gBox_OCX.Location = new System.Drawing.Point(66, 73);
            this.gBox_OCX.Name = "gBox_OCX";
            this.gBox_OCX.Size = new System.Drawing.Size(138, 120);
            this.gBox_OCX.TabIndex = 4;
            this.gBox_OCX.TabStop = false;
            this.gBox_OCX.Text = "OCX";
            this.gBox_OCX.Visible = false;
            // 
            // Inzi_iFile
            // 
            this.Inzi_iFile.Enabled = true;
            this.Inzi_iFile.Location = new System.Drawing.Point(14, 23);
            this.Inzi_iFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Inzi_iFile.Name = "Inzi_iFile";
            this.Inzi_iFile.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("Inzi_iFile.OcxState")));
            this.Inzi_iFile.Size = new System.Drawing.Size(32, 32);
            this.Inzi_iFile.TabIndex = 13;
            // 
            // Inzi_Comp
            // 
            this.Inzi_Comp.Enabled = true;
            this.Inzi_Comp.Location = new System.Drawing.Point(52, 23);
            this.Inzi_Comp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Inzi_Comp.Name = "Inzi_Comp";
            this.Inzi_Comp.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("Inzi_Comp.OcxState")));
            this.Inzi_Comp.Size = new System.Drawing.Size(32, 32);
            this.Inzi_Comp.TabIndex = 16;
            this.Inzi_Comp.Visible = false;
            // 
            // Inzi_ScanTwain
            // 
            this.Inzi_ScanTwain.Enabled = true;
            this.Inzi_ScanTwain.Location = new System.Drawing.Point(68, 72);
            this.Inzi_ScanTwain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Inzi_ScanTwain.Name = "Inzi_ScanTwain";
            this.Inzi_ScanTwain.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("Inzi_ScanTwain.OcxState")));
            this.Inzi_ScanTwain.Size = new System.Drawing.Size(32, 32);
            this.Inzi_ScanTwain.TabIndex = 18;
            this.Inzi_ScanTwain.Visible = false;
            this.Inzi_ScanTwain.OneScanned += new AxINZISCANTWAINLib._DInziScanTwainEvents_OneScannedEventHandler(this.Inzi_ScanTwain_OneScanned);
            this.Inzi_ScanTwain.ScanEnded += new System.EventHandler(this.Inzi_ScanTwain_ScanEnded);
            // 
            // Inzi_Preproc
            // 
            this.Inzi_Preproc.Enabled = true;
            this.Inzi_Preproc.Location = new System.Drawing.Point(30, 72);
            this.Inzi_Preproc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Inzi_Preproc.Name = "Inzi_Preproc";
            this.Inzi_Preproc.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("Inzi_Preproc.OcxState")));
            this.Inzi_Preproc.Size = new System.Drawing.Size(32, 32);
            this.Inzi_Preproc.TabIndex = 17;
            this.Inzi_Preproc.Visible = false;
            // 
            // Inzi_iForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gBox_OCX);
            this.Controls.Add(this.Inzi_ImgViewer);
            this.Name = "Inzi_iForm";
            this.Size = new System.Drawing.Size(271, 267);
            ((System.ComponentModel.ISupportInitialize)(this.Inzi_ImgViewer)).EndInit();
            this.gBox_OCX.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Inzi_iFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Inzi_Comp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Inzi_ScanTwain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Inzi_Preproc)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gBox_OCX;
        private AxINZIIMGVIEWLib.AxInziImgView Inzi_ImgViewer;
        private AxINZI_IFILELib.AxInzi_iFile Inzi_iFile;
        private AxINZICOMPLib.AxInziComp Inzi_Comp;
        private AxINZISCANTWAINLib.AxInziScanTwain Inzi_ScanTwain;
        private AxINZIPREPROCLib.AxInziPreproc Inzi_Preproc;
    }
}
