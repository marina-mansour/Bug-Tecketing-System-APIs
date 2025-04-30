using BugTicketingSystem.DAL;

namespace BugTicketingSystem.BL
{
    public interface IAttachmentManager
    {
        Task<GeneralResult> AddAsync(Guid bugId, AttachmentAddDTO attDTO);
        Task<GeneralResult> DeleteAsync(Guid bugId, Guid attId);

        Task<GeneralResult> GetAttachmentsByBugIdAsync(Guid bugId);
    }
}
