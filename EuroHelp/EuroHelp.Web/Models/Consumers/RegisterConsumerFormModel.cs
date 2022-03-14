using EuroHelp.Data.Models;

namespace EuroHelp.Web.Models.Users
{
    public class RegisterConsumerFormModel
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public List<Damage> Damages { get; set; }

        public List<InsuranceCompany> InsuranceCompanies { get; set; }
    }
}
