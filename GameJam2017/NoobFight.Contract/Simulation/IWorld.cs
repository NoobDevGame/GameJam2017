using NoobFight.Contract.Entities;
using NoobFight.Contract.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Contract.Simulation
{
    public interface IWorld
    {
        IMap CurrentMap { get; }

        IEnumerable<IPlayer> Players { get; }

        GameMode Mode { get;  }

        WorldState State { get; }

        TimeSpan WorldTime { get; }

        IWorldManipulator Manipulator { get; }

        IWorldManipulator CreateNewManipulator();

        Action<IWorld, IWorldEvent> AddEventCallback { get; set; }

        string Name { get; }

        void Start(IMap map);

        void Pause();
        void Resume();

        void AddPlayer(IPlayer player);

        void RemovePlayer(IPlayer player);
        IPlayer FindPlayerById(long playerid);

        IEntity FindEntityById(int id);
    }
}
