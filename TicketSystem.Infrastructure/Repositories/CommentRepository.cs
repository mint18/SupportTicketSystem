using Microsoft.EntityFrameworkCore;
using TicketSystem.Domain.Entities;
using TicketSystem.Domain.Repositories;
using TicketSystem.Infrastructure.Persistence;

namespace TicketSystem.Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly TicketsDbContext _context;

    public CommentRepository(TicketsDbContext context)
    {
        _context = context;
    }

    public async Task<Comment> AddAsync(Comment comment)
    {
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
        return comment;
    }

    public async Task<IEnumerable<Comment>> GetByTicketIdAsync(int ticketId)
    {
        return await _context.Comments
            .Where(c => c.Ticket.Id == ticketId)
            .ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int commentId)
    {
        return await _context.Comments.FindAsync(commentId);
    }
    public async Task DeleteAsync(Comment comment)
    {
        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
    }
    public async Task<Comment?> GetByTicketAndCommentIdAsync(int ticketId, int commentId)
    {
        return await _context.Comments
            .FirstOrDefaultAsync(c => c.Ticket.Id == ticketId && c.CommentId == commentId);
    }
}