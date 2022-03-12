using Microsoft.AspNetCore.Mvc;

using EuroHelp.Data;
using EuroHelp.Data.Models;

using EuroHelp.Web.Models.Damages;
using EuroHelp.Web.Models.Companies;

namespace EuroHelp.Web.Controllers
{
    public class DamagesController : Controller
    {
        private readonly EuroHelpDbContext data;

        public DamagesController(EuroHelpDbContext data)
        {
            this.data = data;
        }

        public IActionResult RegisterDamage()
            => View(new RegisterDamageFormModel
            {
                Companies = this.GetInsuranceCompanies()
            });

        [HttpPost]
        public IActionResult RegisterDamage(RegisterDamageFormModel damage)
        {
            // validation
            // and other checks !

            var testingUser = this.data
                .Users
                .Where(u => u.Id == "12")
                .FirstOrDefault();

            var currCompany = this.data
                .InsuranceCompanies
                .Where(c => c.Id == damage.CompanyId)
                .FirstOrDefault();

            var newDamage = new Damage()
            {
                Id = damage.Id,
                Name = damage.Name,
                CompanyName = currCompany.Name,
                EventDate = DateTime.UtcNow.ToString(),
                EventType = damage.EventType,
                BulgarianRegNumber = damage.BulgarianRegNumber,
                ForeignRegNumber = damage.ForeignRegNumber,
                Property = damage.Property,
                InjuredPerson = damage.InjuredPerson,
                NotifiedBy = damage.NotifiedBy,
                UserId = testingUser.Id,
                CompanyId = damage.CompanyId
            };

            this.data.Damages.Add(newDamage);
            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AllDamages([FromQuery] AllDamagesQueryModel query)
        {
            var damagesQuery = this.data.Damages
                .OrderByDescending(d => d.CompanyName)
                .AsQueryable();

            var damages = damagesQuery
                .Select(d => new DamagesListingViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    CompanyName = d.CompanyName,
                    RegistrationDate = d.RegistrationDate.ToString("f"),
                    EventDate = d.EventDate,
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
        public IActionResult DeleteDamage(string id)
        {
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
        public IActionResult Details(string id)
        {
            var currDamage = this.data
                .Damages
                .Where(d => d.Id == id)
                .FirstOrDefault();

            var model = new EditDamageViewModel
            {
                Id = currDamage.Id,
                Name = currDamage.Name,
                CompanyName = currDamage.CompanyName,
                EventDate = currDamage.EventDate,
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
        public IActionResult Details(EditDamageViewModel model)
        {
            var damage = this.data
                .Damages
                .Where(d => d.Id == model.Id)
                .FirstOrDefault();

            damage.Name = model.Name;
            damage.BulgarianRegNumber = model.BulgarianRegNumber;
            damage.ForeignRegNumber = model.BulgarianRegNumber;
            damage.Property = model.Property;
            damage.InjuredPerson = model.InjuredPerson;

            this.data.SaveChanges();

            return RedirectToAction("AllDamages", "Damages");
        }

        public IActionResult DamageSearch()
        {
            return View();
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
    }
}
