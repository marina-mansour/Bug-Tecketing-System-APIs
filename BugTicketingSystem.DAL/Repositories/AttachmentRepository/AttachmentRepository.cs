using Microsoft.EntityFrameworkCore;

namespace BugTicketingSystem.DAL
{
    public class AttachmentRepository : GenericRepository<Attachment>, IAttachmentRepository
    {
        private readonly DatabaseContext _ctx;

        public AttachmentRepository(DatabaseContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<Attachment>?> GetAttachmentsByBugIdAsync(Guid bugId)
        {
            return await _ctx.Set<Attachment>()
                .Include(a => a.Bugs)
                .Where(a => a.BugID == bugId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> DeleteAttachmentAsync(Guid bugId, Guid attachmentId)
        {
            var attachment = await _ctx.Attachments
            .FirstOrDefaultAsync(a => a.Id == attachmentId && a.BugID == bugId);

            if (attachment == null)
            {
                return false;
            }

            _ctx.Attachments.Remove(attachment);
            await _ctx.SaveChangesAsync();
            return true;
        }
        public async Task<Attachment?> GetAttachmentByIdAndBugIdAsync(Guid attachmentId, Guid bugId)
        {
            return await _ctx.Set<Attachment>()
                .Include(a => a.Bugs)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == attachmentId && a.BugID == bugId)
                ?? null;
        }
        public async Task<Attachment?> AddAttachmentAsync(Guid bugId, Attachment attachment)
        {

            var bug = await _ctx.Bugs
                .FirstOrDefaultAsync(b => b.Id == bugId);
            if (bug == null)
            {
                return null;
            }
            attachment.BugID = bugId;

            await _ctx.Attachments.AddAsync(attachment);
            return attachment;
        }
    }
}
