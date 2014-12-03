using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SeaBattle
{
    public partial class ServerSettings : Form
    {
        public IPAddress Ip;
        TcpClient _clientSocket = new TcpClient();
        
        string _readData;
        public static Thread Server;
        public delegate void UpdateIpDelegate(String textBoxString);
        public UpdateIpDelegate UpdateIp;
        void UpdateIpBox(string str) { ipServer.Text = str; }
        public ServerSettings()
        {
            InitializeComponent();
            UpdateIp = UpdateIpBox;
        }

        private void startServer_Click(object sender, EventArgs e)
        {
            Server = new Thread(SeaBattle.Server.Start);
            Server.Start(this);
            startServer.Enabled = false;
            clientRadio.Enabled = false;
            serverIp.Enabled = false;
            clientConnect.Enabled = false;
        }

        private void Radio_CheckedChanged(object sender, EventArgs e)
        {
            var button = (RadioButton) sender;
            if (button.Name == "clientRadio" && button.Checked)
            {
                startServer.Enabled = false;
                serverIp.Enabled = true;
                clientConnect.Enabled = true;
                if (SeaBattle.Server.IsStarted())
                {
                    ipServer.Text = "";
                    SeaBattle.Server.Stop();
                    Server.Join();
                }
            }
            if (button.Name == "serverRadio" && button.Checked)
            {
                startServer.Enabled = true;
            }
        }

        private void clientConnect_Click(object sender, EventArgs e)
        {
            clientConnect.Enabled = false;
            serverRadio.Enabled = false;
            _readData = "Conected to Chat Server ...";
            Console.WriteLine(_readData);
            try
            {
                Ip = IPAddress.Parse(serverIp.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Error: " + ex);
                _readData = "ERROR";
                Console.WriteLine(_readData);
             }
            Client.CreateClient(Ip);
        }
    }
}
