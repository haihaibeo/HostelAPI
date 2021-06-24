using System;
using System.Linq;
using System.Threading.Tasks;
using HostelWebAPI.DataAccess.Interfaces;
using HostelWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HostelWebAPI
{
    public interface IReservationWorker
    {
        Task UpdateReservationStatus();
        Task UpdatePaymentStatus();
    }

    public class ReservationWorker : IReservationWorker
    {
        private readonly IDbRepo repo;
        private readonly ILogger logger;

        public ReservationWorker(IDbRepo repo, ILogger logger)
        {
            this.repo = repo;
            this.logger = logger;
        }

        public Task UpdatePaymentStatus()
        {
            throw new NotImplementedException();
        }

        // TODO: include payment status
        public async Task UpdateReservationStatus()
        {
            logger.LogInformation("--Update reservation started--");
            var today = DateTime.Today;
            var resv = await repo.ReservationHistories.GetAllAsync();

            var countFromReserved = 0;
            var countFromActive = 0;

            // get on reserved, check date then update status
            var onReserved = resv.Where(r => r.ReservationStatusId == ReservationStatus.OnReserved).ToList();
            onReserved.ForEach(r =>
            {
                if (r.FromDate >= today)
                {
                    r.ReservationStatusId = ReservationStatus.Active;
                    countFromReserved++;
                }
            });

            var onActive = resv.Where(r => r.ReservationStatusId == ReservationStatus.Active).ToList();
            onActive.ForEach(r =>
            {
                if (r.ToDate < today)
                {
                    r.ReservationStatusId = ReservationStatus.Completed;
                    countFromActive++;
                }
            });

            try
            {
                Console.WriteLine($"Current ONRESERVED reservations count: {onReserved.Count}");
                Console.WriteLine($"Current ACTIVE reservations count: {onActive.Count}");

                var save = await repo.SaveChangesAsync();
                if (save > 0) Console.WriteLine($"Updated {save} reservations status");
                else Console.WriteLine("No status updated");
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "Error");
                throw e;
            }
        }
    }
}