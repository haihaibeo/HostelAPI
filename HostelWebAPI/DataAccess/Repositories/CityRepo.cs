﻿using HostelWebAPI.DataAccess.Interfaces;
using HostelWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelWebAPI.DataAccess.Repositories
{
    public interface ICityRepo : IRepository<Models.City>
    {

    }

    public class CityRepo : ICityRepo
    {
        private readonly HostelDBContext ctx;

        public CityRepo(HostelDBContext ctx)
        {
            this.ctx = ctx;
        }

        public void Add(City entity)
        {
            throw new NotImplementedException();
        }

        public Task<City> AddAsync(City entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<City> DeleteByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<City> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<City>> GetAllAsync()
        {
            return ctx.City.Include(c => c.Country).ToListAsync();
        }

        public City GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<City> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangeAsync()
        {
            return ctx.SaveChangesAsync();
        }

        public void Update(City entity)
        {
            throw new NotImplementedException();
        }

        public Task<City> UpdateAsync(City entity)
        {
            throw new NotImplementedException();
        }
    }
}
