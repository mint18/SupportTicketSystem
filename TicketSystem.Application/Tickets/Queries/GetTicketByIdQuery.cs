using MediatR;
using TicketSystem.Application.Tickets.Dtos;

namespace TicketSystem.Application.Tickets.Queries;

public record GetTicketByIdQuery(int Id) : IRequest<TicketResponseDto>;
