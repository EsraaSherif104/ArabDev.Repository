using ArabDev.Data.Contexts;
using ArabDev.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace ArabDevCommunity.PL.Extention
{
    public static class IdentityServicesExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection Services)
        {
            Services.AddIdentity<User, IdentityRole>()
                            .AddEntityFrameworkStores<ArabDevDbContext>();

            Services.AddAuthentication();// usermanger / signinManager / role manager

            return Services;
        }
    }
}