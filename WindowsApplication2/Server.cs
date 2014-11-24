using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsApplication2
{
    class Server
    {
        public static IPAddress serverIp;
        public static Hashtable ClientsList = new Hashtable();
        public static void Start(object o)
        {
            var pb = (Form1)o;
            IPAddress myIp = IPAddress.Parse("127.0.0.1");
            var ips = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (var ip in ips)
            {
                if (ip.ToString().Contains("192.168"))
                    serverIp = ip;
            }
            pb.Invoke(pb.updateTextBox, serverIp.ToString()); 
            var serverSocket = new TcpListener(serverIp, 8888);
            serverSocket.Start();
            Console.WriteLine("Chat Server Started ....");
            while ((true))
            {
                var clientSocket = serverSocket.AcceptTcpClient();
                var bytesFrom = new byte[4096];
                var networkStream = clientSocket.GetStream();
                networkStream.Read(bytesFrom, 0, bytesFrom.Length);
                var dataFromClient = Encoding.UTF8.GetString(bytesFrom);
                dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$", StringComparison.Ordinal));
                ClientsList.Add(dataFromClient, clientSocket);
                Broadcast(dataFromClient + " Joined ", dataFromClient, false);
                Console.WriteLine(dataFromClient + " Joined chat room ");
                var client = new HandleClinet();
                client.StartClient(clientSocket, dataFromClient, ClientsList);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        public static void Broadcast(string msg, string uName, bool flag)
        {
            foreach (DictionaryEntry item in ClientsList)
            {
                var broadcastSocket = (TcpClient)item.Value;
                var broadcastStream = broadcastSocket.GetStream();
                var broadcastBytes = flag ? Encoding.UTF8.GetBytes(uName + " says : " + msg) : Encoding.UTF8.GetBytes(msg);
                broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
                broadcastStream.Flush();
            }
        }
    }
    public class HandleClinet
    {
        TcpClient _clientSocket;
        string _clNo;

        public void StartClient(TcpClient inClientSocket, string clineNo, Hashtable cList)
        {
            _clientSocket = inClientSocket;
            _clNo = clineNo;
            var ctThread = new Thread(DoChat);
            ctThread.Start();
        }
        private void DoChat()
        {
            var bytesFrom = new byte[4096];
            var requestCount = 0;
            while ((true))
            {
                try
                {
                    requestCount = requestCount + 1;
                    var networkStream = _clientSocket.GetStream();
                    networkStream.Read(bytesFrom, 0, bytesFrom.Length);
                    var dataFromClient = Encoding.UTF8.GetString(bytesFrom);
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$", StringComparison.Ordinal));
                    Console.WriteLine("From client - " + _clNo + " : " + dataFromClient);
                    Server.Broadcast(dataFromClient, _clNo, true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

            }
            // ReSharper disable once FunctionNeverReturns
        }
    }
}
