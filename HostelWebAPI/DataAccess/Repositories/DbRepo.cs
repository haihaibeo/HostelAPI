using HostelWebAPI.DataAccess.Interfaces;
using HostelWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelWebAPI.DataAccess.Repositories
{
    public class DbRepo : IDbRepo
    {
        private readonly HostelDBContext ctx;

        public DbRepo(
            IPropertyRepo Properties,
            IReservationHistoryRepo ReservationHistories,
            ICityRepo Cities,
            IPropertyTypeRepo PropertyTypes,
            IUserPropertyLikeRepo Likes,
            IReviewRepo Reviews,
            HostelDBContext ctx
            )
        {
            this.Cities = Cities;
            this.PropertyTypes = PropertyTypes;
            this.Likes = Likes;
            this.Reviews = Reviews;
            this.ctx = ctx;
            this.Properties = Properties;
            this.ReservationHistories = ReservationHistories;
        }
        public IPropertyRepo Properties { get; }
        public IReservationHistoryRepo ReservationHistories { get; }
        public ICityRepo Cities { get; }
        public IPropertyTypeRepo PropertyTypes { get; }
        public IUserPropertyLikeRepo Likes { get; }
        public IReviewRepo Reviews { get; }

        public Task<int> SaveChangesAsync()
        {
            return ctx.SaveChangesAsync();
        }
    }
}
