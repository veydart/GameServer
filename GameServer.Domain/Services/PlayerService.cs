using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GameServer.Contracts.Commands;
using GameServer.Contracts.Dto;
using GameServer.Domain.Abstractions;
using GameServer.Domain.Database;
using GameServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameServer.Domain.Services
{
    public class PlayerService:IPlayerService
    {
        private readonly IMapper _mapper;
        private readonly GameServerDbContext _context;

        public PlayerService(GameServerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreatePlayer(CreatePlayer createPlayer)
        {
            var player = new Player(createPlayer.Nick);
            _context.Players.Add(player);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<PlayerModel>> GetPlayers()
        {
            var players = _context.Players.AsNoTracking();
            var playerModels = _mapper.ProjectTo<PlayerModel>(players);
            return await playerModels.ToListAsync();
        }
    }
}
