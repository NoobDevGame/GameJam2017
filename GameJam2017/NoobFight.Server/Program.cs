using NoobFight.Core.Network.Messages;
using NoobFight.Core.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NoobFight.Contract.Simulation;

namespace NoobFight.Server
{
    class Program
    {
        static Server server;
        static EventWaitHandle eventWait;
        static Simulation simulation;

        static void Main(string[] args)
        {
            eventWait = new EventWaitHandle(false, EventResetMode.ManualReset);
            server = new Server();
            server.Start();

            simulation = new Simulation(SimulationMode.Server);

            server.MessageHandler.RegisterMessageHandler<ConnectedPlayersRequestMessage>((c, m) => new ConnectedPlayersResponseMessage(1));

            eventWait.WaitOne();
        }
    }
}
