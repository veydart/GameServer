using System.Threading.Tasks;

namespace GameServer.Domain.Abstractions
{
    public interface IGameLogicService
    {
        /// <summary>
        /// Реализуем сражение через воркер
        /// </summary>
        /// <returns></returns>
        Task UpdateGameStatus();
    }
}
