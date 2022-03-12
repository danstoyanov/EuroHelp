using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static EuroHelp.Data.DataConstants;

namespace EuroHelp.Data.Models
{
    public class User
    {
        [Key]
        [MaxLength(IdMaxLength)]
        public string Id { get; set; }

        [Required]
        [MaxLength(DefaultUsernameMaxLength)]
        public string? Username { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? SecondNames { get; set; }

        [Required]
        public string? BirthDate { get; set; }

        [Required]
        public string? Gender { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? ConfirmPassword { get; set; }

        public List<Damage> Damages { get; set; } = new List<Damage>();

        public List<InsuranceCompany> InsuranceCompanies { get; set; } = new List<InsuranceCompany>();
    }
}
