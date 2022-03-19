using EuroHelp.Data;
using EuroHelp.Data.Models;
using EuroHelp.Services.Damages;
using EuroHelp.Web.Infrastructure;
using EuroHelp.Web.Models.Companies;
using EuroHelp.Web.Models.Damages;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EuroHelp.Web.Controllers
{
    public class DamagesController : Controller
    {
        private readonly IDamageService damages;
        private readonly EuroHelpDbContext data;

        public DamagesController(EuroHelpDbContext data,
            IDamageService damages)
        {
            this.damages = damages;
            this.data = data;
        }

        [Authorize]
        public IActionResult RegisterDamage()
        {
            return View(new RegisterDamageFormModel
            {
                Companies = this.GetInsuranceCompanies()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult RegisterDamage(RegisterDamageFormModel damage)
        {
            if (!IsConsumer())
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            // check for here !
            var currConsumer = this.data
                .Users
                .Where(u => u.Id == this.User.GetId())
                .FirstOrDefault();

            // check for here !
            var currCompany = this.data
                .InsuranceCompanies
                .Where(c => c.Id == damage.CompanyId)
                .FirstOrDefault();

            this.damages.Create(
                damage.Id,
                damage.DamageType,
                damage.EventDate,
                damage.IdentityNumber,
                damage.PersonFirstName,
                damage.PersonSecondName,
                damage.EventPlace,
                damage.Comment,
                damage.ConsumerId,
                damage.CompanyId,
                currCompany.Name);

            return RedirectToAction("AllDamages", "Damages");
        }

        [Authorize]
        public IActionResult AllDamages([FromQuery] AllDamagesQueryModel query)
        {
            var damagesQuery = this.data.Damages
                .OrderByDescending(d => d.EventDate)
                .AsQueryable();

            var damages = damagesQuery
                .Select(d => new DamagesListingViewModel
                {
                    Id = d.Id,
                    DamageType = d.DamageType,
                    CompanyName = d.CompanyName,
                    EventDate = d.EventDate.ToString("dd/MM/yyyy"),
                    RegisterDate = d.RegistrationDate.ToString("dd/MM/yyyy"),
                    IdentityNumber = d.IdentityNumber,
                    PersonFirstName = d.PersonFirstName,
                    PersonSecondName = d.PersonSecondName,
                    EventPlace = d.EventPlace,
                })
                .ToList();

            query.Damages = damages;

            return View(query);
        }

        [HttpPost]
        [Authorize]
        public IActionResult DeleteDamage(string id)
        {
            if (!this.IsEmployee())
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var currDamage = this.data
                .Damages
                .Where(d => d.Id == id)
                .FirstOrDefault();

            if (currDamage == null)
            {
                return View("Error");
            }
            else
            {
                this.data.Damages.Remove(currDamage);
                this.data.SaveChanges();
            }

            return RedirectToAction("AllDamages", "Damages");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Details(string id)
        {
            if (!this.IsEmployee())
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var currDamage = this.data
                .Damages
                .Where(d => d.Id == id)
                .FirstOrDefault();

            var model = new EditDamageViewModel
            {
                Id = currDamage.Id,
                Name = currDamage.DamageType,
                CompanyName = currDamage.CompanyName,
                EventDate = currDamage.EventDate.ToString(),
                RegistrationDate = currDamage.RegistrationDate.ToString(),
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(EditDamageViewModel model)
        {
            if (!this.IsEmployee())
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var damage = this.data
                .Damages
                .Where(d => d.Id == model.Id)
                .FirstOrDefault();

            damage.DamageType = model.Name;
            damage.EventDate = DateTime.Parse(model.EventDate);

            this.data.SaveChanges();

            return RedirectToAction("AllDamages", "Damages");
        }

        [Authorize]
        public IActionResult DamageSearch([FromQuery] AllDamagesQueryModel query)
        {
            if (!this.IsEmployee())
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var damagesQuery = this.data.Damages.AsQueryable();

            if (query.SearchId != null)
            {
                damagesQuery = this.data.Damages
                    .Where(d => d.Id == query.SearchId)
                    .OrderByDescending(d => d.CompanyName)
                    .AsQueryable();
            }
            else if (query.SearchCompanyName != null)
            {
                damagesQuery = this.data.Damages
                    .Where(d => d.CompanyName == query.SearchCompanyName)
                    .OrderByDescending(d => d.CompanyName)
                    .AsQueryable();
            }

            var damages = damagesQuery
                  .Select(d => new DamagesListingViewModel
                  {
                      Id = d.Id,
                      DamageType = d.DamageType,
                      RegisterDate = d.RegistrationDate.ToString("dd/MM/yyyy"),
                      EventDate = d.EventDate.ToString("dd/MM/yyyy"),
                      CompanyName = d.CompanyName
                  })
                  .ToList();

            query.Damages = damages.OrderByDescending(d => d.EventDate).ToList();

            return View(query);
        }

        private IEnumerable<InsuranceCompanyViewModel> GetInsuranceCompanies()
            => this.data
                .InsuranceCompanies
                .Select(c => new InsuranceCompanyViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();

        private bool IsEmployee()
        {
            var isEmployee = this
                .data
                .Employees
                .Any(e => e.Id == this.User.GetId());

            return isEmployee;
        }

        private bool IsConsumer()
        {
            var isConsumer = this
                .data
                .Consumers
                .Any(c => c.Id == this.User.GetId());

            return isConsumer;
        }
    }
}
