using HostelWebAPI.DataAccess.Interfaces;
using HostelWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelWebAPI.DataAccess.Repositories
{
    public interface IPropertyRepo : IRepository<Models.Property>
    {
        Task<Models.PropertyAddress> GetAddressAsync(string propertyId);
        Task<Models.PropertyService> GetServicesAsync(string propertyId);
    }

    public class PropertyRepo : IPropertyRepo
    {
        private readonly HostelDBContext ctx;

        public PropertyRepo(HostelDBContext ctx)
        {
            this.ctx = ctx;
        }

        public void Add(Property entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<PropertyAddress> GetAddressAsync(string propertyId)
        {
            var property = await ctx.Property
                .Include(p => p.PropertyAddress).ThenInclude(x => x.City)
                .SingleOrDefaultAsync(a => a.PropertyTypeId == propertyId);
            return property.PropertyAddress;
        }

        public IEnumerable<Property> GetAll()
        {
            return ctx.Property.ToList();
        }

        public Task<List<Property>> GetAllAsync()
        {
            return ctx.Property
                .Include(a => a.PropertyAddress).ThenInclude(b => b.City)
                .Include(c => c.PropertyService)
                .Include(d => d.Images)
                .ToListAsync();
        }

        public Property GetById(string id)
        {
            return ctx.Property
                .Include(x => x.PropertyAddress)
                .SingleOrDefault(p => p.PropertyId == id);
        }

        public Task<Property> GetByIdAsync(string id)
        {
            return ctx.Property.Include(a => a.ReservationHistories)
                .SingleOrDefaultAsync(p => p.PropertyId == id);
        }

        public async Task<PropertyService> GetServicesAsync(string propertyId)
        {
            var property = await ctx.Property
                .Include(a => a.PropertyService)
                .SingleOrDefaultAsync(ps => ps.PropertyId == propertyId);

            return property.PropertyService;
        }

        public Task<int> SaveChangeAsync()
        {
            return ctx.SaveChangesAsync();
        }

        public void Update(Property entity)
        {
            throw new NotImplementedException();
        }
    }
}
