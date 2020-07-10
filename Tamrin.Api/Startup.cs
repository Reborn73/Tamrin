using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tamrin.Common;
using Tamrin.WebFramework.Configuration;
using Tamrin.WebFramework.Mapping;
using Tamrin.WebFramework.Middlewares;

namespace Tamrin.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly SiteSettings _siteSettings;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _siteSettings = Configuration.GetSection(nameof(Common.SiteSettings)).Get<SiteSettings>();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.InitializeAutoMapper();
            services.Configure<SiteSettings>(Configuration.GetSection(nameof(Common.SiteSettings)));
            services.AddMinimalMvc();
            services.AddDbContext(Configuration);
            services.Configure<IISServerOptions>(options => { options.AllowSynchronousIO = true; });
            services.AddJwtAuthentication(_siteSettings.JwtSettings);
            services.AddCustomApiVersioning();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.BuildAutofacServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Middleware For Handle Exception
            app.UseCustomExceptionHandlerMiddleware();

            app.UseHsts(env);

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
