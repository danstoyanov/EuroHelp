using System.ComponentModel.DataAnnotations;

using static EuroHelp.Web.Global.GlobalModelsConstants.InsuranceCompany;

namespace EuroHelp.Web.Models.Companies
{
    public class AddCompanyFormModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [Range(MinBulstatValue, MaxBulstatValue)]
        public int Bulstat { get; set; }

        [Required]
        [MinLength(MinBulstatValue)]
        [MaxLength(MaxBulstatValue)]
        public string Address { get; set; }

        [Required]
        [MaxLength(PhoneNumberLength)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(MobilePhoneNumberLength)]
        public string MobilePhoneNumber { get; set; }

        [Required]
        [RegularExpression(EmailRegEx)]
        public string Email { get; set; }

        [Required]
        [Range(FaxMinLength, FaxMaxLength)]
        public int FAX { get; set; }

        [Required]
        [MaxLength(NotesMaxLength)]
        public string Notes { get; set; }
    }
}
