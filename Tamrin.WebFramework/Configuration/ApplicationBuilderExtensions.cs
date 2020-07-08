using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

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
    }
}
