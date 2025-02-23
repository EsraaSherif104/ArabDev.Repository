using ArabDev.Data.Contexts;
using ArabDev.Data.Entities;
using ArabDev.Repository.Interface;
using ArabDev.Repository.Specification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabDev.Repository.Repositories
{
    public class GenaricRepository<TEntity, Tkey> : IGenaricRepository<TEntity, Tkey> where TEntity : class
    {
        private readonly ArabDevDbContext _context;

        public GenaricRepository(ArabDevDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
         => await _context.Set<TEntity>().ToListAsync();

        public async Task<IReadOnlyList<TEntity>> GetAllAsNoTrackingAsync()
        => await _context.Set<TEntity>().AsNoTracking().ToListAsync();


        public async Task<TEntity> GetByIdAsync(Tkey? id)
        => await _context.Set<TEntity>().FindAsync(id);

        public async Task<TEntity> SearchByNameAsync(string username)
       => await _context.Set<TEntity>().FindAsync(username);


        public async Task AddAsync(TEntity entity)
       => await _context.Set<TEntity>().AddAsync(entity);

        public async Task UpdateAsync(TEntity entity)
         => _context.Set<TEntity>().Update(entity);

        public async Task DeleteAsync(TEntity entity)
         => _context.Set<TEntity>().Remove(entity);

        public async Task<TEntity> GetWithSpecificationByIdAsync(ISpecification<TEntity> spac)
        => await ApplySpecification(spac).FirstOrDefaultAsync();


        public async Task<IReadOnlyList<TEntity>> GetAllWithSpecificationAsync(ISpecification<TEntity> spac)
    => await ApplySpecification(spac).ToListAsync();

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spac)
                    => SpecificationEvaluator<TEntity, Tkey>.GetQuery(_context.Set<TEntity>(), spac);

        public async Task<int> GetcountSpecificationAsync(ISpecification<TEntity> spac)
       => await ApplySpecification(spac).CountAsync();

        //public async Task<TEntity> GetByIdAsNoTrackingAsync(Tkey? id)
        //=> await _context.Set<TEntity>().AsNoTracking().FindAsync(id);
    }
}
