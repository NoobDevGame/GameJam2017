﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Core.Network.Messages
{
    public enum MessageType : byte
    {
        Ping=1,
        Pong,
        ConnectedPlayersRequest,
        ConnectedPlayersResponse,
        PlayerLoginRequest,
        PlayerLoginResponse,
        PlayerJoinResponse,
        CreateWorld,
        EntityDataUpdate,
        WorldListRequest,
        WorldListResponse,
        PlayerJoinRequest,
        CreateMapRequest,
        CreateMapResponse,
        StartWorld,
        PlayerNotJoin
    }
}
