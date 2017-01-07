﻿using NoobFight.Core.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Server.Message
{
    public class PingMessage : NetworkMessage
    {
        public PingMessage() : base(1, null)
        {
        }
        public PingMessage(byte[] data) : base(1,data)
        {
        }
        public override string ToString()
        {
            return "Ping";
        }
    }
    public class PongMessage : NetworkMessage
    {
        public PongMessage() : base(2, null)
        {
        }
        public PongMessage(byte[] data) : base(2,data)
        {
        }
        public override string ToString()
        {
            return "Pong";
        }
    }
}
