using EuroHelp.Data.Models;

using System.Security.Claims;

namespace EuroHelp.Services.Users
{
    public interface IUserService
    {
        public bool IsEmployee(ClaimsPrincipal user);

        public bool IsConsumer(ClaimsPrincipal user);

        public string ChangeConsumerStatus(string id);

        public string ChangeEmployeeStatus(string id);

        public Employee GetEmployee(ClaimsPrincipal user);

        public Consumer GetConsumer(ClaimsPrincipal user);

        public List<EmployeesListServiceModel> GetEmployees();

        public List<ConsumersListServiceModel> GetConsumers();

        public bool IsUsernameContains(string username);

        public string CreateEmployee(string id, string name, string phoneNumber);

        public string CreateConsumer(string id, string username, string firstName, string lastName, string gender);
    }
}
