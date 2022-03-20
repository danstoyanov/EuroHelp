using System.ComponentModel.DataAnnotations;

namespace EuroHelp.Web.Models.References
{
    public class ExportFile
    {
        [Required]
        public string StartDate { get; set; }

        [Required]
        public string EndDate { get; set; }
    }
}
