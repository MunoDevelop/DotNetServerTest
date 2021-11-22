using System;
using System.Threading;

namespace DotNetServerTest
{
    internal class Program
    {
        static void MainThread(object state)
        {
            Console.WriteLine("Hello Thread!");
        }
        
        public static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(MainThread);
            // Thread t = new Thread(MainThread);
            // t.IsBackground = true;
            // t.Start();
            // t.Join();
        }
    }
}