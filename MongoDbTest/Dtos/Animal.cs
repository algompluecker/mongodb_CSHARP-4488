using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbTest.Dtos
{
    [BsonKnownTypes(typeof(Cat))]
    public abstract class Animal
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Name { get; set; }
    }
}