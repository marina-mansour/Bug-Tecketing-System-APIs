using BugTicketingSystem.DAL;
using Microsoft.EntityFrameworkCore;

namespace BugTicketingSystem.DAL
{
    public class GenericRepository<T> : IGenericRepository<T> where T: class
    {
        private readonly DatabaseContext _ctx;

        public GenericRepository(DatabaseContext ctx)
        {
            _ctx = ctx;
        }
        public void Add(T ent)
        {
            _ctx.Set<T>().Add(ent);
        }

        public void Delete(T ent)
        {
            _ctx.Set<T>().Remove(ent);
        }

        public async Task<List<T>> getAllAsync()
        {
            return await _ctx.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T?> getByIdAsync(Guid id)
        {
            return await _ctx.Set<T>().FindAsync(id);
        }

        public void Update(T ent)
        {
            
        }
    }
}
