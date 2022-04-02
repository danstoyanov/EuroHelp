using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using EuroHelp.Services.Damages;
using EuroHelp.Services.InsuranceCompanies;
using EuroHelp.Services.Users;
using EuroHelp.Web.Areas.Admin.Models;

namespace EuroHelp.Web.Areas.Admin.Controllers
{
    public class ManageController : AdminController 
    {
        private readonly IDamageService damages;
        private readonly ICompanyService insuranceCompanies;
        private readonly IUserService users;

        public ManageController(
            IDamageService damages, 
            ICompanyService insuranceCompanies,
            IUserService users)
        {
            this.damages = damages;
            this.insuranceCompanies = insuranceCompanies;
            this.users = users;
        }

        public IActionResult Index()
        {
            var totalDamages = this.damages.GetAll();
            var totalCompanies = this.insuranceCompanies.GetAll();
            var totalEmployees = this.users.GetEmployees();
            var totalConsumers = this.users.GetConsumers();

            var statiscis = new AdminStatisticsViewModel
            {
                TotalDamages = totalDamages.Count(),
                TotalConsumers = totalConsumers.Count(),
                TotalCompanies = totalCompanies.Count(),
                TotalEmployees = totalEmployees.Count(),
                TotalUsers = (1 + totalEmployees.Count() + totalConsumers.Count())
            };

            return View(statiscis);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Damages()
        {
            var damageQuery = this.damages.GetAll();

            var damages = damageQuery.Select(d => new AllDamagesModel
            {
                Id = d.Id,
                DamageType = d.DamageType,
                PersonFirstName = d.PersonFirstName,
                PersonSecondName = d.PersonSecondName,
                CompanyName = d.CompanyName,
                EventDate = d.EventDate.ToString(),
                RegisterDate = d.RegisterDate.ToString(),
                IsApproved = d.IsApproved
            })
            .ToList();

            return View(damages);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult ChangeDamageStatus(string id)
        {
            this.damages.ChangeStatus(id);

            return RedirectToAction("Damages", "Manage");
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult InsuranceCompanies()
        {
            var insuranceCompaniesQuery = this.insuranceCompanies.GetAll();

            var insuranceCompanies = insuranceCompaniesQuery
                .Select(ic => new AllInsuranceCompaniesViewModel
                {
                    Id = ic.Id,
                    Bulstat = ic.Bulstat,
                    FAX = ic.FAX,
                    Name = ic.Name,
                    Email = ic.Email,
                    Address = ic.Address,
                    PhoneNumber = ic.PhoneNumber,
                    Status = ic.Status
                })
                .ToList();

            return View(insuranceCompanies);
        }

        public IActionResult ChangeCompanyStatus(string id)
        {
            this.insuranceCompanies.ChangeStatus(id);

            return RedirectToAction("InsuranceCompanies", "Manage");
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Employees()
        {
            var employeeQuery = this.users.GetEmployees();

            var employees = employeeQuery.Select(e => new AllEmployeesViewModel
            {
                Id = e.Id,
                Name = e.Name,
                PhoneNumber = e.PhoneNumber,
                Status = e.Status
            })
            .ToList();

            return View(employees);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult ChangeEmployeeStatus(string id)
        {
            this.users.ChangeEmployeeStatus(id);

            return RedirectToAction("Employees", "Manage");
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Consumers()
        {
            var consumersQuery = this.users.GetConsumers();

            var consumers = consumersQuery
                .Select(c => new AllConsumersViewModel
                {
                    Id = c.Id,
                    UserName = c.UserName,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Gender = c.Gender,
                    Status = c.Status
                })
                .ToList();

            return View(consumers);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult ChangeConsumerStatus(string id)
        {
            this.users.ChangeConsumerStatus(id);

            return RedirectToAction("Consumers", "Manage");
        }

    }
}
