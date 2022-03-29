using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

using static EuroHelp.Data.DataConstants.User;

namespace EuroHelp.Data.Models
{
    public class User : IdentityUser
    {
        [MaxLength(FullNameMaxLength)]
        public string? FullName { get; set; }
    }
}
