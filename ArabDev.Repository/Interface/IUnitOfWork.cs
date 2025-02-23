using ArabDev.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabDev.Repository.Interface
{
    public interface IUnitOfWork
    {
        IGenaricRepository<TEntity, Tkey> Repository<TEntity, Tkey>() where TEntity : class;

        Task<int> CompleteAync();
        ValueTask DisposeAsync();
    }
}