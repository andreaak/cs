using CSTest._34_Mongo;
using Elastic.Clients.Elasticsearch;
using NUnit.Framework;
using System.Diagnostics;

namespace CSTest._35_ELK
{
    /*
    
     */


    [TestFixture]
    public class _01_Test
    {

        [Test]
        public async Task TestAdd()
        {
            try
            {
                var settings = new ElasticsearchClientSettings(new Uri("http://localhost:9200"));
                //.Authentication(new BasicAuthentication("elastic", "password"));

                var client = new ElasticsearchClient(settings);

                var response = await client.Indices.CreateAsync("my-index");
                if (response.IsValidResponse)
                {
                    Console.WriteLine("Индекс создан");
                }
                else
                {
                    Debug.WriteLine(response.ElasticsearchServerError?.Error?.Reason);
                }

                var response2 = await client.IndexAsync(new
                {
                    Id = 1,
                    Name = "Test3",
                    CreatedAt = DateTime.UtcNow
                }, i => i.Index("my-index"));

                if (response2.IsValidResponse)
                {
                    Debug.WriteLine("Документ добавлен");
                }
            }
            catch (Exception ex)
            {
            }

        }

        [Test]
        public async Task TestAddDocument()
        {
            var settings = new ElasticsearchClientSettings(new Uri("http://localhost:9200"));
            //.Authentication(new BasicAuthentication("elastic", "password"));

            var client = new ElasticsearchClient(settings);

            var response2 = await client.IndexAsync(new
            {
                Id = 1,
                Name = "Test 3",
                CreatedAt = DateTime.UtcNow
            }, i => i.Index("my-index"));

            if (response2.IsValidResponse)
            {
                Debug.WriteLine("Документ добавлен");
            }

            var testDoc = new TestDoc
            {
                Key = 2,
                Key1 = 5,
                Name = "Test Name 2"
            };
            var response = await client.CreateAsync(testDoc, c => c
                            .Index("my-index")
                            .Id(testDoc.Key));

            if (response2.IsValidResponse)
            {
                Debug.WriteLine("Документ создан");
            }
        }


        /*
        1 Full-text Query (для текстового поиска) 
        Используются для полей типа text.
        ✅ Match Поиск по тексту с анализатором.
        ✅ MultiMatch Поиск по нескольким полям.
        ✅ MatchPhrase Точный поиск фразы.

        2️ Term-level Query (точные совпадения)
        Работают без анализа (для keyword, id, bool, numeric).
        ✅ Terms Несколько значений.
        ✅ Range Диапазон чисел или дат.
        
        3️ Bool Query (комбинация условий)
            Самый используемый тип.
            must — обязательно
            should — желательно
            filter — без влияния на score
            must_not — исключение

         4 Filter Query
            Используется внутри bool.filter.
            Быстрее, т.к. не считает релевантность.
            Идеально для:
                id
                дат
                статусов
                категорий

         🧠 5️ Advanced Queries
            ✅ Wildcard
                 .Wildcard(w => w
                    .Field("name.keyword")
                    .Value("iph*")
                )
            ✅ Prefix
                .Prefix(p => p
                    .Field("name.keyword")
                    .Value("iph")
                )
            ✅ Fuzzy (с опечатками)
                .Match(m => m
                    .Field(f => f.Name)
                    .Query("ipone")
                    .Fuzziness("AUTO")
                )
            ✅ Exists
                .Exists(e => e.Field("description"))
         
         6️ QueryString (как в Kibana)
            Позволяет писать запрос строкой.
         
         */

        [Test]
        public async Task TestFind()
        {

            var settings = new ElasticsearchClientSettings(new Uri("http://localhost:9200"));
            //.Authentication(new BasicAuthentication("elastic", "password"));
            var client = new ElasticsearchClient(settings);
                
            //Поиск по тексту с анализатором.
            var searchResponse = await client.SearchAsync<dynamic>(s => s
                        .Indices("my-index")
                        .Query(q => q
                            .Match(m => m
                                .Field("name")
                                .Query("Test")
                            )
                        )
                    );

            foreach (var hit in searchResponse.Hits)
            {
                Debug.WriteLine((object)hit.Source);
            }

        }
        
        [Test]
        public async Task TestFind2()
        {

            var settings = new ElasticsearchClientSettings(new Uri("http://localhost:9200"));
            //.Authentication(new BasicAuthentication("elastic", "password"));
            var client = new ElasticsearchClient(settings);

            //Поиск по тексту с анализатором.
            var searchResponse = await client.SearchAsync<dynamic>(s => s
                        .Indices("my-index")
                        .Query(q => q
                            .Bool(b => b
                                .Must(
                                    m => m.Match(mm => mm.Field("Name").Query("iphone"))
                                )
                                .Filter(
                                    f => f.Range(r => r
                                        .Number(n => n.Field("Price").Lte(1500))
                                    )
                                )
                                .MustNot(
                                    mn => mn.Term(t => t.Field("status").Value("archived"))
                                )
                )
            ));
            foreach (var hit in searchResponse.Hits)
            {
                Debug.WriteLine((object)hit.Source);
            }

        }


    }
}
