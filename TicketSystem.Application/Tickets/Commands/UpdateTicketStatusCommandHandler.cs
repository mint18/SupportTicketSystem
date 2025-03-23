using MediatR;
using TicketSystem.Application.Tickets.Commands;
using TicketSystem.Domain.Repositories;

public class UpdateTicketStatusCommandHandler(
    ITicketRepository ticketRepository,
    IStatusRepository statusRepository) : IRequestHandler<UpdateTicketStatusCommand>
{
    public async Task Handle(UpdateTicketStatusCommand request, CancellationToken cancellationToken)
    {
        var ticket = await ticketRepository.GetByIdAsync(request.TicketId);
        if (ticket == null)
        {
            throw new KeyNotFoundException($"Ticket with ID {request.TicketId} not found.");
        }

        var status = await statusRepository.GetByIdAsync(request.StatusId);
        if (status == null)
        {
            throw new KeyNotFoundException($"Status with ID {request.StatusId} not found.");
        }

        ticket.Status = status;
        await ticketRepository.UpdateAsync(ticket);
    }
}