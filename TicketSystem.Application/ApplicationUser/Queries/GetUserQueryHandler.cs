namespace TicketSystem.Application.ApplicationUser.Queries;

using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using TicketSystem.Application.ApplicationUser.Dtos;

public class GetUsersQueryHandler(UserManager<ApplicationUser> userManager) : IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
{
    public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await userManager.Users.ToListAsync(cancellationToken);
        var result = new List<UserDto>();

        foreach (var user in users)
        {
            var roles = await userManager.GetRolesAsync(user);
            result.Add(new UserDto(user.Id, user.UserName, user.Email, roles));
        }

        return result;
    }
}