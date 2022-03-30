using Microsoft.AspNetCore.Mvc;

namespace EuroHelp.Web.Areas.Admin.Controllers
{
    public class DamagesController : AdminController
    {
        public IActionResult Index() => View();
    }
}
