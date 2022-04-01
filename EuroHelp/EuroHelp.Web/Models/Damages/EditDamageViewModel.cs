using System.ComponentModel.DataAnnotations;

namespace EuroHelp.Web.Models.Damages
{
    public class EditDamageViewModel
    {
        public string Id { get; set; }

        [Required]
        public string? DamageType { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string EventDate { get; set; }

        public string RegistrationDate { get; set; }

        [Required]
        public string? PersonFirstName{ get; set; }

        [Required]
        public string? PersonSecondName { get; set; }

        [Required]
        public int? IdentityNumber { get; set; }
    }
}
