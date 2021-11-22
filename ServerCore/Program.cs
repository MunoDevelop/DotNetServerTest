using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerCore
{
    internal class Program
    {
        private static Listener _listener = new Listener();

        static void OnAcceptHandler(Socket clientSocket)
        {
            try
            {
                byte[] recvBuff = new byte[1024];
                int recvBytes = clientSocket.Receive(recvBuff);
                string recvData = Encoding.UTF8.GetString(recvBuff, 0, recvBytes);
                
                Console.WriteLine($"[From Client]{recvData}");

                byte[] sendBuff = Encoding.UTF8.GetBytes("welcome to server");
                clientSocket.Send(sendBuff);
                
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
        
        public static void Main(string[] args)
        {
            string host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddress = ipHost.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddress, 7777);
            
            
            _listener.init(endPoint,OnAcceptHandler);
            Console.WriteLine("listening...");
            while (true)
            {
                

                //Socket clientSocket = _listener.Accept();

                
            }
     
            
            
            
        }
    }
}