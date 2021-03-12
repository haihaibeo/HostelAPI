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
        public DbRepo(
            IPropertyRepo Properties, 
            IReservationHistoryRepo ReservationHistories, 
            ICityRepo Cities,
            IPropertyTypeRepo PropertyTypes
            )
        {
            this.Cities = Cities;
            this.PropertyTypes = PropertyTypes;
            this.Properties = Properties;
            this.ReservationHistories = ReservationHistories;
        }
        public IPropertyRepo Properties { get; }
        public IReservationHistoryRepo ReservationHistories { get; }
        public ICityRepo Cities { get; }
        public IPropertyTypeRepo PropertyTypes { get; }
    }
}
