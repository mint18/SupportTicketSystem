using Microsoft.EntityFrameworkCore;
using TicketSystem.Domain.Entities;
using TicketSystem.Domain.Repositories;
using TicketSystem.Infrastructure.Persistence;

namespace TicketSystem.Infrastructure.Repositories;

public class TicketRepository(TicketsDbContext context) : ITicketRepository
{
    public async Task<Ticket> AddAsync(Ticket ticket)
    {
        context.Tickets.Add(ticket);
        await context.SaveChangesAsync();
        return ticket;
    }

    public async Task<IEnumerable<Ticket>> GetAllAsync(string userId, bool isAdmin)
    {
        return await context.Tickets
            .Include(t => t.Status)
            .Where(t => isAdmin
                ? (t.AssignedToId == userId || t.CreatedById == userId)
                : t.CreatedById == userId)
            .ToListAsync();
    }

    //public async Task<Ticket?> GetByIdAsync(int id)
    //{
    //    return await _context.Tickets.FirstOrDefaultAsync(t => t.Id == id);
    //}

    public async Task<Ticket?> GetByIdAsync(int ticketId)
    {
        return await context.Tickets
            .Include(t => t.Status)
            .Include(t => t.Comments)
            .Include(t => t.CreatedBy)
            .Include(t => t.AssignedTo)
            .FirstOrDefaultAsync(t => t.Id == ticketId);
    }

    public async Task UpdateAsync(Ticket ticket)
    {
        context.Tickets.Update(ticket);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Ticket ticket)
    {
        context.Tickets.Remove(ticket);
        await context.SaveChangesAsync();
    }
}
