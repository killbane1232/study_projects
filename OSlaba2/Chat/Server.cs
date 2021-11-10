using System;
using System.Diagnostics;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Chat
{
    class Server
    {
        private static int port = 8005;
        private static string address = "127.0.0.1";

        static void Main(string[] args)
        {
            var ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

            var listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                if (args.Length > 0 && args[0] == "client")
                {
                    listenSocket.Connect(ipPoint);
                    Console.Write("Введите сообщение:");
                    var message = Console.ReadLine();
                    var data = Encoding.Unicode.GetBytes(message);
                    listenSocket.Send(data);

                    data = new byte[256];
                    var builder = new StringBuilder();
                    var bytes = 0;

                    do
                    {
                        bytes = listenSocket.Receive(data, data.Length, 0);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    } while (listenSocket.Available > 0);

                    Console.WriteLine("ответ сервера: " + builder);
                    listenSocket.Shutdown(SocketShutdown.Both);
                    listenSocket.Close();
                }
                else
                {
                    var cnt = int.Parse(Console.ReadLine());
                    var handler = Task.Factory.StartNew(() => Handler());
                    for (var i = 0; i < cnt; i++)
                        Task.Factory.StartNew(() => Process.Start("Chat.exe", "client"));

                    handler.Wait();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void Handler()
        {
            var ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

            var listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listenSocket.Bind(ipPoint);

            listenSocket.Listen(10);
            Console.WriteLine("Сервер запущен. Ожидание подключений...");
            while (true)
            {
                var handler = listenSocket.Accept();

                var builder = new StringBuilder();
                var bytes = 0;
                var data = new byte[256];

                do
                {
                    bytes = handler.Receive(data);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (handler.Available > 0);

                Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + builder.ToString());
                
                var message = "ваше сообщение доставлено";
                data = Encoding.Unicode.GetBytes(message);
                handler.Send(data);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
        }
}
}