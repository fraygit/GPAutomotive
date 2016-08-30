using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GPA.MongoData.Entities.Base
{
    public class MongoEntity : IMongoEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
    }
}
