using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EuroHelp.Data
{
    public class EuroHelpDbContext : IdentityDbContext
    {
        public EuroHelpDbContext(DbContextOptions<EuroHelpDbContext> options)
            : base(options)
        {

        }
    }
}
