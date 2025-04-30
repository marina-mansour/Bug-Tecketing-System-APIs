using BugTicketingSystem.DAL;

namespace BugTicketingSystem.BL
{
    public class UserRegisterDTO
    {
        public string Name { get; set; } = string.Empty;
        public int? Age { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
