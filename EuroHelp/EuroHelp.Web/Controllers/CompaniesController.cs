using Microsoft.AspNetCore.Mvc;

namespace EuroHelp.Web.Controllers
{
    public class CompaniesController : Controller
    {
        public CompaniesController()
        {

        }

        public IActionResult CompanyMembers()
        {
            return View();
        }
    }
}
