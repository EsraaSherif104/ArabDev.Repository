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
    public class SharesConfigurations : IEntityTypeConfiguration<Shares>
    {
        public void Configure(EntityTypeBuilder<Shares> builder)
        {
            builder.Property(x => x.Id).UseIdentityColumn(1, 1);

            builder.HasOne(s => s.User)
                   .WithMany(u => u.Shares) // تأكد أن لديك ICollection<Shares> داخل User
                   .HasForeignKey(s => s.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Post)
                   .WithMany(p => p.Shares) // تأكد أن لديك ICollection<Shares> داخل Post
                   .HasForeignKey(s => s.PostId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}