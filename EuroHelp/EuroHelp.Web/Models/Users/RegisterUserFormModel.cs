using EuroHelp.Data.Models;

namespace EuroHelp.Web.Models.Users
{
    public class RegisterUserFormModel
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string SecondNames { get; set; }

        public string BirthDate { get; set; }

        public string Gender { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public List<Damage> Damages { get; set; }

        public List<InsuranceCompany> InsuranceCompanies { get; set; }
    }
}
