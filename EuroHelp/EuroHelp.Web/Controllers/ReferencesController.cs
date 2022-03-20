using EuroHelp.Data;
using EuroHelp.Services.References;
using EuroHelp.Web.Infrastructure;
using EuroHelp.Web.Models.References;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EuroHelp.Web.Controllers
{
    public class ReferencesController : Controller
    {
        private readonly IReferenceService references;
        private readonly EuroHelpDbContext data;

        public ReferencesController(EuroHelpDbContext data,
            IReferenceService references)
        {
            this.references = references;
            this.data = data;
        }

        [Authorize]
        public IActionResult GenerateReference()
        {
            if (!this.IsEmployee())
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult GenerateByCompanyName(ExportFile file)
        {
            if (!this.IsEmployee())
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            // more validations !!!

            var generatedFile = this.references.GenerateFile(file.StartDate, file.EndDate);

            return File(generatedFile.FileContests, generatedFile.ContentType, generatedFile.FileName);
        }
        private bool IsEmployee()
        {
            var isEmployee = this
                .data
                .Employees
                .Any(e => e.Id == this.User.GetId());

            return isEmployee;
        }
    }
}
