using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Server_Parser_2
{
    public partial class frmDvars : Form
    {
        public frmDvars()
        {
            InitializeComponent();
        }

        public string selectedIP;
        public static Dictionary<string, Dictionary<string, string>> serverdvars = new Dictionary<string, Dictionary<string, string>>();

        private void frmDvars_Load(object sender, EventArgs e)
        {
            if (mainForm.activated)
            {
                this.Text = "Server Dvars (" + mainForm.selectedIP + ")";
                selectedIP = mainForm.selectedIP.Remove(mainForm.selectedIP.Length - 6);
                serverdvars = mainForm.serverdvars;
            }
            else
            {
                this.Text = "Server Dvars (" + frmDedi.selectedIP + ")";
                selectedIP = frmDedi.selectedIP.Remove(frmDedi.selectedIP.Length - 6);
                serverdvars = frmDedi.serverdvars;
            }
            populateList();
        }

        public void populateList()
        {
            var dictionary = new Dictionary<string, string>();
            foreach (var pair in serverdvars)
            {
                if (pair.Key == selectedIP)
                {
                    dictionary = pair.Value;
                    break;
                }
            }

            if (dictionary != null)
            {
                foreach (var pair in dictionary)
                {
                    ListViewItem lvItem = listView1.Items.Add(pair.Key);
                    lvItem.SubItems.Add(pair.Value);
                }
            }
        }
    }
}
