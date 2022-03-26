using EuroHelp.Data.Models;

using System.ComponentModel.DataAnnotations;

using static EuroHelp.Web.Global.GlobalModelsConstants.Consumer;

namespace EuroHelp.Web.Models.Users
{
    public class RegisterConsumerFormModel
    {
        [Required]
        [MinLength(ConsumerUserNameMinLength)]
        [MaxLength(ConsumerUserNameMaxLength)]
        public string Username { get; set; }

        [Required]
        [MinLength(ConsumerUserFirstNameMinLength)]
        [MaxLength(ConsumerUserLastNameMinLength)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(ConsumerUserLastNameMinLength)]
        [MaxLength(ConsumerUserLastNameMaxLength)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [RegularExpression(EmailRegEx, ErrorMessage = "The current email is not in the valid format !")]
        public string Email { get; set; }

        public List<Damage> Damages { get; set; } = new List<Damage>();

        public List<InsuranceCompany> InsuranceCompanies { get; set; } = new List<InsuranceCompany>();
    }
}
