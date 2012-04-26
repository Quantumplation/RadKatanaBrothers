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
            sock.Connect("localhost", 9001);
            while (true)
            {
                byte[] buffer = new byte[1024];
                buffer[0] = 0;
                buffer[4] = 42;
                sock.Send(buffer);
                int counter = 0;
                while (true) 
                {
                    counter++;
                    Array.Clear(buffer, 0, 1024);
                    buffer[0] = 1;
                    Array.Copy(BitConverter.GetBytes(7), 0, buffer, 1, sizeof(int));
                    System.Text.UTF8Encoding encoding = new UTF8Encoding();
                    Array.Copy(encoding.GetBytes("player2"), 0, buffer, 1 + sizeof(int), 7);
                    Array.Copy(BitConverter.GetBytes(148.0), 0, buffer, 12, sizeof(double));
                    Array.Copy(BitConverter.GetBytes(72.0 + (counter/100000)), 0, buffer, 12 + sizeof(double), sizeof(double));
                    sock.Send(buffer);
                }
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
