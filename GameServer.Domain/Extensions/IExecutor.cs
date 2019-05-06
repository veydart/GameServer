namespace GameServer.Domain.Extensions
{
    public interface IExecutor
    {
        /// <summary>
        /// Запуск исполнителя
        /// </summary>
        void Start();
        /// <summary>
        /// Остановка исполнителя
        /// </summary>
        void Stop();
    }
}
