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
                // model state validation logic !
            }

            // other validation logic !!!
 
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
