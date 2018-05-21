using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace LanChatServer
{
    public class MessageData
    {
        public IPEndPoint Client;

        public byte[] Data;

        public MessageData(IPEndPoint client, byte[] data)
        {
            Client = client;
            Data = data;
        }
    }
}
