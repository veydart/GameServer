using System;
using System.Threading.Tasks;
using GameServer.Domain.Abstractions;
using GameServer.Domain.Extensions;

namespace GameServer.Domain.Jobs
{
    class GameJob:IJob
    {
        private readonly IGameLogicService _gameLogic;

        public GameJob(IGameLogicService gameLogic)
        {
            _gameLogic = gameLogic;
        }

        public TimeSpan Interval => TimeSpan.FromSeconds(1);

        public async Task Execute()
        {
           await _gameLogic.UpdateGameStatus();
        }
    }
}
