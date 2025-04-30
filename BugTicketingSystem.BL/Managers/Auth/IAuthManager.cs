namespace BugTicketingSystem.BL
{
    public interface IAuthManager
    {
        Task<string> LoginAsync(UserLoginDTO dto);
    }
}
