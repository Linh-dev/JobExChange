using Business.Models;
using MongoDB.Driver;

namespace AuthService.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<UserInfo> Users => _database.GetCollection<UserInfo>("Users");
    }
}
