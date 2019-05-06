using System;
using System.Linq;
using System.Threading.Tasks;
using GameServer.Contracts.Enums;
using GameServer.Domain.Abstractions;
using GameServer.Domain.Database;
using Microsoft.EntityFrameworkCore;

namespace GameServer.Domain.Services
{
    public class GameLogicService:IGameLogicService
    {
        private readonly GameServerDbContext _context;

        public GameLogicService(GameServerDbContext context)
        {
            _context = context;
        }

        public async Task UpdateGameStatus()
        {
            var rooms = _context.PlayersRooms
                .Include(x=>x.Room)
                .Include(x=>x.Player)
                .Where(x => x.Room.RoomStatus == RoomStatusOption.InGame);
            var groupedRooms = rooms.GroupBy(x => x.RoomGuid);

            foreach (var fullRoom in groupedRooms)
            {
                var playerGuids = fullRoom.Select(x => x.Player.PlayerGuid);
                var players = _context.Players.Where(x => playerGuids.Contains(x.PlayerGuid));

                foreach (var player in players)
                {
                    player.Health -= new Random().Next(0, 2);
                    if (player.Health <= 0)
                    {
                        var room = fullRoom.Select(x => x.Room).First();

                        room.RoomStatus = RoomStatusOption.Closed;
                        room.WinnerGuid = players.Single(x => x.PlayerGuid != player.PlayerGuid).PlayerGuid;

                        var playerRooms = _context.PlayersRooms.Where(x=>x.RoomGuid == fullRoom.Key);
                        foreach (var playersRoom in playerRooms)
                        {
                            _context.PlayersRooms.Remove(playersRoom);
                        }
                    }

                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
