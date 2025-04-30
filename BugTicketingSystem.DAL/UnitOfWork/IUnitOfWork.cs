namespace BugTicketingSystem.DAL
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; }
        public IBugRepository BugRepository { get; }
        public IProjectRepository ProjectRepository { get; }
        public IAttachmentRepository AttachmentRepository { get; }
        public IUserBugRepository UserBugRepository { get; }

        Task<int> SaveChangesAsync();
    }
}
