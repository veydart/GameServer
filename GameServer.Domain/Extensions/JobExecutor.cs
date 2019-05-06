using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GameServer.Domain.Extensions
{
    /// <summary>
    /// Исполнитель задач
    /// </summary>
    public class JobExecutor<TJob> : IExecutor where TJob : IJob
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<JobExecutor<TJob>> _logger;
        private readonly string _jobName;
        private readonly CancellationTokenSource _cancellationToken;

        private Thread _thread;
        private TimeSpan _nextInterval;

        /// <summary/>
        public JobExecutor(IServiceScopeFactory scopeFactory, ILogger<JobExecutor<TJob>> logger)
        {
            _scopeFactory = scopeFactory;
            _cancellationToken = new CancellationTokenSource();

            _logger = logger;
            _jobName = typeof(TJob).Name;
        }

        /// <inheritdoc />
        public void Start()
        {
            _nextInterval = TimeSpan.Zero;
            _thread = new Thread(Execute);
            _thread.IsBackground = true;
            _thread.Start();
        }

        /// <inheritdoc />
        public void Stop()
        {
            _cancellationToken.Cancel();
        }

        private void Execute()
        {
            var stopWatch = new Stopwatch();
            while (!_cancellationToken.IsCancellationRequested)
            {
                //Интервал выполнения задач
                Thread.Sleep(_nextInterval);

                stopWatch.Start();
                using (var scope = _scopeFactory.CreateScope())
                {
                    //Выполняем задачу
                    try
                    {
                        var provider = scope.ServiceProvider;
                        var job = provider.GetService<TJob>();
                        job.Execute().Wait();

                        _nextInterval = job.Interval;
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(new EventId(), e, $"{_jobName}: Ошибка при выполнении задачи");
                    }
                }


                stopWatch.Stop();

                //Считаем время следующего запуска задачи
                if (stopWatch.Elapsed < _nextInterval)
                {
                    _nextInterval = _nextInterval - stopWatch.Elapsed;
                }
                else
                {
                    _nextInterval = TimeSpan.Zero;
                }

                stopWatch.Reset();

                _logger.LogInformation($"{_jobName}: Задача была успешно выполнена");
            }
        }
    }
}
