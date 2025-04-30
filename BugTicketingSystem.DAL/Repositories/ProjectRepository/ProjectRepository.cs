using Microsoft.EntityFrameworkCore;

namespace BugTicketingSystem.DAL
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        private readonly DatabaseContext _ctx;

        public ProjectRepository(DatabaseContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<Project>> GetAllWithBugsAsync()
        {
            return await _ctx.Set<Project>().Include(p => p.Bugs).AsNoTracking().ToListAsync();
        }

        public async Task<Project> GetByIdWithBugsAsync(Guid id)
        {
            return await _ctx.Set<Project>().Include(p => p.Bugs).AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
