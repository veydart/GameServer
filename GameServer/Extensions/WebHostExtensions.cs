using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace GameServer.Extensions
{
    public static class WebHostExtensions
    {
        public static IWebHost SetUpWithService<TService>(this IWebHost host, Action<TService> setUpAction)
        {
            using (var scope = host.Services.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<TService>();
                setUpAction(service);
            }

            return host;
        }
    }
}
