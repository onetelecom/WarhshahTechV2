using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Helper
{
    public class MyScheduler : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public MyScheduler(IServiceScopeFactory serviceScopeFactory) => _serviceScopeFactory = serviceScopeFactory;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Option 1
            while (!stoppingToken.IsCancellationRequested)
            {
                // do async work
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    Debug.Write("This Is Every Socond Me n/ /n \n ");
                }
                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }

           
        }
    }
}
