using BugTicketingSystem.DAL;
using BugTrackingSystem.BL;
using FluentValidation;

namespace BugTicketingSystem.BL
{
    public class BugManager : IBugManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BugAddDtoValidator _validationRules;

        public BugManager(IUnitOfWork unitOfWork,BugAddDtoValidator validationRules)
        {
            _unitOfWork = unitOfWork;
            _validationRules = validationRules;
        }
        public async Task<GeneralResult> AddAsync(BugAddDTO BugDTO)
        {
            var validationResult = await _validationRules.ValidateAsync(BugDTO);
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

            var newBug = new Bug()
            {
                Name = BugDTO.Name,
                Risk = BugDTO.Risk,
                ProjectID = BugDTO.ProjectID
            };

            _unitOfWork.BugRepository.Add(newBug);
            await _unitOfWork.SaveChangesAsync();

            return new GeneralResult
            {
                Success = true,
                Errors = []
            };
        }

        public async Task<List<BugReadDTO>> GetAllAsync()
        {
            var getBugs = await _unitOfWork.BugRepository.getAllAsync();

            return getBugs.Select(b => new BugReadDTO
            {
                Id = b.Id,
                Name = b.Name,
                Risk = b.Risk,
                ProjectID = b.ProjectID
            }).ToList();
        }

        public async Task<BugReadDTO?> GetBugByIDAsync(Guid id)
        {
            var bug = await _unitOfWork.BugRepository.getByIdAsync(id);
            if (bug is null)
            {
                return null;
            }

            return new BugReadDTO
            {
                Id = bug.Id,
                Name = bug.Name,
                Risk = bug.Risk,
                ProjectID = bug.ProjectID,
                Attachments = bug.Attachments.Select(a => new AttachmentReadDTO
                {
                    //ImgLink = a.ImgLink
                    Id = a.Id,
                    Name = a.Name,
                    FileUrl = a.FileUrl,
                    FilePath = a.FileUrl,
                    Type = a.Type
                }).ToList() ?? new List<AttachmentReadDTO>()
            };
        }
    }
}
