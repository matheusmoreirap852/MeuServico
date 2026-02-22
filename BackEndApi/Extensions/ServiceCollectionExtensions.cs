using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BackEndApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(
            this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var servicesToRegister =
                assembly.GetTypes()
                .Where(t =>
                    t.IsClass &&
                    !t.IsAbstract &&
                    t.Name.EndsWith("Service"));

            foreach (var implementation in servicesToRegister)
            {
                var interfaces = implementation.GetInterfaces();

                foreach (var serviceInterface in interfaces)
                {
                    services.AddScoped(
                        serviceInterface,
                        implementation);
                }
            }
        }
    }
}