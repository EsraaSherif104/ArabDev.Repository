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
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.Property(x => x.Id).UseIdentityColumn(1, 1);

            builder.HasOne(n => n.Users)
                   .WithMany(u => u.Notifications) // تأكد أن لديك ICollection<Notification> داخل User
                   .HasForeignKey(n => n.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(n => n.Post)
                   .WithMany(p => p.Notifications) // تأكد أن لديك ICollection<Notification> داخل Post
                   .HasForeignKey(n => n.PostId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}