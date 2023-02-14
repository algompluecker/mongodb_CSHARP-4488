using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbTest.Driver;
using MongoDbTest.Dtos;

namespace MongoDbTest;

public class FixtureObjectUpdateDefinition : IClassFixture<MongoDbConnection>
{
    private readonly MongoDbConnection _dbConnection;

    public FixtureObjectUpdateDefinition(MongoDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    [Fact]
    public async Task UpdateUsingObjectDefinition()
    {
        var (collection, cat) = await SetupDb(nameof(UpdateUsingObjectDefinition));

        cat.Name = "UpdatedCat";
        var update = new ObjectUpdateDefinition<Animal>(cat);
        await ExecuteUpdate(collection, cat.Id, update, cat.Name);
    }

    [Fact]
    public async Task UpdateUsingFixedObjectDefinition()
    {
        var (collection, cat) = await SetupDb(nameof(UpdateUsingFixedObjectDefinition));

        cat.Name = "UpdatedCat";
        var update = new FixedObjectUpdateDefinition<Animal>(cat);
        await ExecuteUpdate(collection, cat.Id, update, cat.Name);
    }

    private async Task<(IMongoCollection<Animal> collection, Cat cat)> SetupDb(string testName)
    {
        var collection = _dbConnection.Database.GetCollection<Animal>(testName);

        var cat = new Cat
        {
            Name = "TheCat"
        };
        await collection.InsertOneAsync(cat);

        return (collection, cat);
    }

    private async Task ExecuteUpdate(IMongoCollection<Animal> collection, ObjectId id, UpdateDefinition<Animal> update, string expectedName)
    {
        var updatedCat = await collection.FindOneAndUpdateAsync(new ExpressionFilterDefinition<Animal>(animal => animal.Id == id), update,
            new FindOneAndUpdateOptions<Animal>() { ReturnDocument = ReturnDocument.After });

        Assert.Equal(expectedName, updatedCat.Name);
    }
}