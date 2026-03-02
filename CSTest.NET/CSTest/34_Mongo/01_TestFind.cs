using MongoDB.Bson;
using MongoDB.Driver;
using NUnit.Framework;
using System.Diagnostics;

namespace CSTest._34_Mongo
{
    /*
    Поиск. Курсор
    Результат выборки, получаемой с помощью функции find, называется курсором. Методы курсора:
    hasNext – показывает при переборе, имеется ли в наборе еще документ; next – извлекает текущий
    документ и перемещает к следующему документу в наборе; each – перебирает документы в курсоре.
    toArray – возвращает массив документов выборки.
    Проекцией называется включение в выборку заданных полей.
    Для сортировки полученных данных используется функция  sort.
    limit - задает максимально допустимое количество получаемых документов.
    skip – пропускает заданное количество документов.
    Методы sort, skip, limit можно совмещать в одной цепочке.

    Условные операторы
    $eq (равно).
    $ne (не равно).
    $gt (больше чем).
    $lt (меньше чем).
    $gte (больше или равно).
    $lte (меньше или равно).
    $in определяет массив значений, одно из которых должно иметь поле документа.
    $nin определяет массив значений, которые не должно иметь поле документа.

    Логические операторы
    $or: соединяет два условия, и документ должен соответствовать одному из этих условий.
    $and: соединяет два условия, и документ должен соответствовать обоим условиям.
    $not: документ должен НЕ соответствовать условию.
    $nor: соединяет два условия, и документ должен НЕ соответствовать обоим условиям.

    Операторы для работы с массивами
    $all: определяет набор значений, которые должны иметься в массиве.
    $size: определяет количество элементов, которые должны быть в массиве.
    $elemMatch: определяет условие, которым должны соответствовать элементы в массиве.

    Оператор $type
    $type извлекает только те документы, в которых определенный ключ имеет значение определенного типа, например, строку или число.

    Оператор $exists 
    $exists позволяет извлечь только те документы, в которых определенный ключ присутствует или отсутствует.
     */


    [TestFixture]
    public class _01_Test
    {
        private readonly PostContext Context = new PostContext();

        [Test]
        public async Task TestFind()
        {
            var items = Context.TestDocs.Find(x => true).ToList();

            var filter = Builders<TestDoc>.Filter.Eq("Key1", 4);
            var res1 = await Context.TestDocs.Find(filter).ToListAsync();
            var res = Context.TestDocs.Find(x => x.Key1 == 4).ToList();
            
            var filter2 = Builders<TestDoc>.Filter.Eq("Id", new ObjectId("699e48b295434709d4bd729f"));
            var res2 = await Context.TestDocs.Find(filter2).ToListAsync();
            
            //var filter = Builders<TestDoc>.Filter.In("Key1", new[] { 2, 4 });

            var builder = Builders<TestDoc>.Filter;
            
            var filter4 = builder.And(
               builder.Gte(g => g.Key1, 6),
               builder.Ne(r => r.Key1, 8));

            var filter5 = builder.Or(
               builder.Gte(g => g.Key1, 6),
               builder.Ne(r => r.Key1, 8));
        }

        [Test]
        public async Task TestFindCursor()
        {
            var filter = Builders<TestDoc>.Filter.Eq("Key1", 5);
            var cursor = await Context.TestDocs.FindAsync(filter);

            //var list = await cursor.ToListAsync();
            //foreach (var item in list)
            //{
            //    Debug.WriteLine(item.InternalId);
            //}
            await cursor.ForEachAsync(p => Debug.WriteLine(p.InternalId));
            /*
            699f54fe3b7ec56a605d4ca4
            699f5544e440ae20beb4ac37
            699f5b5e0360434b6415112a
            */
        }

        [Test]
        public async Task TestProject()
        {
            var projection = Builders<TestDoc>
                        .Projection
                        .Include(x => x.Key);

            var filter = Builders<TestDoc>.Filter.Eq("Key1", 5);
            var result = await Context.TestDocs
                .Find(filter)
                .Project(projection)
                .ToListAsync();

            Debug.WriteLine(result);//{{ "_id" : { "$oid" : "699f54fe3b7ec56a605d4ca4" }, "Key" : 19 }}

            var docs = await Context.TestDocs
            .Find(_ => true)
            .Project(x => new TestDocDto
            {
                Key = x.Key,
                Name = x.Name,
                IsActive = x.IsActive,
            })
            .ToListAsync();
            Debug.WriteLine(docs);//список объектов

            var keys = await Context.TestDocs
                .Find(_ => true)
                .Project(x => x.Key1)
                .ToListAsync();
            Debug.WriteLine(keys);//3
        }

        [Test]
        public async Task TestAll()
        {
            var filter = Builders<TestDoc>.Filter.Eq("Key1", 5);
            var projection = await Context.TestDocs.Find(filter)
                .Project(x => new { A = x.InternalId, B = x.Key1, C = x.Key })
                .SortBy(x => x.Key)
                .Skip(1)
                .Limit(1)
                .ToListAsync();
            Debug.WriteLine(projection);//{ A = {699f5544e440ae20beb4ac37}, B = 5, C = 19 }
        }

        [Test]
        public async Task TestExist()
        {
            var filter = Builders<TestDoc>.Filter.Exists(x => x.Key);
            var result = await Context.TestDocs.Find(filter).ToListAsync();
        }
        
        [Test]
        public async Task TestType()
        {
            var filter = Builders<TestDoc>.Filter.Type(x => x.Key1, BsonType.Int32);
            var result = await Context.TestDocs.Find(filter).ToListAsync();

            var filter2 =
                Builders<TestDoc>.Filter.Exists(x => x.Key) &
                Builders<TestDoc>.Filter.Type(x => x.Key1, BsonType.Int32);

            var users = await Context.TestDocs.Find(filter2).ToListAsync();
        }
    }
}
