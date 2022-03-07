using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using EuroHelp.Data.Models;

namespace EuroHelp.Data
{
    public class EuroHelpDbContext : IdentityDbContext
    {
        public EuroHelpDbContext(DbContextOptions<EuroHelpDbContext> options)
            : base(options)
        {

        }

        public DbSet<Damage> Damages { get; init; }

        public DbSet<InsuranceCompany> InsuranceCompanies { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
