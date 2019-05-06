using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameServer.Domain.Entities
{
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid PlayerGuid { get; set; }

        [MaxLength(20)]
        public string Nick { get; set; }

        public int Health { get; set; }

        public Player()
        {
            PlayerGuid = Guid.NewGuid();
        }

        public Player(string nick)
        {
            PlayerGuid = Guid.NewGuid();
            Nick = nick;
        }
    }
}
