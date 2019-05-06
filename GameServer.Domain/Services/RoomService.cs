using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GameServer.Contracts.Commands;
using GameServer.Contracts.Dto;
using GameServer.Contracts.Enums;
using GameServer.Domain.Abstractions;
using GameServer.Domain.Database;
using GameServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameServer.Domain.Services
{
    public class RoomService:IRoomService
    {
        private readonly GameServerDbContext _context;
        private readonly IMapper _mapper;

        public RoomService(GameServerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IList<RoomModel>> GetRooms(GetRooms getRooms)
        {
            var rooms = _context.Rooms.Where(x => x.RoomStatus == getRooms.RoomStatus);
            var roomModels = _mapper.ProjectTo<RoomModel>(rooms);
            return await roomModels.ToListAsync();
        }

        public async Task CreateRoom(Guid playerGuid)
        {
            var room = new Room(playerGuid);
            _context.Rooms.Add(room);
            var playersRooms = new PlayersRoom(playerGuid, room.RoomGuid);
            _context.PlayersRooms.Add(playersRooms);
            await _context.SaveChangesAsync();
        }

        public async Task JoinRoom(JoinRoom joinRoom)
        {
            var playersCount = await _context.PlayersRooms.Where(x => x.RoomGuid == joinRoom.RoomGuid).CountAsync();
            var room = await _context.Rooms.FindAsync(joinRoom.RoomGuid);
            if (playersCount < 2 && room.RoomStatus == RoomStatusOption.Opened)
            {
                var playersRooms = new PlayersRoom(joinRoom.PlayerGuid, joinRoom.RoomGuid);
                _context.PlayersRooms.Add(playersRooms);

                await _context.SaveChangesAsync();
                await UpdateRoomStatus(joinRoom.RoomGuid);
                await _context.SaveChangesAsync();
            }
            else if(room.RoomStatus == RoomStatusOption.Closed)
            {
                throw new InvalidOperationException("Нельзя войти в закрытую комнату.");
            }
            else
            {
                throw new InvalidOperationException("Достигнут лимит игроков в комнате.");
            }
        }

        private async Task UpdateRoomStatus(Guid roomGuid)
        {
            var room = await _context.Rooms.FindAsync(roomGuid);
            room.RoomStatus = RoomStatusOption.InGame;
            var players = _context.PlayersRooms
                .Include(x => x.Player)
                .Where(x => x.RoomGuid == roomGuid)
                .Select(x => x.Player);
            foreach (var player in players)
            {
                player.Health = 10;
            }
        }
    }
}
