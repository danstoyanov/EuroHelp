using Microsoft.EntityFrameworkCore;

using Xunit;

using EuroHelp.Data;

namespace EuroHelp.Tests.Web.Controllers
{
    public class InsuranceCompaniesControllerTests
    {
        private DbContextOptionsBuilder<EuroHelpDbContext> optionsBuilder;
        private EuroHelpDbContext db;

        public InsuranceCompaniesControllerTests()
        {
            this.optionsBuilder = new DbContextOptionsBuilder<EuroHelpDbContext>();
            this.db = new EuroHelpDbContext(optionsBuilder.Options);
        }


    }
}
