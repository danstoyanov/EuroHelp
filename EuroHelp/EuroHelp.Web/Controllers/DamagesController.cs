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
                .Where(u => u.Id == "4b3b8269-e623-470f-8852-a981c08b0f64")
                .FirstOrDefault();

            var currCompany = this.data
                .InsuranceCompanies
                .Where(c => c.Id == damage.CompanyId)
                .FirstOrDefault();

            var newDamage = new Damage()
            {
                Name = damage.Name,
                CompanyName = currCompany.Name,
                EventDate = DateTime.UtcNow.ToString(),
                RegistrationDate = DateTime.UtcNow.ToString(),
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

        public IActionResult AllDamages([FromQuery]AllDamagesQueryModel query)
        {
            var damagesQuery = this.data.Damages.AsQueryable();

            var damages = damagesQuery
                .Select(d => new DamagesListingViewModel
                {
                    Name = d.Name,
                    CompanyName = d.CompanyName,
                    EventDate = d.EventDate,
                    RegistrationDate = d.RegistrationDate,
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

        public IActionResult DamageModification()
        {
            return View();
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
