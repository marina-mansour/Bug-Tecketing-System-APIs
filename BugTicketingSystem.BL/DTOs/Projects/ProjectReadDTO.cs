using System.Text.Json.Serialization;
using BugTicketingSystem.DAL;

namespace BugTicketingSystem.BL
{
    public class ProjectReadDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ProjectStatus Status { get; set; }
        public virtual ICollection<BugReadDTO> Bugs { get; set; } = new HashSet<BugReadDTO>();
    }
}
