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
        public readonly EuroHelpDbContext data;
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
            // Model states !

            if (this.user.IsEmployee(this.User))
            {
                // model state validation !
            }

            var userId = this.User.GetId();

            if (this.User.GetId() == null)
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
