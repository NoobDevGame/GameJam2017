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
        private static Dictionary<MessageType, Type> messageTypes;
        private static Dictionary<MessageType, Func<NetworkMessage>> messageConstructors;
        static MessageManager()
        {
            messageConstructors = new Dictionary<MessageType, Func<NetworkMessage>>();
            messageTypes = new Dictionary<MessageType, Type>();
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
            var id = new T().DataType;
            messageConstructors.Add(id, lambda);
            messageTypes.Add(id, type);
        }
        private static void RegisterMessage(Type type)
        {
            var lambda = Expression.Lambda<Func<NetworkMessage>>(Expression.New(type)).Compile();
            var id = lambda().DataType;
            messageConstructors.Add(id, lambda);
            messageTypes.Add(id, type);
        }

        public static NetworkMessage Deserialize(byte[] data)
        {
            if (data.Length == 1)
                return Deserialize((MessageType)data[0], null);

            byte[] payload = new byte[data.Length - 1];
            Array.Copy(data, 1, payload, 0, data.Length - 1);
            return Deserialize((MessageType)data[0], payload);
        }
        public static NetworkMessage Deserialize(MessageType dataType, byte[] payload)
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
