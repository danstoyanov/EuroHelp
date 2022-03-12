using Microsoft.AspNetCore.Mvc;

using EuroHelp.Data;
using EuroHelp.Data.Models;

using EuroHelp.Web.Models.Companies;

namespace EuroHelp.Web.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly EuroHelpDbContext data;

        public CompaniesController(EuroHelpDbContext data)
        {
            this.data = data;
        }

        public IActionResult CompanyMembers()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CompanyMembers(AddCompanyFormModel company)
        {
            // TO-DO:
            //  - make validations !
            //  - make some chakes !

            var testingUser = this.data.Users
                .Where(u => u.Id == "12")
                .FirstOrDefault();

            var newCompany = new InsuranceCompany
            {
                Id = company.Id,
                Name = company.Name,
                Code = company.Code,
                Bulstat = company.Bulstat,
                CompanyEnglName = company.CompanyEnglName,
                Address = company.Address,
                PhoneNumber = company.PhoneNumber,
                MobilePhoneNumber = company.MobilePhoneNumber,
                Email = company.Email,
                FAX = company.FAX,
                Notes = company.Notes,
                UserId = testingUser.Id,
                User = testingUser
            };

            this.data.InsuranceCompanies.Add(newCompany);
            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
