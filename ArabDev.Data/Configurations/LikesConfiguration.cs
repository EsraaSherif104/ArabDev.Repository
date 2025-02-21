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
    public class LikesConfiguration : IEntityTypeConfiguration<Likes>
    {
        public void Configure(EntityTypeBuilder<Likes> builder)
        {
            builder.Property(x => x.Id).UseIdentityColumn(1, 1);

            builder.HasOne(l => l.Users)
                   .WithMany(u => u.Likes) // تأكد أن لديك ICollection<Likes> داخل User
                   .HasForeignKey(l => l.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(l => l.Post)
                   .WithMany(p => p.Likes) // تأكد أن لديك ICollection<Likes> داخل Post
                   .HasForeignKey(l => l.PostId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(l => l.PodCast)
                   .WithMany(p => p.Likes) // تأكد أن لديك ICollection<Likes> داخل PodCast
                   .HasForeignKey(l => l.PodCastId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}