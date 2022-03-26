using EuroHelp.Services.Infrastructure;
using EuroHelp.Services.Users;
using EuroHelp.Web.Models.Users;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EuroHelp.Web.Controllers
{
    public class ConsumersController : Controller
    {
        private readonly IUserService user;

        public ConsumersController(IUserService user)
        {
            this.user = user;
        }

        [Authorize]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Register(RegisterConsumerFormModel consumer)
        {
            if (this.user.IsConsumer(this.User))
            {
                this.ModelState.AddModelError(nameof(consumer.Username), "This user is alredy consumer !");
            }

            if (this.user.IsUsernameContains(consumer.Username))
            {
                this.ModelState.AddModelError(nameof(consumer.Username), "This username alredy exist !!");
            }

            if (!ModelState.IsValid)
            {
                return View(consumer);
            }

            var userId = this.User.GetId();

            if (userId == null)
            {
                return BadRequest();
            }

            this.user.CreateConsumer(
                userId,
                consumer.Username,
                consumer.FirstName,
                consumer.LastName,
                consumer.Gender);

            return RedirectToAction("AllDamages", "Damages");
         }
    }
}
