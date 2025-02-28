using MediatR;

namespace TicketSystem.Application.Tickets.Commands;

public record UpdateTicketStatusCommand(int TicketId, int StatusId) : IRequest;