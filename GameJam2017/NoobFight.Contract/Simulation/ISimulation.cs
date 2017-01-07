using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoobFight.Contract.Entities;

namespace NoobFight.Contract.Simulation
{
    public interface ISimulation
    {
        IEnumerable<IWorld> Worlds { get; }

        IEnumerable<IPlayer> Players { get;  }

        IWorld CreateNewWorld(GameMode mode);

        void Update(GameTime gameTime);

        IPlayer CreateLocalPlayer(string name);

        void InsertPlayer(IPlayer player);

        void RemovePlayer(IPlayer player);
    }
}
