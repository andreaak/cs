using System;
using System.Web.Http;

namespace _01_ASPMVCTest.Areas._09_WebApi.Controllers
{
    public class _03_HttpVerbAttributeController : ApiController
    {
        // Метод сработает при получении GET запроса.
        [HttpGet]
        public string ShowDate()
        {
            return DateTime.Now.ToShortTimeString();
        }

        // GET api/_02_WebApi/5
        [HttpGet]
        public string ShowDate(int id)
        {
            return "value";
        }

        // POST api/_02_WebApi
        [HttpPost]
        public void InsertDate([FromBody]string value)
        {
        }

        // PUT api/_02_WebApi/5
        [HttpPut]
        public void UpdateDate(int id, [FromBody]string value)
        {
        }

        // DELETE api/_02_WebApi/5
        [HttpDelete]
        public void EraseDate(int id)
        {
        }
    }
}
