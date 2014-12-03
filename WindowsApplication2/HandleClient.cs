using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeaBattle
{
    public class HandleClinet // клиент
    {
        TcpClient _clientSocket;
        string _clNo;

        
        public void StartClient(TcpClient inClientSocket, string clineNo, Hashtable cList) //запуск клиента
        {
            _clientSocket = inClientSocket;
            _clNo = clineNo;
            var ctThread = new Thread(DoChat);
            ctThread.Start();
        }
        private void DoChat() // отправка сообщения от клиента клиенту
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
                    Console.WriteLine(@"From client - " + _clNo + @" : " + dataFromClient);
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
