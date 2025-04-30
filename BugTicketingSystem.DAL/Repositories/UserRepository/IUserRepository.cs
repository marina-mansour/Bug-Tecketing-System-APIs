namespace BugTicketingSystem.DAL
{
    public interface IUserRepository:IGenericRepository<User>
    {
        Task<User> GetUserByEmailAsync(string username);
    }
}
