using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using NoobFight.Core.Network;

namespace NoobFight.Server
{
    internal class Server
    {
        TcpListener listener;
        ConcurrentDictionary<int, Client> clients;

        public Server()
        {
            listener = new TcpListener(IPAddress.Any, 667);
            clients = new ConcurrentDictionary<int, Client>();
        }

        public void Start()
        {
            listener.Start();
            listener.BeginAcceptTcpClient(HandShake, null);
        }

        private void HandShake(IAsyncResult ar)
        {
            var tcpClient = listener.EndAcceptTcpClient(ar);
            listener.BeginAcceptTcpClient(HandShake, null);

            Task.Run(() =>
            {
                bool added = false;
                var client = new Client(tcpClient, clients.Count + 1);
                while (!added)
                    added = clients.TryAdd(client.ID, client);

                client.BeginReceived();
                client.OnMessageReceived += client_OnMessageReceived;
            });
        }

        private void client_OnMessageReceived(object sender, NetworkMessage message)
        {
            throw new NotImplementedException("Uuups I did it again >:" + message.ToString());
        }

    }
}
