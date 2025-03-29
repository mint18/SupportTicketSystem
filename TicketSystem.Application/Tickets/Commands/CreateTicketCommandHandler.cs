namespace TicketSystem.Application.Tickets.Commands;

using MediatR;
using TicketSystem.Application.ApplicationUser;
using TicketSystem.Domain.Entities;
using TicketSystem.Domain.Repositories;

public class CreateTicketCommandHandler(ITicketRepository ticketRepository, 
    IStatusRepository statusRepository,
    IUserContext userContext) : IRequestHandler<CreateTicketCommand, int>
{
    public async Task<int> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        var status = await statusRepository.GetByNameAsync("Open");
        if (status == null) throw new Exception("Status 'Open' nie istnieje.");

        var currentUser = userContext.GetCurrentUser();

        var ticket = new Ticket
        {
            Title = request.Title,
            Description = request.Description,
            CreatedDate = DateTime.UtcNow,
            Status = status,
            CreatedById = currentUser.Id
        };

        var createdTicket = await ticketRepository.AddAsync(ticket);
        return createdTicket.Id;
    }
}