using EuroHelp.Data;
using EuroHelp.Services.Infrastructure;
using EuroHelp.Services.Users;
using EuroHelp.Web.Models.Employees;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

namespace EuroHelp.Web.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IUserService user;

        public EmployeesController(IUserService user)
        {
            this.user = user;
        }

        [Authorize]
        public IActionResult Create() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Create(BecomeEmployeeFormModel employee)
        {
            if (this.user.IsEmployee(this.User))
            {
                this.ModelState.AddModelError(nameof(employee.Name), "This user is alredy employee !");
            }

            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            var userId = this.User.GetId();

            if (userId == null)
            {
                return BadRequest();
            }

            this.user.CreateEmployee(
                userId,
                employee.Name,
                employee.PhoneNumber);

            return RedirectToAction("CompanyMembers", "Companies");
        }
    }
}
