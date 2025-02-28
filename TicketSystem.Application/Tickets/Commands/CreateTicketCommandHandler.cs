namespace TicketSystem.Application.Tickets.Commands;

using MediatR;
using TicketSystem.Domain.Entities;
using TicketSystem.Domain.Repositories;

public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, int>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IStatusRepository _statusRepository;

    public CreateTicketCommandHandler(ITicketRepository ticketRepository, IStatusRepository statusRepository)
    {
        _ticketRepository = ticketRepository;
        _statusRepository = statusRepository;
    }

    public async Task<int> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        var status = await _statusRepository.GetByIdAsync(1);
        if (status == null) throw new Exception("Status o ID 1 nie istnieje.");

        var ticket = new Ticket
        {
            Title = request.Title,
            Description = request.Description,
            CreatedDate = DateTime.UtcNow,
            Status = status
        };

        var createdTicket = await _ticketRepository.AddAsync(ticket);
        return createdTicket.Id;
    }
}
