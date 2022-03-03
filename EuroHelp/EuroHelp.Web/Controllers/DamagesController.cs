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

        public IActionResult OptionsDemage()
        {
            return View();
        }

        public IActionResult DamagePayments()
        {
            return View();
        }

        public IActionResult AllDamages()
        {
            return View();
        }
        
        public IActionResult TestView()
        {
            return View();
        }
    }
}
