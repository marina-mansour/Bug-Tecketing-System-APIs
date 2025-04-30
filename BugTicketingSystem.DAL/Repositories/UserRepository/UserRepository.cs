
using Microsoft.EntityFrameworkCore;

namespace BugTicketingSystem.DAL
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly DatabaseContext _ctx;

        public UserRepository(DatabaseContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _ctx.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
