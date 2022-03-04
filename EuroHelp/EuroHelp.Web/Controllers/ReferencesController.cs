using Microsoft.AspNetCore.Mvc;

namespace EuroHelp.Web.Controllers
{
    public class ReferencesController : Controller
    {
        public ReferencesController()
        {

        }

        public IActionResult GenerateReference()
        {
            return View();
        }
    }
}
