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
            world = simulation.CreateNewWorld(GameMode.Timed,"Tolle Welt");

            canceltoken = new CancellationTokenSource();
            thread = new Thread(UpdateSimulation);
            thread.IsBackground = true;
            thread.Start();

            server.RegisterMessageHandler<ConnectedPlayersRequestMessage>(GetConnectedPlayer);
            server.RegisterMessageHandler<PlayerLoginRequestMessage>(PlayerLoginRequest);
            server.RegisterMessageHandler<EntityDataUpdateMessage>(EntityUpdated);
            server.RegisterMessageHandler<WorldListRequestMessage>(GetWorldList);
            server.RegisterMessageHandler<PlayerJoinRequestMessage>(PlayerJoinRequest);
            eventWait.WaitOne();

            canceltoken.Cancel();
        }

        private static void PlayerJoinRequest(Client client, PlayerJoinRequestMessage message)
        {
            var world = simulation.Worlds.FirstOrDefault(i => i.Name == message.WorldName);
                
            if (world == null || world.State == (WorldState.Paused | WorldState.Running ))
            {
                client.writeStream(new PlayerNotJoinResponseMessage());
                return;
            }

           

            var player = simulation.Players.First(i => i.ID == client.ID);
            var joinmessage = new PlayerJoinResponseMessage(client.ID, player.Name);

            

            client.writeStream(joinmessage);

            foreach (var oPlayer in world.Players.OfType<RemotePlayer>())
            {
                client.writeStream(new PlayerJoinResponseMessage(oPlayer.ID, oPlayer.Name));
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
            var entity = simulation.Players.First(i => i.ID == entitydata.ID);
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
                        

                        if (world.State != WorldState.Running && world.State  != WorldState.Paused)
                        {
                            if (world.Players.Count() == 2)
                            {
                                var mapname = "hallo";
                                world.Start(MapGenerator.CreateMap(mapname));

                                foreach (var player in world.Players.OfType<RemotePlayer>())
                                {
                                    player.Client.writeStream(new StartWorldMessage(mapname));

                                }
                            }

                            continue;
                        }

                        List<EntityDataUpdateMessage> updates = new List<EntityDataUpdateMessage>();
                        foreach (var player in world.Players.OfType<RemotePlayer>())
                            updates.Add(new EntityDataUpdateMessage(player));

                        foreach (var player in world.Players.OfType<RemotePlayer>())
                        {
                            foreach (var update in updates)
                            {
                                if (update.ID == player.ID)
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
            //if (simulation.Players.FirstOrDefault(x=>x.Name == message.Nick) == null)
            {
                IPlayer player = new RemotePlayer(client,client.ID, message.Nick, "");
                simulation.InsertPlayer(player);
                client.writeStream(new PlayerLoginResponseMessage(player.ID));
                Console.WriteLine($"New player {message.Nick} joined");
                server.SendBroadcast(new ConnectedPlayersResponseMessage(simulation.Players.Count()));  
            }
        }
    }
}
