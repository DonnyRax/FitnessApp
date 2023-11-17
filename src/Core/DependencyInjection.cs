using Core.Clock;
using Core.Interfaces.Clock;
using Core.Security.Token;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<ITokenService, TokenService>();  

        return services;
    }
}