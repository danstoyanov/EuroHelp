using EuroHelp.Data;
using EuroHelp.Web.Models.Companies;
using System.Security.Claims;

namespace EuroHelp.Web.Infrastructure
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}
