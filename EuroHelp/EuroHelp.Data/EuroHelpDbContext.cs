using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
