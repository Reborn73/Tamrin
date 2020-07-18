using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tamrin.Common.Utilities;
using Tamrin.Data;
using Tamrin.Services.DataInitializer;

namespace Tamrin.WebFramework.Configuration
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseHsts(this IApplicationBuilder app, IHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseHsts();
            }
        }

        public static void InitialDatabaseAndData(this IApplicationBuilder app)
        {
            Assert.NotNull(app, nameof(app));

            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                #region Migrate DataBase

                var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
                dbContext.Database.Migrate();

                #endregion

                #region Initial Data

                var dataInitializer = scope.ServiceProvider.GetServices<IDataInitializer>();
                foreach (var initializer in dataInitializer)
                {
                    initializer.InitializeData();
                }

                #endregion

            }
        }
    }
}
