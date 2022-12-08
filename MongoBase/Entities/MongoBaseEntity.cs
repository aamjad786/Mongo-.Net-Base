using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoBase.Entities;

public class MongoBaseEntity<TKey>
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public TKey Id { get; set; }
}
