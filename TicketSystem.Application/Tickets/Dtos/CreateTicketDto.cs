using System.ComponentModel.DataAnnotations;

namespace TicketSystem.Application.Tickets.Dtos;

public class CreateTicketDto
{
    [Required(ErrorMessage = "Title is required.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; set; }
}