using ArabDev.Data.Contexts;
using ArabDev.Data.Entities;
using ArabDev.Repository.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabDev.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ArabDevDbContext _context;
        private Hashtable _repositories;

        public UnitOfWork(ArabDevDbContext context)
        {
            _repositories = new Hashtable();
            _context = context;
        }
        public async Task<int> CompleteAync()
        => await _context.SaveChangesAsync();

        public ValueTask DisposeAsync()
        => _context.DisposeAsync();




        public IGenaricRepository<TEntity, Tkey> Repository<TEntity, Tkey>()where TEntity : class
        {

            var entitykey = typeof(TEntity).Name;//"key here is entityname"
            if (!_repositories.ContainsKey(entitykey))
            {
                var repositoryType = typeof(GenaricRepository<,>);//هنا ببعتله ال GenaricRepo<User,int>مثلا يعنى بحدد TEntity,TKey
                //ده هيرجعلى ال instance اللى انا عاوزها
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity), typeof(Tkey)), _context);
                _repositories.Add(entitykey, repositoryInstance);
            }
            return (IGenaricRepository<TEntity, Tkey>)_repositories[entitykey];
        }
    }
}