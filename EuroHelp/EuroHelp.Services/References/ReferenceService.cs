using EuroHelp.Data;
using System.Text;

namespace EuroHelp.Services.References
{
    public class ReferenceService : IReferenceService
    {
        private readonly EuroHelpDbContext data;

        public ReferenceService(EuroHelpDbContext data)
        {
            this.data = data;
        }

        public ReferenceExportFomFile GenerateFile(string startDate, string endDate)
        {

            var builder = new StringBuilder();

            builder.AppendLine("Damage type, Insurance Company, Event Date");

            var parsedStartDate = DateTime.Parse(startDate);
            var parsedEndDate = DateTime.Parse(endDate);

            var damages = this.data.Damages
                .Where(d => d.EventDate >= parsedStartDate && d.EventDate <= parsedEndDate)
                .ToList();

            foreach (var damage in damages)
            {
                builder.AppendLine($"{damage.DamageType}, {damage.CompanyName}, {damage.EventDate}");
            }

            var data = Encoding.UTF8.GetBytes(builder.ToString());
            var result = Encoding.UTF8.GetPreamble().Concat(data).ToArray();

            var file = new ReferenceExportFomFile
            {
                FileContests = result,
                ContentType = "text/csv",
                FileName = "tableDamages.csv"
            };

            return file;
        }
    }
}
