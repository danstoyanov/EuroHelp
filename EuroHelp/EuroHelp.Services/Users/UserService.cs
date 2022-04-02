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

        public string CreateEmployee(string id, string name, string phoneNumber)
        {
            var employee = new Employee
            {
                Id = id,
                Name = name,
                PhoneNumber = phoneNumber,
                UserId = id
            };

            this.data.Employees.Add(employee);
            this.data.SaveChanges();

            return employee.Id;
        }

        public string CreateConsumer(string id, string username, string firstName, string lastName, string gender)
        {
            var consumer = new Consumer
            {
                Id = id,
                UserName = username,
                FirstName = firstName,
                LastName = lastName,
                Gender = gender,
                UserId = id
            };

            this.data.Consumers.Add(consumer);
            this.data.SaveChanges();

            return consumer.Id;
        }

        public bool IsUsernameContains(string username)
        {
            return this.data.Consumers
                .Any(c => c.UserName == username);
        }

        public List<EmployeesListServiceModel> GetEmployees()
           => this.data.Employees
            .Select(e => new EmployeesListServiceModel
            {
                Id = e.Id,
                Name = e.Name,
                PhoneNumber = e.PhoneNumber,
                Status = e.Status
            })
            .ToList();

        public List<ConsumersListServiceModel> GetConsumers()
            => this.data.Consumers
            .Select(c => new ConsumersListServiceModel
            {
                Id = c.Id,
                UserName = c.UserName,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Gender = c.Gender,
                Status = c.Status
            })
            .ToList();

        public string ChangeConsumerStatus(string id)
        {
            var consumer = this.data.Consumers
                .Where(c => c.Id == id)
                .FirstOrDefault();

            if (consumer.Status == "Active")
            {
                consumer.Status = "Banned";
            }
            else if (consumer.Status == "Banned")
            {
                consumer.Status = "Active"; 
            }

            this.data.SaveChanges();

            return consumer.Status;
        }

        public string ChangeEmployeeStatus(string id)
        {
            var consumer = this.data.Employees
                .Where(c => c.Id == id)
                .FirstOrDefault();

            if (consumer.Status == "Active")
            {
                consumer.Status = "Non active";
            }
            else if (consumer.Status == "Non active")
            {
                consumer.Status = "Active";
            }

            this.data.SaveChanges();

            return consumer.Status;
        }
    }
}
