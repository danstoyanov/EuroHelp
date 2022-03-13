using System.ComponentModel.DataAnnotations;

using EuroHelp.Data.Models;

using static EuroHelp.Web.Global.GlobalModelsConstants.InsuranceCompany;

namespace EuroHelp.Web.Models.Companies
{
    public class AddCompanyFormModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required]
        [Range(CodeMinValue, CodeMaxValue)]
        public int Code { get; set; }

        [Required]
        [Range(BulstatMinValue, BulstatMaxValue)]
        public int Bulstat { get; set; }

        [Required]
        [StringLength(NameMaxLength,
            MinimumLength = NameMinLength,
            ErrorMessage = "The name must be must be a string with a minimum length of 3")]
        public string CompanyEnglName { get; set; }

        public string Address { get; set; }

        [Required]
        [StringLength(MaxPhoneNumber, MinimumLength = 5)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(MaxMobilePhoneNumber, MinimumLength = 5)]
        public string MobilePhoneNumber { get; set; }

        [Required]
        [StringLength(EmailMaxLength, MinimumLength = 4)]
        public string Email { get; set; }

        public int FAX { get; set; }

        [Required]
        [StringLength(NotesMaxLength,
            MinimumLength = 3,
            ErrorMessage = "The note can't be less 3 characters !!")]
        public string Notes { get; set; }

        public string UserId { get; set; }

        public Consumer User { get; set; }

        public List<Damage> Damages { get; set; }
    }
}
