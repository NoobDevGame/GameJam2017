using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Core.Network
{
    class MessageManager
    {
        private static Dictionary<byte, Type> messageTypes = new Dictionary<byte, Type>();
        private static Dictionary<byte, Func<byte[], NetworkMessage>> messageConstructors = new Dictionary<byte, Func<byte[], NetworkMessage>>();
        static MessageManager()
        {

        }
        public static void RegisterMessage(byte id, Type type)
        {
            messageTypes.Add(id, type);
            var payloadParam = Expression.Parameter(typeof(byte[]), "payload");
            var constructor = type.GetConstructor(new Type[] { typeof(byte[]) });
            if (constructor == null)
                throw new ArgumentException("Not a valid network message type");
            var lambda = Expression.Lambda<Func<byte[], NetworkMessage>>(Expression.New(constructor, payloadParam), payloadParam);
            messageConstructors.Add(id, lambda.Compile());
        }
        public static NetworkMessage Deserialize(byte[] data)
        {
            if (data.Length == 1)
                return Deserialize(data[0], null);

            byte[] payload = new byte[data.Length - 1];
            Array.Copy(data, 1, payload, 0, data.Length - 1);
            return Deserialize(data[0], payload);
        }
        public static NetworkMessage Deserialize(byte dataType, byte[] payload)
        {
            Func<byte[], NetworkMessage> factory;
            if (!messageConstructors.TryGetValue(dataType, out factory))
                return null;

            var res = factory(payload);
            return res;
        }
    }
}
