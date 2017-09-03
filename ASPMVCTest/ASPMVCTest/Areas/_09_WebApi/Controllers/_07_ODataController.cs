using System.Linq;
using System.Web.Http;
using _01_ASPMVCTest.Areas._09_WebApi.Models;

namespace _01_ASPMVCTest.Areas._09_WebApi.Controllers
{
    public class _07_ODataController : ApiController
    {
        // GET api/values
        // Для того, чтоб работал OData протокол необходимо через NuGet добавить в проект библиотеку OData

        [HttpGet]
        [Queryable]
        public IQueryable<W_06_Product> AllProducts()
        {
            return W_06_Products.GetAllProducts().AsQueryable<W_06_Product>();
        }
    }
}
