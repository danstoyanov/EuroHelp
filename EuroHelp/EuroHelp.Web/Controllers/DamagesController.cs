using Microsoft.AspNetCore.Mvc;

namespace EuroHelp.Web.Controllers
{
    public class DamagesController : Controller 
    {
        public DamagesController()
        {

        }

        public IActionResult RegisterDamage()
        {
            return View();
        }

        public IActionResult EditDamage()
        {
            return View();
        }
    }
}
