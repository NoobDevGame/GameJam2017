using System;
using System.Linq;
using NoobFight.Contract.Simulation;

namespace NoobFight.Core.Simulation.Events
{
    public class PlayerWorldEvent : WorldEvent
    {
        public Guid PlayerID { get; set; }

        public PlayerEventMethod Method { get; set; }

        public override void Dispatch(World world, Simulation simulation)
        {
            var player = simulation.Players.First(i => i.ID == PlayerID);

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