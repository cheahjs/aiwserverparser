namespace Server_Parser
{
    partial class serverstatsForm
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
            this.cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.serverNameBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gametypeBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.mapBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ipBox = new System.Windows.Forms.TextBox();
            this.modBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.playersOnlineBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.refreshButton = new System.Windows.Forms.Button();
            this.playerList = new System.Windows.Forms.ListView();
            this.playerName = new System.Windows.Forms.ColumnHeader();
            this.score = new System.Windows.Forms.ColumnHeader();
            this.ping = new System.Windows.Forms.ColumnHeader();
            this.serverCvar = new System.Windows.Forms.Button();
            this.queryIPBox = new System.Windows.Forms.TextBox();
            this.connectLink = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(324, 444);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 1;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Server Name";
            // 
            // serverNameBox
            // 
            this.serverNameBox.Location = new System.Drawing.Point(88, 10);
            this.serverNameBox.Name = "serverNameBox";
            this.serverNameBox.ReadOnly = true;
            this.serverNameBox.Size = new System.Drawing.Size(311, 20);
            this.serverNameBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Game Type";
            // 
            // gametypeBox
            // 
            this.gametypeBox.Location = new System.Drawing.Point(88, 32);
            this.gametypeBox.Name = "gametypeBox";
            this.gametypeBox.ReadOnly = true;
            this.gametypeBox.Size = new System.Drawing.Size(311, 20);
            this.gametypeBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Map";
            // 
            // mapBox
            // 
            this.mapBox.Location = new System.Drawing.Point(88, 54);
            this.mapBox.Name = "mapBox";
            this.mapBox.ReadOnly = true;
            this.mapBox.Size = new System.Drawing.Size(311, 20);
            this.mapBox.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "IP";
            // 
            // ipBox
            // 
            this.ipBox.Location = new System.Drawing.Point(88, 76);
            this.ipBox.Name = "ipBox";
            this.ipBox.ReadOnly = true;
            this.ipBox.Size = new System.Drawing.Size(311, 20);
            this.ipBox.TabIndex = 9;
            // 
            // modBox
            // 
            this.modBox.Location = new System.Drawing.Point(88, 99);
            this.modBox.Name = "modBox";
            this.modBox.ReadOnly = true;
            this.modBox.Size = new System.Drawing.Size(311, 20);
            this.modBox.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Mod";
            // 
            // playersOnlineBox
            // 
            this.playersOnlineBox.Location = new System.Drawing.Point(88, 122);
            this.playersOnlineBox.Name = "playersOnlineBox";
            this.playersOnlineBox.ReadOnly = true;
            this.playersOnlineBox.Size = new System.Drawing.Size(311, 20);
            this.playersOnlineBox.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 125);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Players Online";
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(243, 444);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(75, 23);
            this.refreshButton.TabIndex = 14;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // playerList
            // 
            this.playerList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.playerName,
            this.score,
            this.ping});
            this.playerList.GridLines = true;
            this.playerList.Location = new System.Drawing.Point(16, 148);
            this.playerList.Name = "playerList";
            this.playerList.Size = new System.Drawing.Size(383, 290);
            this.playerList.TabIndex = 15;
            this.playerList.UseCompatibleStateImageBehavior = false;
            this.playerList.View = System.Windows.Forms.View.Details;
            // 
            // playerName
            // 
            this.playerName.Text = "Player Name";
            this.playerName.Width = 150;
            // 
            // score
            // 
            this.score.Text = "Score";
            this.score.Width = 100;
            // 
            // ping
            // 
            this.ping.Text = "Ping";
            this.ping.Width = 70;
            // 
            // serverCvar
            // 
            this.serverCvar.Location = new System.Drawing.Point(162, 444);
            this.serverCvar.Name = "serverCvar";
            this.serverCvar.Size = new System.Drawing.Size(75, 23);
            this.serverCvar.TabIndex = 16;
            this.serverCvar.Text = "Server Dvar";
            this.serverCvar.UseVisualStyleBackColor = true;
            this.serverCvar.Click += new System.EventHandler(this.serverCvar_Click);
            // 
            // queryIPBox
            // 
            this.queryIPBox.Location = new System.Drawing.Point(16, 446);
            this.queryIPBox.Name = "queryIPBox";
            this.queryIPBox.Size = new System.Drawing.Size(140, 20);
            this.queryIPBox.TabIndex = 17;
            this.queryIPBox.Text = "IP:Port to query";
            // 
            // connectLink
            // 
            this.connectLink.AutoSize = true;
            this.connectLink.Location = new System.Drawing.Point(308, 79);
            this.connectLink.Name = "connectLink";
            this.connectLink.Size = new System.Drawing.Size(86, 13);
            this.connectLink.TabIndex = 18;
            this.connectLink.TabStop = true;
            this.connectLink.Text = "Connect to <<<<";
            this.connectLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.connectLink_LinkClicked);
            // 
            // serverstatsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 479);
            this.ControlBox = false;
            this.Controls.Add(this.connectLink);
            this.Controls.Add(this.queryIPBox);
            this.Controls.Add(this.serverCvar);
            this.Controls.Add(this.playerList);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.playersOnlineBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.modBox);
            this.Controls.Add(this.ipBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.mapBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.gametypeBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.serverNameBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "serverstatsForm";
            this.ShowInTaskbar = false;
            this.Text = "Server Stats";
            this.Load += new System.EventHandler(this.serverstatsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox serverNameBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox gametypeBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox mapBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ipBox;
        private System.Windows.Forms.TextBox modBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox playersOnlineBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.ListView playerList;
        private System.Windows.Forms.ColumnHeader playerName;
        private System.Windows.Forms.ColumnHeader score;
        private System.Windows.Forms.ColumnHeader ping;
        private System.Windows.Forms.Button serverCvar;
        private System.Windows.Forms.TextBox queryIPBox;
        private System.Windows.Forms.LinkLabel connectLink;
    }
}