using ArabDev.Repository.Specification.UserSpecification;
using ArabDev.Services.Services.DTOS;
using ArabDev.Services.Services.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabDev.Services.Services.Users
{
    public interface IUserService
    {
        Task<UserDetailsDto> GetUserByIdAsync(string? UserId);

        Task<PaginateResultDto<UserDetailsDto>> GetAllUserAsync(UserSpecification spec);
        Task<UserDetailsDto> AddOrUpdatePictureAsync(UserupdataPictureDTo pictureDto);

        Task<UserDetailsDto> UpdateUserDetailsAsync(UserDetailsDto dto);

        Task DeleteUserAsync(string userId);





    }
}