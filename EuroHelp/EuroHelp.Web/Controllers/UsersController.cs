using Microsoft.AspNetCore.Mvc;

using EuroHelp.Web.Models.Users;
using EuroHelp.Data.Models;
using EuroHelp.Data;

namespace EuroHelp.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly EuroHelpDbContext data;

        public UsersController(EuroHelpDbContext data)
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
        public IActionResult Register(RegisterUserFormModel user)
        {
            // To Do:
            //  Validation
            //  Cheks

            var newUser = new User
            {
                Username = user.Username,
                FirstName = user.FirstName,
                SecondNames = user.SecondNames,
                Gender = user.Gender,
                Email = user.Email,
                BirthDate = user.BirthDate,
                Password = user.Password,
                ConfirmPassword = user.ConfirmPassword
            };

            this.data.Users.Add(newUser);
            this.data.SaveChanges();

            return RedirectToAction("Login", "Users");
         }
    }
}
