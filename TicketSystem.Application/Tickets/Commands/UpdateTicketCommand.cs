namespace TicketSystem.Application.Tickets.Commands;

using MediatR;

public record UpdateTicketCommand(int TicketId, string? Title, string? Description) : IRequest;