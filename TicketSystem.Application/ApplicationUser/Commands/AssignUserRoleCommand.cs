namespace TicketSystem.Application.ApplicationUser.Commands;

using MediatR;

public record AssignUserRoleCommand(string UserId, string Role) : IRequest;