using System.ComponentModel.DataAnnotations;

using static EuroHelp.Data.DataConstants;

namespace EuroHelp.Data.Models
{
    public class Damage
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string? Name { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [Required]
        [MaxLength(CompanyMaxLength)]
        public string? CompanyName { get; set; }

        public Company? Company { get; set; }

        public DateTime EventDate { get; set; }

        public DateTime RegistrationDate { get; set; }

        public string? BulgarianRegNumber { get; set; }

        public string? ForeignRegNumber { get; set; }

        public string? Property { get; set; }

        public string? InjuredPerson { get; set; }

        public string? NotifiedBy { get; set; }
    }
}
