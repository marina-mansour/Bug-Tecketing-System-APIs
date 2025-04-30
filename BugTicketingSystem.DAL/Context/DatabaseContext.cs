using Microsoft.EntityFrameworkCore;
namespace BugTicketingSystem.DAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Bug> Bugs => Set<Bug>();
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<Attachment> Attachments => Set<Attachment>();
        public DbSet<UserBug> UserBugs => Set<UserBug>();

    }
}
