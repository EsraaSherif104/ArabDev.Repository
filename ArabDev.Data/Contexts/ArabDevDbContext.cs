using ArabDev.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabDev.Data.Contexts
{
    internal class ArabDevDbContext :IdentityDbContext<User>
    {
    }
}
