using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationCoreMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        public IActionResult Get()
        {

            TestObj obj = new()
            {
                A = "teststring",
                B = 3,
                C = true
            };
            return Ok(obj);
        }
    }
}
