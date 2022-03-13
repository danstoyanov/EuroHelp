using System.Globalization;

using Microsoft.AspNetCore.Mvc;

using EuroHelp.Data;
using EuroHelp.Data.Models;

using EuroHelp.Web.Models.Damages;
using EuroHelp.Web.Models.Companies;
using EuroHelp.Web.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace EuroHelp.Web.Controllers
{
    public class DamagesController : Controller
    {
        private readonly EuroHelpDbContext data;

        public DamagesController(EuroHelpDbContext data)
        {
            this.data = data;
        }

        [Authorize]
        public IActionResult RegisterDamage()
        {
            return View(new RegisterDamageFormModel
            {
                Companies = this.GetInsuranceCompanies()
            }) ;
        }


        [HttpPost]
        [Authorize]
        public IActionResult RegisterDamage(RegisterDamageFormModel damage)
        {
            var testingUser = this.data
                .Users
                .Where(u => u.Id == this.User.GetId())
                .FirstOrDefault();

            var currCompany = this.data
                .InsuranceCompanies
                .Where(c => c.Id == damage.CompanyId)
                .FirstOrDefault();

            var eventDate = DateTime.Parse(damage.EventDate);

            var newDamage = new Damage()
            {
                Id = damage.Id,
                Name = damage.Name,
                CompanyName = currCompany.Name,
                EventDate = eventDate,
                EventType = damage.EventType,
                BulgarianRegNumber = damage.BulgarianRegNumber,
                ForeignRegNumber = damage.ForeignRegNumber,
                Property = damage.Property,
                InjuredPerson = damage.InjuredPerson,
                NotifiedBy = damage.NotifiedBy,
                ConsumerId = testingUser.Id,
                CompanyId = damage.CompanyId
            };

            this.data.Damages.Add(newDamage);
            this.data.SaveChanges();

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
                    Name = d.Name,
                    CompanyName = d.CompanyName,
                    RegistrationDate = d.RegistrationDate.ToString("dd/MM/yyyy"),
                    EventDate = d.EventDate.ToString("dd/MM/yyyy"),
                    EventType = d.EventType,
                    BulgarianRegNumber = d.BulgarianRegNumber,
                    ForeignRegNumber = d.ForeignRegNumber,
                    Property = d.Property,
                    InjuredPerson = d.InjuredPerson,
                    NotifiedBy = d.NotifiedBy
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
                Name = currDamage.Name,
                CompanyName = currDamage.CompanyName,
                EventDate = currDamage.EventDate.ToString(),
                RegistrationDate = currDamage.RegistrationDate.ToString(),
                EventType = currDamage.EventType,
                BulgarianRegNumber = currDamage.BulgarianRegNumber,
                ForeignRegNumber = currDamage.ForeignRegNumber,
                Property = currDamage.Property,
                InjuredPerson = currDamage.InjuredPerson,
                NotifiedBy = currDamage.NotifiedBy
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

            damage.Name = model.Name;
            damage.EventDate = DateTime.Parse(model.EventDate);
            damage.BulgarianRegNumber = model.BulgarianRegNumber;
            damage.ForeignRegNumber = model.BulgarianRegNumber;
            damage.Property = model.Property;
            damage.InjuredPerson = model.InjuredPerson;

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
                      Name = d.Name,
                      RegistrationDate = d.RegistrationDate.ToString("dd/MM/yyyy"),
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
            var isEmployee = this
                .data
                .Consumers
                .Any(e => e.Id == this.User.GetId());

            return isEmployee;
        }
    }
}
