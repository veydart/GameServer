using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameServer.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerGuid = table.Column<Guid>(nullable: false),
                    Nick = table.Column<string>(maxLength: 20, nullable: true),
                    Health = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerGuid);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomGuid = table.Column<Guid>(nullable: false),
                    HostPlayerGuid = table.Column<Guid>(nullable: false),
                    WinnerGuid = table.Column<Guid>(nullable: true),
                    RoomStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomGuid);
                });

            migrationBuilder.CreateTable(
                name: "PlayersRooms",
                columns: table => new
                {
                    PlayerGuid = table.Column<Guid>(nullable: false),
                    RoomGuid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayersRooms", x => new { x.PlayerGuid, x.RoomGuid });
                    table.ForeignKey(
                        name: "FK_PlayersRooms_Players_PlayerGuid",
                        column: x => x.PlayerGuid,
                        principalTable: "Players",
                        principalColumn: "PlayerGuid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayersRooms_Rooms_RoomGuid",
                        column: x => x.RoomGuid,
                        principalTable: "Rooms",
                        principalColumn: "RoomGuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayersRooms_RoomGuid",
                table: "PlayersRooms",
                column: "RoomGuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayersRooms");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
