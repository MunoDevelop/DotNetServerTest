using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DummyClient
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddress = ipHost.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddress, 7777);

            Socket socket = new Socket(endPoint.AddressFamily,SocketType.Stream,ProtocolType.Tcp);

            try
            {
                socket.Connect(endPoint);
                Console.WriteLine($"Connected to {socket.RemoteEndPoint.ToString()}");

                byte[] sendBuff = Encoding.UTF8.GetBytes("helloworld");
                int sendBytes = socket.Send(sendBuff);

                byte[] recvBuff = new byte[1024];
                int recvBytes = socket.Receive(recvBuff);

                string recvData = Encoding.UTF8.GetString(recvBuff, 0, recvBytes);
                Console.WriteLine($"From server: {recvData}");
            
                socket.Shutdown(SocketShutdown.Both);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
           
            
        }
    }
}