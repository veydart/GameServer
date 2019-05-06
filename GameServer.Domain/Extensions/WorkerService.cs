using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GameServer.Domain.Extensions
{
    /// <summary>
    /// Сервис исполнения задач
    /// </summary>
    public class WorkerService : IDisposable
    {
        /// <summary>
        /// Флаг, запущена ли обработка задач
        /// </summary>
        public bool Started { get; private set; }

        private readonly IServiceProvider _serviceProvider;
        private readonly List<IExecutor> _executors;
        private readonly ILogger<WorkerService> _logger;

        /// <summary/>
        public WorkerService(IServiceScopeFactory scopeFactory)
        {
            _serviceProvider = scopeFactory.CreateScope().ServiceProvider;

            _logger = _serviceProvider.GetService<ILogger<WorkerService>>();
            _executors = new List<IExecutor>();
        }

        /// <summary>
        /// Регистрация задачи
        /// </summary>
        /// <typeparam name="TJob"> Тип задания </typeparam>
        public void RegisterWorker<TJob>() where TJob : IJob
        {
            if (_executors.Any(a => a.GetType() == typeof(TJob)))
            {
                throw new InvalidOperationException("Данная задача уже зарегистрирована");
            }

            var executor = _serviceProvider.GetService<JobExecutor<TJob>>();
            _executors.Add(executor);

            if (Started)
            {
                executor.Start();
            }
        }

        /// <summary>
        /// Отмена регистрации задачи
        /// </summary>
        /// <typeparam name="TJob"> Тип задания </typeparam>
        public void UnregisterWorker<TJob>() where TJob : IJob
        {
            var executor = _executors.Single(a => a.GetType() == typeof(TJob));
            _executors.Remove(executor);
            if (Started)
            {
                executor.Stop();
            }
        }

        /// <summary>
        /// Запуск исполнения заданий
        /// </summary>
        public void Start()
        {
            Started = true;
            foreach (var executor in _executors)
            {
                executor.Start();
            }

            _logger.LogInformation("Обработка задач запущена");
        }

        /// <summary>
        /// Остановка исполнения заданий
        /// </summary>
        public void Stop()
        {
            Started = false;
            foreach (var executor in _executors)
            {
                executor.Stop();
            }

            _logger.LogWarning("Обработка задач остановлена");
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Stop();
        }
    }
}
