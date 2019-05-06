using System.ComponentModel.DataAnnotations;

namespace GameServer.Contracts.Commands
{
    public class CreatePlayer
    {
        [MaxLength(20)]
        public string Nick { get; set; }
    }
}
