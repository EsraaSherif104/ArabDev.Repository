﻿using ArabDev.Data.Contexts;
using ArabDev.Data.Entities;
using ArabDev.Data.Services;
using ArabDev.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ArabDevCommunity.PL.Extention
{
    public static class IdentityServicesExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection Services,IConfiguration configuration)
        {
            Services.AddScoped<ITokenServices, TokenServices>();

            Services.AddIdentity<User, IdentityRole>()
                            .AddEntityFrameworkStores<ArabDevDbContext>();
           
            
            
            Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                           .AddJwtBearer(options =>
                           {
                               options.TokenValidationParameters = new TokenValidationParameters()
                               {
                                   ValidateIssuer = true,
                                   ValidIssuer = configuration["JWT:VaildIssuer"],
                                   ValidateAudience = true,
                                   ValidAudience = configuration["JWT:VaildAudience"],
                                   ValidateLifetime = true,
                                   ValidateIssuerSigningKey = true,
                                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))

                               };
                           });

            return Services;
        }
    }
}