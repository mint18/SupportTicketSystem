namespace TicketSystem.Application.Tickets.Commands;

using MediatR;
using TicketSystem.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand>
{
    private readonly ITicketRepository _ticketRepository;

    public UpdateTicketCommandHandler(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _ticketRepository.GetByIdAsync(request.TicketId);
        if (ticket == null) throw new KeyNotFoundException($"Ticket with ID {request.TicketId} not found.");

        if (request.Title != null) ticket.Title = request.Title;
        if (request.Description != null) ticket.Description = request.Description;

        await _ticketRepository.UpdateAsync(ticket);
    }
}