using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using EuroHelp.Data;
using EuroHelp.Data.Models;
using EuroHelp.Web.Models.Companies;
using EuroHelp.Web.Infrastructure;
using EuroHelp.Services.InsuranceCompanies;

namespace EuroHelp.Web.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly ICompanyService companies;
        private readonly EuroHelpDbContext data;

        public CompaniesController(
            EuroHelpDbContext data,
            ICompanyService companies)
        {
            this.data = data;
            this.companies = companies;
        }

        [Authorize]
        public IActionResult CompanyMembers()
        {
            if (!this.IsEmployee())
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult CompanyMembers(AddCompanyFormModel company)
        {
            if (!this.IsEmployee())
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            if (this.data.InsuranceCompanies.Any(c => c.Id == company.Id))
            {
                this.ModelState.AddModelError(nameof(company.Id), "This Id alrady exist in current data !");

                return View(company);
            }

            if (!ModelState.IsValid)
            {
                return View(company);
            }

            var employee = this.data.Employees
                .Where(u => u.Id == this.User.GetId())
                .FirstOrDefault();

            this.companies.Create(
                company.Id,
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

        private bool IsEmployee()
        {
            var isEmployee = this
                .data
                .Employees
                .Any(e => e.Id == this.User.GetId());

            return isEmployee;
        }
    }
}
