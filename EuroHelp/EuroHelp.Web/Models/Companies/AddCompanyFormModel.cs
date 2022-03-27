using System.ComponentModel.DataAnnotations;

using static EuroHelp.Global.GlobalModelsConstants.InsuranceCompany;

namespace EuroHelp.Web.Models.Companies
{
    public class AddCompanyFormModel
    {
        [Required(ErrorMessage = "The current ID is not valid !")]
        [Display(Name = "ID")]
        public string Id { get; set; }

        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        [Display(Name = "Company name")]
        public string Name { get; set; }

        [Required]
        [Range(MinBulstatValue, MaxBulstatValue, ErrorMessage = "The Bulstat between 5 and 9999 long !")]
        public int Bulstat { get; set; }

        [Required]
        [Range(FaxMinLength, FaxMaxLength, ErrorMessage = "The FAX must between 10000 and 99999 long !")]
        [Display(Name = "FAX")]
        public int FAX { get; set; }

        [Required]
        [MinLength(MinBulstatValue)]
        [MaxLength(MaxBulstatValue)]
        public string Address { get; set; }

        [Required]
        [MaxLength(PhoneNumberLength)]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(MobilePhoneNumberLength)]
        [Display(Name = "Mobile phone number")]
        public string MobilePhoneNumber { get; set; }

        [Required]
        [RegularExpression(EmailRegEx, ErrorMessage = "The current email is not in the valid format !")]
        public string Email { get; set; }

        [Required]
        [MaxLength(NotesMaxLength)]
        public string Notes { get; set; }
    }
}
