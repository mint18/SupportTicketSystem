using AutoMapper;
using MediatR;
using TicketSystem.Application.Tickets.Dtos;
using TicketSystem.Domain.Repositories;

namespace TicketSystem.Application.Tickets.Queries;

public class GetTicketsQueryHandler : IRequestHandler<GetTicketsQuery, IEnumerable<TicketResponseDto>>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IMapper _mapper;

    public GetTicketsQueryHandler(ITicketRepository ticketRepository, IMapper mapper)
    {
        _ticketRepository = ticketRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TicketResponseDto>> Handle(GetTicketsQuery request, CancellationToken cancellationToken)
    {
        var tickets = await _ticketRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<TicketResponseDto>>(tickets);
    }
}
