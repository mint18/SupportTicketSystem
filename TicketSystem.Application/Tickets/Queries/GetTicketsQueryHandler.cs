using AutoMapper;
using MediatR;
using TicketSystem.Application.ApplicationUser;
using TicketSystem.Application.Tickets.Dtos;
using TicketSystem.Domain.Constants;
using TicketSystem.Domain.Repositories;

namespace TicketSystem.Application.Tickets.Queries;

public class GetTicketsQueryHandler(
    ITicketRepository ticketRepository,
    IMapper mapper,
    IUserContext userContext) : IRequestHandler<GetTicketsQuery, IEnumerable<TicketResponseDto>>
{

    public async Task<IEnumerable<TicketResponseDto>> Handle(GetTicketsQuery request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        var isAdmin = currentUser.IsInRole(Roles.Admin);

        var tickets = await ticketRepository.GetAllAsync(currentUser.Id, isAdmin);
        return mapper.Map<IEnumerable<TicketResponseDto>>(tickets);
    }
}
