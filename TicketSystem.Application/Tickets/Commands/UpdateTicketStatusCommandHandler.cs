using MediatR;
using TicketSystem.Application.Tickets.Commands;
using TicketSystem.Domain.Repositories;

public class UpdateTicketStatusCommandHandler : IRequestHandler<UpdateTicketStatusCommand>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IStatusRepository _statusRepository;

    public UpdateTicketStatusCommandHandler(ITicketRepository ticketRepository, IStatusRepository statusRepository)
    {
        _ticketRepository = ticketRepository;
        _statusRepository = statusRepository;
    }

    public async Task Handle(UpdateTicketStatusCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _ticketRepository.GetByIdAsync(request.TicketId);
        if (ticket == null)
        {
            throw new KeyNotFoundException($"Ticket with ID {request.TicketId} not found.");
        }

        var status = await _statusRepository.GetByIdAsync(request.StatusId);
        if (status == null)
        {
            throw new KeyNotFoundException($"Status with ID {request.StatusId} not found.");
        }

        ticket.Status = status;
        await _ticketRepository.UpdateAsync(ticket);
    }
}