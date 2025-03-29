namespace TicketSystem.Application.Tickets.Commands;

using MediatR;
using TicketSystem.Domain.Repositories;

public class AssignTicketCommandHandler(ITicketRepository ticketRepository) : IRequestHandler<AssignTicketCommand>
{
    public async Task Handle(AssignTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = await ticketRepository.GetByIdAsync(request.TicketId);
        if (ticket == null)
            throw new KeyNotFoundException($"Ticket with ID {request.TicketId} not found.");

        ticket.AssignedToId = request.AssignedToId;
        await ticketRepository.UpdateAsync(ticket);
    }
}