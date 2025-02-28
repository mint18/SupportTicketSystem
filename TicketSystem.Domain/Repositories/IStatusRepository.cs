
using TicketSystem.Domain.Entities;

namespace TicketSystem.Domain.Repositories;

public interface IStatusRepository
{
    Task<Status> AddAsync(Status status);

    Task<Status?> GetByIdAsync(int id);
    Task<IEnumerable<Status>> GetAllAsync();
}
