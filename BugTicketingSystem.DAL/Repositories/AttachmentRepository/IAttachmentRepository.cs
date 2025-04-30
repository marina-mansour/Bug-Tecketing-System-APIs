namespace BugTicketingSystem.DAL
{
    public interface IAttachmentRepository:IGenericRepository<Attachment>
    {
        Task<List<Attachment>?> GetAttachmentsByBugIdAsync(Guid bugId);
        Task<Attachment?> AddAttachmentAsync(Guid bugId, Attachment attachment);
        Task<bool> DeleteAttachmentAsync(Guid bugId, Guid attachmentId);
        Task<Attachment?> GetAttachmentByIdAndBugIdAsync(Guid attachmentId, Guid bugId);
    }
}
