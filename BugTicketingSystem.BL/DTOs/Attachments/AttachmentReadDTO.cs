using BugTicketingSystem.DAL;

namespace BugTicketingSystem.BL
{
    public class AttachmentReadDTO
    {
        public Guid Id { get; set; }
        //public string ImgLink { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public Guid? BugID { get; set; }
    }
}
