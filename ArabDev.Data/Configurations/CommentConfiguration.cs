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
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasOne(c => c.User)
        .WithMany(u => u.Comments) // تأكد أن لديك ICollection<Comment> في User
        .HasForeignKey(c => c.UserId)
        .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Post)
                   .WithMany(p => p.Comments)
                   .HasForeignKey(c => c.PostId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.PodCast)
                   .WithMany(p => p.Comments)
                   .HasForeignKey(c => c.PodCastId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}