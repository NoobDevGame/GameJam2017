using engenious;
using NoobFight.Contract.Simulation;
using NoobFight.Core.Entities;
using NoobFight.Core.Map;
using NoobFight.Core.Network;
using NoobFight.Core.Network.Messages;
using NoobFight.Screens;
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

        int frame = 0;

        public string Nick { get; set; }
        public string TextureName { get; set; }
        public int ConnectedPlayers { get; private set; }

        public NetworkComponent(NoobFight game) : base(game)
        {
            Game = game;
            client = new Client();
            messageHandler = new MessageHandler();
            worldLoaded = new AutoResetEvent(false);
            playerLoaded = new AutoResetEvent(false);

            messageHandler.RegisterMessageHandler<ConnectedPlayersResponseMessage>(ConnectedPlayersResp);
            messageHandler.RegisterMessageHandler<PlayerLoginResponseMessage>(PlayerLoginResponse);
            messageHandler.RegisterMessageHandler<PlayerJoinResponseMessage>(PlayerJoin);
            messageHandler.RegisterMessageHandler<EntityDataUpdateMessage>(UpdateEntity);
            messageHandler.RegisterMessageHandler<StartWorldMessage>(StartWorld);
            messageHandler.RegisterMessageHandler<WorldListResponseMessage>((c, e) =>
            {
                Game.ScreenManager.NavigateToScreen(new WorldSelectScreen(Game.ScreenManager, e.Worlds));
            });
            messageHandler.RegisterMessageHandler<CreateWorldResponseMessage>(CreateWorld);
            messageHandler.RegisterMessageHandler<WorldEventMessage>(WorldEvent);
            messageHandler.RegisterMessageHandler<PlayerLoginErrorMessage>(PlayerLoginError);
        }

        private void PlayerLoginError(Client client, PlayerLoginErrorMessage message)
        {
            client.Disconnect();
            Game.ScreenManager.NavigateToScreen(new ConnectingScreen(Game.ScreenManager, true, "Already a player with this name connected"));
        }

        private void WorldEvent(Client client, WorldEventMessage message)
        {
            var @event = message.WorldEvent;
            @event.Refill(Game.SimulationComponent.World);
            Game.SimulationComponent.World.Manipulator.AddServerEvent(@event);
        }

        public void StartWorld(Client client, StartWorldMessage message)
        {
            var map = new Map(message.MapName);
            map.Load();
            Game.SimulationComponent.World.Start(map);
            Game.ScreenManager.NavigateToScreen(new GameScreen(Game.ScreenManager));
        }
        public void StartWorld()
        {
            client.writeStream(new StartWorldRequest());
        }


        private void UpdateEntity(Client client, EntityDataUpdateMessage entitydata)
        {
            var player = Game.SimulationComponent.Simulation.Players.FirstOrDefault(i => i.PlayerID== entitydata.Id);
            if (player != null)
            {
                entitydata.UpdateEntity(player);

            }
        }

        public override void Update(GameTime gameTime)
        {
            if (client.Connected && Game.SimulationComponent.World != null && frame++ % 2 == 0)
            {

                client.writeStream(new EntityDataUpdateMessage(Game.SimulationComponent.Player));
            }
        }

        private void PlayerJoin(Client client, PlayerJoinResponseMessage message)
        {
            if (Game.SimulationComponent.Player == null)
                playerLoaded.WaitOne();
            if (Game.SimulationComponent.World == null)
                worldLoaded.WaitOne();

            if (Game.SimulationComponent.Player.PlayerID == message.Id)
            {
                Game.SimulationComponent.World.AddPlayer(Game.SimulationComponent.Player);
                Game.ScreenManager.NavigateToScreen(new LobbyScreen(Game.ScreenManager));
                return;
            }
            var player = new RemotePlayer(client, message.Id, message.Nick, message.TextureName);//TODO: texturename
            Game.SimulationComponent.Simulation.InsertPlayer(player);

            Game.SimulationComponent.World.AddPlayer(player);


        }

        private void ConnectedPlayersResp(Client arg1, ConnectedPlayersResponseMessage message)
        {
            ConnectedPlayers = message.Count;
        }

        private void PlayerLoginResponse(Client client, PlayerLoginResponseMessage message)
        {
            Game.SimulationComponent.Player = Game.SimulationComponent.Simulation.CreateLocalPlayer(message.PlayerId, Nick, TextureName);
            client.writeStream(new WorldListRequestMessage());
            playerLoaded.Set();
        }

        public void Connect(string host, int port, string nick, string textureName)
        {
            Game.SimulationComponent.CreateNetworkSimulation();

            client.Connect(host, port);
            client.OnMessageReceived += messageHandler.OnMessageReceived;
            client.BeginReceive();

            Nick = nick;
            TextureName = textureName;

            client.writeStream(new PlayerLoginRequestMessage(Nick, textureName));

        }

        public void JoinWorld(string worldName)
        {
            if (client.Connected == false)
                throw new NotSupportedException();
            Game.SimulationComponent.World = Game.SimulationComponent.Simulation.CreateNewWorld(GameMode.Timed, worldName);
            worldLoaded.Set();
            client.writeStream(new PlayerJoinRequestMessage(worldName));
        }
        public void CreateWorld(string name, GameMode mode)
        {
            client.writeStream(new CreateWorldRequestMessage(name, mode));
        }

        private void CreateWorld(Client client, CreateWorldResponseMessage message)
        {
            if (client.Connected == false)
                throw new NotSupportedException();
            Game.SimulationComponent.World = Game.SimulationComponent.Simulation.CreateNewWorld(message.Mode, message.WorldName);
            worldLoaded.Set();
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
