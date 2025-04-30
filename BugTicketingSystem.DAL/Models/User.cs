namespace BugTicketingSystem.DAL
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal? Salary { get; set; }
        public int? Age { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public virtual ICollection<UserBug> UserBugs { get; set; } = new HashSet<UserBug>();

    }

    public enum UserRole
    {
        Manager,
        Developer,
        Tester
    }
}
