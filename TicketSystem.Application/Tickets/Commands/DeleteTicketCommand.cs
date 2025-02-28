namespace TicketSystem.Application.Tickets.Commands;

using MediatR;

public record DeleteTicketCommand(int TicketId) : IRequest;