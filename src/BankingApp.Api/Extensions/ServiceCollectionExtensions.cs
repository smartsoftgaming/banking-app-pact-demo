using System.Reflection;

namespace BankingApp.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProviderCore(this IServiceCollection services)
    {
        services.AddControllers()
            .AddApplicationPart(Assembly.GetExecutingAssembly());

        return services;
    }
}