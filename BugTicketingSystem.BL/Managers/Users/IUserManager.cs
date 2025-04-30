using System.Security.Claims;
using BugTicketingSystem.DAL;

namespace BugTicketingSystem.BL
{
    public interface IUserManager
    {
        Task<List<UserReadDTO>> GetAllAsync();
        Task<UserReadDTO> GetByIdAsync(Guid id);
        Task<GeneralResult> AddAsync(UserRegisterDTO userDTO);
        Task<GeneralResult> UpdateAsync(UserUpdateDTO userDTO);
        Task<GeneralResult> ManagerUpdateAsync(UserManagerUpdateDTO userDTO);
        Task<GeneralResult> DeleteAsync(User userDTO);
        Task<IEnumerable<string>> GetManagerDataAsync(ClaimsPrincipal userClaims);
        Task<IEnumerable<string>> GetTestingDataAsync(ClaimsPrincipal userClaims);
        Task<IEnumerable<string>> GetDevelopingDataAsync(ClaimsPrincipal userClaims);
    }
}
