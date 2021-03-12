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

        public IEnumerable<Property> GetAll()
        {
            return ctx.Property.ToList();
        }

        public Task<List<Property>> GetAllAsync()
        {
            return ctx.Property.ToListAsync();
        }

        public Property GetById(string id)
        {
            return ctx.Property.SingleOrDefault(p => p.PropertyId == id);
        }

        public Task<Property> GetByIdAsync(string id)
        {
            return ctx.Property.SingleOrDefaultAsync(p => p.PropertyId == id);
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
