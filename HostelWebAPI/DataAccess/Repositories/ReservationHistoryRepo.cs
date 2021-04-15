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
        Task<List<ReservedDate>> GetReservationSchedule(string propertyId, int? numOfMonthAhead);
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
            throw new NotImplementedException();
        }
        #endregion

        public Task<List<ReservedDate>> GetReservationSchedule(string propertyId,int? numOfMonthAhead)
        {
            if (numOfMonthAhead == null) numOfMonthAhead = 3;
            var today = DateTime.UtcNow;
            var reserv = ctx.ReservationHistory.Where(rh => rh.ToDate >= today && rh.PropertyId == propertyId).Select(sch => new ReservedDate(sch)).ToListAsync();
            return reserv;
        }

        public async Task<IEnumerable<ReservationHistory>> GetByPropertyIdAsync(string propertyId)
        {
            var reservs = await this.GetAllAsync();
            var founds = reservs.FindAll(r => r.PropertyId == propertyId);
            return founds;
        }

        public async Task<IEnumerable<ReservationHistory>> GetByUserIdAsync(string userId)
        {
            var reservs = await this.GetAllAsync();
            var founds = reservs.FindAll(r => r.UserId == userId);
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
    }
}
