namespace BugTicketingSystem.DAL
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ProjectStatus Status { get; set; }
        public virtual ICollection<Bug> Bugs { get; set; } = new HashSet<Bug>();
    }

    public enum ProjectStatus
    {
        NotStarted,
        InProgress,
        Completed,
        Cancelled
    }
}
