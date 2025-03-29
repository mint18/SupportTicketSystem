namespace TicketSystem.Application.Tickets.Commands;

using MediatR;

public record AssignTicketCommand(int TicketId, string? AssignedToId) : IRequest;