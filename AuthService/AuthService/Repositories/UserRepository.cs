using Business.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AuthService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<UserInfo> _users;

        public UserRepository(IMongoDatabase database)
        {
            _users = database.GetCollection<UserInfo>("Users");
        }

        public async Task<IEnumerable<UserInfo>> GetAllAsync()
        {
            return await _users.Find(u => !u.IsDeleted).ToListAsync();
        }

        public async Task<UserInfo> GetByIdAsync(ObjectId _id)
        {
            return await _users.Find(u => u._id == _id).FirstOrDefaultAsync();
        }

        public async Task<UserInfo> GetByUsernameAsync(string username)
        {
            return await _users.Find(u => u.Username == username && !u.IsDeleted).FirstOrDefaultAsync();
        }

        public async Task AddAsync(UserInfo info)
        {
            await _users.InsertOneAsync(info);
        }

        public async Task UpdateAsync(UserInfo info)
        {
            await _users.ReplaceOneAsync(u => u._id == info._id, info);
        }

        public async Task DeleteAsync(ObjectId _id)
        {
            await _users.DeleteOneAsync(u => u._id == _id);
        }

        public async Task<UserInfo> GetByProviderAsync(string providerIdStr, int provider)
        {
            return await _users.Find(u => u.ProviderIdStr == providerIdStr && u.ProviderType == provider && !u.IsDeleted).FirstOrDefaultAsync();
        }
    }
}
