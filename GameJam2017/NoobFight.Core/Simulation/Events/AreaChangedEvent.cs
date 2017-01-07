using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoobFight.Contract.Simulation;
using NoobFight.Contract.Entities;

namespace NoobFight.Core.Simulation.Events
{
    public class AreaChangedEvent : WorldEvent
    {
        public IPlayer Player { get; private set; }
        public string DestinationArea { get; private set; }

        public AreaChangedEvent(IPlayer player, string destionationArea)
        {
            this.Player = player;
            this.DestinationArea = destionationArea;
        }

        public override void Dispatch(IWorld world, ISimulation simulation)
        {
            var area = world.CurrentMap.Areas.First(x => x.Name == this.DestinationArea);

            if (area != null)
            {
                Player.CurrentArea.RemoveEntity(Player);
                Player.Position = area.SpawnPoint;
                area.AddEntity(Player);
            }
        }
    }
}
