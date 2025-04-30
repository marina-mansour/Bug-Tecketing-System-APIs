using System.Security.Claims;
using BugTicketingSystem.BL.Helpers;
using BugTicketingSystem.DAL;
using BugTrackingSystem.BL;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace BugTicketingSystem.BL
{
    public class UserManager : IUserManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RegisterDtoValidator _validationRules;
        private readonly UpdateDtoValidator _userValidationRules;
        private readonly ManagerUpdateDtoValidator _managerValidationRules;

        public UserManager(IUnitOfWork unitOfWork, RegisterDtoValidator validationRules,
            UpdateDtoValidator userValidationRules, ManagerUpdateDtoValidator managerValidationRules)
        {
            _unitOfWork = unitOfWork;
            _validationRules = validationRules;
            _userValidationRules = userValidationRules;
            _managerValidationRules = managerValidationRules;
        }
        public async Task<GeneralResult> AddAsync(UserRegisterDTO userDTO)
        {
            var validationResult = await _validationRules.ValidateAsync(userDTO);
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

            var newUser = new User()
            {
                Name = userDTO.Name,
                Age = userDTO.Age,
                Email = userDTO.Email,
                Password = PasswordHelper.HashPassword(userDTO.Password)
            };

            _unitOfWork.UserRepository.Add(newUser);
            await _unitOfWork.SaveChangesAsync();

            return new GeneralResult
            {
                Success = true,
                Errors = []
            };
        }

        public async Task<GeneralResult> DeleteAsync(User userDTO)
        {
            var delUser = await _unitOfWork.UserRepository.getByIdAsync(userDTO.Id);
            if (delUser == null)
            {
                return new GeneralResult
                {
                    Success = false,
                    Errors = [new ResultError() { Msg = "User Not Found!" }]
                };
            }
            _unitOfWork.UserRepository.Delete(userDTO);
            await _unitOfWork.SaveChangesAsync();
            return new GeneralResult
            {
                Success = true,
                Errors = []
            };
        }

        public async Task<List<UserReadDTO>> GetAllAsync()
        {
            var getUsers = await _unitOfWork.UserRepository.getAllAsync();

            return getUsers.Select(u => new UserReadDTO
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role
            }).ToList();
        }

        public async Task<UserReadDTO> GetByIdAsync(Guid id)
        {
            var getUser = await _unitOfWork.UserRepository.getByIdAsync(id);

            var user = new UserReadDTO
            {
                Id = getUser.Id,
                Name = getUser.Name,
                Email = getUser.Email,
                Role = getUser.Role
            };

            return user;
        }

        public async Task<IEnumerable<string>> GetManagerDataAsync(ClaimsPrincipal userClaims)
        {
            var userIdString = userClaims.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString))
                throw new UnauthorizedAccessException("User ID not found.");

            if (!Guid.TryParse(userIdString, out Guid userId))
            {
                throw new UnauthorizedAccessException("Invalid User ID format.");
            }

            var user = await _unitOfWork.UserRepository.getByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found.");

            return new[] { "Manager-Only Data 1", "Manager-Only Data 2" };
        }


        public async Task<IEnumerable<string>> GetTestingDataAsync(ClaimsPrincipal userClaims)
        {
            var userIdString = userClaims.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString))
                throw new UnauthorizedAccessException("User ID not found.");

            if (!Guid.TryParse(userIdString, out Guid userId))
            {
                throw new UnauthorizedAccessException("Invalid User ID format.");
            }

            var user = await _unitOfWork.UserRepository.getByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found.");

            return new[] { "Testing-Only Data 1", "Testing-Only Data 2" };
        }

        public async Task<IEnumerable<string>> GetDevelopingDataAsync(ClaimsPrincipal userClaims)
        {
            var userIdString = userClaims.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString))
                throw new UnauthorizedAccessException("User ID not found.");

            if (!Guid.TryParse(userIdString, out Guid userId))
            {
                throw new UnauthorizedAccessException("Invalid User ID format.");
            }

            var user = await _unitOfWork.UserRepository.getByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found.");

            return new[] { "Developing-Only Data 1", "Developing-Only Data 2" };
        }

        public async Task<GeneralResult> ManagerUpdateAsync(UserManagerUpdateDTO userDTO)
        {
            var validationResult = await _managerValidationRules.ValidateAsync(userDTO);
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

            var updateUser = await _unitOfWork.UserRepository.getByIdAsync(userDTO.Id);

            if (updateUser is null)
            {
                return new GeneralResult
                {
                    Success = false,
                    Errors = [new ResultError() { Msg = "User Not Found!" }]
                };
            }
            else
            {
                updateUser.Name = userDTO.Name;
                updateUser.Email = userDTO.Email;
                updateUser.Age = userDTO.Age;
                updateUser.Salary = userDTO.Salary;
                updateUser.Role = userDTO.Role;

                _unitOfWork.UserRepository.Update(updateUser);

                await _unitOfWork.SaveChangesAsync();

                return new GeneralResult
                {
                    Success = true,
                    Errors = []
                };
            }
        }

        public async Task<GeneralResult> UpdateAsync(UserUpdateDTO userDTO)
        {
            var validationResult = await _userValidationRules.ValidateAsync(userDTO);
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

            var updateUser = await _unitOfWork.UserRepository.getByIdAsync(userDTO.Id);

            if (updateUser is null)
            {
                return new GeneralResult
                {
                    Success = false,
                    Errors = [new ResultError() { Msg = "User Not Found!" }]
                };
            }
            else
            {
                updateUser.Name = userDTO.Name;
                updateUser.Email = userDTO.Email;
                updateUser.Age = userDTO.Age;
                updateUser.Password = PasswordHelper.HashPassword(userDTO.Password);

                _unitOfWork.UserRepository.Update(updateUser);

                await _unitOfWork.SaveChangesAsync();

                return new GeneralResult
                {
                    Success = true,
                    Errors = []
                };
            }
        }
    }
}
