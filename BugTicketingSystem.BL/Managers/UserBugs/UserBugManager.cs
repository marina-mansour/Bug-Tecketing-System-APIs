using BugTicketingSystem.DAL;
using BugTrackingSystem.BL;
using FluentValidation;

namespace BugTicketingSystem.BL
{
    public class UserBugManager : IUserBugManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserBugsAddDtoValidator _validationRules;

        public UserBugManager(IUnitOfWork unitOfWork, UserBugsAddDtoValidator validationRules)
        {
            _unitOfWork = unitOfWork;
            _validationRules = validationRules;
        }

        public async Task<GeneralResult> AddAsync(Guid bugId,UserBugsAddDTO userBugDTO)
        {
            var validationResult = await _validationRules.ValidateAsync(userBugDTO);
            if (!(validationResult.IsValid))
            {
                return new GeneralResult
                {
                    Success = false,
                    Errors = validationResult.Errors.Select(e => new ResultError
                    {
                        Code = e.ErrorCode,
                        Msg = e.ErrorMessage
                    }).ToArray()
                };
            }

            var newUserBug = new UserBug()
            {
                BugId = bugId,
                UserId = userBugDTO.UserId,
            };

            _unitOfWork.UserBugRepository.Add(newUserBug);
            await _unitOfWork.SaveChangesAsync();

            return new GeneralResult
            {
                Success = true,
                Errors = []
            };
        }

        public async Task<GeneralResult> DeleteAsync(Guid userId, Guid bugId)
        {
            var delUser = await _unitOfWork.UserBugRepository.getByCompositeIdAsync(userId, bugId);
            if (delUser == null)
            {
                return new GeneralResult
                {
                    Success = false,
                    Errors = [new ResultError() { Msg = "User that is not assigned to a this bug!" }]
                };
            }
                _unitOfWork.UserBugRepository.Delete(delUser);
                await _unitOfWork.SaveChangesAsync();

            return new GeneralResult
            {
                Success = true,
                Errors = []
            };
        }

        public async Task<List<UserBugReadDTO>> GetAllAsync()
        {
            var getUsers = await _unitOfWork.UserBugRepository.getAllAsync();

            return getUsers.Select(u => new UserBugReadDTO
            {
                UserId = u.UserId,
                BugId = u.BugId,
            }).ToList();
        }
    }
}
