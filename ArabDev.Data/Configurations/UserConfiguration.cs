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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(x => x.UserName).IsRequired().HasMaxLength(200);
            builder.HasIndex(x => x.UserName) // إضافة فهرس لجعل اسم المستخدم فريدًا
                   .IsUnique();
            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(256);

            // Add unique index for Email
            builder.HasIndex(u => u.Email)
                   .IsUnique();


        }
    }
}