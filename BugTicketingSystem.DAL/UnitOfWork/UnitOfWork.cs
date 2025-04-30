

namespace BugTicketingSystem.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _ctx;

        public IUserRepository UserRepository { get; }
        public IBugRepository BugRepository { get; }
        public IProjectRepository ProjectRepository { get; }
        public IAttachmentRepository AttachmentRepository { get; }
        public IUserBugRepository UserBugRepository { get; }

        public UnitOfWork(
            IUserRepository userRepository,
            IBugRepository bugRepository,
            IProjectRepository projectRepository,
            IAttachmentRepository attachmentRepository,
            IUserBugRepository userBugRepository,
            DatabaseContext ctx)
        {
            UserRepository = userRepository;
            BugRepository = bugRepository;
            ProjectRepository = projectRepository;
            AttachmentRepository = attachmentRepository;
            UserBugRepository = userBugRepository;
            _ctx = ctx;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _ctx.SaveChangesAsync();
        }
    }
}
