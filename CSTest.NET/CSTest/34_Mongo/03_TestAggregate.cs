using MongoDB.Driver;
using NUnit.Framework;

namespace CSTest._34_Mongo
{
    /*
     */


    [TestFixture]
    public class _03_TestAggregate
    {
        private readonly PostContext Context = new PostContext();

        [Test]
        public async Task TestAggregateGroup()
        {
            var collection = Context.Users;
            var result = await collection
                        .Aggregate()
                        .Group(
                            x => x.City,
                            g => new
                            {
                                City = g.Key,
                                Count = g.Count()
                            })
                        .ToListAsync();
            var result2 = await collection
                    .Aggregate()
                    .Group(
                        x => x.City,
                        g => new
                        {
                            City = g.Key,
                            AvgSalary = g.Average(x => x.Salary)
                        })
                    .ToListAsync(); 
        }

        [Test]
        public async Task TestAggregateProject()
        {
            var collection = Context.Users;
            var result = await collection
                .Aggregate()
                .Project(x => new
                {
                    x.Name,
                    x.Age
                })
                .ToListAsync();

        }
        
        [Test]
        public async Task TestAggregateSort()
        {
            var collection = Context.Users;
            var result = await collection
                .Aggregate()
                .SortByDescending(x => x.Salary)
                .ToListAsync();

        }
        
        [Test]
        public async Task TestAggregateLimit()
        {
            var collection = Context.Users;
            var result = await collection
                .Aggregate()
                .SortByDescending(x => x.Salary)
                .Limit(5)
                .ToListAsync();

        }
        
        [Test]
        public async Task TestAggregateCount()
        {
            var collection = Context.Users;
            var result = await collection
                .Aggregate()
                .Match(x => x.Age > 18)
                .Count()
                .FirstOrDefaultAsync();
        }
        
        //[Test]
        //public async Task TestAggregateLookup()
        //{
        //    var collection = Context.Users;
        //    var result = await collection.Aggregate()
        //        .Lookup(
        //            Context.Orders,
        //            user => user.Id,
        //            order => order.UserId,
        //            result => result.Orders)
        //        .ToListAsync();
        //}
        
        [Test]
        public async Task TestAggregateFull()
        {
            var collection = Context.Users;
            var result = await collection
                .Aggregate()

                .Match(x => x.Age > 18)

                .Group(
                    x => x.City,
                    g => new
                    {
                        City = g.Key,
                        AvgSalary = g.Average(x => x.Salary),
                        Count = g.Count()
                    })

                .SortByDescending(x => x.AvgSalary)

                .ToListAsync();

        }
    }
}
