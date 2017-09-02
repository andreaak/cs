using System;
using System.Web.Http;

namespace _01_ASPMVCTest.Controllers.WebApi
{
    public class _02_HttpVerbActionNameController : ApiController
    {
        // Метод выполнится при получении HTTP GET запроса
        public string Get()
        {
            return DateTime.Now.ToShortTimeString();
        }

        // GET api/_02_WebApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/_02_WebApi
        public string Post([FromBody]string value)
        {
            return "Value: " + value;
        }

        // PUT api/_02_WebApi/5
        public string Put(int id, [FromBody]string value)
        {
            return "Id = " + id + " Value = " + value;
        }

        // DELETE api/_02_WebApi/5
        public string Delete(int id)
        {
            return "Id = " + id;
        }
    }
}
