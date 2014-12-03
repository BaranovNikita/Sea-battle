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
        public static IPAddress Ip;
        readonly TcpClient _clientSocket = new TcpClient();
        NetworkStream _serverStream = default(NetworkStream);
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
                return;
            }
            try
            {
                _clientSocket.Connect(Ip, 8888);
            }
            catch (Exception)
            {
                MessageBox.Show(@"Нет ответа от сервера");
                _readData = "ERROR";
                Console.WriteLine(_readData);
                ipServer.Enabled = true;
                clientConnect.Enabled = true;
                return;
            }
            _serverStream = _clientSocket.GetStream();
            var outStream = Encoding.UTF8.GetBytes("client$");
            _serverStream.Write(outStream, 0, outStream.Length);
            _serverStream.Flush();
            var ctThread = new Thread(GetMessage);
            ctThread.Start();
        }
        private void SendMessage(string message)
        {
            var outStream = Encoding.UTF8.GetBytes(message + "$");
            _serverStream.Write(outStream, 0, outStream.Length);
            _serverStream.Flush();
        }
        private void GetMessage()
        {
            while (true)
            {
                _serverStream = _clientSocket.GetStream();
                var inStream = new byte[4096];
                _serverStream.Read(inStream, 0, inStream.Length);
                var returndata = System.Text.Encoding.UTF8.GetString(inStream);
                _readData = "" + returndata;
                Console.WriteLine(_readData);
            }
            // ReSharper disable once FunctionNeverReturns
        }
    }
}
