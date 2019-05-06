using System;

namespace GameServer.Contracts.Dto
{
    public class PlayerModel
    {
        public Guid PlayerGuid { get; set; }

        public string Nick { get; set; }

        public int Health { get; set; }
    }
}
