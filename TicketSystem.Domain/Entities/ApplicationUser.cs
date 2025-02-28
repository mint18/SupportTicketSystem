using Microsoft.AspNetCore.Identity;

namespace TicketSystem.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string? Department { get; set; }
    public bool IsApproved { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public string? Position { get; set; }
}