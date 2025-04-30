
using Microsoft.EntityFrameworkCore;

namespace BugTicketingSystem.DAL
{
    public class BugRepository : GenericRepository<Bug>, IBugRepository
    {
        private readonly DatabaseContext _ctx;

        public BugRepository(DatabaseContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public async Task<Bug?> GetBugByID(Guid id)
        {
            return await _ctx.Set<Bug>().AsNoTracking().Include(b => b.Attachments).FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
