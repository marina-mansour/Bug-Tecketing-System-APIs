using BugTicketingSystem.DAL;

namespace BugTicketingSystem.BL
{
    public interface IProjectManager
    {
        Task<List<ProjectReadDTO>> GetAllAsync();
        Task<ProjectReadDTO> GetProjectByIDAsync(Guid id);
        Task<GeneralResult> AddAsync(ProjectAddDTO prjDTO);
    }
}
