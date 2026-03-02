using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace CSTest._34_Mongo
{
    public class PostContext
    {
        private IMongoDatabase Database { get; }
        static IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        static string connectionString = config.GetSection("ConnectionStrings").GetSection("MongoConnection").Value;

        public PostContext()
        {
            var url = new MongoUrl(connectionString);
            var client = new MongoClient(url);
            Database = client.GetDatabase(url.DatabaseName);
        }

        public IMongoCollection<TestDoc> TestDocs => Database.GetCollection<TestDoc>("TestDoc");
        public IMongoCollection<User> Users => Database.GetCollection<User>("Users");
        public IMongoCollection<Order> Orders => Database.GetCollection<Order>("Orders");

    }
}
