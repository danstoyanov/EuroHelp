using EuroHelp.Data;
using EuroHelp.Data.Models;
using EuroHelp.Services.Infrastructure;

using System.Security.Claims;

namespace EuroHelp.Services.Users
{
    public class UserService : IUserService
    {
        private readonly EuroHelpDbContext data;

        public UserService(EuroHelpDbContext data)
        {
            this.data = data;
        }

        public bool IsEmployee(ClaimsPrincipal user)
        {
            var isEmployee = this.data
                .Employees
                .Any(e => e.Id == user.GetId());

            return isEmployee;
        }

        public bool IsConsumer(ClaimsPrincipal user)
        {
            var isConsumer = this.data
                .Consumers
                .Any(c => c.Id == user.GetId());

            return isConsumer;
        }

        public Employee GetEmployee(ClaimsPrincipal user)
        {
            var employee = this.data.Employees
                .Where(u => u.Id == user.GetId())
                .FirstOrDefault();

            return employee;
        }

        public Consumer GetConsumer(ClaimsPrincipal user)
        {
            var consumer = this.data.Consumers
                .Where(u => u.Id == user.GetId())
                .FirstOrDefault();

            return consumer;
        }

        public string CreateEmployee()
        {
            throw new NotImplementedException();
        }

        public string CreateConsumer()
        {
            throw new NotImplementedException();
        }

        public string CreateEmployee(string id, string name, string phoneNumber)
        {
            var employee = new Employee
            {
                Id = id,
                Name = name,
                PhoneNumber = phoneNumber
            };

            this.data.Employees.Add(employee);
            this.data.SaveChanges();

            return employee.Id;
        }
    }
}
