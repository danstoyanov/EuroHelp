using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


using static EuroHelp.Data.DataConstants.User;

namespace EuroHelp.Data.Models
{
    public class Consumer
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(FullNameMaxLength)]
        public string? UserName { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        public string Gender { get; set; }

        public List<Damage> Damages { get; set; } = new List<Damage>();

        public List<InsuranceCompany> InsuranceCompanies { get; set; } = new List<InsuranceCompany>();
    }
}
