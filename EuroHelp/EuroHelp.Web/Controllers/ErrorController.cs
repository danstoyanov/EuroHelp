using Microsoft.AspNetCore.Mvc;

namespace EuroHelp.Web.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/NotFound")]
        public IActionResult PageNotFound()
        {
            return View();
        }
    }
}
