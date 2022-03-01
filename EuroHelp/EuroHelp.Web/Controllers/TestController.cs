using Microsoft.AspNetCore.Mvc;

namespace EuroHelp.Web.Controllers
{
    public class TestController : Controller 
    {
        public TestController()
        {

        }

        public IActionResult Test()
        {
            return View();
        }

        public IActionResult Options()
        {
            return View();
        }
    }
}
