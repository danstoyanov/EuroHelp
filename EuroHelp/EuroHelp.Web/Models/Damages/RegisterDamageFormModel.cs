using System.ComponentModel.DataAnnotations;

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
        public string? DamageType { get; set; }

        [Required]
        public string EventDate { get; set; }

        [Required]
        public int? IdentityNumber { get; set; }

        public string PersonFirstName { get; set; }

        public string PersonSecondName { get; set; }

        public string EventPlace { get; set; }

        public string Comment { get; set; }

        public string CompanyId { get; set; }

        public IEnumerable<InsuranceCompanyViewModel> Companies { get; set; }
    }
}
