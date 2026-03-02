using MongoDB.Bson;

namespace CSTest._34_Mongo
{
    public class User
    {
        public ObjectId Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public int Age { get; set; }

        public decimal Salary { get; set; }
    }
}
