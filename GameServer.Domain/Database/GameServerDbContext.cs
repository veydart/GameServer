using GameServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameServer.Domain.Database
{
    public class GameServerDbContext : DbContext
    {
        public DbSet<Room> Rooms { get; private set; }

        public DbSet<Player> Players { get; private set; }

        public DbSet<PlayersRoom> PlayersRooms { get; private set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayersRoom>().HasKey(x => new {x.PlayerGuid, x.RoomGuid});
        }

        public GameServerDbContext(DbContextOptions<GameServerDbContext> options) : base(options)
        {
            
        }
    }
}