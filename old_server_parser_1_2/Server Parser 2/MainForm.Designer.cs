namespace Server_Parser_2
{
    partial class mainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.serverList = new System.Windows.Forms.ListView();
            this.name = new System.Windows.Forms.ColumnHeader();
            this.ip = new System.Windows.Forms.ColumnHeader();
            this.map = new System.Windows.Forms.ColumnHeader();
            this.players = new System.Windows.Forms.ColumnHeader();
            this.gametype = new System.Windows.Forms.ColumnHeader();
            this.mod = new System.Windows.Forms.ColumnHeader();
            this.ping = new System.Windows.Forms.ColumnHeader();
            this.status = new System.Windows.Forms.ColumnHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.refreshButton = new System.Windows.Forms.Button();
            this.continuousRefresh = new System.Windows.Forms.CheckBox();
            this.playerList = new System.Windows.Forms.ListView();
            this.player = new System.Windows.Forms.ColumnHeader();
            this.score = new System.Windows.Forms.ColumnHeader();
            this.playerping = new System.Windows.Forms.ColumnHeader();
            this.label2 = new System.Windows.Forms.Label();
            this.displayDvars = new System.Windows.Forms.Button();
            this.ipQuery = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.servernumBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dvarfilterBox = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label5 = new System.Windows.Forms.Label();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.txtAddIP = new System.Windows.Forms.TextBox();
            this.btnAddIP = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblServerCount = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.btnSwitchDedi = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // serverList
            // 
            this.serverList.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.serverList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.serverList.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.serverList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name,
            this.ip,
            this.map,
            this.players,
            this.gametype,
            this.mod,
            this.ping,
            this.status});
            this.serverList.FullRowSelect = true;
            this.serverList.GridLines = true;
            this.serverList.HideSelection = false;
            this.serverList.Location = new System.Drawing.Point(13, 31);
            this.serverList.MultiSelect = false;
            this.serverList.Name = "serverList";
            this.serverList.Size = new System.Drawing.Size(667, 216);
            this.serverList.TabIndex = 0;
            this.serverList.UseCompatibleStateImageBehavior = false;
            this.serverList.View = System.Windows.Forms.View.Details;
            this.serverList.SelectedIndexChanged += new System.EventHandler(this.serverList_SelectedIndexChanged);
            // 
            // name
            // 
            this.name.Text = "Server Name";
            this.name.Width = 123;
            // 
            // ip
            // 
            this.ip.Text = "IP";
            this.ip.Width = 127;
            // 
            // map
            // 
            this.map.Text = "Map";
            this.map.Width = 73;
            // 
            // players
            // 
            this.players.Text = "Players";
            this.players.Width = 55;
            // 
            // gametype
            // 
            this.gametype.Text = "Game Type";
            this.gametype.Width = 74;
            // 
            // mod
            // 
            this.mod.Text = "Mod";
            this.mod.Width = 65;
            // 
            // ping
            // 
            this.ping.Text = "Ping";
            this.ping.Width = 47;
            // 
            // status
            // 
            this.status.Text = "Status";
            this.status.Width = 93;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server List";
            // 
            // refreshButton
            // 
            this.refreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshButton.Location = new System.Drawing.Point(605, 257);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(75, 23);
            this.refreshButton.TabIndex = 2;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // continuousRefresh
            // 
            this.continuousRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.continuousRefresh.AutoSize = true;
            this.continuousRefresh.Location = new System.Drawing.Point(480, 261);
            this.continuousRefresh.Name = "continuousRefresh";
            this.continuousRefresh.Size = new System.Drawing.Size(119, 17);
            this.continuousRefresh.TabIndex = 3;
            this.continuousRefresh.Text = "Continuous Refresh";
            this.continuousRefresh.UseVisualStyleBackColor = true;
            // 
            // playerList
            // 
            this.playerList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.playerList.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.playerList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.player,
            this.score,
            this.playerping});
            this.playerList.FullRowSelect = true;
            this.playerList.GridLines = true;
            this.playerList.Location = new System.Drawing.Point(688, 31);
            this.playerList.Name = "playerList";
            this.playerList.Size = new System.Drawing.Size(221, 216);
            this.playerList.TabIndex = 4;
            this.playerList.UseCompatibleStateImageBehavior = false;
            this.playerList.View = System.Windows.Forms.View.Details;
            // 
            // player
            // 
            this.player.Text = "Name";
            this.player.Width = 97;
            // 
            // score
            // 
            this.score.Text = "Score";
            this.score.Width = 76;
            // 
            // playerping
            // 
            this.playerping.Text = "Ping";
            this.playerping.Width = 43;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(685, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Server Players";
            // 
            // displayDvars
            // 
            this.displayDvars.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.displayDvars.Location = new System.Drawing.Point(832, 287);
            this.displayDvars.Name = "displayDvars";
            this.displayDvars.Size = new System.Drawing.Size(77, 23);
            this.displayDvars.TabIndex = 6;
            this.displayDvars.Text = "Server Dvars";
            this.displayDvars.UseVisualStyleBackColor = true;
            this.displayDvars.Click += new System.EventHandler(this.displayDvars_Click);
            // 
            // ipQuery
            // 
            this.ipQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ipQuery.Location = new System.Drawing.Point(687, 289);
            this.ipQuery.Name = "ipQuery";
            this.ipQuery.ReadOnly = true;
            this.ipQuery.Size = new System.Drawing.Size(138, 20);
            this.ipQuery.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 262);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Max servers :";
            // 
            // servernumBox
            // 
            this.servernumBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.servernumBox.Location = new System.Drawing.Point(89, 259);
            this.servernumBox.Name = "servernumBox";
            this.servernumBox.Size = new System.Drawing.Size(67, 20);
            this.servernumBox.TabIndex = 9;
            this.servernumBox.Text = "50";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(162, 262);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Filters :";
            // 
            // dvarfilterBox
            // 
            this.dvarfilterBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dvarfilterBox.Location = new System.Drawing.Point(208, 259);
            this.dvarfilterBox.Name = "dvarfilterBox";
            this.dvarfilterBox.Size = new System.Drawing.Size(100, 20);
            this.dvarfilterBox.TabIndex = 11;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(319, 262);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "0%";
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            this.backgroundWorker2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker2_RunWorkerCompleted);
            // 
            // txtAddIP
            // 
            this.txtAddIP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtAddIP.Location = new System.Drawing.Point(13, 289);
            this.txtAddIP.Name = "txtAddIP";
            this.txtAddIP.Size = new System.Drawing.Size(132, 20);
            this.txtAddIP.TabIndex = 13;
            // 
            // btnAddIP
            // 
            this.btnAddIP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddIP.Location = new System.Drawing.Point(151, 287);
            this.btnAddIP.Name = "btnAddIP";
            this.btnAddIP.Size = new System.Drawing.Size(75, 23);
            this.btnAddIP.TabIndex = 14;
            this.btnAddIP.Text = "Add Server";
            this.btnAddIP.UseVisualStyleBackColor = true;
            this.btnAddIP.Click += new System.EventHandler(this.btnAddIP_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.Location = new System.Drawing.Point(605, 287);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 15;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopy.Location = new System.Drawing.Point(524, 287);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 16;
            this.btnCopy.Text = "Copy IP";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(687, 262);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Selected server:";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(232, 292);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Server count :";
            // 
            // lblServerCount
            // 
            this.lblServerCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblServerCount.AutoSize = true;
            this.lblServerCount.Location = new System.Drawing.Point(312, 292);
            this.lblServerCount.Name = "lblServerCount";
            this.lblServerCount.Size = new System.Drawing.Size(0, 13);
            this.lblServerCount.TabIndex = 19;
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Location = new System.Drawing.Point(441, 287);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(77, 23);
            this.btnStop.TabIndex = 20;
            this.btnStop.Text = "Stop Refresh";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(881, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(28, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "2.07";
            // 
            // btnSwitchDedi
            // 
            this.btnSwitchDedi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSwitchDedi.Location = new System.Drawing.Point(399, 257);
            this.btnSwitchDedi.Name = "btnSwitchDedi";
            this.btnSwitchDedi.Size = new System.Drawing.Size(75, 23);
            this.btnSwitchDedi.TabIndex = 22;
            this.btnSwitchDedi.Text = "Dedicated";
            this.btnSwitchDedi.UseVisualStyleBackColor = true;
            this.btnSwitchDedi.Click += new System.EventHandler(this.btnSwitchDedi_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(921, 321);
            this.Controls.Add(this.btnSwitchDedi);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.lblServerCount);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnAddIP);
            this.Controls.Add(this.txtAddIP);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dvarfilterBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.servernumBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ipQuery);
            this.Controls.Add(this.displayDvars);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.playerList);
            this.Controls.Add(this.continuousRefresh);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.serverList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(842, 357);
            this.Name = "mainForm";
            this.Text = "Server List Parser 2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView serverList;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader ip;
        private System.Windows.Forms.ColumnHeader players;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.CheckBox continuousRefresh;
        private System.Windows.Forms.ColumnHeader map;
        private System.Windows.Forms.ColumnHeader mod;
        private System.Windows.Forms.ColumnHeader status;
        private System.Windows.Forms.ColumnHeader ping;
        private System.Windows.Forms.ListView playerList;
        private System.Windows.Forms.ColumnHeader player;
        private System.Windows.Forms.ColumnHeader score;
        private System.Windows.Forms.ColumnHeader playerping;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button displayDvars;
        private System.Windows.Forms.TextBox ipQuery;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox servernumBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox dvarfilterBox;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label5;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.TextBox txtAddIP;
        private System.Windows.Forms.Button btnAddIP;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblServerCount;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ColumnHeader gametype;
        private System.Windows.Forms.Button btnSwitchDedi;
    }
}

