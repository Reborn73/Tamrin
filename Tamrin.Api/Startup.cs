using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tamrin.Common;
using Tamrin.Data;
using Tamrin.Data.Contracts;
using Tamrin.Data.Repositories;
using Tamrin.Services.Services.Contracts;
using Tamrin.Services.Services.Implementation;
using Tamrin.WebFramework.Configuration;
using Tamrin.WebFramework.Middlewares;

namespace Tamrin.Api
{
    public class Startup
    {
        #region Constructor

        public IConfiguration Configuration { get; }
        private readonly SiteSettings _siteSettings;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            _siteSettings = Configuration.GetSection(nameof(Common.SiteSettings)).Get<SiteSettings>();
        }

        #endregion

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<SiteSettings>(Configuration.GetSection(nameof(Common.SiteSettings)));

            services.AddControllers(options =>
            {
                options.Filters.Add(new AuthorizeFilter());
            });

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("TamrinSqlServer"));
            });

            services.Configure<IISServerOptions>(options => { options.AllowSynchronousIO = true; });

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(IJwtService), typeof(JwtService));

            services.AddJwtAuthentication(_siteSettings.JwtSettings);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Middleware For Handle Exception
            app.UseCustomExceptionHandlerMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
