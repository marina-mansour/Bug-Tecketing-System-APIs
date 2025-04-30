namespace BugTicketingSystem.DAL
{
    public interface IBugRepository:IGenericRepository<Bug>
    {
        Task<Bug?> GetBugByID(Guid id);
    }
}
