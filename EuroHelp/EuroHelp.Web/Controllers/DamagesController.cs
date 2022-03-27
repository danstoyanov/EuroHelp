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

            if (ModelState.IsValid)
            {
                damage.Companies = this.companies.GetInsuranceCompanies();
                return View(damage);
            }

            // Fix .....
            var consumer = this.users.GetConsumer(this.User);
            var company = this.companies.GetCompany(damage.CompanyId);

            if (company == null || consumer == null)
            {
                return BadRequest();
            }

            this.damages.Create(
                damage.Id,
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

            return RedirectToAction("AllDamages", "Damages");
        }

        // ADD Validations or some checks !!!!!!!!
        [Authorize]
        public IActionResult AllDamages([FromQuery] AllDamagesQueryModel query)
        {
            var damages = this.damages.All();

            if (damages == null)
            {
                return BadRequest();
            }

            query.Damages = damages;

            return View(query);
        }

        [HttpPost]
        [Authorize]
        public IActionResult DeleteDamage(string id)
        {
            if (!this.users.IsEmployee(this.User))
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            if (!this.damages.IsValid(id))
            {
                return BadRequest();
            }

            this.damages.Delete(id);

            return RedirectToAction("AllDamages", "Damages");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Details(string id)
        {
            if (!this.users.IsEmployee(this.User))
            {
                return RedirectToAction("AccessDenied", "Home");
            }

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
        public IActionResult Details(EditDamageViewModel model)
        {
            if (!this.users.IsEmployee(this.User))
            {
                return RedirectToAction("AccessDenied", "Home");
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
        public IActionResult DamageSearch([FromQuery] AllDamagesQueryModel query)
        {
            if (!this.users.IsEmployee(this.User))
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var damages = this.damages.Search(query.SearchId, query.SearchCompanyName);

            if (damages == null)
            {
                return BadRequest();
            }

            query.Damages = damages;

            return View(query);
        }
    }
}
