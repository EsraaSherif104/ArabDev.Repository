using ArabDev.Data.Contexts;
using ArabDev.Data.Entities;
using ArabDev.Data.Services;
using ArabDev.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

namespace ArabDevCommunity.PL.Extention
{
    public static class IdentityServicesExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection Services)
        {
            Services.AddScoped<ITokenServices, TokenServices>();

            Services.AddIdentity<User, IdentityRole>()
                            .AddEntityFrameworkStores<ArabDevDbContext>();

            Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);// usermanger / signinManager / role manager

            return Services;
        }
    }
}