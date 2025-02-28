using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using TicketSystem.Application.ApplicationUser;

namespace TicketSystem.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtension).Assembly;

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
        services.AddValidatorsFromAssembly(applicationAssembly)
                .AddFluentValidationAutoValidation();
        services.AddAutoMapper(applicationAssembly);

        services.AddScoped<UserContext>();
        services.AddHttpContextAccessor();
    }
}
