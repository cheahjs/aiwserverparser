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
    public partial class frmDedi : Form
    {
        public frmDedi()
        {
            InitializeComponent();

            //////////////////////////
            //Listview Sorting Stuff//
            //////////////////////////
            m_sortMgr = new ListViewSortManager(serverList2,
    new Type[] {
                    typeof(ListViewTextCaseInsensitiveSort),
                    typeof(ListViewTextCaseInsensitiveSort),
                    typeof(ListViewTextCaseInsensitiveSort),
                    typeof(ListViewTextCaseInsensitiveSort),
                    typeof(ListViewTextCaseInsensitiveSort),
                    typeof(ListViewTextCaseInsensitiveSort),
                    typeof(ListViewInt32Sort),
                    typeof(ListViewInt32Sort)
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
        public List<string> connectips = new List<string>();
        delegate void setDataListCallback(string[] args);
        delegate void SetTextCallback(string name, string ip, string map, string players, string gametype, string mod, string status);
        delegate void setPingCallback(string ip, int ping);
        public Dictionary<string, string[,]> playerstats = new Dictionary<string, string[,]>();
        public static Dictionary<string, Dictionary<string, string>> serverdvars = new Dictionary<string, Dictionary<string, string>>();
        char[] numbers = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
        char[] ipnumbers = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '.' };
        public byte[] hping = { 0xFF, 0xFF, 0xFF, 0XFF, 0x30, 0x68, 0x70, 0x69, 0x6e, 0x67 }; //sends 4 oob bytes + 0hping
        public byte[] getstatus = { 0xFF, 0xFF, 0xFF, 0XFF, 0x67, 0x65, 0x74, 0x73, 0x74, 0x61, 0x74, 0x75, 0x73 };  //sends 4 OOB bytes + getstatus
        public byte[] getinfo = { 0xFF, 0xFF, 0xFF, 0XFF, 0x67, 0x65, 0x74, 0x69, 0x6E, 0x66, 0x6F };  //sends 4 OOB bytes + getstatus
        public static string selectedIP;
        public int servercount;
        private ListViewSortManager m_sortMgr;
        public string masterresponsedata;

        //////////
        //events//
        //////////
        private void refreshButton_Click(object sender, EventArgs e)
        {
            clearVars();
            refreshCheck();
            label5.Text = "Querying master server";
            backgroundWorker1.RunWorkerAsync();
            refreshButton.Enabled = false;
            timer2.Enabled = true;
        }

        private void serverList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection server = this.serverList2.SelectedItems;
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
            Thread init = new Thread(initQuerys);
            init.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void btnSwitchDedi_Click(object sender, EventArgs e)
        {
            this.Hide();
            mainForm form = new mainForm();
            form.Show();
            mainForm.activated = true;
        }

        private void frmDedi_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        ////////////////////
        //background stuff//
        ////////////////////
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string address = "server.alteriw.net";
            int port = 20810;
            var getserverbyte = System.Text.Encoding.ASCII.GetBytes("getservers IW4 142");
            byte[] oob = { 0xFF, 0xFF, 0xFF, 0XFF };

            try
            {
                UdpClient udpClient = new UdpClient();
                try
                {
                    udpClient.Connect(address, port);
                    var query = new byte[oob.Length + getserverbyte.Length];
                    Array.Copy(oob, 0, query, 0, oob.Length);
                    Array.Copy(getserverbyte, 0, query, oob.Length, getserverbyte.Length);
                    udpClient.Send(query, query.Length);
                    IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    udpClient.Client.ReceiveTimeout = 800;
                    try
                    {
                        Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
                        var returnData = Encoding.ASCII.GetString(receiveBytes);
                        udpClient.Close();
                        masterresponsedata = returnData;
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

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            parseBytes2(masterresponsedata);
            label5.Text = "Done";
            refreshButton.Enabled = true;
        }
        //////////////
        //querystuff//
        //////////////
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
                    udpClient.Client.ReceiveTimeout = 2000;
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
        public void backupQuery(object args)
        {
            string[] args2 = (args.ToString()).Split(':');
            int port = Convert.ToInt32(args2[1]);
            try
            {
                UdpClient udpClient = new UdpClient();
                try
                {
                    udpClient.Connect(args2[0], port);
                    var query = getinfo;
                    udpClient.Send(query, query.Length);
                    IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    udpClient.Client.ReceiveTimeout = 2000;
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
                        parseBytes("faile2d2", args2[0], port);
                    }
                }
                catch
                {
                    udpClient.Close();
                    parseBytes("faile2d2", args2[0], port);
                }
            }
            catch
            {
            }
        }
        public void lastEffortQuery(object args)
        {
            string[] args2 = (args.ToString()).Split(':');
            int port = Convert.ToInt32(args2[1]);
            try
            {
                UdpClient udpClient = new UdpClient();
                try
                {
                    udpClient.Connect(args2[0], port);
                    var query = hping;
                    udpClient.Send(query, query.Length);
                    IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    udpClient.Client.ReceiveTimeout = 2000;
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
                        parseBytes("faile123123d2", args2[0], port);
                    }
                }
                catch
                {
                    udpClient.Close();
                    parseBytes("faile123123d2", args2[0], port);
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
                ListViewItem lvItem = serverList2.Items.Insert(0, ""); //insert blank here since we only want IPs
                lvItem.SubItems.Add(ip);    //Time to add IPs
                string[] data = ip.Split(':');
                lvItem.Name = data[0];
            }
            if (refreshcheckState)
            {
                timer1.Enabled = true;
            }
            else
            {
                Thread init = new Thread(initQuerys);
                init.Start();
            }
        }

        public void setList(string name, string ip, string map, string players, string gametype, string mod, string status)
        {
            if (serverList2.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(setList);
                this.Invoke(d, new object[] { name, ip, map, players, gametype, mod, status });
            }
            else
            {
                try
                {
                    var founditem = serverList2.Items.Find(ip, false);
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
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
        }
        public void setPing(string ip, int ping)
        {
            if (serverList2.InvokeRequired)
            {
                setPingCallback d = new setPingCallback(setPing);
                this.Invoke(d, new object[] { ip, ping });
            }
            else
            {
                var founditem = serverList2.Items.Find(ip, false);
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
                    serverdvars.Remove(hostip);
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
                    playerstats.Remove(hostip);
                    playerstats.Add(hostip, playerstat2);
                    status = (Int32.Parse(getDictValue(dictionary, "sv_maxclients")) - numPlayers).ToString();
                    setList(removeQuakeColorCodes(getDictValue(dictionary, "sv_hostname")), hostip, getDictValue(dictionary, "mapname"), numPlayers.ToString() + "/" + getDictValue(dictionary, "sv_maxclients"), getDictValue(dictionary, "g_gametype"), getDictValue(dictionary, "fs_game"), status);
                    pingServer(hostip + ":" + port.ToString());
                }
            }
            else if (lines[0].StartsWith("infoResponse"))
            {
                if (lines.Length >= 2)
                {
                    try
                    {
                        var dictionary = GetParams(lines[1].Split('\\'));
                        serverdvars.Remove(hostip);
                        serverdvars.Add(hostip, dictionary);
                        playerstat2[0, 0] = "0";
                        playerstat2[0, 1] = "0";
                        playerstat2[0, 2] = "No Data";
                        playerstat2[0, 3] = getDictValue(dictionary, "clients");
                        playerstats.Remove(hostip);
                        playerstats.Add(hostip, playerstat2);
                        status = (Int32.Parse(getDictValue(dictionary, "sv_maxclients")) - Int32.Parse(getDictValue(dictionary, "clients"))).ToString();
                        //setList(getDictValue(dictionary, "sv_hostname"), hostip, getDictValue(dictionary, "mapname"), getDictValue(dictionary, "clients") + "/" + getDictValue(dictionary, "sv_maxclients"), getDictValue(dictionary, "gametype"), "???", status);
                        setList("hi", "hi", "hi", "hi", "hi", "hi", "hi");
                        pingServer(hostip + ":" + port.ToString());
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.ToString());
                    }
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
                    fullStr = "0";
                else
                    fullStr = (maxPlayers - players).ToString();

                setList("--", hostip, "--", players.ToString() + "/" + maxPlayers.ToString(), "--", "--", fullStr);
                pingServer(hostip + ":" + port.ToString());
            }
            else if (lines[0].StartsWith("ed"))
            {
                backupQuery(hostip + ":" + port.ToString());
            }
            else if (lines[0].StartsWith("e2d"))
            {
                lastEffortQuery(hostip + ":" + port.ToString());
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

        private void parseBytes2(string data)
        {
            var strData = data.Substring(4).Split('\\');
            for (int i = 0; i < strData.Length; i++)
            {
                if (strData[i].Contains("serverresponse"))
                    continue;
                else if (strData[i].Contains("EOT"))
                    break;
                else
                {
                    string[] iparray = new string[100];
                    int arraycount = 0;
                    if (strData[i] != "")
                    {
                        if (strData[i].Length == 6)
                        {
                            foreach (char alpha in strData[i])
                            {
                                iparray[arraycount] = ((int)alpha).ToString();
                                arraycount++;
                            }
                            connectips.Add(iparray[0] + "." + iparray[1] + "." + iparray[2] + "." + iparray[3] + ":" + (256 * Convert.ToInt32(iparray[4]) + Convert.ToInt32(iparray[5])).ToString());                        
                        }
                    }
                    if (strData[i] == "")
                        strData[i + 1] = "\\" + strData[i + 1];
                }
            }
            displayIPs();
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

        public void clearVars()
        {
            refreshcheckState = false;
            connectips.Clear();
            playerstats.Clear();
            serverdvars.Clear();
            serverList2.Items.Clear();
            servercount = 0;
            selectedIP = "";
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
                object args = ip;
                Thread querythread = new Thread(new ParameterizedThreadStart(advancedQuery));
                querythread.Start(args);
            }
        }
        public static byte[] StrToByteArray(string str)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            return encoding.GetBytes(str);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            foreach (ListViewItem item in serverList2.Items)        
            {
                if (item.SubItems[0].Text == "")
                    item.Remove();
                else
                    continue;
            }
            timer2.Enabled = false;
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
    }
}
