namespace TicketSystem.Application.Tickets.Commands;

using MediatR;

public record CreateTicketCommand(string Title, string Description) : IRequest<int>;
