using ArabDev.Data.Entities;
using ArabDev.Services.Services.DTOS;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ArabDev.Services.Services.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDetailsDto>()
                .ForMember(d => d.PictureUrl, o => o.MapFrom<UserPictureURlResolve>());

        }

    }
}