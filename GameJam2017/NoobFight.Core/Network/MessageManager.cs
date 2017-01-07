using NoobFight.Core.Network.Messages;
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
        private static Dictionary<byte, Func<NetworkMessage>> messageConstructors = new Dictionary<byte, Func<NetworkMessage>>();
        static MessageManager()
        {
            RegisterMessage<PingMessage>();
            RegisterMessage<PongMessage>();
            RegisterMessage<ConnectedPlayersRequestMessage>();
            RegisterMessage<ConnectedPlayersResponseMessage>();
        }
        public static void RegisterMessage<T>()
               where T : NetworkMessage, new ()
        {
            var type = typeof(T);
            var lambda = Expression.Lambda<Func<NetworkMessage>>(Expression.New(type)).Compile();
            byte id = new T().DataType;
            messageConstructors.Add(id, lambda);
            messageTypes.Add(id, type);
        }
        private static void RegisterMessage(Type type)
        {
            var lambda = Expression.Lambda<Func<NetworkMessage>>(Expression.New(type)).Compile();
            byte id = lambda().DataType;
            messageConstructors.Add(id, lambda);
            messageTypes.Add(id, type);
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
            Func<NetworkMessage> factory;
            if (!messageConstructors.TryGetValue(dataType, out factory))
                return null;

            var res = factory();
            res.Deserialize(payload);
            return res;
        }
    }
}
