using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelWebAPI.DataAccess.Interfaces
{
    public interface IRepository<TEntity> where TEntity: class
    {
        Task<TEntity> GetByIdAsync(string id);
        TEntity GetById(string id);
        Task<List<TEntity>> GetAllAsync();
        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);
        void Update(TEntity entity);
        void DeleteById(string id);
        Task<int> SaveChangeAsync();
    }

}
