using ArabDev.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabDev.Repository.Specification
{
    public class SpecificationEvaluator<TEntity, TKey> where TEntity : class
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;
            if (spec.Criteria is not null)
                query = query.Where(spec.Criteria);

            if (spec.Orderby is not null)
                query = query.OrderBy(spec.Orderby);

            if (spec.orderbyDesc is not null)
                query = query.OrderByDescending(spec.orderbyDesc);

            if (spec.IsPaginated)
                query = query.Skip(spec.Skip).Take(spec.Take);


            query = spec.includes.Aggregate(query, (current, includeExpression) => current.Include(includeExpression));
            return query;

        }

    }
}