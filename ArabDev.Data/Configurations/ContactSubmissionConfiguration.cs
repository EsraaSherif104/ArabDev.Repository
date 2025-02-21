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
    public class ContactSubmissionConfiguration : IEntityTypeConfiguration<ContactSubmission>
    {
        public void Configure(EntityTypeBuilder<ContactSubmission> builder)
        {
            builder.Property(x => x.Id).UseIdentityColumn(1, 1);
            builder.Property(c => c.Name).HasMaxLength(100).IsRequired();
            builder.Property(c => c.Email).HasMaxLength(255).IsRequired();
            builder.Property(c => c.Message).IsRequired();

        }
    }
}