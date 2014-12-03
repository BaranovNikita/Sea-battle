using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeaBattle
{
    internal class Client
    {
        private static readonly TcpClient ClientSocket  = new TcpClient();
        static NetworkStream _serverStream = default(NetworkStream);

        public static void CreateClient(IPAddress ip)
        {
            try
            {
                ClientSocket.Connect(ip, 8888);
            }
            catch (Exception)
            {
                MessageBox.Show(@"Нет ответа от сервера");
                Console.WriteLine(@"ERROR");
                //ipServer.Enabled = true;
                //clientConnect.Enabled = true;
                return;
            }
            _serverStream = ClientSocket.GetStream();
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
        private static void GetMessage()
        {
            while (true)
            {
                _serverStream = ClientSocket.GetStream();
                var inStream = new byte[4096];
                _serverStream.Read(inStream, 0, inStream.Length);
                var returndata = Encoding.UTF8.GetString(inStream);
                Console.WriteLine(returndata);
            }
            // ReSharper disable once FunctionNeverReturns
        }

    }
}
