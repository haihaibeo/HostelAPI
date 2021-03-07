using HostelWebAPI.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelWebAPI.DataAccess.Interfaces
{
    public interface IDbRepo
    {
        IPropertyRepo Properties { get; }
        IReservationHistoryRepo ReservationHistories { get; }
        ICityRepo Cities { get; }
    }
}
