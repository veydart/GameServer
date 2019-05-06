using AutoMapper;
using GameServer.Domain.Abstractions;
using GameServer.Domain.Extensions;
using GameServer.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GameServer.Domain
{
    public static class DomainExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection collection)
        {
            collection.AddScoped<IGameLogicService, GameLogicService>();
            collection.AddScoped<IPlayerService, PlayerService>();
            collection.AddScoped<IRoomService, RoomService>();

            collection.AddAutoMapper();
            Mapper.Initialize(x=>x.AddProfile(typeof(DomainMapper)));

            return collection;
        }
    }
}
