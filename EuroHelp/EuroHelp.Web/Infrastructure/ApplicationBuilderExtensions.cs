using EuroHelp.Data;

using Microsoft.EntityFrameworkCore;


namespace EuroHelp.Web.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase
            (this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();
            var data = scopedServices.ServiceProvider.GetService<EuroHelpDbContext>();

            data.Database.Migrate();

            return app;
        }
    }
}
