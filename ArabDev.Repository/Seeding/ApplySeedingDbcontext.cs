using ArabDev.Data.Contexts;
using ArabDev.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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
        public static async Task SeedAsync(ArabDevDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
              

                if (context.Posts != null && !context.Posts.Any())
                {
                    //D:\FCI\GraduationProject\Final\ArabDev.Repository\ArabDev.Repository\Seeding\SeedingData\Post.json
                    var postsdata = File.ReadAllText("../ArabDev.Repository/Seeding/SeedingData/Post.json");
                    var post = JsonSerializer.Deserialize<List<Post>>(postsdata);
                    if (post is not null)
                    {
                        await context.Posts.AddRangeAsync(post);
                    }
                }
                if (context.Skills != null && !context.Skills.Any())
                {
                    //C:\Users\DELL\Downloads\ArabDevCommunityGrad.PL\ArabDev.Repository\seeding\seeding\Skills.json
                    var skillsdata = File.ReadAllText("../ArabDev.Repository/Seeding/SeedingData/Skiils.json");
                    var skill = JsonSerializer.Deserialize<List<Skills>>(skillsdata);
                    if (skill is not null)
                    {
                        await context.Skills.AddRangeAsync(skill);
                    }
                }
                if (context.User_Learnings != null && !context.User_Learnings.Any())
                {
                    //C:\Users\DELL\Downloads\ArabDevCommunityGrad.PL\ArabDev.Repository\seeding\seeding\User Learning.json
                    var userslearningdata = File.ReadAllText("../ArabDev.Repository/Seeding/SeedingData/UserLearning.json");
                    var userslearning = JsonSerializer.Deserialize<List<User_Learning>>(userslearningdata);
                    if (userslearning is not null)
                    {
                        await context.User_Learnings.AddRangeAsync(userslearning);
                    }
                }
                if (context.Shares != null && !context.Shares.Any())
                {
                    //c:\users\dell\downloads\arabdevcommunitygrad.pl\arabdev.repository\seeding\seeding\share.json
                    var sharesdata = File.ReadAllText("../ArabDev.Repository/Seeding/SeedingData/Share.json");
                    var share = JsonSerializer.Deserialize<List<Shares>>(sharesdata);
                    if (share is not null)
                    {
                        await context.Shares.AddRangeAsync(share);
                    }
                }
                if (context.SavedPosts != null && !context.SavedPosts.Any())
                {
                    // C: \Users\DELL\Downloads\ArabDevCommunityGrad.PL\ArabDev.Repository\seeding\seeding\SavedPost.json
                    var savedpostsdata = File.ReadAllText("../ArabDev.Repository/Seeding/SeedingData/SavedPost.json");
                    var savedpost = JsonSerializer.Deserialize<List<SavedPost>>(savedpostsdata);
                    if (savedpost is not null)
                    {
                        await context.SavedPosts.AddRangeAsync(savedpost);
                    }
                }
                if (context.PodCasts != null && !context.PodCasts.Any())
                {
                    //C:\Users\DELL\Downloads\ArabDevCommunityGrad.PL\ArabDev.Repository\seeding\seeding\PodCast.json
                    var podcastdata = File.ReadAllText("../ArabDev.Repository/Seeding/SeedingData/PodCast.json");
                    var podcast = JsonSerializer.Deserialize<List<PodCast>>(podcastdata);
                    if (podcast is not null)
                    {
                        await context.PodCasts.AddRangeAsync(podcast);
                    }
                }
                if (context.SavedPodcasts != null && !context.SavedPodcasts.Any())
                {
                    //C:\Users\DELL\Downloads\ArabDevCommunityGrad.PL\ArabDev.Repository\seeding\seeding\SavedPodcast.json
                    var savedpodcastdata = File.ReadAllText("../ArabDev.Repository/Seeding/SeedingData/SavedPodcast.json");
                    var savedpodcast = JsonSerializer.Deserialize<List<SavedPodcast>>(savedpodcastdata);
                    if (savedpodcast is not null)
                    {
                        await context.SavedPodcasts.AddRangeAsync(savedpodcast);
                    }
                }

                if (context.Notifications != null && !context.Notifications.Any())
                {
                    //C:\Users\DELL\Downloads\ArabDevCommunityGrad.PL\ArabDev.Repository\seeding\seeding\Notification.json
                    var notificationdata = File.ReadAllText("../ArabDev.Repository/Seeding/SeedingData/Notification.json");
                    var notification = JsonSerializer.Deserialize<List<Notification>>(notificationdata);
                    if (notification is not null)
                    {
                        await context.Notifications.AddRangeAsync(notification);
                    }
                }
                if (context.Likes != null && !context.Likes.Any())
                {
                    //C:\Users\DELL\Downloads\ArabDevCommunityGrad.PL\ArabDev.Repository\seeding\seeding\Likes.json
                    var likesdata = File.ReadAllText("../ArabDev.Repository/Seeding/SeedingData/Likes.json");
                    var likes = JsonSerializer.Deserialize<List<Likes>>(likesdata);
                    if (likes is not null)
                    {
                        await context.Likes.AddRangeAsync(likes);
                    }
                }
                if (context.ContactSubmission != null && !context.ContactSubmission.Any())
                {
                    //C:\Users\DELL\Downloads\ArabDevCommunityGrad.PL\ArabDev.Repository\seeding\seeding\ContactSubmission.json
                    var contactsdata = File.ReadAllText("../ArabDev.Repository/Seeding/SeedingData/Connect.json");
                    var contact = JsonSerializer.Deserialize<List<ContactSubmission>>(contactsdata);
                    if (contact is not null)
                    {
                        await context.ContactSubmission.AddRangeAsync(contact);
                    }
                }
                if (context.Comments != null && !context.Comments.Any())
                {
                    //C:\Users\DELL\Downloads\ArabDevCommunityGrad.PL\ArabDev.Repository\seeding\seeding\Comment.json
                    var commentsdata = File.ReadAllText("../ArabDev.Repository/Seeding/SeedingData/Comment.json");
                    var comment = JsonSerializer.Deserialize<List<Comment>>(commentsdata);
                    if (comment is not null)
                    {
                        await context.Comments.AddRangeAsync(comment);
                    }
                }
                //if (context.Followers != null && !context.Followers.Any())
                //{
                //    //C:\Users\DELL\Downloads\ArabDevCommunityGrad.PL\ArabDev.Repository\seeding\seeding\Follower.json
                //    var followerdata = File.ReadAllText("../ArabDev.Repository/seeding/seeding/Follower.json");
                //    var follower = JsonSerializer.Deserialize<List<Follower>>(followerdata);
                //    if (follower is not null)
                //    {
                //        await context.Followers.AddRangeAsync(follower);
                //    }
                //}
                await context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<ArabDevDbContext>();
                logger.LogError(ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"InnerExceptionis: {ex.InnerException.Message}");
                }
            }

        }
    }
}

    

