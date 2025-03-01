using ArabDev.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabDev.Data.Services
{
    public interface ITokenServices
    {
        Task<string> CreateTokenAsync(User user,UserManager<User> userManager);
    }
}
