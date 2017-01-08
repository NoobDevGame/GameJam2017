using System;
using NoobFight.Core.Network.Messages;

namespace NoobFight.Core.Network.Messages
{
    public class PlayerLoginErrorMessage : NetworkMessage
    {
        public override MessageType DataType => MessageType.PlayerLoginError;
    }
}