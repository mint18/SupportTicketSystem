namespace TicketSystem.Application.Identity.Commands;

using MediatR;
using Microsoft.AspNetCore.Identity;
using TicketSystem.Application.ApplicationUser;
using TicketSystem.Domain.Entities;

public class UpdateUserDetailsCommandHandler(IUserContext userContext, IUserStore<ApplicationUser> userStore)
   : IRequestHandler<UpdateUserDetailsCommand>
{
    public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        if (currentUser == null) throw new UnauthorizedAccessException("User not authenticated");

        var user = await userStore.FindByIdAsync(currentUser.Id, cancellationToken);
        if (user == null) throw new KeyNotFoundException("User not found");

        user.Department = request.Department;
        user.Position = request.Position;

        await userStore.UpdateAsync(user, cancellationToken);
    }
}