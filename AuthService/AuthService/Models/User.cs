using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Business.Utilities;

namespace AuthService.Models
{
    public class User
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Provider { get; set; }
        public Profile Profile { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? CreatedAt { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? UpdatedAt { get; set; }

        [BsonIgnore]
        public string IdStr
        {
            get
            {
                return _id.ToString();
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _id = new ObjectId(value);
                }
            }
        }
        [BsonIgnore]
        public string CreatedAtStr
        {
            get
            {
                return DateUtil.DateTimeToString(CreatedAt);
            }
            set
            {
                CreatedAt = DateUtil.StringToDateTime(value);
            }
        }
        [BsonIgnore]
        public string UpdatedAtStr
        {
            get
            {
                return DateUtil.DateTimeToString(UpdatedAt);
            }
            set
            {
                UpdatedAt = DateUtil.StringToDateTime(value);
            }
        }
    }
    public class Profile
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
    }
}
