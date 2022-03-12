using EuroHelp.Data.Models;
using EuroHelp.Web.Models.Companies;

namespace EuroHelp.Web.Models.Damages
{
    public class RegisterDamageFormModel
    {

        public string Id { get; set; }

        public string? Name { get; set; }

        public string CompanyName { get; set; }

        public string EventDate { get; set; }

        public string RegistrationDate { get; set; }

        public string EventType { get; set; }

        public int? BulgarianRegNumber { get; set; }

        public int? ForeignRegNumber { get; set; }

        public string? Property { get; set; }

        public string? InjuredPerson { get; set; }

        public string? NotifiedBy { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public string CompanyId { get; set; }

        public InsuranceCompany Company { get; set; }

        public IEnumerable<InsuranceCompanyViewModel> Companies { get; set; }
    }
}
