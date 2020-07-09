using Autofac;
using Tamrin.Common;
using Tamrin.Data;
using Tamrin.Data.Contracts;
using Tamrin.Data.Repositories;
using Tamrin.Entities.Common;
using Tamrin.Services.Services.Contracts;

namespace Tamrin.WebFramework.Configuration
{
    public static class AutofacConfigurationExtensions
    {
        public static void BuildAutofacServiceProvider(this ContainerBuilder containerBuilder)
        {
            containerBuilder.AddDependencyInjectionServices();
        }

        private static void AddDependencyInjectionServices(this ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            var commonAssembly = typeof(SiteSettings).Assembly;
            var entitiesAssembly = typeof(IBaseEntity).Assembly;
            var dataAssembly = typeof(ApplicationDbContext).Assembly;
            var servicesAssembly = typeof(IJwtService).Assembly;
            var webFrameworkAssembly = typeof(AutofacConfigurationExtensions).Assembly;

            containerBuilder.RegisterAssemblyTypes(commonAssembly, entitiesAssembly, dataAssembly, servicesAssembly,
                    webFrameworkAssembly)
                .AssignableTo<IScopedDependency>().AsImplementedInterfaces().InstancePerLifetimeScope();

            containerBuilder.RegisterAssemblyTypes(commonAssembly, entitiesAssembly, dataAssembly, servicesAssembly,
                    webFrameworkAssembly)
                .AssignableTo<ITransientDependency>().AsImplementedInterfaces().InstancePerDependency();

            containerBuilder.RegisterAssemblyTypes(commonAssembly, entitiesAssembly, dataAssembly, servicesAssembly,
                    webFrameworkAssembly)
                .AssignableTo<ISingletonDependency>().AsImplementedInterfaces().SingleInstance();

        }
    }
}
