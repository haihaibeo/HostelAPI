using HostelWebAPI.DataAccess.Interfaces;
using HostelWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelWebAPI.DataAccess.Repositories
{
    public class UserPropertyLikeRepo : IUserPropertyLikeRepo
    {
        private readonly HostelDBContext ctx;

        public UserPropertyLikeRepo(HostelDBContext ctx)
        {
            this.ctx = ctx;
        }

        public void Add(UserPropertyLike entity)
        {
            ctx.UserPropertyLikes.Add(entity);
        }

        public void DeleteById(string id)
        {
            var like = ctx.UserPropertyLikes.SingleOrDefaultAsync(l => l.UserPropertyId == id).Result;
            ctx.UserPropertyLikes.Remove(like);
        }

        public async Task DeleteByPropertyId(string propertyId, string userId)
        {
            var like = await ctx.UserPropertyLikes.SingleOrDefaultAsync(l => l.UserId == userId && l.PropertyId == propertyId);
            ctx.UserPropertyLikes.Remove(like);
        }

        public IEnumerable<UserPropertyLike> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<UserPropertyLike>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserPropertyLike> GetByPropertyIdAsync(string propertyId, string userId)
        {
            var like = ctx.UserPropertyLikes.FirstOrDefaultAsync(upl => upl.UserId == userId && upl.PropertyId == propertyId);
            return like;
        }

        public Task<List<UserPropertyLike>> GetAllByUserIdAsync(string userId)
        {
            return ctx.UserPropertyLikes.Where(upl => upl.UserId == userId).Include(u => u.Property).ThenInclude(p => p.Images).Distinct().ToListAsync();
        }

        public UserPropertyLike GetById(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<UserPropertyLike> GetByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangeAsync()
        {
            return ctx.SaveChangesAsync();
        }

        public void Update(UserPropertyLike entity)
        {
            throw new NotImplementedException();
        }

        public async Task<UserPropertyLike> AddAsync(UserPropertyLike entity)
        {
            var upl = await ctx.UserPropertyLikes.AddAsync(entity);
            return upl.Entity;
        }

        public Task<UserPropertyLike> UpdateAsync(UserPropertyLike entity)
        {
            throw new NotImplementedException();
        }

        public Task<UserPropertyLike> DeleteByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }

    public interface IUserPropertyLikeRepo : IRepository<UserPropertyLike>
    {
        Task<List<UserPropertyLike>> GetAllByUserIdAsync(string userId);
        Task<UserPropertyLike> GetByPropertyIdAsync(string propertyId, string userId);

        Task DeleteByPropertyId(string propertyId, string userId);
    }
}
