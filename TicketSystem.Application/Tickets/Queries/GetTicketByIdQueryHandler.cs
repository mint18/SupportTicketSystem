using AutoMapper;
using MediatR;
using TicketSystem.Application.Tickets.Dtos;
using TicketSystem.Domain.Repositories;

namespace TicketSystem.Application.Tickets.Queries;

public class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, TicketResponseDto>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IMapper _mapper;

    public GetTicketByIdQueryHandler(ITicketRepository ticketRepository, IMapper mapper)
    {
        _ticketRepository = ticketRepository;
        _mapper = mapper;
    }

    public async Task<TicketResponseDto> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
    {
        var ticket = await _ticketRepository.GetByIdAsync(request.Id);
        if (ticket == null)
        {
            throw new KeyNotFoundException($"Ticket with ID {request.Id} not found.");
        }
        return _mapper.Map<TicketResponseDto>(ticket);
    }
}
