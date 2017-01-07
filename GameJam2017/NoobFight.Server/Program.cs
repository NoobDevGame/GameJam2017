using NoobFight.Core.Network.Messages;
using NoobFight.Core.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

            simulation = new Simulation();
            world = simulation.CreateNewWorld(GameMode.Timed);
            world.Start(MapGenerator.CreateMap());

            canceltoken = new CancellationTokenSource();
            thread = new Thread(UpdateSimulation);
            thread.IsBackground = true;
            thread.Start();

            server.RegisterMessageHandler<ConnectedPlayersRequestMessage>((c, m) => new ConnectedPlayersResponseMessage(1));
            server.RegisterMessageHandler<PlayerLoginRequestMessage>(PlayerLoginRequest);
            eventWait.WaitOne();

            canceltoken.Cancel();
        }

        private static void UpdateSimulation()
        {
            var token = canceltoken.Token;
            while (!token.IsCancellationRequested)
            {
                simulation.Update(new Contract.GameTime(TimeSpan.FromMilliseconds(13)));
                Thread.Sleep(13);
            }
        }

        private static void PlayerLoginRequest(Client client, PlayerLoginRequestMessage message)
        {
            //if (simulation.Players.FirstOrDefault(x=>x.Name == message.Nick) == null)
            {
                IPlayer player = new RemotePlayer(client,BitConverter.ToInt64(Guid.NewGuid().ToByteArray(),0), message.Nick, "");
                simulation.InsertPlayer(player);
                client.writeStreamAsync(new PlayerLoginResponseMessage(player.ID));
                Console.WriteLine($"New player {message.Nick} joined");
                server.SendBroadcast(new ConnectedPlayersResponseMessage(simulation.Players.Count()));
                //Thread.Sleep(1000);
                client.writeStreamAsync(new CreateWorldMessage());
                world.Manipulator.AddPlayer(player);
                //Thread.Sleep(1000);
                var joinMessage = new PlayerJoinMessage(player.ID, player.Name);
                client.writeStreamAsync(joinMessage);
                //Thread.Sleep(1000);
                foreach (var remotePlayer in world.Players.OfType<RemotePlayer>())
                {
                    remotePlayer.Client.writeStreamAsync(joinMessage);
                }
                
            }
        }
    }
}
