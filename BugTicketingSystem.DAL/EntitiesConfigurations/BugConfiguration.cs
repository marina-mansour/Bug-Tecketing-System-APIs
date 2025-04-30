using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugTicketingSystem.DAL
{
    public class BugConfiguration : IEntityTypeConfiguration<Bug>
    {
        public void Configure(EntityTypeBuilder<Bug> builder)
        {
            builder.Property(u => u.Name).HasMaxLength(255).IsRequired();
            builder.Property(u => u.Risk).IsRequired().HasConversion<string>();
            builder
            .HasOne(b => b.Projects)
            .WithMany(p => p.Bugs)
            .HasForeignKey(b => b.ProjectID);
        }
    }
}
