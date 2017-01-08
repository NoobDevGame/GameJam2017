using System;
using System.Linq;
using NoobFight.Contract.Simulation;

namespace NoobFight.Core.Simulation.Events
{
    public class PlayerWorldEvent : WorldEvent
    {
        public long PlayerId { get; set; }

        public PlayerEventMethod Method { get; set; }

        public override void Dispatch(IWorld world, ISimulation simulation)
        {
            var player = simulation.Players.First(i => i.PlayerID == 1);

            if (Method == PlayerEventMethod.Insert)
            {
                world.AddPlayer(player);

            }
            else if (Method == PlayerEventMethod.Remove)
            {
                world.RemovePlayer(player);
            }
        }
    }
}