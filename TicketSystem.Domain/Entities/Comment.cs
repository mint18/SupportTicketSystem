namespace TicketSystem.Domain.Entities;

public class Comment
{
    public int Id { get; set; }
    public int CommentId { get; set; }
    public string Content { get; set; } = default!;
    public DateTime CreatedDate { get; set; }

    public Ticket Ticket { get; set; } = default!;

}

