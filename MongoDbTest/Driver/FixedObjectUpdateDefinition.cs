using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Core.Misc;
using MongoDB.Driver.Linq;

namespace MongoDbTest.Driver;

/// <summary>
/// This is a fix for an issue in the MongoDB driver.
/// The issue is tracked here: https://jira.mongodb.org/browse/CSHARP-4488
/// </summary>
/// <typeparam name="TDocument">The type of the document.</typeparam>
public sealed class FixedObjectUpdateDefinition<TDocument> : UpdateDefinition<TDocument>
{
    private readonly object _obj;

    /// <summary>
    /// Initializes a new instance of the <see cref="FixedObjectUpdateDefinition{TDocument}"/> class.
    /// </summary>
    /// <param name="obj">The object.</param>
    public FixedObjectUpdateDefinition(object obj)
    {
        _obj = Ensure.IsNotNull(obj, nameof(obj));
    }

    /// <summary>
    /// Gets the object.
    /// </summary>
    public object Object
    {
        get { return _obj; }
    }

    /// <inheritdoc />
    public override BsonValue Render(IBsonSerializer<TDocument> documentSerializer, IBsonSerializerRegistry serializerRegistry, LinqProvider linqProvider)
    {
        return new BsonDocumentWrapper(_obj, documentSerializer);
    }
}