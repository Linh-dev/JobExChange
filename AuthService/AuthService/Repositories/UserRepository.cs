using AuthService.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Security.Cryptography;

namespace AuthService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IMongoDatabase database)
        {
            _users = database.GetCollection<User>("Users");
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _users.Find(u => !u.IsDeleted).ToListAsync();
        }

        public async Task<User> GetByIdAsync(ObjectId _id)
        {
            return await _users.Find(u => u._id == _id).FirstOrDefaultAsync();
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _users.Find(u => u.Username == username && !u.IsDeleted).FirstOrDefaultAsync();
        }

        public async Task AddAsync(User info)
        {
            await _users.InsertOneAsync(info);
        }

        public async Task UpdateAsync(User info)
        {
            await _users.ReplaceOneAsync(u => u._id == info._id, info);
        }

        public async Task DeleteAsync(ObjectId _id)
        {
            await _users.DeleteOneAsync(u => u._id == _id);
        }

        public async Task<User> GetByProviderAsync(string providerIdStr, int provider)
        {
            return await _users.Find(u => u.ProviderIdStr == providerIdStr && u.ProviderType == provider && !u.IsDeleted).FirstOrDefaultAsync();
        }
    }
}
