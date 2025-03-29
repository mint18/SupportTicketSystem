using TicketSystem.Domain.Entities;

namespace TicketSystem.Domain.Repositories;

public interface ITicketRepository
{
    Task<Ticket> AddAsync(Ticket ticket);
    Task<IEnumerable<Ticket>> GetAllAsync(string userId, bool isAdmin);
    Task<Ticket?> GetByIdAsync(int id);
    Task UpdateAsync(Ticket ticket);
    Task DeleteAsync(Ticket ticket);

}
