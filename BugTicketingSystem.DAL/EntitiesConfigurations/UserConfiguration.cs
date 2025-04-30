using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BugTicketingSystem.DAL
{
    public class UserConfiguration:IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Salary).HasColumnType("decimal(8,2)");
            builder.Property(u => u.Age).HasColumnType("int");
            builder.Property(u => u.Name).HasMaxLength(255).IsRequired();
            builder.Property(u => u.Role).IsRequired().HasConversion<string>();
            //builder
            //.HasMany(u => u.Bugs)
            //.WithMany(b => b.Users);
        }
    }
}
