﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using EuroHelp.Data;
using EuroHelp.Data.Models;
using EuroHelp.Web.Infrastructure;
using EuroHelp.Web.Models.Employees;

namespace EuroHelp.Web.Controllers
{
    public class EmployeesController : Controller
    {
        public readonly EuroHelpDbContext data;

        public EmployeesController(EuroHelpDbContext data)
        {
            this.data = data;
        }

        [Authorize]
        public IActionResult Create() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Create(BecomeEmployeeFormModel employee)
        {
            var userId = this.User.GetId();

            var userIdAlreadyEmployee = this.data
                .Employees
                .Any(e => e.Id == this.User.GetId());

            if (userIdAlreadyEmployee)
            {
                return BadRequest();
            }

            var employeeData = new Employee
            {
                Id = userId,
                Name = employee.Name,
                PhoneNumber = employee.PhoneNumber,
            };

            this.data.Employees.Add(employeeData);
            this.data.SaveChanges();

            return RedirectToAction("CompanyMembers", "Companies");
        }
    }
}
