using NoobFight.Contract;
using NoobFight.Contract.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Core.Network.Messages
{
    public class EntityDataUpdateMessage : NetworkMessage
    {
        public override MessageType DataType => MessageType.EntityDataUpdate;

        public long ID { get; private set; }
        public Vector2 _position;
        public Vector2 _velocity;

        public EntityDataUpdateMessage()
        {

        }

        //TODO: Entity: ID
        public EntityDataUpdateMessage(IPlayer entity)
        {
            ID = entity.ID;
            _position = entity.Position;
            _velocity = entity.Velocity;
        }

        public override byte[] Serialize()
        {
            byte[] data = new byte[24];
            using (MemoryStream stream = new MemoryStream(data))
            using (BinaryWriter bw = new BinaryWriter(stream))
            {
                bw.Write(ID);
                bw.Write(_position.X);
                bw.Write(_position.Y);
                bw.Write(_velocity.X);
                bw.Write(_velocity.Y);
            }

            return data;
        }

        public override void Deserialize(byte[] payload)
        {
            ID = BitConverter.ToInt64(payload, 0);
            var x = BitConverter.ToSingle(payload, 8);
            var y = BitConverter.ToSingle(payload, 12);
            _position = new Vector2(x, y);

            x = BitConverter.ToSingle(payload, 16);
            y = BitConverter.ToSingle(payload, 20);
            _velocity = new Vector2(x, y);
        



        }

        public void UpdateEntity(IEntity entity)
        {
            entity.Position = _position;
            entity.Velocity = _velocity;
        }
    }
}
