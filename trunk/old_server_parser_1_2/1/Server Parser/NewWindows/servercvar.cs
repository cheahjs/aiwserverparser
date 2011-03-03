using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Server_Parser
{
    public partial class servercvar : Form
    {
        public servercvar()
        {
            InitializeComponent();
        }

        private void servercvar_Load(object sender, EventArgs e)
        {
            Dictionary<string, string> data = serverstatsForm.paramDict;
            if (data != null)
            {
                foreach (var pair in data)
                {
                    ListViewItem lvItem = dvarList.Items.Insert(0, pair.Key);
                    lvItem.SubItems.Add(pair.Value);
                }
            }
            if (data == null)
            {
                MessageBox.Show("Are you sure you selected a server and hit the Refresh button?\nMake sure you hit the Refresh button on the server stats window before coming here");

            }
        }
    }
}
