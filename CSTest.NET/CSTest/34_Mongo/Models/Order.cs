using MongoDB.Bson;

namespace CSTest._34_Mongo
{
    public class Order
    {
        public ObjectId Id { get; set; }

        public ObjectId UserId { get; set; }

        public decimal Total { get; set; }
    }
}
