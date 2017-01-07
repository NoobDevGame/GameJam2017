﻿using engenious;
using NoobFight.Contract.Simulation;
using NoobFight.Core.Entities;
using NoobFight.Core.Map;
using NoobFight.Core.Network;
using NoobFight.Core.Network.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NoobFight.Components
{
    public class NetworkComponent : GameComponent
    {
        public new NoobFight Game { get; private set; }

        private Client client;

        private MessageHandler messageHandler;
        private AutoResetEvent worldLoaded, playerLoaded;

        public NetworkComponent(NoobFight game) : base(game)
        {
            Game = game;
            client = new Client();
            messageHandler = new MessageHandler();
            worldLoaded = new AutoResetEvent(false);
            playerLoaded = new AutoResetEvent(false);

            messageHandler.RegisterMessageHandler<PongMessage>((client, message) => Console.WriteLine("PONG"));
            messageHandler.RegisterMessageHandler<ConnectedPlayersResponseMessage>(ConnectedPlayersResp);
            messageHandler.RegisterMessageHandler<PlayerLoginResponseMessage>(PlayerLoginResponse);
            messageHandler.RegisterMessageHandler<PlayerJoinMessage>(PlayerJoin);
            messageHandler.RegisterMessageHandler<CreateWorldMessage>(CreateWorld);
        }

        private void CreateWorld(Client client, CreateWorldMessage message)
        {
            Game.SimulationComponent.World = Game.SimulationComponent.Simulation.CreateNewWorld(GameMode.Timed);
            Game.SimulationComponent.World.Start(MapGenerator.CreateMap());
            worldLoaded.Set();
        }
        private void PlayerJoin(Client client, PlayerJoinMessage message)
        {
            if (Game.SimulationComponent.Player == null)
                playerLoaded.WaitOne();
            if (Game.SimulationComponent.World == null)
                worldLoaded.WaitOne();
            if (Game.SimulationComponent.Player.ID == message.Id)
            {
                Game.SimulationComponent.World.Manipulator.AddPlayer(Game.SimulationComponent.Player);
                Game.ScreenManager.NavigateToScreen(new Screens.GameScreen(Game.ScreenManager));
                return;
            }
            var player = new RemotePlayer(client,message.Id,message.Nick,"monkey");//TODO: texturename
            Game.SimulationComponent.Simulation.InsertPlayer(player);

            Game.SimulationComponent.World.Manipulator.AddPlayer(player);


        }

        private void ConnectedPlayersResp(Client arg1, ConnectedPlayersResponseMessage message)
        {
            Debug.WriteLine($"{message.Count} players online");
        }

        public string Nick { get; set; }
        public string TextureName { get; set; }
        private void PlayerLoginResponse(Client client, PlayerLoginResponseMessage message)
        {
            Game.SimulationComponent.Player = Game.SimulationComponent.Simulation.CreateLocalPlayer(message.PlayerId, Nick, TextureName);
            playerLoaded.Set();
        }

        public void Connect(string host, int port,string nick,string textureName)
        {
            client.Connect(host, port);
            client.OnMessageReceived += messageHandler.OnMessageReceived;
            client.BeginReceive();

            Nick = nick;
            TextureName = textureName;

            client.writeStream(new PlayerLoginRequestMessage(nick));
        }

        public void Disconnect()
        {
            client.Disconnect();
        }

        /*
        public void SendMessage(NetworkMessage message)
        {
            client.writeStream(message);
        }
        */

    }
}
