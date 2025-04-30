using System.Text.Json.Serialization;
using BugTicketingSystem.DAL;

namespace BugTicketingSystem.BL
{
    public class UserReadDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserRole Role { get; set; }
        public virtual ICollection<UserBug> UserBugs { get; set; } = new HashSet<UserBug>();

    }
}
