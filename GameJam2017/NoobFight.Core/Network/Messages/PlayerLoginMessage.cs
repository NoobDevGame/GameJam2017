using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Core.Network.Messages
{
    public class PlayerLoginRequestMessage : NetworkMessage
    {
        public override MessageType DataType => MessageType.PlayerLoginRequest;
    }
    public class PlayerLoginResponseMessage : NetworkMessage
    {
        public override MessageType DataType => MessageType.PlayerLoginResponse;
    }
}
