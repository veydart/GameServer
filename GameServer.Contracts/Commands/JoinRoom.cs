using System;

namespace GameServer.Contracts.Commands
{
    public class JoinRoom
    {
        public Guid RoomGuid { get; set; }
        public Guid PlayerGuid { get; set; }

    }
}
