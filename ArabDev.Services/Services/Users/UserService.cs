using ArabDev.Data.Entities;
using ArabDev.Repository.Interface;
using ArabDev.Repository.Specification.UserSpecification;
using ArabDev.Services.Services.DTOS;
using ArabDev.Services.Services.Helper;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabDev.Services.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UserService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            this._logger = logger;
        }



        public async Task<PaginateResultDto<UserDetailsDto>> GetAllUserAsync(UserSpecification input)
        {
            var spec = new UserWithSpecification(input);
            var users = await _unitOfWork.Repository<User, int>().GetAllWithSpecificationAsync(spec);
            var countSpec = new UserWithCountSpecification(input);
            var count = await _unitOfWork.Repository<User, int>().GetcountSpecificationAsync(countSpec);
            var mappedUsers = _mapper.Map<IReadOnlyList<UserDetailsDto>>(users);

            return new PaginateResultDto<UserDetailsDto>(input.PageIndex, input.PageSize, count, mappedUsers);
        }

        public async Task<UserDetailsDto> GetUserByIdAsync(string? UserId)
        {
            if (UserId is null)
                throw new Exception("Id Is NUll");

            var spac = new UserWithSpecification(UserId);
            var user = await _unitOfWork.Repository<ArabDev.Data.Entities.User, int>().GetWithSpecificationByIdAsync(spac);
            if (user is null)
                throw new Exception("User Not eXIST");
            var mappedUser = _mapper.Map<UserDetailsDto>(user);

            return mappedUser;
        }
        public async Task<UserDetailsDto> AddOrUpdatePictureAsync(UserupdataPictureDTo pictureDto)
        {
            // التحقق من وجود الملف
            if (pictureDto?.Picture == null || pictureDto.Picture.Length == 0)
                throw new ArgumentException("No file was uploaded.");

            // التحقق من نوع الملف
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(pictureDto.Picture.FileName).ToLower();
            if (!allowedExtensions.Contains(fileExtension))
                throw new ArgumentException("Invalid file type. Only JPG, JPEG, and PNG are allowed");

            // التحقق من حجم الملف (5MB كحد أقصى)
            if (pictureDto.Picture.Length > 5 * 1024 * 1024)
                throw new ArgumentException("File size exceeds the allowed limit (5MB).");

            // البحث عن المستخدم
            var user = await _unitOfWork.Repository<User, string>().GetByIdAsync(pictureDto.UserId);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {pictureDto.UserId} not found..");

            // حذف الصورة القديمة إن وجدت
            if (!string.IsNullOrEmpty(user.PictureUrl))
            {
                var oldFileName = Path.GetFileName(user.PictureUrl);
                DocumentSetting.DeleteFile(oldFileName, "Images");
            }

            // رفع الصورة الجديدة
            var savedFilePath = await DocumentSetting.UploadFileAsync(pictureDto.Picture, "Images");
            user.PictureUrl = savedFilePath;

            // تحديث البيانات وحفظها
            _unitOfWork.Repository<User, string>().UpdateAsync(user);
            await _unitOfWork.CompleteAync();

            return _mapper.Map<UserDetailsDto>(user);
        }

        public async Task<UserDetailsDto> UpdateUserDetailsAsync(UserDetailsDto dto)
        {
            if (string.IsNullOrEmpty(dto.Id))
                throw new ArgumentException("User ID is required");

            var user = await _unitOfWork.Repository<User, string>().GetByIdAsync(dto.Id);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            _mapper.Map(dto, user);

            _unitOfWork.Repository<User, string>().UpdateAsync(user);
            await _unitOfWork.CompleteAync();

            return _mapper.Map<UserDetailsDto>(user);
        }

        public async Task DeleteUserAsync(string userId)
        {
            var userRepository = _unitOfWork.Repository<User, string>();
            var user = await userRepository.GetByIdAsync(userId);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            await userRepository.DeleteAsync(user);
            await _unitOfWork.CompleteAync();
        }

        public async Task<List<UserDetailsDto>> SearchUsersByNameAsync(string userName)
        {
            var users = await _unitOfWork.Repository<User, string>().SearchByNameAsync(userName);
            return _mapper.Map<List<UserDetailsDto>>(users);
        }
    }
}
    
    
