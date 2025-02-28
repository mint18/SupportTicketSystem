namespace TicketSystem.Application.Statuses.Commands;

using MediatR;

public record CreateStatusCommand(string StatusName) : IRequest<int>;
