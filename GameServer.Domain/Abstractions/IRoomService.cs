using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameServer.Contracts.Commands;
using GameServer.Contracts.Dto;

namespace GameServer.Domain.Abstractions
{
    public interface IRoomService
    {
        Task<IList<RoomModel>> GetRooms(GetRooms getRooms);

        Task CreateRoom(Guid playerGuid);

        Task JoinRoom(JoinRoom joinRoom);
    }
}
