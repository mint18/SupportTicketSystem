namespace TicketSystem.Application.ApplicationUser.Queries;

using MediatR;
using TicketSystem.Application.ApplicationUser.Dtos;

public record GetUsersQuery : IRequest<IEnumerable<UserDto>>;