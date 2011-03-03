namespace Server_Parser
{
    partial class serverParserForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(serverParserForm));
            this.refreshButton = new System.Windows.Forms.Button();
            this.serverList = new System.Windows.Forms.ListView();
            this.columnName = new System.Windows.Forms.ColumnHeader();
            this.IP = new System.Windows.Forms.ColumnHeader();
            this.ping = new System.Windows.Forms.ColumnHeader();
            this.gametype = new System.Windows.Forms.ColumnHeader();
            this.mapname = new System.Windows.Forms.ColumnHeader();
            this.mod = new System.Windows.Forms.ColumnHeader();
            this.country = new System.Windows.Forms.ColumnHeader();
            this.players = new System.Windows.Forms.ColumnHeader();
            this.status = new System.Windows.Forms.ColumnHeader();
            this.serverNumLabel = new System.Windows.Forms.Label();
            this.serverNumLabel2 = new System.Windows.Forms.Label();
            this.copyIP = new System.Windows.Forms.Button();
            this.runMPIP = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.serverStats = new System.Windows.Forms.Button();
            this.ipLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(756, 214);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(57, 23);
            this.refreshButton.TabIndex = 0;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // serverList
            // 
            this.serverList.CausesValidation = false;
            this.serverList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnName,
            this.IP,
            this.ping,
            this.gametype,
            this.mapname,
            this.mod,
            this.country,
            this.players,
            this.status});
            this.serverList.GridLines = true;
            this.serverList.Location = new System.Drawing.Point(12, 12);
            this.serverList.MultiSelect = false;
            this.serverList.Name = "serverList";
            this.serverList.Size = new System.Drawing.Size(801, 196);
            this.serverList.TabIndex = 1;
            this.serverList.UseCompatibleStateImageBehavior = false;
            this.serverList.View = System.Windows.Forms.View.Details;
            this.serverList.SelectedIndexChanged += new System.EventHandler(this.serverList_SelectedIndexChanged);
            // 
            // columnName
            // 
            this.columnName.Tag = "string";
            this.columnName.Text = "Host Name";
            this.columnName.Width = 162;
            // 
            // IP
            // 
            this.IP.Tag = "string";
            this.IP.Text = "IP";
            this.IP.Width = 121;
            // 
            // ping
            // 
            this.ping.Tag = "int";
            this.ping.Text = "Ping";
            this.ping.Width = 38;
            // 
            // gametype
            // 
            this.gametype.Tag = "string";
            this.gametype.Text = "Game Type";
            this.gametype.Width = 67;
            // 
            // mapname
            // 
            this.mapname.Tag = "string";
            this.mapname.Text = "Map Name";
            this.mapname.Width = 91;
            // 
            // mod
            // 
            this.mod.Tag = "string";
            this.mod.Text = "Mod";
            this.mod.Width = 86;
            // 
            // country
            // 
            this.country.Tag = "string";
            this.country.Text = "Country";
            this.country.Width = 54;
            // 
            // players
            // 
            this.players.Text = "Players";
            this.players.Width = 66;
            // 
            // status
            // 
            this.status.Text = "Status";
            this.status.Width = 100;
            // 
            // serverNumLabel
            // 
            this.serverNumLabel.AutoSize = true;
            this.serverNumLabel.Location = new System.Drawing.Point(12, 219);
            this.serverNumLabel.Name = "serverNumLabel";
            this.serverNumLabel.Size = new System.Drawing.Size(99, 13);
            this.serverNumLabel.TabIndex = 2;
            this.serverNumLabel.Text = "Number of servers :";
            // 
            // serverNumLabel2
            // 
            this.serverNumLabel2.AutoSize = true;
            this.serverNumLabel2.Location = new System.Drawing.Point(117, 219);
            this.serverNumLabel2.Name = "serverNumLabel2";
            this.serverNumLabel2.Size = new System.Drawing.Size(10, 13);
            this.serverNumLabel2.TabIndex = 3;
            this.serverNumLabel2.Text = "-";
            // 
            // copyIP
            // 
            this.copyIP.Location = new System.Drawing.Point(694, 214);
            this.copyIP.Name = "copyIP";
            this.copyIP.Size = new System.Drawing.Size(56, 23);
            this.copyIP.TabIndex = 4;
            this.copyIP.Text = "Copy IP";
            this.copyIP.UseVisualStyleBackColor = true;
            this.copyIP.Click += new System.EventHandler(this.copyIP_Click);
            // 
            // runMPIP
            // 
            this.runMPIP.Location = new System.Drawing.Point(613, 214);
            this.runMPIP.Name = "runMPIP";
            this.runMPIP.Size = new System.Drawing.Size(75, 23);
            this.runMPIP.TabIndex = 5;
            this.runMPIP.Text = "Run with IP";
            this.runMPIP.UseVisualStyleBackColor = true;
            this.runMPIP.Click += new System.EventHandler(this.runMPIP_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(187, 214);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(130, 23);
            this.progressBar1.TabIndex = 6;
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(323, 214);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(130, 23);
            this.progressBar2.TabIndex = 7;
            // 
            // serverStats
            // 
            this.serverStats.Location = new System.Drawing.Point(532, 214);
            this.serverStats.Name = "serverStats";
            this.serverStats.Size = new System.Drawing.Size(75, 23);
            this.serverStats.TabIndex = 8;
            this.serverStats.Text = "Server Stats";
            this.serverStats.UseVisualStyleBackColor = true;
            this.serverStats.Click += new System.EventHandler(this.serverStats_Click);
            // 
            // ipLabel
            // 
            this.ipLabel.AutoSize = true;
            this.ipLabel.Location = new System.Drawing.Point(693, 247);
            this.ipLabel.Name = "ipLabel";
            this.ipLabel.Size = new System.Drawing.Size(35, 13);
            this.ipLabel.TabIndex = 9;
            this.ipLabel.Text = "label1";
            // 
            // serverParserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 249);
            this.Controls.Add(this.ipLabel);
            this.Controls.Add(this.serverStats);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.runMPIP);
            this.Controls.Add(this.copyIP);
            this.Controls.Add(this.serverNumLabel2);
            this.Controls.Add(this.serverNumLabel);
            this.Controls.Add(this.serverList);
            this.Controls.Add(this.refreshButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "serverParserForm";
            this.Text = "Server List Parser";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.ListView serverList;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.ColumnHeader IP;
        private System.Windows.Forms.Label serverNumLabel;
        private System.Windows.Forms.Label serverNumLabel2;
        private System.Windows.Forms.Button copyIP;
        private System.Windows.Forms.ColumnHeader ping;
        private System.Windows.Forms.ColumnHeader gametype;
        private System.Windows.Forms.ColumnHeader mapname;
        private System.Windows.Forms.ColumnHeader mod;
        private System.Windows.Forms.Button runMPIP;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.Button serverStats;
        private System.Windows.Forms.Label ipLabel;
        private System.Windows.Forms.ColumnHeader country;
        private System.Windows.Forms.ColumnHeader players;
        private System.Windows.Forms.ColumnHeader status;
    }
}

