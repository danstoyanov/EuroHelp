using EuroHelp.Services.References;
using EuroHelp.Services.Users;
using EuroHelp.Web.Models.References;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EuroHelp.Web.Controllers
{
    public class ReferencesController : Controller
    {
        private readonly IReferenceService references;
        private readonly IUserService user;

        public ReferencesController(
            IReferenceService references,
            IUserService user)
        {
            this.user = user;
            this.references = references;
        }

        [Authorize]
        public IActionResult GenerateReference()
        {

            if (!this.user.IsEmployee(this.User))
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult GenerateByCompanyName(ExportFile file)
        {
            if (!this.user.IsEmployee(this.User))
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var generatedFile = this.references.GenerateFile(file.StartDate, file.EndDate);

            return File(generatedFile.FileContests, generatedFile.ContentType, generatedFile.FileName);
        }
    }
}
