


using System;
using System.Net;
using System.Net.Sockets;

namespace ServerCore
{
    class Listener
    {
        public Socket listenSocket;
        private Action<Socket> _onAcceptHandler;
        public void init(IPEndPoint endPoint,Action<Socket> onAcceptHandler)
        {
            listenSocket = new Socket(endPoint.AddressFamily,SocketType.Stream,ProtocolType.Tcp);
            _onAcceptHandler += onAcceptHandler;
            listenSocket.Bind(endPoint);

            listenSocket.Listen(10);

            SocketAsyncEventArgs args = new SocketAsyncEventArgs();
            args.Completed += new EventHandler<SocketAsyncEventArgs>(OnAcceptCompleted);
            RegisterAccept(args);
        }

        public void RegisterAccept(SocketAsyncEventArgs args)
        {
            args.AcceptSocket = null;
            
            bool pending = listenSocket.AcceptAsync(args);
            if (pending == false)
            {
                OnAcceptCompleted(null,args);
            }
            else
            {
                
            }
        }

        public void OnAcceptCompleted(object sender,SocketAsyncEventArgs args)
        {
            if (args.SocketError == SocketError.Success)
            {
                _onAcceptHandler.Invoke(args.AcceptSocket);
            }
            else
            {
                Console.WriteLine(args.SocketError.ToString());
            }
            
            RegisterAccept(args);
        }

        public Socket Accept()
        {
            
            return listenSocket.Accept();
        }
        
    }
}