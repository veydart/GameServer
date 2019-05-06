﻿// <auto-generated />
using System;
using GameServer.Domain.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace GameServer.Migrations
{
    [DbContext(typeof(GameServerDbContext))]
    [Migration("20190506212330_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("GameServer.Domain.Entities.Player", b =>
                {
                    b.Property<Guid>("PlayerGuid");

                    b.Property<int>("Health");

                    b.Property<string>("Nick")
                        .HasMaxLength(20);

                    b.HasKey("PlayerGuid");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("GameServer.Domain.Entities.PlayersRoom", b =>
                {
                    b.Property<Guid>("PlayerGuid");

                    b.Property<Guid>("RoomGuid");

                    b.HasKey("PlayerGuid", "RoomGuid");

                    b.HasIndex("RoomGuid");

                    b.ToTable("PlayersRooms");
                });

            modelBuilder.Entity("GameServer.Domain.Entities.Room", b =>
                {
                    b.Property<Guid>("RoomGuid");

                    b.Property<Guid>("HostPlayerGuid");

                    b.Property<int>("RoomStatus");

                    b.Property<Guid?>("WinnerGuid");

                    b.HasKey("RoomGuid");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("GameServer.Domain.Entities.PlayersRoom", b =>
                {
                    b.HasOne("GameServer.Domain.Entities.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerGuid")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GameServer.Domain.Entities.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomGuid")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
