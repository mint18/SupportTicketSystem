namespace TicketSystem.Application.Identity.Commands;

using MediatR;

public record UpdateUserDetailsCommand(string? Department, string? Position) : IRequest;