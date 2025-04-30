namespace BugTicketingSystem.DAL
{
    public interface IProjectRepository:IGenericRepository<Project>
    {
        Task<List<Project>> GetAllWithBugsAsync();
        Task<Project> GetByIdWithBugsAsync(Guid id);

    }
}
