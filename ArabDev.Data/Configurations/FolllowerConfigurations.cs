using ArabDev.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabDev.Data.Configurations
{
    public class FolllowerConfigurations : IEntityTypeConfiguration<Follower>
    {
        public void Configure(EntityTypeBuilder<Follower> builder)
        {
            builder.HasKey(f => new { f.FollowerUserId, f.FollowedUserId }); // Composite Key

            builder.HasOne(f => f.FollowerUser)
                .WithMany(u => u.Following) // المستخدم الذي يتابع
                .HasForeignKey(f => f.FollowerUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(f => f.FollowedUser)
                .WithMany(u => u.Followers) // المستخدم الذي يُتابع
                .HasForeignKey(f => f.FollowedUserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}