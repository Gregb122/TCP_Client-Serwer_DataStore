using System;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            // Start the server  
            TcpHelper.StartServer(1234);
            TcpHelper.Listen(); // Start listening.  
        }
    }
}
