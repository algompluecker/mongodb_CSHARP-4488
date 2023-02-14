using MongoDB.Driver;

namespace MongoDbTest
{
    public class MongoDbConnection : IAsyncLifetime
    {
        private const string MongoConnectionString = "mongodb://127.0.0.1:27018";

        public MongoDbConnection()
        {
            DbClient = new MongoClient(MongoConnectionString);
        }

        public MongoClient DbClient { get; }
        public IMongoDatabase Database { get; set; }

        public Task InitializeAsync()
        {
            Database = DbClient.GetDatabase(Guid.NewGuid().ToString());
            return Task.CompletedTask;
        }

        public Task DisposeAsync()
        {
            return DbClient.DropDatabaseAsync(Database.DatabaseNamespace.DatabaseName);
        }
    }
}