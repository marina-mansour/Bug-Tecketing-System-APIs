using BugTicketingSystem.DAL;

namespace BugTicketingSystem.BL
{
    public class ProjectAddDTO
    {
        public string Name { get; set; } = string.Empty;
        public ProjectStatus Status { get; set; }
    }
}
