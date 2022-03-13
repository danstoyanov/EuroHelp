using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static EuroHelp.Data.DataConstants.Damage;

namespace EuroHelp.Data.Models
{
    public class Damage
    {
        [Key]
        public string? Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string? Name { get; set; }

        public string? CompanyName { get; set; }

        public DateTime EventDate { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        [Required]
        public string? EventType { get; set; }

        [Range(BgRegMinValue, BgRegMaxValue)]
        public int? BulgarianRegNumber { get; set; }

        [Range(OtherRegMinValue, OtherRegMaxValue)]
        public int? ForeignRegNumber { get; set; }

        [Required]
        public string? Property { get; set; }

        [Required]
        public string? InjuredPerson { get; set; }

        [Required]
        public string? NotifiedBy { get; set; }

        public string? ConsumerId { get; set; }

        [ForeignKey("UserId")]
        public Consumer? Consumer { get; set; }

        public string? CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public InsuranceCompany? Company { get; set; }
    }
}
