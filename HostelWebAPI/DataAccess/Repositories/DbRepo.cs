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
        private PropertyRepo PropertyRepo;

        public IPropertyRepo Properties
        {
            get
            {
                if (PropertyRepo == null) PropertyRepo = new PropertyRepo(ctx);
                return PropertyRepo;
            }
        }
    }
}
