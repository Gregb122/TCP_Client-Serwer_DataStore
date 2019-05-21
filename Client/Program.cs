using SharedData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpHelper.StartClient(1234);
            var user = new UserData("Janek", "a@a.pl", "Hello my sweet home");
            var xmlSerializer = new XmlSerializer(typeof(UserData));
            using (Stream writer = new MemoryStream())
            {
                xmlSerializer.Serialize(writer, user);
                writer.Position = 0;
                using (StreamReader reader = new StreamReader(writer))
                {
                    TcpHelper.SendData(System.Text.Encoding.Unicode.GetBytes(reader.ReadToEnd()));
                }
            }
            TcpHelper.StopClient();
            Console.ReadKey();
        }
    }
}
