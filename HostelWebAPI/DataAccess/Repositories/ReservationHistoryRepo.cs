using HostelWebAPI.DataAccess.Interfaces;
using HostelWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelWebAPI.DataAccess.Repositories
{
    public interface IReservationHistoryRepo : IRepository<ReservationHistory>
    {
        Task<IEnumerable<ReservationHistory>> GetByPropertyIdAsync(string propertyId);
        Task<IEnumerable<ReservationHistory>> GetByUserIdAsync(string userId);
        Task<List<ReservedDate>> GetReservationSchedule(string propertyId, int numOfMonthAhead);
        Task<bool> CanUserBookWithDate(ReservedDate wantDate, string propId);
        int CountNight(DateTime from, DateTime to);

    }


    public class ReservationHistoryRepo : IReservationHistoryRepo
    {
        private readonly HostelDBContext ctx;

        public ReservationHistoryRepo(HostelDBContext ctx)
        {
            this.ctx = ctx;
        }

        #region Basic CRUD
        public void Add(ReservationHistory entity)
        {
            ctx.ReservationHistory.Add(entity);
        }

        public void DeleteById(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReservationHistory> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<ReservationHistory>> GetAllAsync()
        {
            return ctx.ReservationHistory.ToListAsync();
        }

        public ReservationHistory GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ReservationHistory> GetByIdAsync(string id)
        {
            return ctx.ReservationHistory.SingleOrDefaultAsync(rh => rh.ReservationId == id);
        }
        #endregion

        public Task<List<ReservedDate>> GetReservationSchedule(string propertyId, int numOfMonthAhead = 3)
        {
            var today = DateTime.UtcNow;
            var reserv = ctx.ReservationHistory.Where(rh => rh.ToDate >= today && rh.PropertyId == propertyId)
                .Select(sch => new ReservedDate(sch))
                .ToListAsync();
            return reserv;
        }

        public async Task<bool> CanUserBookWithDate(ReservedDate wantDate, string propId)
        {
            var reserv = await GetReservationSchedule(propId);
            if (!UserCanBookFromTo(wantDate, reserv.ToArray(), null))
                return false;
            return true;
        }

        public int CountNight(DateTime from, DateTime to)
        {
            if (from < to)
            {
                var count = 0;
                for (var f = from.Date; f < to.Date; f = f.AddDays(1)) count++;
                return count;
            }
            return -1;
        }

#nullable enable
        private bool UserCanBookFromTo(ReservedDate wantDate, ReservedDate[] reserved, DateTime[]? dayoff)
        {
            if (wantDate.FromDate < DateTime.Today) return false;

            if (wantDate.FromDate.Date >= wantDate.ToDate.Date) return false;

            foreach (var r in reserved)
            {
                if ((wantDate.FromDate < r.ToDate && wantDate.FromDate > r.FromDate) || (wantDate.ToDate > r.FromDate && wantDate.ToDate < r.ToDate))
                    return false;

                if (wantDate.FromDate >= r.FromDate && wantDate.ToDate <= r.ToDate) return false;

                if (wantDate.FromDate <= r.FromDate && wantDate.ToDate >= r.ToDate) return false;
            }

            if (dayoff != null)
                foreach (var d in dayoff)
                {
                    if (d.Date > wantDate.FromDate.Date && d.Date < wantDate.ToDate.Date) return false;
                }

            return true;
        }
#nullable disable

        public async Task<IEnumerable<ReservationHistory>> GetByPropertyIdAsync(string propertyId)
        {
            var reservs = await this.GetAllAsync();
            var founds = reservs.FindAll(r => r.PropertyId == propertyId);
            return founds;
        }

        public async Task<IEnumerable<ReservationHistory>> GetByUserIdAsync(string userId)
        {
            var founds = await ctx.ReservationHistory.Where(rs => rs.UserId == userId)
                .Include(rs => rs.ReservationStatus)
                .Include(rs => rs.PaymentStatus)
                .Include(rs => rs.Property).ThenInclude(p => p.Images)
                .Include(rs => rs.Property).ThenInclude(p => p.PropertyStatus)
                .ToListAsync();

            return founds;
        }
        public Task<int> SaveChangeAsync()
        {
            return ctx.SaveChangesAsync();
        }

        public void Update(ReservationHistory entity)
        {
            throw new NotImplementedException();
        }

        public Task<ReservationHistory> AddAsync(ReservationHistory entity)
        {
            throw new NotImplementedException();
        }

        public Task<ReservationHistory> UpdateAsync(ReservationHistory entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ReservationHistory> DeleteByIdAsync(string id)
        {
            var reserv = await ctx.ReservationHistory.SingleOrDefaultAsync(rh => rh.ReservationId == id);
            if (reserv == null) return null;
            ctx.ReservationHistory.Remove(reserv);
            return reserv;
        }
    }
}
