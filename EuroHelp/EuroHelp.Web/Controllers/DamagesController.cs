using Microsoft.AspNetCore.Mvc;

namespace EuroHelp.Web.Controllers
{
    public class DamagesController : Controller 
    {
        public DamagesController()
        {

        }

        public IActionResult DemageRegister()
        {
            return View();
        }
    }
}
