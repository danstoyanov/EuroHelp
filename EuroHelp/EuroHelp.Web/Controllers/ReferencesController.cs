using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using EuroHelp.Data;
using EuroHelp.Web.Infrastructure;
using EuroHelp.Web.Models.References;

namespace EuroHelp.Web.Controllers
{
    public class ReferencesController : Controller
    {
        private readonly EuroHelpDbContext data;

        public ReferencesController(EuroHelpDbContext data)
        {
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

            var builder = new StringBuilder();

            var parsedStartDate = DateTime.Parse(file.StartDate);
            var parsedEndDate = DateTime.Parse(file.EndDate);

            builder.AppendLine("Име на щета, Застрафователна компания, Дата на събитие");

            var damages = this.data.Damages
                .Where(d => d.EventDate >= parsedStartDate && d.EventDate <= parsedEndDate)
                .ToList();

            foreach (var damage in damages)
            {
                builder.AppendLine($"{damage.DamageType}, {damage.CompanyName}, {damage.EventDate}");
            }

            var data = Encoding.UTF8.GetBytes(builder.ToString());
            var result = Encoding.UTF8.GetPreamble().Concat(data).ToArray();

            return File(result, "text/csv", "damagesByCompany.csv");
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
