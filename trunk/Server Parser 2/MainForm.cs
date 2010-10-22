using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Xml;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using EV.Windows.Forms;

namespace Server_Parser_2
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();

            //////////////////////////
            //Listview Sorting Stuff//
            //////////////////////////
            m_sortMgr = new ListViewSortManager(serverList,
    new Type[] {
                    typeof(ListViewTextCaseInsensitiveSort),
                    typeof(ListViewTextCaseInsensitiveSort),
                    typeof(ListViewTextCaseInsensitiveSort),
                    typeof(ListViewTextCaseInsensitiveSort),
                    typeof(ListViewTextCaseInsensitiveSort),
                    typeof(ListViewTextCaseInsensitiveSort),
                    typeof(ListViewInt32Sort),
                    typeof(ListViewTextCaseInsensitiveSort)
                            }
                );
            m_sortMgr = new ListViewSortManager(playerList,
new Type[] {
                    typeof(ListViewTextCaseInsensitiveSort),
                    typeof(ListViewInt32Sort),
                    typeof(ListViewInt32Sort),
                            }
    );
        }
        /////////////
        //variables//
        /////////////
        public bool refreshcheckState;
        public string maxservercount;
        public string filters;
        public List<string> connectips = new List<string>();
        delegate void setDataListCallback(string[] args);
        delegate void SetTextCallback(string name, string ip, string map, string players, string gametype, string mod, string status);
        delegate void setPingCallback(string ip, int ping);
        public Dictionary<string, string[,]> playerstats = new Dictionary<string, string[,]>();
        public static Dictionary<string, Dictionary<string, string>> serverdvars = new Dictionary<string, Dictionary<string, string>>();
        char[] numbers = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
        char[] ipnumbers = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '.' };
        //public byte[] hping = { 0xFF, 0xFF, 0xFF, 0XFF, 0x30, 0x68, 0x70, 0x69, 0x6e, 0x67, 0x20, 0x31, 0x30, 0x30, 0x30 }; //sends 4 oob bytes + 0hping 1000
        public byte[] hping = { 0xFF, 0xFF, 0xFF, 0XFF, 0x30, 0x68, 0x70, 0x69, 0x6e, 0x67 }; //sends 4 oob bytes + 0hping
        public byte[] getstatus = { 0xFF, 0xFF, 0xFF, 0XFF, 0x67, 0x65, 0x74, 0x73, 0x74, 0x61, 0x74, 0x75, 0x73 };  //sends 4 OOB bytes + getstatus
        public static string selectedIP;
        public int servercount;
        private ListViewSortManager m_sortMgr;
        public static bool activated;

        //////////
        //events//
        //////////
        private void refreshButton_Click(object sender, EventArgs e)
        {
            clearVars();
            refreshCheck();
            getURIrelated();
            backgroundWorker1.RunWorkerAsync();
            refreshButton.Enabled = false;
        }

        private void serverList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection server = this.serverList.SelectedItems;
            foreach (ListViewItem item in server)
            {
                selectedIP = item.SubItems[1].Text;
            }
            ipQuery.Text = selectedIP;
            displayPlayers(selectedIP);
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (selectedIP != null)
            {
                Clipboard.SetDataObject(selectedIP);
                MessageBox.Show(selectedIP + " has been copied to clipboard!");
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Process.Start("aiw://connect/" + selectedIP);
        }

        private void displayDvars_Click(object sender, EventArgs e)
        {
            frmDvars form = new frmDvars();
            form.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (selectedIP != "")
                displayPlayers(selectedIP);
            multiClear();
            initQuerys();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void btnAddIP_Click(object sender, EventArgs e)
        {
            ListViewItem lvItem = serverList.Items.Add("", 0);
            lvItem.SubItems.Add(txtAddIP.Text);
            connectips.Add(txtAddIP.Text);
        }

        private void btnSwitchDedi_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmDedi form = new frmDedi();
            form.Show();
            activated = false;
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        ///////////////////////////
        //dobackgroundworkerstuff//
        ///////////////////////////
        //retrieving servers.xml//
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string uri = "http://server.alteriw.net:13000/servers/password=yeQA4reD/" + filters + @"/" + "count=" + maxservercount + ".xml";
                Uri url = new Uri(uri);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                response.Close();
                Int64 iSize = response.ContentLength;
                Int64 iRunningByteTotal = 0;
                using (WebClient client = new WebClient())
                {
                    using (Stream streamRemote = client.OpenRead(new Uri(uri)))
                    {
                        using (Stream streamLocal = new FileStream("servers.xml", FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            int iByteSize = 0;
                            byte[] byteBuffer = new byte[iSize];
                            while ((iByteSize = streamRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                            {
                                streamLocal.Write(byteBuffer, 0, iByteSize);
                                iRunningByteTotal += iByteSize;
                                double dIndex = (double)(iRunningByteTotal);
                                double dTotal = (double)byteBuffer.Length;
                                double dProgressPercentage = (dIndex / dTotal);
                                int iProgressPercentage = (int)(dProgressPercentage * 100);
                                backgroundWorker1.ReportProgress(iProgressPercentage);
                            }
                            streamLocal.Close();
                        }
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
            label5.Text = e.ProgressPercentage + "%";
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            label5.Text = "DONE";
            backgroundWorker2.RunWorkerAsync();
        }
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("servers.xml");
                XmlElement root = doc.DocumentElement;
                XmlNodeList nodes = root.SelectNodes("//Server");

                foreach (XmlNode node in nodes)
                {
                    string ip = node["ConnectIP"].InnerText;
                    connectips.Add(ip);
                    servercount++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR\n" + ex);
            }

        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            label5.Text = "DONE";
            lblServerCount.Text = servercount.ToString();
            refreshButton.Enabled = true;
            displayIPs();
        }

        //////////////
        //querystuff//
        //////////////
        public void queryServers(object args)
        {
            string[] args2 = (args.ToString()).Split(':');
            int port = Convert.ToInt32(args2[1]);
            try
            {
                UdpClient udpClient = new UdpClient();
                try
                {
                    udpClient.Connect(args2[0], port);
                    var tickbyte = StrToByteArray(" [" + Environment.TickCount.ToString() + "]");
                    var query = new byte[hping.Length + tickbyte.Length];
                    Array.Copy(hping, 0, query, 0, hping.Length);
                    Array.Copy(tickbyte, 0, query, hping.Length, tickbyte.Length);
                    udpClient.Send(query, query.Length);
                    IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    udpClient.Client.ReceiveTimeout = 800;
                    try
                    {
                        Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
                        var returnData = Encoding.ASCII.GetString(receiveBytes);
                        parseBytes(returnData, args2[0], port);
                        udpClient.Close();
                    }
                    catch
                    {
                        udpClient.Close();
                        parseBytes("failed", args2[0], port);
                    }
                }
                catch
                {
                    udpClient.Close();
                    parseBytes("failed", args2[0], port);
                }
            }
            catch
            {
            }
        }

        public void advancedQuery(object args)
        {
            string[] args2 = (args.ToString()).Split(':');
            int port = Convert.ToInt32(args2[1]);
            try
            {
                UdpClient udpClient = new UdpClient();
                try
                {
                    udpClient.Connect(args2[0], port);
                    var query = getstatus;
                    udpClient.Send(query, query.Length);
                    IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    udpClient.Client.ReceiveTimeout = 800;
                    try
                    {
                        Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
                        var returnData = Encoding.ASCII.GetString(receiveBytes);
                        parseBytes(returnData, args2[0], port);
                        udpClient.Close();
                    }
                    catch
                    {
                        udpClient.Close();
                        parseBytes("failed", args2[0], port);
                    }
                }
                catch
                {
                    udpClient.Close();
                    parseBytes("failed", args2[0], port);
                }
            }
            catch
            {
            }
        }

        ///////////////
        //displayshit//
        ///////////////
        public void displayIPs()
        {
            foreach (string ip in connectips)
            {
                ListViewItem lvItem = serverList.Items.Insert(0, ""); //insert blank here since we only want IPs
                lvItem.SubItems.Add(ip);    //Time to add IPs
                string[] data = ip.Split(':');
                lvItem.Name = data[0];
            }
            if (refreshcheckState)
                timer1.Enabled = true;
            else
                initQuerys();
        }

        public void setList(string name, string ip, string map, string players, string gametype , string mod, string status)
        {
            if (serverList.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(setList);
                this.Invoke(d, new object[] { name, ip, map, players, gametype, mod, status });
            }
            else
            {
                var founditem = serverList.Items.Find(ip, false);
                if (founditem.Length > 0 && founditem.Length < 2)
                {
                    if (founditem[0].SubItems.Count < 7)
                    {
                        founditem[0].SubItems[0].Text = name;
                        founditem[0].SubItems.Add(map);
                        founditem[0].SubItems.Add(players);
                        founditem[0].SubItems.Add(gametype);
                        if (mod != "" && mod != "--")
                            founditem[0].SubItems.Add(mod.Substring(5));
                        else if (mod == "--")
                            founditem[0].SubItems.Add(mod);
                        else
                            founditem[0].SubItems.Add("");
                        founditem[0].SubItems.Add("");
                        founditem[0].SubItems.Add(status);
                    }
                    else
                    {
                        founditem[0].SubItems[0].Text = name;
                        founditem[0].SubItems[2].Text = map;
                        founditem[0].SubItems[3].Text = players;
                        founditem[0].SubItems[4].Text = gametype;
                        if (mod != "")
                            founditem[0].SubItems[5].Text = mod.Substring(5);
                        else if (mod == "--")
                            founditem[0].SubItems[5].Text = mod;
                        else
                            founditem[0].SubItems[5].Text = "";
                        founditem[0].SubItems[7].Text = status;
                    }
                }
            }
        }
        public void setPing(string ip, int ping)
        {
            if (serverList.InvokeRequired)
            {
                setPingCallback d = new setPingCallback(setPing);
                this.Invoke(d, new object[] { ip, ping });
            }
            else
            {
                var founditem = serverList.Items.Find(ip, false);
                if (founditem.Length > 0 && founditem.Length < 2)
                {
                    if (founditem[0].SubItems.Count < 7)
                    {
                        founditem[0].SubItems.Add("");
                        founditem[0].SubItems.Add("");
                        founditem[0].SubItems.Add("");
                        founditem[0].SubItems.Add("");
                        founditem[0].SubItems.Add(ping.ToString());
                        founditem[0].SubItems.Add("");
                    }
                    else
                    {
                        founditem[0].SubItems[6].Text = ping.ToString();
                    }
                }
            }
        }

        public void displayPlayers(string ip)
        {
            playerList.Items.Clear();
            int tryparsecrap;
            ip = ip.Remove(ip.Length - 6);
            string[,] array = new string[18, 4];
            foreach (var pair in playerstats)
            {
                if (pair.Key == ip)
                {
                    array = pair.Value;
                    break;
                }
            }
            if (Int32.TryParse(array[0, 3], out tryparsecrap))
            {
                for (int i = 0; i < tryparsecrap; i++)
                {
                    ListViewItem lvItem = playerList.Items.Insert(0, array[i, 2]);
                    lvItem.SubItems.Add(array[i, 0]);
                    lvItem.SubItems.Add(array[i, 1]);
                }
            }
        }
        ///////////////////////
        //parsingresponseshit//
        //////////////////////
        public void parseBytes(string stringdata, string hostip, int port)
        {
            var strData = stringdata;
            var lines = strData.Substring(4).Split('\n');
            int numPlayers = 0;
            int multiArray = 0;
            string status;
            string[,] playerstat2 = new string[18, 4];
            if (lines[0].StartsWith("statusResponse"))
            {
                if (lines.Length >= 2)
                {
                    var dictionary = GetParams(lines[1].Split('\\'));

                    if (!dictionary.ContainsKey("fs_game"))
                    {
                        dictionary.Add("fs_game", "");
                    }
                    serverdvars.Add(hostip, dictionary);
                    foreach (var line in lines)
                    {
                        if (line.Contains("\""))
                        {
                            numPlayers++;
                        }
                    }
                    foreach (var line in lines)
                    {
                        if (line.Contains("\""))
                        {
                            char[] trimInt = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ' ' };
                            string[] playerstat = line.Split(' ');
                            playerstat[2] = line.TrimStart(trimInt);
                            playerstat[2] = playerstat[2].TrimStart('"');
                            playerstat[2] = playerstat[2].TrimEnd('"');
                            playerstat2[multiArray, 0] = playerstat[0];
                            playerstat2[multiArray, 1] = playerstat[1];
                            playerstat2[multiArray, 2] = playerstat[2];
                            playerstat2[multiArray, 3] = numPlayers.ToString();
                            multiArray++;
                        }
                    }

                    playerstats.Add(hostip, playerstat2);
                    if (numPlayers == Int32.Parse(getDictValue(dictionary, "sv_maxclients")))
                        status = "(Full)";
                    else
                        status = "(" + (Int32.Parse(getDictValue(dictionary, "sv_maxclients")) - numPlayers).ToString() + ")";
                    setList(removeQuakeColorCodes(getDictValue(dictionary, "sv_hostname")), hostip, getDictValue(dictionary, "mapname"), numPlayers.ToString() + "/" + getDictValue(dictionary, "sv_maxclients"), getDictValue(dictionary, "g_gametype"), getDictValue(dictionary, "fs_game"), "In Game" + status);
                    pingServer(hostip + ":" + port.ToString());
                }
            }
            else if (lines[0].StartsWith("0hpong"))
            {
                var data = lines[0].Split(' ');
                var ingame = (data[3] == "1");
                var players = int.Parse(data[4]);
                var maxPlayers = int.Parse(data[5]);
                var full = (players == maxPlayers);
                string fullStr;

                if (full)
                    fullStr = "(Full)";
                else
                    fullStr = "(" + (maxPlayers - players).ToString() + ")";

                if (ingame)
                {
                    object args = hostip + ":" + port.ToString();
                    advancedQuery(args);
                }
                else
                {
                    setList("--", hostip, "--", players.ToString() + "/" + maxPlayers.ToString(), "--", "--", "In Session" + fullStr);
                    pingServer(hostip + ":" + port.ToString());
                }
            }
            else if (lines[0].StartsWith("ed"))
            {
                setList("--", hostip, "--", "--", "--", "--", "Timeout");
                setPing(hostip, 1337);
            }
            else
            {
                setList("--", hostip, "--", "--", "--", "--", "Timeout");
                setPing(hostip, 1337);
            }
        }

        public void parsePing(string data, string hostip)
        {
            if (data.Substring(4).StartsWith("0hpong"))
            {
                var currenttick = Environment.TickCount;
                var strData = data.Split(' ');
                var tick = strData[2].TrimStart('[');
                tick = tick.TrimEnd(']');
                int tickInt = Int32.Parse(tick);
                int ping = currenttick - tickInt;
                setPing(hostip, ping);
            }
            else
            {
                setPing(hostip, 1337);
            }
        }

        //////////////
        //ping stuff//
        //////////////
        public void pingServer(string args)
        {
            string[] args2 = (args.ToString()).Split(':');
            int port = Convert.ToInt32(args2[1]);
            try
            {
                UdpClient udpClient = new UdpClient();
                try
                {
                    udpClient.Connect(args2[0], port);
                    var tickbyte = StrToByteArray(" [" + Environment.TickCount.ToString() + "]");
                    var query = new byte[hping.Length + tickbyte.Length];
                    Array.Copy(hping, 0, query, 0, hping.Length);
                    Array.Copy(tickbyte, 0, query, hping.Length, tickbyte.Length);
                    udpClient.Send(query, query.Length);
                    IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    udpClient.Client.ReceiveTimeout = 800;
                    try
                    {
                        Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
                        var returnData = Encoding.ASCII.GetString(receiveBytes);
                        parsePing(returnData, args2[0]);
                        udpClient.Close();
                    }
                    catch
                    {
                        udpClient.Close();
                    }
                }
                catch
                {
                    udpClient.Close();
                }
            }
            catch
            {
            }
        }

        ///////////////
        //checks/gets//
        ///////////////
        public void refreshCheck()
        {
            if (continuousRefresh.Checked)
            {
                refreshcheckState = true;
            }
            if (!continuousRefresh.Checked)
            {
                refreshcheckState = false;
            }
        }

        public void getURIrelated()
        {
            maxservercount = servernumBox.Text;
            filters = dvarfilterBox.Text;
        }

        public void clearVars()
        {
            refreshcheckState = false;
            maxservercount = "";
            filters = "";
            connectips.Clear();
            label5.Text = "0%";
            playerstats.Clear();
            serverdvars.Clear();
            serverList.Items.Clear();
            servercount = 0;
            selectedIP = "";
            lblServerCount.Text = "";
        }

        public void multiClear()
        {
            playerstats.Clear();
            serverdvars.Clear();
        }

        public string getDictValue(Dictionary<string, string> dict, string key)
        {
            foreach (var head in dict)
            {
                if (head.Key == key)
                {
                    return head.Value;
                }
                else
                    continue;
            }
            return "";
        }
        ///////////////////////////////////
        //code I don't understand but use//
        ///////////////////////////////////
        private static Dictionary<string, string> GetParams(string[] parts) //Thanks to NTAuthority
        {
            string key, val;    //create 2 strings
            var paras = new Dictionary<string, string>();   //create a dictionary

            for (int i = 0; i < parts.Length; i++)  //for all of the strings in the array
            {
                if (parts[i].Length == 0)   //if the array is empty
                {
                    continue;   //continue with the next one
                }

                key = parts[i++];   //uh, does it do i and then ++ or i++?
                val = parts[i]; //next one is the value

                paras[key] = val;
            }

            return paras;
        }

        //////////////
        //Other shit//
        //////////////
        public void initQuerys()
        {
            foreach (string ip in connectips)
            {
                object args;
                args = ip;
                Thread querythread = new Thread(new ParameterizedThreadStart(queryServers));
                querythread.Start(args);
            }
        }

        public static byte[] StrToByteArray(string str)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            return encoding.GetBytes(str);
        }

        public string removeQuakeColorCodes(string remove)
        {
            string filteredout = "";
            var array = remove.Split('^');
            filteredout += array[0];
            foreach (string part in array)
            {
                if (part.StartsWith("0") || part.StartsWith("1") || part.StartsWith("2") || part.StartsWith("3") || part.StartsWith("4") || part.StartsWith("5") || part.StartsWith("6") || part.StartsWith("7") || part.StartsWith("8") || part.StartsWith("9"))
                    filteredout += part.Substring(1);
                else
                    filteredout += part;
            }
            return filteredout;
        }
        ////////////////
        //Legacy stuff//
        ////////////////
        /*public void pingIP(string IP)
        {
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();
            AutoResetEvent waiter = new AutoResetEvent(false);
            pingSender.PingCompleted += new PingCompletedEventHandler(PingCompletedCallback);

            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 700;
            pingSender.SendAsync(IP, timeout, buffer, options, waiter);
        }

        private void PingCompletedCallback(object sender, PingCompletedEventArgs e)
        {
            PingReply reply = e.Reply;
            passArgsData(reply);
        }

        public void passArgsData(PingReply reply)
        {
            string[] data = new string[2] { reply.Address.ToString(), reply.RoundtripTime.ToString() };
            setPingData(data);
        }*/
        /*public void setPingData(string[] data)
        {
            if (serverList.InvokeRequired)
            {
                setDataListCallback d = new setDataListCallback(setPingData);
                Invoke(d, new object[] { data });
            }
            else
            {
                var pingedIP = serverList.Items.Find(data[0], true);
                if (pingedIP.Length > 0 && pingedIP.Length < 2)
                {
                    pingedIP[0].SubItems[2].Text = data[1];
                }
            }
        }*/
    }
}
