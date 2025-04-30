
namespace BugTicketingSystem.DAL
{
    public interface IGenericRepository<T> where T: class
    {
        Task<List<T>> getAllAsync();
        Task<T?> getByIdAsync(Guid id);
        void Add(T ent);
        void Update(T ent);
        void Delete(T ent);
    }
}
