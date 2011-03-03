using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;

namespace Server_Parser
{
    public partial class serverstatsForm : Form
    {
        public serverstatsForm()
        {
            InitializeComponent();
        }

        public static string ipport;

        public string getIP()
        {
            string queryIP = queryIPBox.Text;
            if (queryIP != "")
            {
                return queryIP;
            }
            else
            {
                return "127.0.0.1:28960";
            }
        }

        public static int called;

        public string cleanIP()
        {
            char[] numberTrim = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            char[] specialTrim = { ':', '.' };
            char[] ipTrim = { '.', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };

            string oriIP = getIP();

            string ipP = oriIP.TrimEnd(numberTrim);  //ping fails with ports
            string cleanIP = ipP.TrimEnd(specialTrim);   //pings fails with colon

            return cleanIP;
        }

        public int cleanPort()
        {
            try
            {
                string oriIP = getIP();
                char[] specialTrim = { ':', '.' };
                char[] ipTrim = { '.', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '-' };
                string port1 = oriIP.TrimStart(ipTrim); //trim away the IP/hostname
                string cleanPort = port1.TrimStart(specialTrim);    //trim away that colon
                int cleanPortInt = Convert.ToInt32(cleanPort);
                return cleanPortInt;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString() + "\n\n\nEXCEPTION THROWN, USING DEFAULT PORT 28960");
                return 28960;
            }
        }

        public void sendQuery(string hostip, int port)
        {
            try
            {
                UdpClient udpClient = new UdpClient();
                try
                {
                    udpClient.Connect(hostip, port);
                    //udpClient.Connect("79.118.172.136", 28960);

                    // Sends a message to the host to which you have connected.
                    var query = new byte[] { 0xFF, 0xFF, 0xFF, 0XFF, 0x67, 0x65, 0x74, 0x73, 0x74, 0x61, 0x74, 0x75, 0x73 };  //sends 4 OOB bytes + getstatus
                    //var query = new byte[] { 0xFF, 0xFF, 0xFF, 0XFF, 0x67, 0X65, 0X74, 0X69, 0X6e, 0x66, 0x6f };  //sends 4 OOB bytes + getinfo
                    //var query = new byte[] { 0xFF, 0xFF, 0xFF, 0XFF, 0x67, 0X65, 0X74, 0x73, 0x74, 0x61, 0x74 };

                    udpClient.Send(query, query.Length);

                    //IPEndPoint object will allow us to read datagrams sent from any source.
                    IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    udpClient.Client.ReceiveTimeout = 1500;
                    // Blocks until a message returns on this socket from a remote host.
                    try
                    {
                        Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
                        var returnData = Encoding.ASCII.GetString(receiveBytes);

                        // Uses the IPEndPoint object to determine which of these two hosts responded.

                        /*MessageBox.Show("This is the message you received " +
                                                     returnData.ToString());*/
                        /*MessageBox.Show("This message was sent from " +
                                                    RemoteIpEndPoint.Address.ToString() +
                                                    " on their port number " +
                                                    RemoteIpEndPoint.Port.ToString());*/
                        parseBytes(returnData);

                        udpClient.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        udpClient.Close();
                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    udpClient.Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            
        }

        public static Dictionary<string, string> paramDict;

        public void parseBytes(string data)
        {
            var strData = data;
            var lines = strData.Substring(4).Split('\n');
            if (lines.Length >= 2)
            {
                var dictionary = GetParams(lines[1].Split('\\'));

                if (!dictionary.ContainsKey("fs_game"))
                {
                    dictionary.Add("fs_game", "");
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
                        displayPlayerData(playerstat);
                        numPlayers++;
                    }
                }

                paramDict = dictionary;
                displayDictData();
            }
        }

        public void displayDictData()
        {
            Dictionary<string, string> data = paramDict;
            
            foreach (var pair in data)
            {
                if (pair.Key == "fs_game")
                {
                    char[] trimmod = { 'm', 'o', 'd', 's' };
                    var mod = pair.Value.ToString();
                    var modtr = mod.TrimStart(trimmod);
                    var modtrimed = modtr.TrimStart('/');
                    modBox.Text = modtrimed;
                }
                if (pair.Key == "g_gametype")
                {
                    gametypeBox.Text = pair.Value;
                }
                if (pair.Key == "sv_maxclients")
                {
                    playersOnlineBox.Text = numPlayers.ToString() + "/" + pair.Value;
                }
                if (pair.Key == "sv_hostname")
                {
                    serverNameBox.Text = pair.Value;
                }
                if (pair.Key == "mapname")
                {
                    mapBox.Text = pair.Value;
                }
            }
            data.Remove("fs_game");
            data.Remove("g_gametype");
            data.Remove("sv_maxclients");
            data.Remove("sv_hostname");
            data.Remove("mapname");

            ipBox.Text = getIP();
        }

        public void displayPlayerData(string[] playerstat)
        {
            ListViewItem lvItem = playerList.Items.Insert(0, playerstat[2]);
            lvItem.SubItems.Add(playerstat[0]);
            lvItem.SubItems.Add(playerstat[1]);

            //SHOULDIDO : Filter out/parse colour codes?
        }


        private static Dictionary<string, string> GetParams(string[] parts)
        {
            string key, val;
            var paras = new Dictionary<string, string>();

            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i].Length == 0)
                {
                    continue;
                }

                key = parts[i++];
                val = parts[i];

                paras[key] = val;
            }

            return paras;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            clearBoxes();
            sendQuery(cleanIP(), cleanPort());
        }

        public void clearBoxes()
        {
            playerList.Items.Clear();
            serverNameBox.Text = "";
            gametypeBox.Text = "";
            mapBox.Text = "";
            ipBox.Text = "";
            modBox.Text = "";
            playersOnlineBox.Text = "";
            numPlayers = 0;
            paramDict = null;
        }

        public static int numPlayers;

        private void serverCvar_Click(object sender, EventArgs e)
        {
            servercvar form = new servercvar();
            form.Show();
        }

        private void serverstatsForm_Load(object sender, EventArgs e)
        {
            called = 0;
            queryIPBox.Text = serverParserForm.selectedIP;
        }

        private void connectLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ipBox.Text != "")
            {
                Process.Start("aiw://connect/" + ipBox.Text);
            }
            else
            {
                MessageBox.Show("!!!\nWHY YOU TRY TO RUN WITHOUT TEH IP?!");
            }
        }

    }
}
