using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using EuroHelp.Data;
using EuroHelp.Data.Models;
using EuroHelp.Web.Models.Companies;
using EuroHelp.Web.Infrastructure;

namespace EuroHelp.Web.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly EuroHelpDbContext data;

        public CompaniesController(
            EuroHelpDbContext data)
        {
            this.data = data;
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
                return BadRequest();
            }

            var employee = this.data.Employees
                .Where(u => u.Id == this.User.GetId())
                .FirstOrDefault();

            var newCompany = new InsuranceCompany
            {
                Id = company.Id,
                Name = company.Name,
                Bulstat = company.Bulstat,
                Address = company.Address,
                PhoneNumber = company.PhoneNumber,
                MobilePhoneNumber = company.MobilePhoneNumber,
                Email = company.Email,
                FAX = company.FAX,
                Notes = company.Notes,
                EmployeeId = employee.Id,
                Employee = employee
            };

            this.data.InsuranceCompanies.Add(newCompany);
            this.data.SaveChanges();

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
