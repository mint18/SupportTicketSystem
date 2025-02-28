using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TicketSystem.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using TicketSystem.Domain.Repositories;
using TicketSystem.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using TicketSystem.Domain.Entities;

namespace TicketSystem.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        var connectionString = configuration.GetConnectionString("TicketsDb");
        services.AddDbContext<TicketsDbContext>(options => options.UseSqlServer(connectionString));

        services.AddIdentityApiEndpoints<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<TicketsDbContext>();

        services.AddScoped<IStatusRepository, StatusRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();
        
    }

}
