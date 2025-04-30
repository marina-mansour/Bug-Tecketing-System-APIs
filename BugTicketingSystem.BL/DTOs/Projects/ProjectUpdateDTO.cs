using BugTicketingSystem.DAL;

namespace BugTicketingSystem.BL
{
    public class ProjectUpdateDTO
    {
        public string Name { get; set; } = string.Empty;
        public ProjectStatus Status { get; set; }
        public virtual ICollection<Bug> Bugs { get; set; } = new HashSet<Bug>();
    }
}
