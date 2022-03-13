using System.Text;
using Microsoft.AspNetCore.Mvc;

using EuroHelp.Data;
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


        public IActionResult GenerateReference()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GenerateByCompanyName(ExportFile file)
        {
            var builder = new StringBuilder();

            var parsedStartDate = DateTime.Parse(file.StartDate);
            var parsedEndDate = DateTime.Parse(file.EndDate);

            builder.AppendLine("Име на щета, Застрафователна компания, Дата на събитие");

            var damages = this.data.Damages
                .Where(d => d.EventDate >= parsedStartDate && d.EventDate <= parsedEndDate)
                .ToList();

            foreach (var damage in damages)
            {
                builder.AppendLine($"{damage.Name}, {damage.CompanyName}, {damage.EventDate}");
            }

            var data = Encoding.UTF8.GetBytes(builder.ToString());
            var result = Encoding.UTF8.GetPreamble().Concat(data).ToArray();

            return File(result, "text/csv", "damagesByCompany.csv");
        }

        [HttpPost]
        public IActionResult GenerateByPretentionNumber(ExportFile file)
        {
            var builder = new StringBuilder();

            builder.AppendLine("Name, Property");

            var damages = this.data.Damages
                .Where(d => d.Property == "Няма")
                .ToList();

            foreach (var damage in damages)
            {
                builder.AppendLine($"{damage.Name}, {damage.Property}");
            }

            var data = Encoding.UTF8.GetBytes(builder.ToString());
            var result = Encoding.UTF8.GetPreamble().Concat(data).ToArray();

            return File(result, "text/csv", "damagesPretention.csv");
        }
    }
}
