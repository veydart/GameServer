using System;
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
    public class RoomsController : Controller
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet("GetRooms")]
        public async Task<IList<RoomModel>> GetRooms([FromQuery]GetRooms getRooms)
        {
            return await _roomService.GetRooms(getRooms);
        }

        [HttpPost("CreateRoom")]
        public async Task CreateRoom(Guid playerGuid)
        {
            await _roomService.CreateRoom(playerGuid);
        }

        [HttpPost("JoinRoom")]
        public async Task JoinRoom(JoinRoom joinRoom)
        {
            await _roomService.JoinRoom(joinRoom);
        }
    }
}