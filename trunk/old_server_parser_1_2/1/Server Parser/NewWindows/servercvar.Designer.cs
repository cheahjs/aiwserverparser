namespace Server_Parser
{
    partial class servercvar
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
            this.dvarList = new System.Windows.Forms.ListView();
            this.Dvar = new System.Windows.Forms.ColumnHeader();
            this.value = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // dvarList
            // 
            this.dvarList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Dvar,
            this.value});
            this.dvarList.GridLines = true;
            this.dvarList.Location = new System.Drawing.Point(12, 12);
            this.dvarList.Name = "dvarList";
            this.dvarList.Size = new System.Drawing.Size(212, 317);
            this.dvarList.TabIndex = 0;
            this.dvarList.UseCompatibleStateImageBehavior = false;
            this.dvarList.View = System.Windows.Forms.View.Details;
            // 
            // Dvar
            // 
            this.Dvar.Text = "Dvar";
            this.Dvar.Width = 108;
            // 
            // value
            // 
            this.value.Text = "Value";
            this.value.Width = 98;
            // 
            // servercvar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(236, 341);
            this.Controls.Add(this.dvarList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "servercvar";
            this.Text = "Server Dvars";
            this.Load += new System.EventHandler(this.servercvar_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView dvarList;
        private System.Windows.Forms.ColumnHeader Dvar;
        private System.Windows.Forms.ColumnHeader value;
    }
}