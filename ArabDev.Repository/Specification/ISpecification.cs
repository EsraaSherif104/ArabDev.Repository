using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ArabDev.Repository.Specification
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        Expression<Func<T, object>> Orderby { get; }
        Expression<Func<T, object>> orderbyDesc { get; }



        List<Expression<Func<T, object>>> includes { get; }

        int Take { get; }
        int Skip { get; }
        bool IsPaginated { get; }
    }
}