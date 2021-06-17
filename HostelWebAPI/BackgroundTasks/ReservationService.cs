using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HostelWebAPI.DataAccess.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HostelWebAPI
{
    public class ReservationService : BackgroundService
    {
        private readonly ILogger<ReservationService> logger;
        private readonly IServiceProvider sp;
        private readonly IDbRepo repo;

        public ReservationService(ILogger<ReservationService> logger, IServiceProvider sp)
        {
            this.logger = logger;
            this.sp = sp;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var recycleTime = new TimeSpan(1, 0, 0); // 1 hour
            while (!stoppingToken.IsCancellationRequested)
            {
                using (IServiceScope scope = sp.CreateScope())
                {
                    var repo = scope.ServiceProvider.GetRequiredService<IDbRepo>();
                    var worker = new ReservationWorker(repo, logger);
                    await worker.UpdateReservationStatus();
                }
                await Task.Delay(recycleTime, stoppingToken);
            }
        }
    }
}