using TicketSystem.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TicketSystem.Domain.Entities;

namespace TicketSystem.Infrastructure.Seeders;

public class StatusSeeder(TicketsDbContext dbContext) : IStatusSeeder
{
    public async Task SeedStatusesAsync()
    {
        if (!await dbContext.Statuses.AnyAsync())
        {
            await dbContext.Statuses.AddRangeAsync(
                new Status { StatusName = "Open" },
                new Status { StatusName = "In progress" },
                new Status { StatusName = "Closed" }
            );
            await dbContext.SaveChangesAsync();
        }
    }
}
