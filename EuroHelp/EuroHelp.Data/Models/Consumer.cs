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
        [MaxLength(DefaultUsernameMaxLength)]
        public string? Username { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Gender { get; set; }

        public List<Damage> Damages { get; set; } = new List<Damage>();

        public List<InsuranceCompany> InsuranceCompanies { get; set; } = new List<InsuranceCompany>();
    }
}
