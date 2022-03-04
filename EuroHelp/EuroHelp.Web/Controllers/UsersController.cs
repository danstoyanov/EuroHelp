using Microsoft.AspNetCore.Mvc;

namespace EuroHelp.Web.Controllers
{
    public class UsersController : Controller
    {
        public UsersController()
        {

        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
