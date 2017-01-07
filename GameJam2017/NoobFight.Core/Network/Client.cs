﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NoobFight.Core.Network
{
    public class Client
    {
        public NetworkStream Stream { get { return tcpClient.GetStream(); } }
        public int ID { get; private set; }
        public bool Connected => tcpClient.Connected;

        public delegate void MessageEventHandler(object sender, NetworkMessage message);
        public event MessageEventHandler OnMessageReceived;

        public delegate void ConnectionEventHandler(object sender, EventArgs args);
        public event ConnectionEventHandler OnConnected;
        public event ConnectionEventHandler OnDisconnected;

        private TcpClient tcpClient;
        private CancellationToken mainToken;

        public Client()
        {
            tcpClient = new TcpClient();
        }
        public Client(TcpClient tcpClient) : this()
        {
            this.tcpClient = tcpClient;
            mainToken = new CancellationToken();
        }
        public Client(TcpClient tcpClient, int id) : this(tcpClient)
        {
            ID = id;
        }

        public void Connect(string host, int port)
        {
            tcpClient.Connect(host, port);

            if (!Connected)
                throw new SocketException(500);

            OnConnected?.Invoke(this, new EventArgs());

            /*Task.Run(() =>
            {
                while (tcpClient.Connected)
                {
                    Thread.Sleep(1);
                }
                OnDisconnected?.Invoke(this, new EventArgs());
            });*/
        }

        public void Disconnect()
        {
            tcpClient.Close();
        }

        public async void BeginReceive()
        {
            var data = await readStream();
            var message = MessageManager.Deserialize(data);
            BeginReceive();
            OnMessageReceived?.Invoke(this, message);
        }

        private Task<byte[]> readStream() => Task.Run<byte[]>(() =>
        {
            try
            {
                using (var reader = new BinaryReader(Stream, Encoding.UTF8, true))
                    return reader.ReadBytes(reader.ReadInt32());
            }catch(Exception)
            {
                OnDisconnected?.Invoke(this, new EventArgs());
                return null;
            }
        }, mainToken);
        //TODO:
        public Task writeStream(NetworkMessage message) => Task.Run(() =>
        {
            using (var writer = new BinaryWriter(Stream, Encoding.UTF8, true))
            {
                var data = message.GetBytes();
                writer.Write(data.Length);
                writer.Write(data);
            }
        }, mainToken);
        
    }
}
