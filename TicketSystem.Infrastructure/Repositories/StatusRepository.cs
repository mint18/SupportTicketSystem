using Microsoft.EntityFrameworkCore;
using TicketSystem.Domain.Entities;
using TicketSystem.Domain.Repositories;
using TicketSystem.Infrastructure.Persistence;

namespace TicketSystem.Infrastructure.Repositories;

public class StatusRepository : IStatusRepository

{
    private readonly TicketsDbContext _context;

    public StatusRepository(TicketsDbContext context)
    {
        _context = context;
    }

    public async Task<Status> AddAsync(Status status)
    {
        _context.Statuses.Add(status);
        await _context.SaveChangesAsync();
        return status;
    }

    public async Task<Status?> GetByIdAsync(int id)
    {
        return await _context.Statuses.FindAsync(id);
    }

    public async Task<Status?> GetByNameAsync(string name)
    {
        return await _context.Statuses
            .FirstOrDefaultAsync(s => s.StatusName == name);
    }

    public async Task<IEnumerable<Status>> GetAllAsync()
    {
        return await _context.Statuses.ToListAsync();
    }
}
