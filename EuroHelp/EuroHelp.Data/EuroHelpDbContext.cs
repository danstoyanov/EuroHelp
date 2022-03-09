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

        public DbSet<User> Users { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Damage>()
                .HasOne(u => u.User)
                .WithMany(u => u.Damages)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Damage>()
                .HasOne(c => c.Company)
                .WithMany(c => c.Damages)
                .HasForeignKey(c => c.CompanyId)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }
    }
}
