using BugTicketingSystem.DAL;

namespace BugTicketingSystem.BL
{
    public class UserManagerUpdateDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal? Salary { get; set; }
        public int? Age { get; set; }
        public string Email { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        //public virtual ICollection<UserBug> UserBugs { get; set; } = new HashSet<UserBug>();
    }
}
