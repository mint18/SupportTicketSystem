namespace TicketSystem.Infrastructure.Seeders;

using Microsoft.AspNetCore.Identity;
using TicketSystem.Domain.Constants;

public class RoleSeeder(RoleManager<IdentityRole> roleManager) : IRoleSeeder
{
    public async Task SeedRolesAsync()
    {
        if (!roleManager.Roles.Any())
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Customer));
            await roleManager.CreateAsync(new IdentityRole(Roles.Manager));
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
        }
    }
}