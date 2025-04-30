namespace BugTicketingSystem.DAL
{
    public class UserBugRepository : GenericRepository<UserBug>, IUserBugRepository
    {
        private readonly DatabaseContext _ctx;

        public UserBugRepository(DatabaseContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public async Task<UserBug?> getByCompositeIdAsync(Guid userId, Guid bugId)
        {
            return await _ctx.Set<UserBug>().FindAsync(userId, bugId);
        }
    }
}
