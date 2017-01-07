using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Core.Network
{
    public class MessageHandler
    {
        private Dictionary<byte, Action<Client, NetworkMessage>> messageHandlers;

        public MessageHandler()
        {
            messageHandlers = new Dictionary<byte, Action<Client, NetworkMessage>>();
        }

        public void RegisterMessageHandler<T>(Action<Client, T> handler) where T : NetworkMessage, new()
        {
            var id = new T().DataType;
            messageHandlers.Add(id, (Client c, NetworkMessage msg) => handler(c, (T)msg));
        }

        public void OnMessageReceived(object sender, NetworkMessage message)
        {
            Action<Client, NetworkMessage> handler;
            if (messageHandlers.TryGetValue(message.DataType, out handler))
                handler((Client)sender, message);
        }

    }
}
