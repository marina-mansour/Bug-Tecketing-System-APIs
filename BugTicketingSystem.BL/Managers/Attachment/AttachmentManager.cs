using Azure.Core;
using System;
using BugTicketingSystem.DAL;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using BugTrackingSystem.BL;
using System.Net.Mail;

namespace BugTicketingSystem.BL
{
    public class AttachmentManager : IAttachmentManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AttachmentUploadDtoValidator _validationRules;

        public AttachmentManager(IUnitOfWork unitOfWork, AttachmentUploadDtoValidator validationRules)
        {
            _unitOfWork = unitOfWork;
            _validationRules = validationRules;
        }

        public async Task<GeneralResult> AddAsync(Guid bugId,AttachmentAddDTO attDTO)
        {
            var validationResult = await _validationRules.ValidateAsync(attDTO);
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
            var bug = await _unitOfWork.BugRepository
                .getByIdAsync(bugId);
            if (bug == null)
            {
                return new GeneralResult
                {
                    Success = false,
                    Errors = [new ResultError() { Msg = "Bug Not Found!"}]
                };
            }
            var fileName = Guid.NewGuid() + Path.GetExtension(attDTO.File.FileName);
            var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var attachmentsFolder = Path.Combine(webRootPath, "attachments");

            if (!Directory.Exists(attachmentsFolder))
            {
                Directory.CreateDirectory(attachmentsFolder);
            }

            var filePath = Path.Combine(attachmentsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await attDTO.File.CopyToAsync(stream);
            }

            var fileUrl = $"/attachments/{fileName}";

            var attachment = new DAL.Attachment
            {
                Name = attDTO.File.FileName,
                Type = attDTO.File.ContentType,
                FileUrl = fileUrl,
                BugID = bugId
            };

            await _unitOfWork.AttachmentRepository
                .AddAttachmentAsync(bugId,attachment);
            await _unitOfWork.SaveChangesAsync();

            return new GeneralResult<AttachmentReadDTO>
            {
                Success = true,
                Errors = []
            };

        }

        public async Task<GeneralResult> GetAttachmentsByBugIdAsync(Guid bugId)
        {
            var attachments = await _unitOfWork.AttachmentRepository
                .GetAttachmentsByBugIdAsync(bugId);
            if (attachments == null || !attachments.Any())
            {
                return new GeneralResult
                {
                    Success = false,
                    Errors = [new ResultError() { Msg = "Attachment Not Found!" }]
                };
            }

            var attachmentDtos = attachments.Select(a => new AttachmentReadDTO
            {
                Id = a.Id,
                Name = a.Name,
                FilePath = $"http://localhost:5279{a.FileUrl}"
            }).ToList();

            return new GeneralResult<List<AttachmentReadDTO>>
            {
                Success = true,
                Data = attachmentDtos
            };
        }

        public async Task<GeneralResult> DeleteAsync(Guid bugId, Guid attId)
        {
            var bug = await _unitOfWork.BugRepository.getByIdAsync(bugId);
            if (bug == null)
            {
                return new GeneralResult
                {
                    Success = false,
                    Errors = [new ResultError() { Msg = "Bug Not Found!" }]
                };
            }

            var attachment = await _unitOfWork.AttachmentRepository.GetAttachmentByIdAndBugIdAsync(attId, bugId);
            if (attachment == null)
            {
                return new GeneralResult
                {
                    Success = false,
                    Errors = [new ResultError() { Msg = "Attachment Not Found!" }]
                };
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", attachment.FileUrl);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            var deleted = await _unitOfWork.AttachmentRepository.DeleteAttachmentAsync(bugId, attId);
            if (!deleted)
            {
                return new GeneralResult
                {
                    Success = false,
                    Errors = [new ResultError() { Msg = "Attachment not found or already deleted." }]
                };
            }

            await _unitOfWork.SaveChangesAsync();

            return new GeneralResult
            {
                Success = true,
                Errors = []
            };
        }

    }
}
