using BugTicketingSystem.DAL;

namespace BugTicketingSystem.BL
{
    public interface IBugManager
    {
        Task<List<BugReadDTO>> GetAllAsync();
        Task<BugReadDTO> GetBugByIDAsync(Guid id);
        Task<GeneralResult> AddAsync(BugAddDTO bugDTO);
    }
}
