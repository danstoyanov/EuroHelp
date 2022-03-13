using Microsoft.AspNetCore.Mvc;

using EuroHelp.Web.Models.Users;
using EuroHelp.Data.Models;
using EuroHelp.Data;

namespace EuroHelp.Web.Controllers
{
    public class ConsumersController : Controller
    {
        private readonly EuroHelpDbContext data;

        public ConsumersController(EuroHelpDbContext data)
        {
            this.data = data;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterConsumerFormModel consumer)
        {
            var newUser = new Consumer
            {
                Id = consumer.Id,
                Username = consumer.Username,
                FirstName = consumer.FirstName,
                Gender = consumer.Gender,
            };

            this.data.Consumers.Add(newUser);
            this.data.SaveChanges();

            return RedirectToAction("AllDamages", "Damages");
         }
    }
}
