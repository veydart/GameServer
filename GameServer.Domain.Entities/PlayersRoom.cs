using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameServer.Domain.Entities
{
    public class PlayersRoom
    {
        [ForeignKey(nameof(Player))]
        public Guid PlayerGuid { get; set; }

        [ForeignKey(nameof(Room))]
        public Guid RoomGuid { get; set; }

        public virtual Player Player { get; set; }

        public virtual Room Room { get; set; }

        public PlayersRoom(Guid playerGuid, Guid roomGuid)
        {
            PlayerGuid = playerGuid;
            RoomGuid = roomGuid;
        }
    }
}
