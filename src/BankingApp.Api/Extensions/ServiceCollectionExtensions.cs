using System.Reflection;

namespace BankingApp.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAccountProviderCore(this IServiceCollection services)
    {
        services.AddControllers()
            .AddApplicationPart(Assembly.GetExecutingAssembly());

        return services;
    }
}