using EuroHelp.Services.Damages;
using EuroHelp.Services.InsuranceCompanies;
using EuroHelp.Services.Users;
using EuroHelp.Web.Models.Damages;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EuroHelp.Web.Controllers
{
    public class DamagesController : Controller
    {
        private readonly IDamageService damages;
        private readonly IUserService users;
        private readonly ICompanyService companies;

        public DamagesController(
            IUserService users,
            IDamageService damages,
            ICompanyService companies)
        {
            this.users = users;
            this.damages = damages;
            this.companies = companies;
        }

        [Authorize]
        public IActionResult RegisterDamage()
        {
            var user = this.users.GetConsumer(this.User);

            if (user.Status == "Banned")
            {
                return RedirectToAction("Banned", "Home");
            }

            if (!this.users.IsConsumer(this.User))
            {
                return RedirectToAction("Register", "Consumers");
            }

            return View(new RegisterDamageFormModel
            {
                Companies = this.companies.GetInsuranceCompanies()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult RegisterDamage(RegisterDamageFormModel damage)
        {
            if (!this.users.IsConsumer(this.User))
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            if (!ModelState.IsValid)
            {
                damage.Companies = this.companies.GetInsuranceCompanies();
                return View(damage);
            }

            var consumer = this.users.GetConsumer(this.User);
            var company = this.companies.GetCompany(damage.CompanyId);

            if (company == null || consumer == null)
            {
                return BadRequest();
            }

            this.damages.Create(
                damage.DamageType,
                damage.EventDate,
                damage.IdentityNumber,
                damage.PersonFirstName,
                damage.PersonSecondName,
                damage.EventPlace,
                damage.Comment,
                consumer.Id,
                company.Id,
                company.Name);

            if (this.users.IsConsumer(User))
            {
                return RedirectToAction("Mine", "Damages");
            }

            return RedirectToAction("AllDamages", "Damages");
        }

        [Authorize]
        public IActionResult AllDamages([FromQuery] AllDamagesQueryModel query)
        {
            if (!this.users.IsEmployee(User))
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var queryResult = this.damages.All(
                query.DamageType,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllDamagesQueryModel.DamagesPerPage);

            var damageTypes = this.damages.AllDamageTypes();

            query.DamageTypes = damageTypes;
            query.TotalDamages = queryResult.TotalDamages;
            query.Damages = queryResult.Damages;

            return View(query);
        }

        [HttpPost]
        [Authorize]
        public IActionResult DeleteDamage(string id)
        {
            if (!this.users.IsEmployee(this.User) && !this.User.IsInRole("Administrator"))
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            if (!this.damages.IsValid(id))
            {
                return BadRequest();
            }

            this.damages.Delete(id);

            if (this.User.IsInRole("Administrator"))
            {
                return RedirectToAction("Damages", "Manage", new { area = "Admin" });
            }

            return RedirectToAction("AllDamages", "Damages");
        }

        [Authorize]
        public IActionResult Mine()
        {
            if (!this.users.IsConsumer(this.User))
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var consumer = this.users.GetConsumer(this.User);

            var myDamages = this.damages.DamagesByConsumer(consumer.Id);
            return View(myDamages);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit(string id)
        {
            if (!this.damages.IsValid(id))
            {
                return BadRequest();
            }

            var damage = this.damages
                .GetDamage(id);

            var model = new EditDamageViewModel
            {
                Id = damage.Id,
                DamageType = damage.DamageType,
                CompanyName = damage.CompanyName,
                EventDate = damage.EventDate.ToString(),
                RegistrationDate = damage.RegistrationDate.ToString(),
                PersonFirstName = damage.PersonFirstName,
                PersonSecondName = damage.PersonSecondName,
                IdentityNumber = damage.IdentityNumber
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(EditDamageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!this.damages.IsValid(model.Id))
            {
                return BadRequest();
            }

            this.damages.Edit(
                model.Id,
                model.DamageType,
                model.EventDate,
                model.PersonFirstName,
                model.PersonSecondName,
                model.IdentityNumber);

            return RedirectToAction("AllDamages", "Damages");
        }

        [Authorize]
        public IActionResult Info(string id)
        {
            var currDamage = this.damages.GetDamage(id);

            if (currDamage == null)
            {
                return RedirectToAction("InvalidObject", "Home");
            }

            var result = new DamageServiceListingModel
            {
                DamageType = currDamage.DamageType,
                CompanyName = currDamage.CompanyName,
                EventPlace = currDamage.EventPlace,
                EventDate = currDamage.EventDate.ToString(),
                RegisterDate = currDamage.RegistrationDate.ToString(),
                IdentityNumber = currDamage.IdentityNumber,
                PersonFirstName = currDamage.PersonFirstName,
                PersonSecondName = currDamage.PersonSecondName,
                InsuranceCompanyId = currDamage.CompanyId,
                IsApproved = currDamage.IsApproved,
                Id = currDamage.Id
            };

            return View(result);
        }
    }
}
