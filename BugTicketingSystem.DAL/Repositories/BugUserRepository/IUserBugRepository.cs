namespace BugTicketingSystem.DAL
{
    public interface IUserBugRepository : IGenericRepository<UserBug>
    {
        Task<UserBug?> getByCompositeIdAsync(Guid userId, Guid bugId);
    }
}
