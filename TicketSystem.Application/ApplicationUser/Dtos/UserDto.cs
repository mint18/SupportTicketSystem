namespace TicketSystem.Application.ApplicationUser.Dtos;

public record UserDto(string Id, string UserName, string Email, IEnumerable<string> Roles);