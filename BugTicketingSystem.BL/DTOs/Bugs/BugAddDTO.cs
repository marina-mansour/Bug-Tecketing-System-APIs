using BugTicketingSystem.DAL;

namespace BugTicketingSystem.BL
{
    public class BugAddDTO
    {
        public string Name { get; set; } = string.Empty;
        public BugRisk Risk { get; set; }
        public Guid? ProjectID { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; } = new HashSet<Attachment>();
    }
}
