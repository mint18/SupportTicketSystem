namespace TicketSystem.Application.ApplicationUser.Commands;

using MediatR;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;

public class AssignUserRoleCommandHandler(UserManager<ApplicationUser> userManager) : IRequestHandler<AssignUserRoleCommand>
{
    public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId);
        if (user == null) throw new KeyNotFoundException("User not found");

        if (!await userManager.IsInRoleAsync(user, request.Role))
        {
            var result = await userManager.AddToRoleAsync(user, request.Role);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Failed to assign role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
    }
}