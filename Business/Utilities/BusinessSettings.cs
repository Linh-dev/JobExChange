using Microsoft.Extensions.Configuration;

namespace Business.Utilities
{
    public class BusinessSettings
    {
        private static IConfiguration _configuration;

        // Phương thức để khởi tạo IConfiguration
        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public static string GetConfigValue(string key)
        {
            return _configuration[key];
        }

        public static string MongoDBConnectionStrings
        {
            get
            {
                return GetConfigValue("ConnectionStrings:MongoDb");
            }
        }

        public static string DatabaseName
        {
            get
            {
                return GetConfigValue("DatabaseName");
            }
        }

        public static bool IsTest
        {
            get
            {
                return Boolean.Parse(GetConfigValue("IsTest"));
            }
        }

        public static bool IsAzure
        {
            get
            {
                return Boolean.Parse(GetConfigValue("IsAzure"));
            }
        }
    }
}
