using MongoDB.Bson.Serialization.Attributes;

namespace Business.Models
{
    public class UserInfo : BaseModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public int ProviderType { get; set; }
        public string ProviderIdStr { get; set; }
        public bool IsDeleted { get; set; }
        public Profile Profile { get; set; }

        [BsonIgnore]
        public string Token { get; set; }
    }
    public class Profile
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
    }
}
