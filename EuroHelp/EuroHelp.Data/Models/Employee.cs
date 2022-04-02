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

        [Required]
        public string UserId { get; set; }

        [Required]
        public string Status { get; set; } = "Non active";

        public List<InsuranceCompany> Companies { get; set; } = new List<InsuranceCompany>();
    }
}
