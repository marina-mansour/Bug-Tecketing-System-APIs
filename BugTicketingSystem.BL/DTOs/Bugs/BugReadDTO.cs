using System.Text.Json.Serialization;
using BugTicketingSystem.DAL;

namespace BugTicketingSystem.BL
{
    public class BugReadDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BugRisk Risk { get; set; }
        public Guid? ProjectID { get; set; }
        public virtual ICollection<AttachmentReadDTO> Attachments { get; set; } = new HashSet<AttachmentReadDTO>();
    }
}
