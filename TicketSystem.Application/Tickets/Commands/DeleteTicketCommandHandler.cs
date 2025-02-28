namespace TicketSystem.Application.Tickets.Commands;

using MediatR;
using TicketSystem.Domain.Entities;
using TicketSystem.Domain.Repositories;

public class DeleteTicketCommandHandler : IRequestHandler<DeleteTicketCommand>
{
    private readonly ITicketRepository _ticketRepository;

    public DeleteTicketCommandHandler(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _ticketRepository.GetByIdAsync(request.TicketId);
        if (ticket == null) throw new Exception("Ticket not found");

        await _ticketRepository.DeleteAsync(ticket);
    }
}