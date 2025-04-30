using Microsoft.AspNetCore.Http;
using BugTicketingSystem.DAL;

namespace BugTicketingSystem.BL
{
    public class AttachmentAddDTO
    {
        //public string ImgLink { get; set; } = string.Empty;
        public IFormFile File { get; set; } = null!;
        public Guid BugID { get; set; }
    }
}
