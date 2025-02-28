using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem.Application.Tickets.Dtos;

public class TicketResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime CreatedDate { get; set; }
    public string? StatusName { get; set; } 
}
