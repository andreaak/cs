using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplicationCoreMVC.Models;

namespace WebApplicationCoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult JSON()
        {
            TestObj obj = new()
            {
                A = "teststring",
                B = 3,
                C = true
            };
            return new JsonResult(obj);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

public class TestObj
{
    public string A { get; set; }
    public int B { get; set; }
    public bool C { get; set; }
}