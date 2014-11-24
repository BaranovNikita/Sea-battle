using System;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Text;

namespace WindowsApplication2
{
    

    public partial class Form1 : Form
    {
        public delegate void updateTextBoxDelegate(String textBoxString); 
        public updateTextBoxDelegate updateTextBox;
        void updateTextBox1(string str) { textBox4.Text = str; } 
        public static IPAddress ip;
        readonly TcpClient _clientSocket = new TcpClient();
        NetworkStream _serverStream = default(NetworkStream);
        string _readData;
        public Form1()
        {
            InitializeComponent();
            updateTextBox = new updateTextBoxDelegate( updateTextBox1 );
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var outStream = System.Text.Encoding.UTF8.GetBytes(textBox3.Text + "$");
            _serverStream.Write(outStream, 0, outStream.Length);
            _serverStream.Flush();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            _readData = "Conected to Chat Server ...";
            Msg();
            try
            {
                ip = IPAddress.Parse(ipAddressControl1.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: "+ex);
                _readData = "ERROR";
                Msg();
                return;
            }
            try
            {
                _clientSocket.Connect(ip, 8888);
            }
            catch (Exception)
            {
                MessageBox.Show("Нет ответа от сервера");
                _readData = "ERROR";
                Msg();
                return;
            }
            _serverStream = _clientSocket.GetStream();
            textBox2.Text = textBox2.Text == "" ? "user" : textBox2.Text;
            var outStream = Encoding.UTF8.GetBytes(textBox2.Text + "$");
            _serverStream.Write(outStream, 0, outStream.Length);
            _serverStream.Flush();
            var ctThread = new Thread(GetMessage);
            ctThread.Start();
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
                Msg();
            }
            // ReSharper disable once FunctionNeverReturns
        }

        private void Msg()
        {
            if (InvokeRequired)
                Invoke(new MethodInvoker(Msg));
            else
                textBox1.Text = textBox1.Text + Environment.NewLine + @" >> " + _readData;
        }
        private delegate void UpdateStatusHandler(string msg);
        public void UpdateStatus(string str)
        {
            if (InvokeRequired)
                BeginInvoke(new UpdateStatusHandler(UpdateStatus), new object[] { str });
            else
            {
                textBox4.Text = str;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Thread serv = new Thread(Server.Start);
            serv.Start(this);
            button3.Enabled = false;
        }

    }
}