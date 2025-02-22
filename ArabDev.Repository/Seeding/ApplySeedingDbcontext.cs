using ArabDev.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabDev.Repository.Seeding
{
    public static class ApplySeedingDbcontext
    {
        public static async Task SeedUserAsync(UserManager<User> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new User()
                {
                    DisplayName="esraa sherif",
                    UserName = "Esraa_Sherif",
                    Email = "esraasherif9992@gmail.com",
                    PhoneNumber = "01063277063",
                    Interests = new List<string> { "Coding", "Reading" },
                    Job = "Software Engineer",
                    PictureUrl = "5.jpg",
                    Address = "Tants"

                };

                var result = await userManager.CreateAsync(user, "Esraa104.com");
                if (result.Succeeded)
                {
                    Console.WriteLine("Admin user created successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to create admin user:");

                }




            }
        }

    }
}
