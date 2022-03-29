using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static EuroHelp.Data.DataConstants.Damage;

namespace EuroHelp.Data.Models
{
    public class Damage
    {
        [Key]
        public string? Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(NameMaxLength)]
        public string? DamageType { get; set; }

        [Required]
        public string? CompanyName { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        [Required]
        public int? IdentityNumber { get; set; }

        [Required]
        public string PersonFirstName { get; set; }

        [Required]
        public string PersonSecondName { get; set; }

        [Required]
        public string EventPlace { get; set; }

        public string Comment { get; set; }

        public string? ConsumerId { get; set; }

        [ForeignKey("UserId")]
        public Consumer? Consumer { get; set; }

        public string? CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public InsuranceCompany? Company { get; set; }
    }
}
