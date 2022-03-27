using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EuroHelp.Web.Models.References
{
    public class ExportFile
    {
        [Required]
        [DisplayName("From Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public string StartDate { get; set; }

        [Required]
        [DisplayName("To Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public string EndDate { get; set; }
    }
}
