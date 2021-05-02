using System.Collections.Generic;
using System.Threading.Tasks;
using HostelWebAPI.DataAccess.Interfaces;
using HostelWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HostelWebAPI.DataAccess.Repositories
{
    public interface IReviewRepo : IRepository<Models.Review>
    {
        Task<Review> GetByUserPropAsync(string userId, string propId);
    }

    public class ReviewRepo : IReviewRepo
    {
        private HostelDBContext context;

        public ReviewRepo(HostelDBContext ctx)
        {
            this.context = ctx;
        }

        #region Basic CRUD
        public void Add(Review entity)
        {
            context.Add(entity);
        }

        public Task<Review> AddAsync(Review entity)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteById(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Review> DeleteByIdAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Review> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Review>> GetAllAsync()
        {
            return context.Reviews.ToListAsync();
        }

        public Review GetById(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Review> GetByIdAsync(string id)
        {
            return context.Reviews.SingleOrDefaultAsync(r => r.ReviewId == id);
        }

        public Task<Review> GetByUserPropAsync(string userId, string propId)
        {
            return context.Reviews.SingleOrDefaultAsync(r => r.UserId == userId && r.PropId == propId);
        }

        public Task<int> SaveChangeAsync()
        {
            return context.SaveChangesAsync();
        }

        public void Update(Review entity)
        {
            context.Reviews.Update(entity);
        }

        public Task<Review> UpdateAsync(Review entity)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}