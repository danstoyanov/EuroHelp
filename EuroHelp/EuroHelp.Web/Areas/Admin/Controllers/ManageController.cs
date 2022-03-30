using Microsoft.AspNetCore.Mvc;

namespace EuroHelp.Web.Areas.Admin.Controllers
{
    public class ManageController : AdminController 
    {
        public ManageController()
        {

        }

        public IActionResult Index() => View();

        public IActionResult Damages()
        {
            return View();
        }

        public IActionResult InsuranceCompanies()
        {
            return View();
        }

        public IActionResult Employees()
        {
            return View();
        }

        public IActionResult Consumers()
        {
            return View();
        }
    }
}
