using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Domain.Extensions
{
    public interface IJob
    {
        /// <summary>
        /// Интервал выполнения задачи
        /// </summary>
        TimeSpan Interval { get; }
        /// <summary>
        /// Выполнение задачи
        /// </summary>
        /// <returns> <see cref="Task"/> </returns>
        Task Execute();
    }
}
