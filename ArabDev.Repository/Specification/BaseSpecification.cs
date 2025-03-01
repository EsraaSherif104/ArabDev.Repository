using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ArabDev.Repository.Specification
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> includes { get; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> Orderby { get; private set; }

        public Expression<Func<T, object>> orderbyDesc { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPaginated { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> include)
        => includes.Add(include);

        protected void Addorderby(Expression<Func<T, object>> orderbyExpression)
        => Orderby = orderbyExpression;
        protected void AddorderbyAcend(Expression<Func<T, object>> orderbyDescExpression)
                => orderbyDesc = orderbyDescExpression;


        protected void ApplyPagination(int skip, int take)
        {
            Take = take;
            Skip = skip;
            IsPaginated = true;
        }


    }
}