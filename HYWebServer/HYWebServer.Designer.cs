using System;
using HYWebServer_Interface.DATA;

namespace Mois_LocalWebServer_TEST
{
    partial class HYWebServer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HYWebServer));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.plnControl = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnScan = new System.Windows.Forms.Button();
            this.btnWeb = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            webToServerMessage = new System.Windows.Forms.RichTextBox();
            serverToProcessMessage = new System.Windows.Forms.RichTextBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.ctxms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverStartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverStopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tlpMain.SuspendLayout();
            this.plnControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.ctxms.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.plnControl, 0, 1);
            this.tlpMain.Controls.Add(this.splitContainer1, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpMain.Size = new System.Drawing.Size(754, 467);
            this.tlpMain.TabIndex = 1;
            // 
            // plnControl
            // 
            this.plnControl.BackColor = System.Drawing.Color.Gray;
            this.plnControl.Controls.Add(this.btnExit);
            this.plnControl.Controls.Add(this.btnStop);
            this.plnControl.Controls.Add(this.btnStart);
            this.plnControl.Controls.Add(this.btnScan);
            this.plnControl.Controls.Add(this.btnWeb);
            this.plnControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plnControl.Location = new System.Drawing.Point(0, 427);
            this.plnControl.Margin = new System.Windows.Forms.Padding(0);
            this.plnControl.Name = "plnControl";
            this.plnControl.Size = new System.Drawing.Size(754, 40);
            this.plnControl.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.OrangeRed;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.FlatAppearance.BorderSize = 2;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnExit.Location = new System.Drawing.Point(654, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(88, 36);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.BackColor = System.Drawing.Color.DarkOrange;
            this.btnStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnStop.FlatAppearance.BorderSize = 2;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnStop.Location = new System.Drawing.Point(557, 2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(88, 36);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.BackColor = System.Drawing.Color.DarkOrange;
            this.btnStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnStart.FlatAppearance.BorderSize = 2;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnStart.Location = new System.Drawing.Point(460, 2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(88, 36);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnScan
            // 
            this.btnScan.BackColor = System.Drawing.Color.Orange;
            this.btnScan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnScan.FlatAppearance.BorderSize = 2;
            this.btnScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScan.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnScan.Location = new System.Drawing.Point(104, 2);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(88, 36);
            this.btnScan.TabIndex = 2;
            this.btnScan.Text = "SCAN";
            this.btnScan.UseVisualStyleBackColor = false;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // btnWeb
            // 
            this.btnWeb.BackColor = System.Drawing.Color.Orange;
            this.btnWeb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnWeb.FlatAppearance.BorderSize = 2;
            this.btnWeb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWeb.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnWeb.Location = new System.Drawing.Point(4, 2);
            this.btnWeb.Name = "btnWeb";
            this.btnWeb.Size = new System.Drawing.Size(88, 36);
            this.btnWeb.TabIndex = 0;
            this.btnWeb.Text = "WEB";
            this.btnWeb.UseVisualStyleBackColor = false;
            this.btnWeb.Click += new System.EventHandler(this.btnWeb_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(webToServerMessage);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(serverToProcessMessage);
            this.splitContainer1.Size = new System.Drawing.Size(748, 421);
            this.splitContainer1.SplitterDistance = 373;
            this.splitContainer1.TabIndex = 2;
            // 
            // webToServerMessage
            // 
            webToServerMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            webToServerMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            webToServerMessage.Location = new System.Drawing.Point(0, 0);
            webToServerMessage.Name = "webToServerMessage";
            webToServerMessage.Size = new System.Drawing.Size(369, 417);
            webToServerMessage.TabIndex = 3;
            webToServerMessage.Text = "";
            // 
            // serverToProcessMessage
            // 
            serverToProcessMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            serverToProcessMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            serverToProcessMessage.Location = new System.Drawing.Point(0, 0);
            serverToProcessMessage.Name = "serverToProcessMessage";
            serverToProcessMessage.Size = new System.Drawing.Size(367, 417);
            serverToProcessMessage.TabIndex = 4;
            serverToProcessMessage.Text = "";
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.ctxms;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Mois_LocalWebServer";
            this.notifyIcon.Visible = true;
            // 
            // ctxms
            // 
            this.ctxms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolStripMenuItem,
            this.serverStartToolStripMenuItem,
            this.serverStopToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.ctxms.Name = "ctxms";
            this.ctxms.Size = new System.Drawing.Size(133, 92);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.viewToolStripMenuItem.Text = "View";
            this.viewToolStripMenuItem.Click += new System.EventHandler(this.viewToolStripMenuItem_Click);
            // 
            // serverStartToolStripMenuItem
            // 
            this.serverStartToolStripMenuItem.Name = "serverStartToolStripMenuItem";
            this.serverStartToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.serverStartToolStripMenuItem.Text = "ServerStart";
            this.serverStartToolStripMenuItem.Click += new System.EventHandler(this.serverStartToolStripMenuItem_Click);
            // 
            // serverStopToolStripMenuItem
            // 
            this.serverStopToolStripMenuItem.Name = "serverStopToolStripMenuItem";
            this.serverStopToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.serverStopToolStripMenuItem.Text = "ServerStop";
            this.serverStopToolStripMenuItem.Click += new System.EventHandler(this.serverStopToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // HYWebServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 467);
            this.Controls.Add(this.tlpMain);
            this.Name = "HYWebServer";
            this.Text = "HYWebServer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Mois_LocalWebServer_FormClosing);
            this.Load += new System.EventHandler(this.Mois_LocalWebServer_Load);
            this.tlpMain.ResumeLayout(false);
            this.plnControl.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ctxms.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Panel plnControl;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Button btnWeb;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip ctxms;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serverStartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serverStopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnExit;
        private static System.Windows.Forms.RichTextBox webToServerMessage;
        private static System.Windows.Forms.RichTextBox serverToProcessMessage;
    }
}

