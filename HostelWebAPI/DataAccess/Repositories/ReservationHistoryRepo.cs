using HostelWebAPI.DataAccess.Interfaces;
using HostelWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelWebAPI.DataAccess.Repositories
{
    public interface IReservationHistoryRepo : IRepository<ReservationHistory>, IDisposable
    {
        Task<IEnumerable<ReservationHistory>> GetByPropertyIdAsync(string propertyId);
        Task<IEnumerable<ReservationHistory>> GetByUserIdAsync(string userId);
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
            throw new NotImplementedException();
        }

        public void DeleteById(string id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
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
