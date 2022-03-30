using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using static EuroHelp.Web.Areas.Admin.AdminConstants;

namespace EuroHelp.Web.Areas.Admin.Controllers
{
    [Area(AreaName)]
    [Authorize(Roles = AdministratorRoleName)]
    public class AdminController : Controller
    {
        public IActionResult AdminManager()
        {
            // With statistics about 

            return View();
        }
    }
}
