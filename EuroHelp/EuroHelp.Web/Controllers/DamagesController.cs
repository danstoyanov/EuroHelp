using EuroHelp.Data;
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
        private readonly EuroHelpDbContext data;

        public DamagesController(EuroHelpDbContext data,
            IUserService users,
            IDamageService damages,
            ICompanyService companies)
        {
            this.users = users;
            this.damages = damages;
            this.companies = companies;
            this.data = data;
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

            var consumer = this.users.GetConsumer(this.User);
            var company = this.companies.GetCompany(damage.CompanyId);

            if (company == null)
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
            if (!this.users.IsEmployee(this.User))
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
            if (!this.users.IsEmployee(this.User))
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
            if (!this.users.IsEmployee(this.User))
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
            if (!this.users.IsEmployee(this.User))
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
    }
}
