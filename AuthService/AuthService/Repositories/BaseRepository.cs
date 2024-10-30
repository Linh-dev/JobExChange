using MongoDB.Bson;

namespace AuthService.Repositories
{
    public interface BaseRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(ObjectId _id);
        Task AddAsync(T info);
        Task UpdateAsync(T info);
        Task DeleteAsync(ObjectId _id);
    }
}
