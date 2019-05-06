using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GameServer.Contracts.Enums;

namespace GameServer.Domain.Entities
{
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid RoomGuid { get; set; }

        [ForeignKey(nameof(Player))]
        public Guid HostPlayerGuid { get; set; }

        [ForeignKey(nameof(Player))]
        public Guid? WinnerGuid { get; set; }

        public RoomStatusOption RoomStatus { get; set; }

        public Room()
        {
            RoomGuid = Guid.NewGuid();
            RoomStatus = RoomStatusOption.Opened;
        }

        public Room(Guid hostPlayerGuid)
        {
            RoomStatus = RoomStatusOption.Opened;
            RoomGuid = Guid.NewGuid();
            HostPlayerGuid = hostPlayerGuid;
        }
    }
}
