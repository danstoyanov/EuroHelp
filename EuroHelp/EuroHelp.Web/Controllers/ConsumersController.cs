using Microsoft.AspNetCore.Mvc;

using EuroHelp.Data;
using EuroHelp.Data.Models;
using EuroHelp.Web.Models.Users;
using EuroHelp.Web.Infrastructure;

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
            var currUserId = this.User.GetId();

            var newUser = new Consumer
            {
                Id = currUserId,
                Username = consumer.Username,
                FirstName = consumer.FirstName,
                LastName = consumer.LastName,
                Gender = consumer.Gender,
            };

            this.data.Consumers.Add(newUser);
            this.data.SaveChanges();

            return RedirectToAction("AllDamages", "Damages");
         }
    }
}
