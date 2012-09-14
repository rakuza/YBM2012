namespace YBMServer
{
    partial class Main
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
            this.staticTitle = new System.Windows.Forms.Label();
            this.staticServerStatus = new System.Windows.Forms.Label();
            this.lblPowerStatus = new System.Windows.Forms.Label();
            this.staticUserCount = new System.Windows.Forms.Label();
            this.lblUserCount = new System.Windows.Forms.Label();
            this.staticlog = new System.Windows.Forms.Label();
            this.btnPower = new System.Windows.Forms.Button();
            this.staticServerInfo = new System.Windows.Forms.Label();
            this.staticIP = new System.Windows.Forms.Label();
            this.staticPort = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listActiveProjectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.staticServerName = new System.Windows.Forms.Label();
            this.lblServerName = new System.Windows.Forms.Label();
            this.lbxIP = new System.Windows.Forms.ListBox();
            this.lbxLog = new System.Windows.Forms.ListBox();
            this.testBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.testBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // staticTitle
            // 
            this.staticTitle.AutoSize = true;
            this.staticTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.staticTitle.Location = new System.Drawing.Point(13, 27);
            this.staticTitle.Name = "staticTitle";
            this.staticTitle.Size = new System.Drawing.Size(276, 25);
            this.staticTitle.TabIndex = 1;
            this.staticTitle.Text = "Year Book Maker Server Stats";
            // 
            // staticServerStatus
            // 
            this.staticServerStatus.AutoSize = true;
            this.staticServerStatus.Location = new System.Drawing.Point(15, 62);
            this.staticServerStatus.Name = "staticServerStatus";
            this.staticServerStatus.Size = new System.Drawing.Size(77, 13);
            this.staticServerStatus.TabIndex = 2;
            this.staticServerStatus.Text = "Server Status :";
            // 
            // lblPowerStatus
            // 
            this.lblPowerStatus.AutoSize = true;
            this.lblPowerStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(150)))));
            this.lblPowerStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPowerStatus.Location = new System.Drawing.Point(98, 62);
            this.lblPowerStatus.Name = "lblPowerStatus";
            this.lblPowerStatus.Size = new System.Drawing.Size(82, 15);
            this.lblPowerStatus.TabIndex = 3;
            this.lblPowerStatus.Text = "<online/offline>";
            // 
            // staticUserCount
            // 
            this.staticUserCount.AutoSize = true;
            this.staticUserCount.Location = new System.Drawing.Point(208, 62);
            this.staticUserCount.Name = "staticUserCount";
            this.staticUserCount.Size = new System.Drawing.Size(94, 13);
            this.staticUserCount.TabIndex = 4;
            this.staticUserCount.Text = "Users Logged on :";
            // 
            // lblUserCount
            // 
            this.lblUserCount.AutoSize = true;
            this.lblUserCount.Location = new System.Drawing.Point(309, 62);
            this.lblUserCount.Name = "lblUserCount";
            this.lblUserCount.Size = new System.Drawing.Size(54, 13);
            this.lblUserCount.TabIndex = 5;
            this.lblUserCount.Text = "<number>";
            // 
            // staticlog
            // 
            this.staticlog.AutoSize = true;
            this.staticlog.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.staticlog.Location = new System.Drawing.Point(17, 155);
            this.staticlog.Name = "staticlog";
            this.staticlog.Size = new System.Drawing.Size(86, 20);
            this.staticlog.TabIndex = 6;
            this.staticlog.Text = "Server Log";
            // 
            // btnPower
            // 
            this.btnPower.Location = new System.Drawing.Point(18, 78);
            this.btnPower.Name = "btnPower";
            this.btnPower.Size = new System.Drawing.Size(75, 23);
            this.btnPower.TabIndex = 7;
            this.btnPower.Text = "Start/Stop";
            this.btnPower.UseVisualStyleBackColor = true;
            // 
            // staticServerInfo
            // 
            this.staticServerInfo.AutoSize = true;
            this.staticServerInfo.Location = new System.Drawing.Point(18, 108);
            this.staticServerInfo.Name = "staticServerInfo";
            this.staticServerInfo.Size = new System.Drawing.Size(90, 13);
            this.staticServerInfo.TabIndex = 8;
            this.staticServerInfo.Text = "Server Infomation";
            // 
            // staticIP
            // 
            this.staticIP.AutoSize = true;
            this.staticIP.Location = new System.Drawing.Point(198, 108);
            this.staticIP.Name = "staticIP";
            this.staticIP.Size = new System.Drawing.Size(75, 13);
            this.staticIP.TabIndex = 9;
            this.staticIP.Text = "IP Addresses :";
            // 
            // staticPort
            // 
            this.staticPort.AutoSize = true;
            this.staticPort.Location = new System.Drawing.Point(18, 142);
            this.staticPort.Name = "staticPort";
            this.staticPort.Size = new System.Drawing.Size(32, 13);
            this.staticPort.TabIndex = 10;
            this.staticPort.Text = "Port :";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(95, 142);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(37, 13);
            this.lblPort.TabIndex = 12;
            this.lblPort.Text = "<port>";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(502, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startServerToolStripMenuItem,
            this.stopServerToolStripMenuItem,
            this.toolStripSeparator1,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // startServerToolStripMenuItem
            // 
            this.startServerToolStripMenuItem.Name = "startServerToolStripMenuItem";
            this.startServerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.startServerToolStripMenuItem.Text = "Start Server";
            // 
            // stopServerToolStripMenuItem
            // 
            this.stopServerToolStripMenuItem.Name = "stopServerToolStripMenuItem";
            this.stopServerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.stopServerToolStripMenuItem.Text = "Stop Server";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.quitToolStripMenuItem.Text = "Exit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listActiveProjectsToolStripMenuItem,
            this.toolStripSeparator2,
            this.preferencesToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // listActiveProjectsToolStripMenuItem
            // 
            this.listActiveProjectsToolStripMenuItem.Name = "listActiveProjectsToolStripMenuItem";
            this.listActiveProjectsToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.listActiveProjectsToolStripMenuItem.Text = "List Active Projects";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(170, 6);
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.preferencesToolStripMenuItem.Text = "Preferences";
            this.preferencesToolStripMenuItem.Click += new System.EventHandler(this.preferencesToolStripMenuItem_Click);
            // 
            // staticServerName
            // 
            this.staticServerName.AutoSize = true;
            this.staticServerName.Location = new System.Drawing.Point(18, 126);
            this.staticServerName.Name = "staticServerName";
            this.staticServerName.Size = new System.Drawing.Size(75, 13);
            this.staticServerName.TabIndex = 14;
            this.staticServerName.Text = "Server Name :";
            // 
            // lblServerName
            // 
            this.lblServerName.AutoSize = true;
            this.lblServerName.Location = new System.Drawing.Point(95, 126);
            this.lblServerName.Name = "lblServerName";
            this.lblServerName.Size = new System.Drawing.Size(77, 13);
            this.lblServerName.TabIndex = 15;
            this.lblServerName.Text = "<server name>";
            // 
            // lbxIP
            // 
            this.lbxIP.FormattingEnabled = true;
            this.lbxIP.Location = new System.Drawing.Point(279, 108);
            this.lbxIP.Name = "lbxIP";
            this.lbxIP.Size = new System.Drawing.Size(211, 56);
            this.lbxIP.TabIndex = 16;
            // 
            // lbxLog
            // 
            this.lbxLog.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lbxLog.FormattingEnabled = true;
            this.lbxLog.Location = new System.Drawing.Point(13, 179);
            this.lbxLog.Name = "lbxLog";
            this.lbxLog.Size = new System.Drawing.Size(477, 375);
            this.lbxLog.TabIndex = 17;
            this.lbxLog.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbxLog_DrawItem);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 558);
            this.Controls.Add(this.lbxLog);
            this.Controls.Add(this.lbxIP);
            this.Controls.Add(this.lblServerName);
            this.Controls.Add(this.staticServerName);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.staticPort);
            this.Controls.Add(this.staticIP);
            this.Controls.Add(this.staticServerInfo);
            this.Controls.Add(this.btnPower);
            this.Controls.Add(this.staticlog);
            this.Controls.Add(this.lblUserCount);
            this.Controls.Add(this.staticUserCount);
            this.Controls.Add(this.lblPowerStatus);
            this.Controls.Add(this.staticServerStatus);
            this.Controls.Add(this.staticTitle);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "Year Book Maker Server Stats";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.testBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label staticTitle;
        private System.Windows.Forms.Label staticServerStatus;
        private System.Windows.Forms.Label lblPowerStatus;
        private System.Windows.Forms.Label staticUserCount;
        private System.Windows.Forms.Label lblUserCount;
        private System.Windows.Forms.Label staticlog;
        private System.Windows.Forms.Button btnPower;
        private System.Windows.Forms.Label staticServerInfo;
        private System.Windows.Forms.Label staticIP;
        private System.Windows.Forms.Label staticPort;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.Label staticServerName;
        private System.Windows.Forms.Label lblServerName;
        private System.Windows.Forms.ListBox lbxIP;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listActiveProjectsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.BindingSource testBindingSource;
        private System.Windows.Forms.ListBox lbxLog;
    }
}

