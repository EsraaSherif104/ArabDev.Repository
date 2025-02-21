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
    public class SkillsConfiguration : IEntityTypeConfiguration<Skills>
    {
        public void Configure(EntityTypeBuilder<Skills> builder)
        {
            builder.Property(x => x.Id).UseIdentityColumn(1, 1);

            builder.HasOne(s => s.Users)
                   .WithMany(u => u.Skills) // تأكد أن لديك ICollection<Skills> داخل User
                   .HasForeignKey(s => s.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}