using MediatR;
using Microsoft.AspNetCore.Identity;
using TicketSystem.Application.ApplicationUser.Dtos;
using TicketSystem.Domain.Constants;

namespace TicketSystem.Application.ApplicationUser.Queries;

public record GetAssignableUsersQuery : IRequest<IEnumerable<UserDto>>;

public class GetAssignableUsersQueryHandler(UserManager<Domain.Entities.ApplicationUser> userManager)
    : IRequestHandler<GetAssignableUsersQuery, IEnumerable<UserDto>>
{
    public async Task<IEnumerable<UserDto>> Handle(GetAssignableUsersQuery request, CancellationToken cancellationToken)
    {
        var admins = await userManager.GetUsersInRoleAsync(Roles.Admin);

        return admins.Select(u => new UserDto(
            u.Id,
            u.UserName,
            u.Email,
            new[] { Roles.Admin }
        ));
    }
}