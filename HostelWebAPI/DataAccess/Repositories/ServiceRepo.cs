using System.Collections.Generic;
using System.Threading.Tasks;
using HostelWebAPI.DataAccess.Interfaces;
using HostelWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HostelWebAPI.DataAccess.Repositories
{
    public interface IServiceRepo : IRepository<Service>
    {

    }
    public class ServiceRepo : IServiceRepo
    {
        private readonly HostelDBContext context;

        public ServiceRepo(HostelDBContext context)
        {
            this.context = context;
        }
        public void Add(Service entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<Service> AddAsync(Service entity)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteById(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Service> DeleteByIdAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Service> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Service>> GetAllAsync()
        {
            return context.Services.ToListAsync();
        }

        public Service GetById(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Service> GetByIdAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> SaveChangeAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Update(Service entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<Service> UpdateAsync(Service entity)
        {
            throw new System.NotImplementedException();
        }
    }
}