using EuroHelp.Data.Models;

using System.Security.Claims;

namespace EuroHelp.Services.Users
{
    public interface IUserService
    {
        public bool IsEmployee(ClaimsPrincipal user);

        public bool IsConsumer(ClaimsPrincipal user);

        public Employee GetEmployee(ClaimsPrincipal user);

        public Consumer GetConsumer(ClaimsPrincipal user);

        public string CreateEmployee(string id, string name, string phoneNumber);

        public string CreateConsumer();
    }
}
