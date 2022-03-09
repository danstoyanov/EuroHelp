using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static EuroHelp.Data.DataConstants;

namespace EuroHelp.Data.Models
{
    public class Damage
    {
        [Key]
        [MaxLength(IdMaxLength)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(DamageNameMaxLength)]
        public string? Name { get; set; }

        public string CompanyName { get; set; }

        [Required]
        public string EventDate { get; set; }

        [Required]
        public string RegistrationDate { get; set; }

        public string EventType { get; set; }

        [Range(0, BgRegNumber)]
        public int? BulgarianRegNumber { get; set; }

        [Range(0, otherRegNumber)]
        public int? ForeignRegNumber { get; set; }

        public string? Property { get; set; }

        [Required]
        public string? InjuredPerson { get; set; }

        public string? NotifiedBy { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public string CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public InsuranceCompany Company { get; set; }
    }
}
