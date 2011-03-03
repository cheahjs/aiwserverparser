using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Net;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.IO;
using System.Threading;
using EV.Windows.Forms;

namespace Server_Parser
{
    public partial class serverParserForm : Form
    {
        public serverParserForm()
        {
            InitializeComponent();

            m_sortMgr = new ListViewSortManager(serverList,
                new Type[] {
                    typeof(ListViewTextCaseInsensitiveSort),
                    typeof(ListViewTextCaseInsensitiveSort),
                    typeof(ListViewInt32Sort),
                    typeof(ListViewTextCaseInsensitiveSort),
                    typeof(ListViewTextCaseInsensitiveSort),
                    typeof(ListViewTextCaseInsensitiveSort),
                    typeof(ListViewTextCaseInsensitiveSort),
                    typeof(ListViewTextCaseInsensitiveSort),
                    typeof(ListViewTextCaseInsensitiveSort)
                            }
                            );
        }

        private ListViewSortManager m_sortMgr;

        private void refreshButton_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
            refreshButton.Enabled = false;
            serverList.Items.Clear();
        }
        
        public void parseXML()
        {
            try
            {
                int totalservercount = 0;
                int timescalled = 0;
                char[] portTrim = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
                char[] colonTrim = { ':' };
                XmlDocument doc = new XmlDocument();
                doc.Load("servers.xml");
                XmlElement root = doc.DocumentElement;
                XmlNodeList nodes = root.SelectNodes("//Server");
                storeArrays();

                foreach (XmlNode node in nodes)
                {
                    totalservercount++;
                }

                foreach (XmlNode node in nodes)
                {
                    string host = node["HostName"].InnerText;
                    string ip = node["ConnectIP"].InnerText;
                    string ipP = ip.TrimEnd(portTrim);  //ping fails with ports
                    string ipPC = ipP.TrimEnd(colonTrim);   //pings fails with colon
                    pingIP(ipPC);   //run ping async
                    string gametype = getGameType(timescalled);
                    string mapname = getMapname(timescalled);
                    string mod = getMod(timescalled);
                    string countrycode = getCountrycode(timescalled);
                    timescalled++;  //get the next line in the array since some shitty api doesn't accept arrays
                    string serverNumS = totalservercount.ToString();
                    double timescalled1 = (double)(timescalled);
                    double totalservercount1 = (double)(totalservercount);
                    double calc1 = (timescalled1 / totalservercount1);
                    int calc2 = (int)(calc1 * 100);
                    string calc3 = calc2.ToString();
                    string playeronline = node["PlayerCount"].InnerText;
                    string maxplayer = node["MaxPlayerCount"].InnerText;
                    string players = playeronline + "/" + maxplayer;
                    string ingame = node["InGame"].InnerText;
                    if (playeronline == maxplayer)
                    {
                        ingame = ingame + "_Full";
                    }
                    string[] list = new string[11]{host,ip,"",gametype,mapname,mod,serverNumS,calc3,countrycode,players,ingame};
                    setList(list);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR\n" + e);
            }
            
        }
        
        private void copyIP_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection server = this.serverList.SelectedItems;

            foreach (ListViewItem item in server)
            {
                string IP = item.SubItems[1].Text;
                Clipboard.SetDataObject(IP);
                MessageBox.Show(IP + " has been copied to clipboard!");
            }
        }

            public void pingIP(string IP)
            {
                Ping pingSender = new Ping();
                PingOptions options = new PingOptions();
                AutoResetEvent waiter = new AutoResetEvent(false);
                pingSender.PingCompleted += new PingCompletedEventHandler(PingCompletedCallback);

                // Create a buffer of 32 bytes of data to be transmitted.
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 1200;
                //PingReply reply = pingSender.Send(IP, timeout, buffer, options);
                pingSender.SendAsync(IP, timeout, buffer, options, waiter);
                /*
                if (reply.Status == IPStatus.Success)
                {
                    return reply.RoundtripTime;
                }
                else
                {
                    return 9999;
                }*/
            }

            private void PingCompletedCallback(object sender, PingCompletedEventArgs e)
            {
                PingReply reply = e.Reply;
                passArgsData(reply);
            }

