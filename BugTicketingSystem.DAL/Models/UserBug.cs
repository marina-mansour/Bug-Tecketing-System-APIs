namespace BugTicketingSystem.DAL
{
    public class UserBug
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;
        public Guid BugId { get; set; }
        public virtual Bug Bug { get; set; } = null!;
    }
}
