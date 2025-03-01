using ArabDev.Data.Contexts;
using ArabDev.Data.Entities;
using ArabDev.Repository.Seeding;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ArabDevCommunity.PL.Helper
{
    public class AppliySeeding
    {
        public static async Task ApplySeedingAsync(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var context = services.GetRequiredService<ArabDevDbContext>();
                    await context.Database.MigrateAsync();
                    var userManager=services.GetRequiredService<UserManager<User>>();
                    await ApplySeedingDbcontext.SeedUserAsync(userManager);

                    await ApplySeedingDbcontext.SeedAsync(context, loggerFactory);
                  //  await DevContextSeeding.SeedAsync(context, loggerFactory);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<AppliySeeding>();
                    logger.LogError(ex.Message);
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"InnerException: {ex.InnerException.Message}");
                    }
                }
            }

        }
    }
}
    

