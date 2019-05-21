using SharedData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Server
{
    class TcpHelper
    {
        private static TcpListener Listener { get; set; }
        private static bool Accept { get; set; } = false;

        public static void StartServer(int port)
        {
            IPAddress address = IPAddress.Loopback;
            Listener = new TcpListener(address, port);

            Listener.Start();
            Accept = true;

            Console.WriteLine($"Server started. Listening to TCP clients at 127.0.0.1:{port}");
        }

        public static void Listen()
        {
            if (Listener != null && Accept)
            {

                // Continue listening.  
                while (true)
                {
                    Console.WriteLine("Waiting for client...");
                    var clientTask = Listener.AcceptTcpClientAsync(); // Get the client  

                    if (clientTask.Result != null)
                    {
                        Console.WriteLine("Client connected. Waiting for data.");
                        var client = clientTask.Result;

                        byte[] buffer = new byte[1024];
                        client.GetStream().Read(buffer, 0, buffer.Length);
                        var data = CleanMessage(buffer);
                        Console.Write(data);

                        using (StreamWriter writer = new StreamWriter("XmlFile.xml", false))
                        {
                            writer.Write(data, 0, data.Length);
                        }

                        

                        

                        byte[] message = Encoding.ASCII.GetBytes("Saving your data...");
                        client.GetStream().Write(message, 0, message.Length);

                        Console.WriteLine("Closing connection.");
                        client.GetStream().Dispose();
                    }
                }
            }
        }

        private static string CleanMessage(byte[] bytes)
        {
            // Get the string of the message from bytes  
            string message = Encoding.ASCII.GetString(bytes);

            string messageToPrint = null;
            // Loop through each character in that message  
            foreach (var nullChar in message)
            {
                // Only store the characters, that are not null character  
                if (nullChar != '\0')
                {
                    messageToPrint += nullChar;
                }
            }

            // Return the message without null characters.   
            return messageToPrint;
        }
    }
}
