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
    public class SavedPostConfiguration : IEntityTypeConfiguration<SavedPost>
    {
        public void Configure(EntityTypeBuilder<SavedPost> builder)
        {

            //builder.HasKey(sp => new { sp.UserId, sp.PostId ,sp.Id});
            builder.Property(x => x.Id).UseIdentityColumn(1, 1);

            builder.HasOne(U => U.User)
                .WithMany(s => s.SavedPosts)
                .HasForeignKey(U => U.UserId);
            builder.HasOne(P => P.Post)
                .WithMany(s => s.SavedPosts)
                .HasForeignKey(P => P.PostId);

        }
    }
}