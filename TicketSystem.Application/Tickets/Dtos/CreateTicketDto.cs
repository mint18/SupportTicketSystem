namespace TicketSystem.Application.Tickets.Dtos;

public class CreateTicketDto
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
}