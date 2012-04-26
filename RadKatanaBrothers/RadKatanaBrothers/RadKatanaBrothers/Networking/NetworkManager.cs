using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace RadKatanaBrothers
{
    public enum MessageType : byte
    {
        MazeSeed,
        PropertyUpdate,
    }

    public class NetworkManager : Manager
    {
        Socket connection;
        List<NetworkRepresentation> _networkedItems;

        public override void AddRepresentation(Representation rep)
        {
            _networkedItems.Add(rep as NetworkRepresentation);
        }

        public NetworkManager()
        {
            _networkedItems = new List<NetworkRepresentation>();
        }

        public void Initialize()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 9001);
            listener.Start();
            listener.BeginAcceptSocket(OnConnection, listener);
        }

        public override void Run(float elapsedMilliseconds)
        {
            if (connection == null)
                return;
            byte[] buffer = new byte[1024];
            int offset = 0;
            foreach (var item in _networkedItems)
            {
                if (offset > 1024)
                    break;
                foreach (var monitor in item.Monitors)
                {
                    if (offset > 1024)
                        break;
                    offset += monitor.WriteTo(offset, ref buffer);
                }
                item.SetNeutral();
            }

            connection.BeginSend(buffer, 0, offset, SocketFlags.None, null, null);
        }

        public override void ClearRepresentations()
        {
            throw new NotImplementedException();
        }

        public void OnConnection(IAsyncResult ar)
        {
            var listener = ar.AsyncState as TcpListener;
            connection = listener.EndAcceptSocket(ar);
            byte[] buffer = new byte[1024];
            connection.BeginReceive(buffer, 0, 1024, SocketFlags.None, OnReceiveData, buffer);
        }

        public void OnReceiveData(IAsyncResult ar)
        {
            connection.EndReceive(ar);
            byte[] buffer = ar.AsyncState as byte[];
            switch ((MessageType)buffer[0])
            {
                case MessageType.MazeSeed:
                    break;
                case MessageType.PropertyUpdate:
                    break;
            }
            Array.Clear(buffer, 0, 1024);
            connection.BeginReceive(buffer, 0, 1024, SocketFlags.None, OnReceiveData, buffer);
        }
    }
}
