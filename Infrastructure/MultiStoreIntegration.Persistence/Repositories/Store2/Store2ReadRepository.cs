﻿using Microsoft.EntityFrameworkCore;
using MultiStoreIntegration.Application.Repositories;
using MultiStoreIntegration.Application.Repositories.Store2;
using MultiStoreIntegration.Domain.Entities.common;
using MultiStoreIntegration.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Persistence.Repositories.Store2
{
    public class Store2ReadRepository<T> : Store2IReadRepository<T> where T : BaseEntity
    {
        private readonly Store2DbContext _context;
        public Store2ReadRepository(Store2DbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll(bool tracking = true)
            => tracking ? Table : Table.AsNoTracking();

        public async Task<IEnumerable<T>> GetAllAsync(bool tracking = true)
            => await GetAll(tracking).ToListAsync();

        public async Task<T> GetByIdAsync(Guid id, bool tracking = true)
            => await GetAll(tracking).FirstOrDefaultAsync(x => x.Id == id);

        public async Task<T> GetByIdAsync(string id, bool tracking = true)
            => await GetAll(tracking).FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
            => tracking ? Table.Where(method) : Table.Where(method).AsNoTracking();

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
            => await GetWhere(method, tracking).FirstOrDefaultAsync();

        public async Task<List<(string Category, int TotalQuantity)>> GetTotalStockPerCategoryAsync()
        {
            return await _context.Stocks
                .Where(s => s.Category != null)
                .GroupBy(s => s.Category)
                .Select(g => new ValueTuple<string, int>(g.Key!, g.Sum(s => s.Quantity)))
                .ToListAsync();
        }

    }
}
