using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using Microsoft.Xna.Framework;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace RadKatanaBrothers
{
    public enum MessageType : byte
    {
        MazeSeed = 0,
        PlayerPosition
    }

    public class NetworkManager : Manager
    {
        Socket connection;
        List<NetworkRepresentation> reps;

        public override void AddRepresentation(Representation rep)
        {
            reps.Add(rep as NetworkRepresentation);
        }

        public NetworkManager()
        {
            reps = new List<NetworkRepresentation>();
        }

        public void Initialize()
        {
            updates = new Dictionary<string, Vector2>();
            if (SERVER)
            {
                updates["player2"] = new Vector2(72, 72);
                TcpListener listener = new TcpListener(IPAddress.Any, 9001);
                listener.Start();
                listener.BeginAcceptSocket(OnConnection, listener);
            }
            else
            {
                updates["player1"] = new Vector2(72, 72);
                Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sock.BeginConnect("localhost", 9001, OnConnected, sock);
            }
        }

        public override void Run(float elapsedMilliseconds)
        {
            if (connection == null)
                return;
            foreach (var netRep in this.reps)
            {
                netRep.Run(this);
            }
            //byte[] buffer = new byte[1024];
            //int offset = 0;
            //foreach (var item in _networkedItems)
            //{
            //    if (offset > 1024)
            //        break;
            //    foreach (var monitor in item.Monitors)
            //    {
            //        if (offset > 1024)
            //            break;
            //        Array.Clear(buffer, 0, 1024);
            //        buffer[offset] = (byte)MessageType.PropertyUpdate;
            //        offset += monitor.WriteTo(offset, ref buffer);
            //        connection.BeginSend(buffer, 0, offset, SocketFlags.None, null, null);
            //        offset = 0;
            //    }
            //    item.SetNeutral();
            //}
        }

        public const bool SERVER = true;

        public void UpdateProperty(string entityID, Vector2 position)
        {
            byte[] buffer = new byte[1024];
            buffer[0] = 1;
            Array.Copy(BitConverter.GetBytes(entityID.Length), 0, buffer, 1, sizeof(int));
            System.Text.UTF8Encoding encoding = new UTF8Encoding();
            Array.Copy(encoding.GetBytes(entityID), 0, buffer, 1 + sizeof(int), entityID.Length);
            Array.Copy(BitConverter.GetBytes((double)position.X), 0, buffer, 1 + sizeof(int) + entityID.Length, sizeof(double));
            Array.Copy(BitConverter.GetBytes((double)position.Y), 0, buffer, 1 + sizeof(int) + entityID.Length + sizeof(double), sizeof(double));
            connection.BeginSend(buffer, 0, 1024, SocketFlags.None, OnDataSent, null);
        }

        public void OnDataSent(IAsyncResult ar)
        {
            connection.EndSend(ar);
        }

        Dictionary<string, Vector2> updates;
        public bool HasProperty(string entityID)
        {
            return updates.ContainsKey(entityID);
        }

        public Vector2 ReadProperty(string entityID)
        {
            return updates[entityID];
        }

        public override void ClearRepresentations()
        {
        }

        public void OnConnection(IAsyncResult ar)
        {
            var listener = ar.AsyncState as TcpListener;
            connection = listener.EndAcceptSocket(ar);
            byte[] buffer = new byte[1024];
            Random rand = new Random();
            if (!mazeMade)
            {
                buffer[0] = 0;
                int seed = rand.Next();
                Array.Copy(BitConverter.GetBytes(seed), 0, buffer, 1, sizeof(int));
                World.LoadMaze(seed);
                connection.Send(buffer);
                Array.Clear(buffer, 0, 1024);
                mazeMade = true;
            }
            connection.BeginReceive(buffer, 0, 1024, SocketFlags.None, OnReceiveData, buffer);
        }

        public void OnConnected(IAsyncResult ar)
        {
            connection = ar.AsyncState as Socket;
            connection.EndConnect(ar);
            byte[] buffer = new byte[1024];
            connection.BeginReceive(buffer, 0, 1024, SocketFlags.None, OnReceiveData, buffer);
        }

        bool mazeMade = false;
        public void OnReceiveData(IAsyncResult ar)
        {
            connection.EndReceive(ar);
            byte[] buffer = ar.AsyncState as byte[];
            switch ((MessageType)buffer[0])
            {
                case MessageType.MazeSeed:
                    if (mazeMade)
                        break;
                    World.Running = false;
                    System.Threading.Thread.Sleep(5000);
                    World.LoadMaze(System.BitConverter.ToInt32(buffer, 1));
                    World.Running = true;
                    mazeMade = true;
                    break;
                case MessageType.PlayerPosition:
                    int size = BitConverter.ToInt32(buffer, 1);
                    System.Text.UTF8Encoding encoding = new UTF8Encoding();
                    string id = encoding.GetString(buffer, 5, size);
                    if (!string.IsNullOrEmpty(id))
                    {
                        double x = BitConverter.ToDouble(buffer, 5 + size);
                        double y = BitConverter.ToDouble(buffer, 5 + size + sizeof(double));
                        updates[id] = new Vector2((float)x, (float)y);
                    }
                    break;
            }
            Array.Clear(buffer, 0, 1024);
            connection.BeginReceive(buffer, 0, 1024, SocketFlags.None, OnReceiveData, buffer);
        }
    }
}
