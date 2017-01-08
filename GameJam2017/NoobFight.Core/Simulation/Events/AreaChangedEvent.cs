using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoobFight.Contract.Simulation;
using NoobFight.Contract.Entities;
using System.IO;

namespace NoobFight.Core.Simulation.Events
{
    public class AreaChangedEvent : WorldEvent
    {
        private long _playerid;

        public IPlayer Player { get; private set; }
        public string DestinationArea { get; private set; }

        public override WorldEventType EventType => WorldEventType.AreaChange;

        public override SimulationMode SimulationMode => SimulationMode.Server;

        public override bool ShareMode => true;

        public AreaChangedEvent()
        {

        }
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
                if (Player.CurrentArea != null)
                {
                    Player.CurrentArea.RemoveEntity(Player);
                }

                
                Player.Position = area.SpawnPoint;
                area.AddEntity(Player);
            }
        }

        public override byte[] Serialize()
        {
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter bw = new BinaryWriter(ms))
            {
                bw.Write(Player.PlayerID);
                bw.Write(DestinationArea);
                return ms.ToArray();
            }
        }

        public override void Deserialize(byte[] payload)
        {
            using (MemoryStream ms = new MemoryStream(payload))
            using (BinaryReader br = new BinaryReader(ms))
            {
                _playerid = br.ReadInt64();
                DestinationArea = br.ReadString();
            }
        }

        public override void Refill(IWorld world)
        {
            Player = world.FindPlayerById(_playerid);
        }
    }
}
