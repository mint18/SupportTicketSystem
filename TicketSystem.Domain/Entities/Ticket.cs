namespace TicketSystem.Domain.Entities;

public class Ticket
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime CreatedDate { get; set; }

    public Status? Status { get; set; }
    public ICollection<Comment> Comments { get; set; } = [];
}
