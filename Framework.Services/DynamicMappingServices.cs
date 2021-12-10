using System;
using System.Linq;
using Framework.Services.Internal;
using Framework.Utilities.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Services
{
    public static class DynamicMappingServices
    {
        public static IServiceCollection MapAllServicesByConvention(this IServiceCollection services, Type typeClassToGetServicesAssembly)
        {
            var servicesAssemblyName = typeClassToGetServicesAssembly.Assembly.GetName().Name;

            if (!string.IsNullOrEmpty(servicesAssemblyName))
            {
                var serviceInterfaces =
                    AssemblyExplorer.GetInterfacesInAssemblyDescendantsOfType(servicesAssemblyName, typeof(IServiceBaseIndicator));

                var serviceImplementations =
                    AssemblyExplorer.GetClassesInAssemblyDescendantsOfType(servicesAssemblyName, typeof(IServiceBaseIndicator));

                foreach (var service in serviceImplementations)
                {
                    services.AddScoped(service);

                    var serviceImplementation =
                        serviceInterfaces.FirstOrDefault(i => i.Name[1..].Equals(service.Name));

                    if (serviceImplementation != null)
                        services.AddScoped(serviceImplementation, service);
                }
            }

            return services;
        }
    }
}
