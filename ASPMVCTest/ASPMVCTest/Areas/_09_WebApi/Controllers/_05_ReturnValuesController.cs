using System.Collections.Generic;
using System.Web.Http;

namespace _01_ASPMVCTest.Areas._09_WebApi.Controllers
{
    public class _05_ReturnValuesController : ApiController
    {
        /*
         В WebAPI есть несколько MediaTypeFormatter (форматеров), 
         которые позволяют сериализовать и десириализовать
         данные передающиеся через HTTP в определенном формате.
         Стандартные форматтеры поддерживают JSON, XML, URL Encoded Data.
         Форматером по умолчанию является JSON форматтер.

         Для того, что бы определить какого типа контент нужно вернуть пользователю, 
         проверяется значения заголовка Accept полученного со стороны клиента.
         Используя вкладку Composer приложения Fiddler попробуйте отправить запрос 
         с заголовком 'Accept : application/xml' и 'Accept : application/json' 
         проанализируйте разницу между полученными ответами.
        */


        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
