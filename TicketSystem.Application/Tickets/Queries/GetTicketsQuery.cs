using MediatR;
using TicketSystem.Application.Tickets.Dtos;

namespace TicketSystem.Application.Tickets.Queries;
public record GetTicketsQuery : IRequest<IEnumerable<TicketResponseDto>>;
