using Business.Utilities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Business.Models
{
    public class BaseModel
    {
        public ObjectId _id { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? CreatedAt { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? UpdatedAt { get; set; }
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
        [BsonIgnore]
        public string IdStr
        {
            get
            {
                return _id.ToString();
            }
            set
            {
                try
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        _id = new ObjectId(value);
                    }
                }
                catch
                {
                    _id = ObjectId.Empty;
                }

            }
        }
    }
}
