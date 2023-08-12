using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreDbContext _context;

        public GenericRepository(StoreDbContext context)
        {
            _context = context;
        }
        public void Add(T entity)
            => _context.Set<T>().Add(entity);

        public void Delete(T entity)
            => _context.Set<T>().Remove(entity);

        public async Task<T> GetByIdAsync(int id)
            => await _context.Set<T>().FindAsync(id);


        public async Task<IReadOnlyList<T>> ListAllAsync()
            => await _context.Set<T>().ToListAsync();

        public void Update(T entity)
            => _context.Set<T>().Update(entity);

        public async Task<T> GetEntityWithSpecifications(ISpecifications<T> specifications)
            => await ApplaySpecifications(specifications).FirstOrDefaultAsync();

        public async Task<IReadOnlyList<T>> ListAsync(ISpecifications<T> specifications)
            => await ApplaySpecifications(specifications).ToListAsync();

        private IQueryable<T> ApplaySpecifications(ISpecifications<T> specifications)
            => SpecificationsEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), specifications);

        public Task<int> CountAsync(ISpecifications<T> specifications)
            => ApplaySpecifications(specifications).CountAsync();
    }
}
