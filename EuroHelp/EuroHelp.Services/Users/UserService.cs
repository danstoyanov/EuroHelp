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
            return this.data.Employees
                .Where(u => u.Id == user.GetId())
                .FirstOrDefault();
        }
    }
}
