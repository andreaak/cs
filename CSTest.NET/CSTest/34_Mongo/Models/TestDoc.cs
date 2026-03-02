using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CSTest._34_Mongo
{
    public class TestDoc
    {
        [BsonId]
        public ObjectId InternalId { get; set; }
        public int Key { get; set; }
        public int Key1 { get; set; }
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public bool IsActive { get; set; }
    }
}
