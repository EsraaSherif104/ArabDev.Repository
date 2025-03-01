using ArabDev.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArabDev.Data.Contexts
{
    public class ArabDevDbContext :IdentityDbContext<User>
    {
        public ArabDevDbContext(DbContextOptions<ArabDevDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //this Line For Configurations If Exists 
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User_Learning> User_Learnings { get; set; }
        public DbSet<Likes> Likes { get; set; }
        public DbSet<Follower> Followers { get; set; }
        public DbSet<ContactSubmission> ContactSubmission { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<PodCast> PodCasts { get; set; }
        public DbSet<SavedPodcast> SavedPodcasts { get; set; }

        public DbSet<SavedPost> SavedPosts { get; set; }
        public DbSet<Shares> Shares { get; set; }



    }
}
    

