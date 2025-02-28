namespace TicketSystem.Application.Statuses.Queries;

using MediatR;
using TicketSystem.Domain.Entities;

public record GetAllStatusesQuery() : IRequest<IEnumerable<Status>>;
