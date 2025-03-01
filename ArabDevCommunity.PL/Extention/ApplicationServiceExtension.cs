using ArabDev.Repository.Interface;
using ArabDev.Repository.Repositories;
using ArabDev.Services.Services.Helper;
using ArabDev.Services.Services.Users;
using ArabDevCommunity.PL.Error;
using Microsoft.AspNetCore.Mvc;

namespace ArabDevCommunity.PL.Extention
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<IUserService, UserService>();
            Services.AddAutoMapper(typeof(MappingProfile));
            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ActionContext =>
                {
                    var errors = ActionContext.ModelState
                                            .Where(model => model.Value?.Errors.Count > 0)
                                            .SelectMany(model => model.Value?.Errors)
                                            .Select(error => error.ErrorMessage)
                                            .ToList();

                    var errorRespone = new ValidationErrorRespone
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(errorRespone);
                };
            });
            return Services;
        }

    }
}
