using System.ComponentModel.DataAnnotations;

using static EuroHelp.Data.DataConstants;

namespace EuroHelp.Data.Models
{
    public class InsuranceCompany
    {
        [Key]
        [MaxLength(IdMaxLength)]
        public string Id { get; set; }

        public string Name { get; set; }

        public int Code { get; set; }

        public int Bulstat { get; set; }

        [Required]
        [MaxLength(CompanyNameMaxLength)]
        public string CompanyEnglName { get; set; }

        public string Address { get; set; }

        [Required]
        [MaxLength(MaxPhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(MaxMobilePhoneNumber)]
        public string MobilePhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        public int FAX { get; set; }

        [MaxLength(NotesMaxLength)]
        public string Notes { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public List<Damage> Damages { get; set; } = new List<Damage>();
    }
}
