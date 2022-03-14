using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using EuroHelp.Data.Models;
using Microsoft.AspNetCore.Identity;

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

        public DbSet<Consumer> Consumers { get; init; }

        public DbSet<Employee> Employees { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Damage>()
                .HasOne(c => c.Company)
                .WithMany(c => c.Damages)
                .HasForeignKey(c => c.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InsuranceCompany>()
                .HasOne(c => c.Employee)
                .WithMany(e => e.Companies)
                .HasForeignKey(c => c.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
