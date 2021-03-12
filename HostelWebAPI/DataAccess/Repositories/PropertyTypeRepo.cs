using HostelWebAPI.DataAccess.Interfaces;
using HostelWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelWebAPI.DataAccess.Repositories
{
    public interface IPropertyTypeRepo : IRepository<Models.PropertyType>
    {
        Task<int> CountAsync(string propertyTypeId);
    }

    public class PropertyTypeRepo : IPropertyTypeRepo
    {
        private readonly HostelDBContext cx;

        public PropertyTypeRepo(HostelDBContext cx)
        {
            this.cx = cx;
        }
        public void Add(PropertyType entity)
        {
            throw new NotImplementedException();
        }

        // IMPORTANT NOTE: Use .Include() to enable eagle loading
        public async Task<int> CountAsync(string propertyTypeId)
        {
            var pt = await cx.PropertyType.Include(x => x.Properties).SingleOrDefaultAsync(t => t.PropertyTypeId == propertyTypeId);
            if (pt == null) return 0;
            return pt.Properties.Count;
        }

        public void DeleteById(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PropertyType> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<PropertyType>> GetAllAsync()
        {
            return cx.PropertyType.ToListAsync();
        }

        public PropertyType GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<PropertyType> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangeAsync()
        {
            return cx.SaveChangesAsync();
        }

        public void Update(PropertyType entity)
        {
            throw new NotImplementedException();
        }
    }
}
