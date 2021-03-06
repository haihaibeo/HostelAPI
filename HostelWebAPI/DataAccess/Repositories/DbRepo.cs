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
        public DbRepo(HostelDBContext ctx)
        {
            this.ctx = ctx;
        }
        private readonly HostelDBContext ctx;
        private PropertyRepo propertyRepo;
        private ReservationHistoryRepo reservationHistoryRepo;

        public IPropertyRepo Properties
        {
            get
            {
                if (propertyRepo == null) propertyRepo = new PropertyRepo(ctx);
                return propertyRepo;
            }
        }

        public IReservationHistoryRepo ReservationHistories
        {
            get
            {
                if (reservationHistoryRepo == null) reservationHistoryRepo = new ReservationHistoryRepo(ctx);
                return reservationHistoryRepo;
            }
        }
    }
}
