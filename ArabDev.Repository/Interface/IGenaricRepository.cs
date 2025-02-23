using ArabDev.Data.Entities;
using ArabDev.Repository.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabDev.Repository.Interface
{
    public interface IGenaricRepository<TEntity, TKey> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(TKey? id);
        Task<TEntity> GetWithSpecificationByIdAsync(ISpecification<TEntity> spac);


        //  Task<TEntity> GetByIdAsNoTrackingAsync(TKey? id);

        Task<TEntity> SearchByNameAsync(string username);
        Task<IReadOnlyList<TEntity>> GetAllAsync();
        Task<IReadOnlyList<TEntity>> GetAllWithSpecificationAsync(ISpecification<TEntity> spac);


        Task<IReadOnlyList<TEntity>> GetAllAsNoTrackingAsync();
        Task<int> GetcountSpecificationAsync(ISpecification<TEntity> spac);

        Task AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
    }
}