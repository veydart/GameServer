using GameServer.Contracts.Enums;

namespace GameServer.Contracts.Commands
{
    public class GetRooms
    {
        public RoomStatusOption RoomStatus { get; set; }
    }
}
