namespace BugTicketingSystem.DAL
{
    public class Attachment
    {
        public Guid Id { get; set; }
        //public string ImgLink { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public Guid? BugID { get; set; }
        public virtual Bug Bugs { get; set; } = null!;
    }

}
