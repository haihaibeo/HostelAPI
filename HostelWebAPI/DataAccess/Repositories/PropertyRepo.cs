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
        void Add(Property entity, PropertyAddress address, List<Image> images, PropertyService service);
        Task<List<Service>> GetServices(string propertyId);
        Task<List<Property>> GetPropsLikedAsync(string userId);
        Task<List<Property>> GetPropertiesByPropStatusId(string propStatusId);

        void CountStarTotalReview(out int starCount, out int totalReview, string PropId);
    }

    public class PropertyRepo : IPropertyRepo
    {
        private readonly HostelDBContext context;

        public PropertyRepo(HostelDBContext ctx)
        {
            this.context = ctx;
        }

        public void Add(Property entity)
        {
            throw new NotImplementedException();
        }

        public void Add(Property entity, PropertyAddress address, List<Image> images, PropertyService service)
        {
            context.Property.Add(entity);
            context.PropertyAddress.Add(address);
            context.Image.AddRange(images);
        }

        public Task<Property> AddAsync(Property entity)
        {
            throw new NotImplementedException();
        }

        public void CountStarTotalReview(out int starCount, out int totalReview, string propId)
        {
            totalReview = 0;
            starCount = 0;
            var rev = context.Reviews.Where(r => r.PropId == propId).ToListAsync().Result;
            totalReview = rev.Count;
            foreach (var r in rev)
            {
                starCount += r.Star;
            }
        }

        public void DeleteById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Property> DeleteByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<PropertyAddress> GetAddressAsync(string propertyId)
        {
            var property = await context.Property
                .Include(p => p.PropertyAddress).ThenInclude(x => x.City)
                .SingleOrDefaultAsync(a => a.PropertyTypeId == propertyId);
            return property.PropertyAddress;
        }

        public IEnumerable<Property> GetAll()
        {
            return context.Property.ToList();
        }

        public Task<List<Property>> GetAllAsync()
        {
            return context.Property
                .Include(p => p.PropertyStatus)
                .Include(a => a.PropertyAddress).ThenInclude(b => b.City)
                .Include(d => d.Images)
                .ToListAsync();
        }

        public Property GetById(string id)
        {
            return context.Property
                .Include(x => x.PropertyAddress)
                .SingleOrDefault(p => p.PropertyId == id);
        }

        public Task<Property> GetByIdAsync(string id)
        {
            return context.Property.Include(a => a.ReservationHistories)
                .Include(b => b.Images)
                .Include(p => p.PropertyStatus)
                .Include(c => c.PropertyAddress).ThenInclude(add => add.City)
                .SingleOrDefaultAsync(p => p.PropertyId == id);
        }

        public async Task<List<Property>> GetPropsLikedAsync(string userId)
        {
            var upl = await context.UserPropertyLikes.Where(upl => upl.UserId == userId).Include(u => u.Property).ToListAsync();
            var props = new List<Property>();

            foreach (var l in upl)
            {
                // props.Add(context.Property.SingleOrDefaultAsync(p => p.PropertyId == l.PropertyId))
            }

            return props;
        }

        public Task<List<Service>> GetServices(string propertyId)
        {
            return context.PropertyWithServices
                .Where(s => s.PropertyId == propertyId)
                .Select(pws => pws.Service)
                .ToListAsync();
        }

        public Task<List<Property>> GetPropertiesByPropStatusId(string propStatusId)
        {
            return context.Property.Where(p => p.PropertyStatusId == propStatusId).ToListAsync();
        }

        public Task<int> SaveChangeAsync()
        {
            return context.SaveChangesAsync();
        }

        public void Update(Property entity)
        {
            context.Property.Update(entity);
        }

        public Task<Property> UpdateAsync(Property entity)
        {
            throw new NotImplementedException();
        }
    }
}
