namespace TicketSystem.Application.Comments.Commands;

using MediatR;
using TicketSystem.Domain.Entities;
using TicketSystem.Domain.Repositories;

public class CreateCommentCommandHandler(ITicketRepository ticketRepository, ICommentRepository commentRepository) : IRequestHandler<CreateCommentCommand, int>
{
    public async Task<int> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var ticket = await ticketRepository.GetByIdAsync(request.TicketId);
        if (ticket == null) throw new Exception("Ticket not found");

        var maxCommentId = ticket.Comments.Any() ? ticket.Comments.Max(c => c.CommentId) : 0;

        var comment = new Comment
        {
            CommentId = maxCommentId + 1,
            Content = request.Content,
            CreatedDate = DateTime.UtcNow,
            Ticket = ticket
        };

        var createdComment = await commentRepository.AddAsync(comment);
        return createdComment.CommentId;
    }
}