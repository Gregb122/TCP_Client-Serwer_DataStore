using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class TcpHelper
    {
        private static TcpClient Client { get; set; }
        private static bool Accept { get; set; } = false;

        public static void StartClient(int port)
        {
            try
            {
                Client = new System.Net.Sockets.TcpClient("127.0.0.1", port);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Accept = false;
            }
            Accept = true;

        }
        public static byte[] SendData(byte[] messageBytes)
        {
            if (Client == null && !Accept)
                return messageBytes;

            const int bytesize = 1024 * 1024;
            try // Try connecting and send the message bytes  
            {
                NetworkStream stream = Client.GetStream();

                stream.Write(messageBytes, 0, messageBytes.Length); // Write the bytes  
                Console.WriteLine("Connected to the server");
                Console.WriteLine("Waiting for response...");

                messageBytes = new byte[bytesize]; // Clear the message   

                // Receive the stream of bytes  
                stream.Read(messageBytes, 0, messageBytes.Length);
                Console.WriteLine(CleanMessage(messageBytes));
                // Clean up  
                stream.Dispose();
            }
            catch (Exception e) // Catch exceptions  
            {
                Console.WriteLine(e.Message);
            }

            return messageBytes; // Return response  
        }

        public static void StopClient()
        {
            try
            {
                Client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
