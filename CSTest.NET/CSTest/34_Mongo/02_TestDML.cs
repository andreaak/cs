using MongoDB.Bson;
using MongoDB.Driver;
using NUnit.Framework;

namespace CSTest._34_Mongo
{
    /*

     */


    [TestFixture]
    public class _02_TestDML
    {
        private readonly PostContext Context = new PostContext();

        [Test]
        public async Task TestInsert()
        {
            try
            {
                var item = new TestDoc
                {
                    InternalId = new ObjectId("699f54fe3b7ec56a605d4ca5"),
                    Key1 = 5,
                    Key = 8
                };
                await Context.TestDocs.InsertOneAsync(item);

            }
            catch (Exception ex)
            {
            }
            
        }

        [Test]
        public async Task TestInsertMany()
        {
            var items = new List<TestDoc>
            {
                new TestDoc
                {
                    
                    Key1 = 5,
                    Key = 8
                },
                new TestDoc
                {
                    Key1 = 15,
                    Key = 28
                }
            };

            await Context.TestDocs.InsertManyAsync(items);
        }

        [Test]
        public async Task TestUpdate()
        {
            var filter = Builders<TestDoc>.Filter.Eq("_id", new ObjectId("699f54fe3b7ec56a605d4ca4"));
            var update = Builders<TestDoc>.Update.Set(p => p.Key, 19).CurrentDate(p => p.Date);
            await Context.TestDocs.UpdateOneAsync(filter, update);


            filter = Builders<TestDoc>.Filter.Gt("Key", 32);
            update = Builders<TestDoc>.Update.Set(p => p.Key, 19).CurrentDate(p => p.Date);
            var res = await Context.TestDocs.FindOneAndUpdateAsync(filter, update);

            
            //var id = new ObjectId("65f123abc123abc123abc123");

            //var user = new TestDoc
            //{
            //    InternalId = id,
            //    Key = 50,
            //    Key1 = 40,
            //    Name = "Ivan Updated"
            //};


            //await Context.TestDocs.ReplaceOneAsync(x => x.InternalId == new ObjectId("699f5b5e0360434b6415112b"),
            //    user);
        }


        [Test]
        public async Task TestUpdateMany()
        {
            var filter = Builders<TestDoc>.Filter.Lt("Key", 10);
            var update = Builders<TestDoc>.Update
                .Set(x => x.Key, 35);

            await Context.TestDocs.UpdateManyAsync(filter, update);
        }

        [Test]
        public async Task TestDelete()
        {
            var filter = Builders<TestDoc>.Filter.Eq("_id", new ObjectId("699e48b295434709d4bd729f"));
            var res = await Context.TestDocs.DeleteOneAsync(filter);
        }
    }
}
