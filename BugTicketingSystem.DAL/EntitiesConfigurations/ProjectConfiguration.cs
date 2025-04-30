using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugTicketingSystem.DAL
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(255).IsRequired();
            builder.Property(p => p.Status).IsRequired().HasConversion<string>();
        }
    }
}
