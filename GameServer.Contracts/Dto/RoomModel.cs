using System;
using GameServer.Contracts.Enums;

namespace GameServer.Contracts.Dto
{
    public class RoomModel
    {
        public Guid RoomGuid { get; set; }

        public Guid HostPlayerGuid { get; set; }

        public Guid? WinnerGuid { get; set; }

        public RoomStatusOption RoomStatus { get; set; }
    }
}
