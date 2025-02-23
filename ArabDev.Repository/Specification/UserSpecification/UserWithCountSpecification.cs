using ArabDev.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabDev.Repository.Specification.UserSpecification
{
    public class UserWithCountSpecification : BaseSpecification<User>
    {
        public UserWithCountSpecification(UserSpecification spec)
           : base(u =>
               (string.IsNullOrEmpty(spec.Id) || u.Id == spec.Id)
&&
               (string.IsNullOrEmpty(spec.Job) || u.Job == spec.Job) &&
               (string.IsNullOrEmpty(spec.Address) || u.Address.Contains(spec.Address)))
        {

        }

    }
}