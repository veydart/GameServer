using System.Collections.Generic;
using System.Threading.Tasks;
using GameServer.Contracts.Commands;
using GameServer.Contracts.Dto;
using GameServer.Domain.Entities;

namespace GameServer.Domain.Abstractions
{
    public interface IPlayerService
    {
        Task CreatePlayer(CreatePlayer createPlayer);

        Task<IList<PlayerModel>> GetPlayers();
    }
}
