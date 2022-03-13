using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EuroHelp.Data.Models
{
    public class Employee
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        public List<InsuranceCompany> Companies { get; set; } = new List<InsuranceCompany>();
    }
}
