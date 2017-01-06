using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NoobFight.Server
{
    class Program
    {
        static Server server;
        static EventWaitHandle eventWait;

        static void Main(string[] args)
        {
            eventWait = new EventWaitHandle(false, EventResetMode.ManualReset);
            server = new Server();
            server.Start();
            eventWait.WaitOne();
        }
    }
}
