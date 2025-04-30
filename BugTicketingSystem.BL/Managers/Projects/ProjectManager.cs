using BugTicketingSystem.DAL;
using BugTrackingSystem.BL;
using FluentValidation;

namespace BugTicketingSystem.BL
{
    public class ProjectManager : IProjectManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ProjectAddDtoValidator _validationRules;

        public ProjectManager(IUnitOfWork unitOfWork, ProjectAddDtoValidator validationRules)
        {
            _unitOfWork = unitOfWork;
            _validationRules = validationRules;
        }
        public async Task<GeneralResult> AddAsync(ProjectAddDTO prjDTO)
        {
            var validationResult = await _validationRules.ValidateAsync(prjDTO);
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

            var newPrj = new Project()
            {
                Name = prjDTO.Name,
                Status = prjDTO.Status
            };

            _unitOfWork.ProjectRepository.Add(newPrj);
            await _unitOfWork.SaveChangesAsync();

            return new GeneralResult
            {
                Success = true,
                Errors = []
            };
        }

        public async Task<List<ProjectReadDTO>> GetAllAsync()
        {
            var getPrjs = await _unitOfWork.ProjectRepository.GetAllWithBugsAsync();

            return getPrjs.Select(p => new ProjectReadDTO
            {
                Id = p.Id,
                Name = p.Name,
                Status = p.Status,
                Bugs = p.Bugs.Select(b => new BugReadDTO
                {
                    Id = b.Id,
                    Name = b.Name,
                    Risk = b.Risk,
                }).ToList() ?? new List<BugReadDTO>()
            }).ToList();
        }

        public async Task<ProjectReadDTO?> GetProjectByIDAsync(Guid id)
        {
            var prj = await _unitOfWork.ProjectRepository.GetByIdWithBugsAsync(id);
            if (prj is null)
            {
                return null;
            }

            return new ProjectReadDTO
            {
                Id = prj.Id,
                Name = prj.Name,
                Status = prj.Status,
                Bugs = prj.Bugs.Select(b => new BugReadDTO
                {
                    Id = b.Id,
                    Name = b.Name,
                    Risk = b.Risk,
                }).ToList() ?? new List<BugReadDTO>()
            };
        }
    }
}
