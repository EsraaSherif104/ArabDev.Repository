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
    public class User_CurrentlyLearning : IEntityTypeConfiguration<User_Learning>
    {
        public void Configure(EntityTypeBuilder<User_Learning> builder)
        {
            builder.HasOne(ul => ul.User)
               .WithMany(u => u.User_Learnings) // تحديد العلاقة بين User و User_Learning
               .HasForeignKey(ul => ul.UserId)  // تأكد من أن الـ Foreign Key هو UserId
               .OnDelete(DeleteBehavior.NoAction);  // أو تغيير السلوك حسب الحاجة

        }
    }
}