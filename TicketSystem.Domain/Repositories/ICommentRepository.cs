using TicketSystem.Domain.Entities;

namespace TicketSystem.Domain.Repositories;

public interface ICommentRepository
{
    Task<Comment> AddAsync(Comment comment);
    Task<IEnumerable<Comment>> GetByTicketIdAsync(int ticketId);
    Task<Comment?> GetByIdAsync(int commentId);
    Task DeleteAsync(Comment comment);
    Task<Comment?> GetByTicketAndCommentIdAsync(int ticketId, int commentId);
}