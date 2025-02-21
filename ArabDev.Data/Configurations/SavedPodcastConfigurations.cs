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
    public class SavedPodcastConfigurations : IEntityTypeConfiguration<SavedPodcast>
    {
        public void Configure(EntityTypeBuilder<SavedPodcast> builder)
        {

            //builder.HasKey(sp => new { sp.UserId, sp.PodCastId,sp.Id });
            builder.Property(x => x.Id).UseIdentityColumn(1, 1);
            builder.HasOne(U => U.User)
                .WithMany(s => s.SavedPodcast)
                .HasForeignKey(U => U.UserId);
            // .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(sp => sp.PodCast)
                .WithMany(s => s.SavedPodcasts)
                .HasForeignKey(sp => sp.PodCastId);
            // .OnDelete(DeleteBehavior.NoAction) ;

        }
    }
}