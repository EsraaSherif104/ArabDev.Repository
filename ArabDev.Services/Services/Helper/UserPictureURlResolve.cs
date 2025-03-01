using ArabDev.Data.Entities;
using ArabDev.Services.Services.DTOS;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabDev.Services.Services.Helper
{
    public class UserPictureURlResolve : IValueResolver<User, UserDetailsDto, string>
    {
        private readonly IConfiguration _configuration;

        public UserPictureURlResolve(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public string Resolve(User source, UserDetailsDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_configuration["ApiBaseUrl"]}{source.PictureUrl}";

            }
            return string.Empty;
        }
    }
}