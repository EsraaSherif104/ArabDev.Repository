using ArabDev.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabDev.Repository.Specification.UserSpecification
{
    public class UserWithSpecification : BaseSpecification<User>
    {
        public UserWithSpecification(UserSpecification spec)
            : base(u =>
                (string.IsNullOrEmpty(spec.Id) || u.Id == spec.Id)
 &&
                (string.IsNullOrEmpty(spec.Job) || u.Job == spec.Job) &&
                (string.IsNullOrEmpty(spec.Address) || u.Address.Contains(spec.Address)) &&
                (string.IsNullOrEmpty(spec.Search) || u.UserName.Trim().ToLower().Contains(spec.Search.ToLower()))
            )
        {
            AddInclude(u => u.Posts);
            AddInclude(u => u.Skills);
            Addorderby(u => u.UserName);
            ApplyPagination(spec.PageSize * (spec.PageIndex - 1), spec.PageSize);

            if (!string.IsNullOrEmpty(spec.Sort))
            {
                switch (spec.Sort)
                {
                    case "NameDesc":
                        AddorderbyAcend(u => u.UserName);
                        break;
                    default:
                        Addorderby(u => u.UserName);
                        break;
                }
            }
        }

        public UserWithSpecification(string? id) : base(u => u.Id == id)
        {
            AddInclude(u => u.Posts);
            AddInclude(u => u.Skills);
            Addorderby(u => u.UserName);
        }
    }
}