using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugTicketingSystem.DAL
{
    public class UserBugConfiguration : IEntityTypeConfiguration<UserBug>
    {
        public void Configure(EntityTypeBuilder<UserBug> builder)
        {
            builder
            .HasKey(ub => new { ub.UserId, ub.BugId });
        }
    }
}
