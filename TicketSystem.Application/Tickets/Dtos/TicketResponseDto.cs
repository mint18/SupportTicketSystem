namespace TicketSystem.Application.Tickets.Dtos;

public class TicketResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime CreatedDate { get; set; }
    public string? StatusName { get; set; }
    public string? CreatedById { get; set; }
    public string? CreatedByName { get; set; }
    public string? AssignedToId { get; set; }
    public string? AssignedToName { get; set; }
}
