using System.Threading;
using GameServer.Domain.Extensions;
using GameServer.Domain.Jobs;
using Microsoft.Extensions.DependencyInjection;

namespace GameServer.Domain
{
    public static class WorkerExtensions
    {
        public static IServiceCollection AddWorkerServices(this IServiceCollection collection)
        {

            collection.AddSingleton<WorkerService>();
            collection.AddScoped(typeof(JobExecutor<>));
            collection.AddScoped<GameJob>();

            var provider = collection.BuildServiceProvider();
            var worker = provider.GetService<WorkerService>();
            worker.RegisterWorker<GameJob>();
            worker.Start();
            Thread.CurrentThread.Join();
            return collection;
        }
    }
}
