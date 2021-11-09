using System;
using System.Net;
using System.Net.Sockets;

namespace Chat
{
    class Program
    {
        static int port = 8005;
        static void Main(string[] args)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
            Console.WriteLine("Hello World!");
        }
    }
}
