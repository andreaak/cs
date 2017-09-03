using System;
using System.Web.Http;

namespace _01_ASPMVCTest.Areas._09_WebApi.Controllers
{
    public class _04_StartActionNameWithHttpVerbController : ApiController
    {
        // Метод сработает при получении GET запроса.
        public string GetDate()
        {
            return DateTime.Now.ToShortTimeString();
        }

        // GET api/_02_WebApi/5
        public string GetDate(int id)
        {
            return "value = " + id;
        }

        // POST api/_02_WebApi
        public void PostDate([FromBody]string value)
        {
        }

        // PUT api/_02_WebApi/5
        public void PutDate(int id, [FromBody]string value)
        {
        }

        // DELETE api/_02_WebApi/5
        public void DeleteDate(int id)
        {
        }
    }
}
