This is a reproducer for an issue with the MongoDB C# driver tracked at https://jira.mongodb.org/browse/CSHARP-4488

Please update the connection string in the class MongoDbConnection if necessary:

    private const string MongoConnectionString = "mongodb://127.0.0.1:27017";
