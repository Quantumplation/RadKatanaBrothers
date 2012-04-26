using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sock.Connect("128.238.243.78", 9001);
            while (true)
            {
                byte[] buffer = new byte[1024];
                buffer[0] = 0;
                buffer[4] = 42;
                sock.Send(buffer);
                while (true) {}
                //sock.Receive(buffer, SocketFlags.None);
                //using (MemoryStream DataArray = new MemoryStream(buffer))
                //{
                //    BinaryFormatter DataSerializer = new BinaryFormatter();
                //    var obj = DataSerializer.Deserialize(DataArray);
                //    Console.WriteLine((int)obj);
                //}
            }
        }
    }
}
