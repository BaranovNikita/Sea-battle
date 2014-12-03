using System;
using System.Collections;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SeaBattle
{
    class Server
    {
        private static IPAddress _serverIp;//ip сервера
        public static Hashtable ClientsList = new Hashtable();// подключенные клиентыы
        private static bool _serverStarted; // проверка запущен ли сервер
        private static TcpListener _serverSocket; // сервер
        public static void Start(object o) //запуск сервера
        {
            var pb = (ServerSettings)o;
            var ips = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (var ip in ips.Where(ip => ip.ToString().Contains("192.168")))
            {
                _serverIp = ip;
            }
            pb.Invoke(pb.UpdateIp, _serverIp.ToString());
            _serverSocket = new TcpListener(_serverIp, 8888);
            _serverSocket.Start();
            _serverStarted = true;
            Console.WriteLine(@"Chat Server Started ....");
            while (_serverStarted)
            {
                try
                {
                    var clientSocket = _serverSocket.AcceptTcpClient();
                    var bytesFrom = new byte[4096];
                    var networkStream = clientSocket.GetStream();
                    networkStream.Read(bytesFrom, 0, bytesFrom.Length);
                    var dataFromClient = Encoding.UTF8.GetString(bytesFrom);
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$", StringComparison.Ordinal));
                    ClientsList.Add(dataFromClient, clientSocket);
                    Broadcast(dataFromClient + @" Joined ", dataFromClient, false);
                    Console.WriteLine(dataFromClient + @" Joined chat room ");
                    var client = new HandleClinet();
                    client.StartClient(clientSocket, dataFromClient, ClientsList);
                }
                catch (Exception)
                {
                }
            }
            // ReSharper disable once FunctionNeverReturns
        }

        public static void Stop()//остановкас сервера
        {
            _serverStarted = false;
            _serverSocket.Stop();
            Console.WriteLine(@" STOP");
        }
        public static bool IsStarted() // метод проверки запуска сервера
        {
            return _serverStarted;
        }

        public static void Broadcast(string msg, string uName, bool flag) // сообщения от клиентов
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
}
