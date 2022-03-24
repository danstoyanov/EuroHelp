using System.ComponentModel.DataAnnotations;

using EuroHelp.Services.InsuranceCompanies;

using static EuroHelp.Web.Global.GlobalModelsConstants.Damage;

namespace EuroHelp.Web.Models.Damages
{
    public class RegisterDamageFormModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string? DamageType { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = DateFormat)]
        public DateTime EventDate { get; set; }

        [Required]
        [Range(IdentityNumberMinValue, IdentityNumberMaxValue)]
        public int IdentityNumber { get; set; }

        [Required]
        [StringLength(PersonNameMaxLength, MinimumLength = PersonNameMinLength)]
        public string PersonFirstName { get; set; }

        [Required]
        [StringLength(PersonNameMaxLength, MinimumLength = PersonNameMinLength)]
        public string PersonSecondName { get; set; }

        [Required]
        [StringLength(EventPlaceNameMaxLength, MinimumLength = EventPlaceNameMinLength)]
        public string EventPlace { get; set; }

        [Required]
        [StringLength(CommentMaxLength, MinimumLength = CommentMinLength)]
        public string Comment { get; set; }

        public string CompanyId { get; set; }

        public IEnumerable<InsuranceCompaniesServiceModel> Companies { get; set; }
    }
}
