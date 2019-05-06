using System.Collections.Generic;
using System.Threading.Tasks;
using GameServer.Contracts.Commands;
using GameServer.Contracts.Dto;
using GameServer.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GameServer.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class PlayersController : Controller
    {
        private readonly IPlayerService _playerService;

        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpPost("CreatePlayer")]
        public async Task CreatePlayer(CreatePlayer createPlayer)
        {
            await _playerService.CreatePlayer(createPlayer);
        }

        [HttpGet("GetPlayers")]
        public async Task<IList<PlayerModel>> GetPlayers()
        {
            return await _playerService.GetPlayers();
        }
    }
}