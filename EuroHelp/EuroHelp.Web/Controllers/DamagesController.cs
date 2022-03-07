using Microsoft.AspNetCore.Mvc;

using EuroHelp.Data;
using EuroHelp.Data.Models;

using EuroHelp.Web.Models.Damages;

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
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterDamage(RegisterDamageFormModel damage)
        {
            // validation
            // and other checks !

            var damageData = new Damage()
            {
                Name = damage.Name,
                CompanyName = damage.CompanyName,
                EventDate = DateTime.Today,
                RegistrationDate = DateTime.Today,
                EventType = damage.EventType,
                BulgarianRegNumber = damage.BulgarianRegNumber,
                ForeignRegNumber = damage.ForeignRegNumber,
                Property = damage.Property,
                InjuredPerson = damage.InjuredPerson,
                NotifiedBy = damage.NotifiedBy
            };
            
            this.data.Damages.Add(damageData);
            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AllDamages()
        {
            return View();
        }
        
        public IActionResult DamageModification()
        {
            return View();
        }        
        
        public IActionResult DamageSearch()
        {
            return View();
        }

        public IActionResult TestView()
        {
            return View();
        }
    }
}
