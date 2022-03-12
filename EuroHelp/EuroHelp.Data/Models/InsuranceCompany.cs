using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static EuroHelp.Data.DataConstants.InsuranceCompany;

namespace EuroHelp.Data.Models
{
    public class InsuranceCompany
    {
        [Key]
        public string? Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string? Name { get; set; }

        [Required]
        [Range(CodeMinValue, CodeMaxValue)]
        public int Code { get; set; }

        [Required]
        [Range(BulstatMinValue, BulstatMaxValue)]
        public int Bulstat { get; set; }

        [MaxLength(NameMaxLength)]
        public string? CompanyEnglName { get; set; }

        [MaxLength(AddressMaxLength)]
        public string? Address { get; set; }

        [Required]
        [MaxLength(MaxPhoneNumber)]
        public string? PhoneNumber { get; set; }

        [Required]
        [MaxLength(MaxMobilePhoneNumber)]
        public string? MobilePhoneNumber { get; set; }

        [Required]
        [MaxLength(EmailMaxLength)]
        public string? Email { get; set; }

        [Range(FaxMinValue, FaxMaxValue)]
        public int FAX { get; set; }

        [MaxLength(NotesMaxLength)]
        public string? Notes { get; set; }

        [ForeignKey("User")]
        public string? UserId { get; set; }

        public User? User { get; set; }

        public List<Damage> Damages { get; set; } = new List<Damage>();
    }
}
