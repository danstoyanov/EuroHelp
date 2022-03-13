using System.ComponentModel.DataAnnotations;

using EuroHelp.Data.Models;
using EuroHelp.Web.Models.Companies;

using static EuroHelp.Web.Global.GlobalModelsConstants.Damage;

namespace EuroHelp.Web.Models.Damages
{
    public class RegisterDamageFormModel
    {
        [Required]
        public string Id { get; set; }

        [StringLength(NameMaxLength, MinimumLength = 2)]
        [Required]
        public string? Name { get; set; }

        [Required]
        public string EventDate { get; set; }

        [Required]
        public string EventType { get; set; }

        [Required]
        public int? BulgarianRegNumber { get; set; }

        [Required]
        public int? ForeignRegNumber { get; set; }

        [Required]
        public string? Property { get; set; }

        public string? InjuredPerson { get; set; }

        [Required]
        public string? NotifiedBy { get; set; }

        public string CompanyId { get; set; }

        public IEnumerable<InsuranceCompanyViewModel> Companies { get; set; }
    }
}
