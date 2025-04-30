namespace BugTicketingSystem.BL
{
    public class UserTokenDTO
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiry { get; set; }
    }
}
