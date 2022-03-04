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

        public IActionResult AllDamages()
        {
            return View();
        }
        
        public IActionResult DamageModification()
        {
            return View();
        }        
        
        public IActionResult DamageSearch()
        {
            return View();
        }
    }
}
