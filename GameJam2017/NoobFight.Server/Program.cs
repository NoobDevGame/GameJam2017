using NoobFight.Core.Network.Messages;
using NoobFight.Core.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NoobFight.Contract.Simulation;
using NoobFight.Core.Network;
using NoobFight.Contract.Entities;
using NoobFight.Contract.Simulation;
using NoobFight.Core.Map;
using NoobFight.Core.Entities;

namespace NoobFight.Server
{
    class Program
    {
        static Server server;
        static EventWaitHandle eventWait;
        static Simulation simulation;
        static IWorld world;
        static Thread thread;
        static CancellationTokenSource canceltoken;

        static void Main(string[] args)
        {
            eventWait = new EventWaitHandle(false, EventResetMode.ManualReset);
            server = new Server();
            server.Start();

            simulation = new Simulation(SimulationMode.Server);

            canceltoken = new CancellationTokenSource();
            thread = new Thread(UpdateSimulation);
            thread.IsBackground = true;
            thread.Start();

            server.RegisterMessageHandler<ConnectedPlayersRequestMessage>(GetConnectedPlayer);
            server.RegisterMessageHandler<PlayerLoginRequestMessage>(PlayerLoginRequest);
            server.RegisterMessageHandler<EntityDataUpdateMessage>(EntityUpdated);
            server.RegisterMessageHandler<WorldListRequestMessage>(GetWorldList);
            server.RegisterMessageHandler<PlayerJoinRequestMessage>(PlayerJoinRequest);
            server.RegisterMessageHandler<CreateWorldRequestMessage>(CreateWorld);
            server.RegisterMessageHandler<StartWorldRequest>(StartWorld);
            eventWait.WaitOne();

            canceltoken.Cancel();
        }

        private static void StartWorld(Client client, StartWorldRequest message)
        {
            var world = simulation.Worlds.FirstOrDefault(i => i.Players.Count() > 0 && i.Players.Any(p => p.PlayerID == client.ID));

            if (world != null)
            {
                var mapstart = new StartWorldMessage("Hallo");
                var map = new Map(mapstart.MapName);
                map.Load();
                world.Start(map);

                foreach (var player in world.Players.OfType<RemotePlayer>())
                {
                    player.Client.writeStream(mapstart);
                }
            }
        }

        private static void CreateWorld(Client client, CreateWorldRequestMessage message)
        {
            var world = simulation.CreateNewWorld(message.Mode, message.WorldName);
            world.AddEventCallback = SendEvent;

            var player = simulation.Players.First(i => i.PlayerID == client.ID);
            world.AddPlayer(player);

            var joinmessage = new PlayerJoinResponseMessage(client.ID, player.Name, player.TextureName);

            Console.WriteLine("Erstelle neue Welt:{0}", message.WorldName);

            client.writeStream(new CreateWorldResponseMessage(message));
            client.writeStream(joinmessage);

            server.SendBroadcast(new NewWorldBroadcast(message.WorldName));
        }

        private static void SendEvent(IWorld world, IWorldEvent @event)
        {
            foreach (var player in world.Players.OfType<RemotePlayer>())
            {
                player.Client.writeStream(new WorldEventMessage(@event));
            }
        }

        private static void PlayerJoinRequest(Client client, PlayerJoinRequestMessage message)
        {
            var world = simulation.Worlds.FirstOrDefault(i => i.Name == message.WorldName);

            if (world == null || world.State == (WorldState.Paused | WorldState.Running))
            {
                client.writeStream(new PlayerNotJoinResponseMessage());
                return;
            }



            var player = simulation.Players.First(i => i.PlayerID == client.ID);
            var joinmessage = new PlayerJoinResponseMessage(client.ID, player.Name, player.TextureName);



            client.writeStream(joinmessage);

            foreach (var oPlayer in world.Players.OfType<RemotePlayer>())
            {
                client.writeStream(new PlayerJoinResponseMessage(oPlayer.PlayerID, oPlayer.Name, oPlayer.TextureName));
                oPlayer.Client.writeStream(joinmessage);
            }

            world.AddPlayer(player);

        }

        private static void GetConnectedPlayer(Client client, ConnectedPlayersRequestMessage message)
        {
            client.writeStream(new ConnectedPlayersResponseMessage(simulation.Players.Count()));
        }

        private static void GetWorldList(Client client, WorldListRequestMessage message)
        {
            client.writeStream(new WorldListResponseMessage(simulation.Worlds.Select(t => t.Name).ToArray()));
        }

        private static void EntityUpdated(Client client, EntityDataUpdateMessage entitydata)
        {
            var entity = simulation.Players.First(i => i.PlayerID == entitydata.Id);
            entitydata.UpdateEntity(entity);
        }

        private static void UpdateSimulation()
        {
            int frame = 0;

            var token = canceltoken.Token;
            while (!token.IsCancellationRequested)
            {
                simulation.Update(new Contract.GameTime(TimeSpan.FromMilliseconds(13)));

                if (frame++ % 2 == 0)
                {
                    foreach (var world in simulation.Worlds)
                    {

                        List<EntityDataUpdateMessage> updates = new List<EntityDataUpdateMessage>();
                        foreach (var player in world.Players.OfType<RemotePlayer>())
                            updates.Add(new EntityDataUpdateMessage(player));

                        foreach (var player in world.Players.OfType<RemotePlayer>())
                        {
                            foreach (var update in updates)
                            {
                                if (update.Id == player.PlayerID)
                                    continue;

                                player.Client.writeStream(update);
                            }
                        }
                    }


                }

                Thread.Sleep(13);
            }
        }

        private static void PlayerLoginRequest(Client client, PlayerLoginRequestMessage message)
        {
            if (simulation.Players.FirstOrDefault(x=>x.Name == message.Nick) == null)
            {
                IPlayer player = new RemotePlayer(client, client.ID, message.Nick, message.TextureName);
                simulation.InsertPlayer(player);
                client.writeStream(new PlayerLoginResponseMessage(player.PlayerID));
                Console.WriteLine($"New player {message.Nick} joined");
                server.SendBroadcast(new ConnectedPlayersResponseMessage(simulation.Players.Count()));
            }
            else
            {
                client.writeStream(new PlayerLoginErrorMessage());
            }
        }
    }
}