            public void passArgsData(PingReply reply)
            {
                string[] data = new string[2]{reply.Address.ToString(),reply.RoundtripTime.ToString()};
                setDataList(data);
            }

            delegate void setDataListCallback(string[] args);

            public void setDataList(string[] args)
            {
                if (serverList.InvokeRequired)
                {
                    setDataListCallback d = new setDataListCallback(setDataList);
                    Invoke(d, new object[] {args});
                }
                else
                {
                    var pingedIP = serverList.Items.Find(args[0], true);
                    if (pingedIP.Length > 0 && pingedIP.Length < 2)
                    {
                        pingedIP[0].SubItems[2].Text = args[1];
                    }
                }
            }


            public static string[] typeArray = new string[5000];
            public static string[] nameArray = new string[5000];
            public static string[] countryArray = new string[5000];
            public static string[] modArray = new string[5000];

            public void storeArrays()
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("servers.xml");
                XmlElement root = doc.DocumentElement;
                XmlNodeList deepnodes = root.SelectNodes("//DataEntries//DataEntry");
                int i1 = 0;
                int i2 = 0;
                int i3 = 0;
                int i4 = 0;
                char[] mods = { 'm', 'o', 'd', 's' };
                char[] slash = { '/' };

                foreach (XmlNode node in deepnodes)
                {
                    if (node["Key"].InnerText == "g_gametype")
                    {
                        object type = node["Value"].InnerText;
                        string typeS = type.ToString();
                        typeArray[i1] = typeS;
                        i1++;
                    }
                    if (node["Key"].InnerText == "mapname")
                    {
                        object mapname = node["Value"].InnerText;
                        string mapnameS = mapname.ToString();
                        nameArray[i2] = mapnameS;
                        i2++;
                    }
                    if (node["Key"].InnerText == "countrycode")
                    {
                        object mapname = node["Value"].InnerText;
                        string mapnameS = mapname.ToString();
                        countryArray[i3] = mapnameS;
                        i3++;
                    }
                    if (node["Key"].InnerText == "fs_game")
                    {
                        object modname = node["Value"].InnerText;
                        string modnameS = modname.ToString();
                        string modSM = modnameS.TrimStart(mods);
                        string modSMT = modSM.TrimStart(slash);
                        modArray[i4] = modSMT;
                        i4++;
                    }
                }
            }

            public string getGameType(int array)
            {
                return typeArray[array];
            }

            public string getMapname(int array)
            {
                return nameArray[array];
            }

            public string getCountrycode(int array)
            {
                return countryArray[array];
            }

            public string getMod(int array)
            {
                return modArray[array];
            }

            private void runMPIP_Click(object sender, EventArgs e)
            {
                ListView.SelectedListViewItemCollection server = this.serverList.SelectedItems;

                foreach (ListViewItem item in server)
                {
                    string connectip = item.SubItems[1].Text;
                    if (item.SubItems[8].Text.Contains("Full"))
                    {
                        DialogResult result = MessageBox.Show("Server is full\nDo you still want to attempt to join?", "Warning", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            Process.Start("aiw://connect/" + connectip);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    if (!item.SubItems[8].Text.Contains("Full"))
                    {
                        Process.Start("aiw://connect/" + connectip);
                    }
                }
            }

            private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
            {
                try
                {
                    string count = "count=" + Microsoft.VisualBasic.Interaction.InputBox("Please enter the max number of servers to retrieve", "Number of servers", "", 100, 100);
                    string filter = Microsoft.VisualBasic.Interaction.InputBox("Please enter whatever dvar you want filtered.\ndvar=filter, seperate with \\", "Filter", "", 100, 100);
                    string uri = "http://server.alteriw.net:13000/servers/yeQA4reD/" + filter + @"/" + count + ".xml";
                    string file = "servers.xml";
                    // first, we need to get the exact size (in bytes) of the file we are downloading
                    Uri url = new Uri(uri);
                    System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(uri);
                    System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                    response.Close();
                    // gets the size of the file in bytes
                    Int64 iSize = response.ContentLength;

                    // keeps track of the total bytes downloaded so we can update the progress bar
                    Int64 iRunningByteTotal = 0;

                    // use the webclient object to download the file
                    using (System.Net.WebClient client = new System.Net.WebClient())
                    {
                        // open the file at the remote URL for reading
                        using (System.IO.Stream streamRemote = client.OpenRead(new Uri(uri)))
                        {
                            // using the FileStream object, we can write the downloaded bytes to the file system
                            using (Stream streamLocal = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.None))
                            {
                                // loop the stream and get the file into the byte buffer
                                int iByteSize = 0;
                                byte[] byteBuffer = new byte[iSize];
                                while ((iByteSize = streamRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                                {
                                    // write the bytes to the file system at the file path specified
                                    streamLocal.Write(byteBuffer, 0, iByteSize);
                                    iRunningByteTotal += iByteSize;

                                    // calculate the progress out of a base "100"
                                    double dIndex = (double)(iRunningByteTotal);
                                    double dTotal = (double)byteBuffer.Length;
                                    double dProgressPercentage = (dIndex / dTotal);
                                    int iProgressPercentage = (int)(dProgressPercentage * 100);

                                    // update the progress bar
                                    backgroundWorker1.ReportProgress(iProgressPercentage);
                                }

                                // clean up the file stream
                                streamLocal.Close();
                            }

                            // close the connection to the remote server
                            streamRemote.Close();
                        }
                    }
                }
                catch (Exception asdsa)
                {
                    MessageBox.Show("ERROR\n" + asdsa);
                }
            }

            private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
            {
                progressBar1.Value = e.ProgressPercentage;
            }

            private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
            {
                progressBar1.Value = 100;
                refreshButton.Enabled = true;
                Thread parsethread = new Thread(new ThreadStart(this.parseXML));
                parsethread.Start();
            }

            delegate void SetTextCallback(string[] text);

            public void setList(string[] args)
            {
                if (serverList.InvokeRequired)
                {
                    SetTextCallback d = new SetTextCallback(setList);
                    this.Invoke(d, new object[] { args });
                }
                else
                {
                    char[] portTrim = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
                    char[] colonTrim = { ':' };
                    ListViewItem lvItem = serverList.Items.Insert(0, args[0]); //add host names to list
                    lvItem.SubItems.Add(args[1]);    //add host ip to list
                    lvItem.SubItems.Add("");
                    lvItem.SubItems.Add(args[3]);
                    lvItem.SubItems.Add(args[4]);
                    lvItem.SubItems.Add(args[5]);
                    serverNumLabel2.Text = args[6];  //display server number result
                    int calc4 = Convert.ToInt32(args[7]);
                    string calc5 = calc4.ToString();
                    progressBar2.Value = calc4;
                    lvItem.SubItems.Add(args[8]);
                    args[1] = args[1].TrimEnd(portTrim);
                    args[1] = args[1].TrimEnd(colonTrim);
                    lvItem.Name = args[1];
                    lvItem.SubItems.Add(args[9]);
                    if (args[10] == "False")
                    {
                        lvItem.SubItems.Add("Free(Lobby)");
                    }
                    if (args[10] == "True")
                    {
                        lvItem.SubItems.Add("Free(In Game)");
                    }
                    if (args[10] == "False_Full")
                    {
                        lvItem.SubItems.Add("Full(Lobby)");
                    }
                    if (args[10] == "True_Full")
                    {
                        lvItem.SubItems.Add("Full(In Game)");
                    }
                }
            }

            public static string selectedIP1 = "";

            public static string selectedIP
            {
                get { return selectedIP1; }
            }


            private void serverStats_Click(object sender, EventArgs e)
            {
                serverstatsForm serverform = new serverstatsForm();

                ListView.SelectedListViewItemCollection server = this.serverList.SelectedItems;

                foreach (ListViewItem item in server)
                {
                    string IP1 = item.SubItems[1].Text;
                    selectedIP1 = IP1;
                }
                serverform.ShowDialog();
            }

            private void serverList_SelectedIndexChanged(object sender, EventArgs e)
            {

            }
    }
}
