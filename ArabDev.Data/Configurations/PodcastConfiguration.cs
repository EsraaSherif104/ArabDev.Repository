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
    public class PodcastConfiguration : IEntityTypeConfiguration<PodCast>
    {
        public void Configure(EntityTypeBuilder<PodCast> builder)
        {
            builder.Property(x => x.Id).UseIdentityColumn(1, 1);
            builder.HasOne(p => p.User)
                .WithMany(u => u.PodCasts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}