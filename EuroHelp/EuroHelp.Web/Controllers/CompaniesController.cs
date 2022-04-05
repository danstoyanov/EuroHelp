using EuroHelp.Services.InsuranceCompanies;
using EuroHelp.Services.Users;
using EuroHelp.Web.Models.Companies;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EuroHelp.Web.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly IUserService user;
        private readonly ICompanyService companies;

        public CompaniesController(
            IUserService user,
            ICompanyService companies)
        {
            this.user = user;
            this.companies = companies;
        }

        [Authorize]
        public IActionResult CreateInsuranceCompany()
        {
            var curremployee = this.user.GetEmployee(this.User);

            if (curremployee.Status == "Non active")
            {
                return RedirectToAction("Restricted", "Home");
            }

            if (!this.user.IsEmployee(this.User))
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateInsuranceCompany(AddCompanyFormModel company)
        {
            if (!this.user.IsEmployee(this.User))
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            if (!ModelState.IsValid)
            {
                return View(company);
            }

            var employee = this.user.GetEmployee(this.User);

            if (employee == null)
            {
                return BadRequest();
            }

            this.companies.Create(
                company.Name,
                company.Bulstat,
                company.Address,
                company.PhoneNumber,
                company.MobilePhoneNumber,
                company.Email,
                company.FAX,
                company.Notes,
                employee.Id);

            return RedirectToAction("Index", "Home");
        }
    }
}
