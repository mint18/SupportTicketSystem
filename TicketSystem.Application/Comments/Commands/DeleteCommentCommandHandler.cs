namespace TicketSystem.Application.Comments.Commands;

using MediatR;
using TicketSystem.Domain.Repositories;

public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand>
{
    private readonly ICommentRepository _commentRepository;
    private readonly ITicketRepository _ticketRepository;

    public DeleteCommentCommandHandler(ICommentRepository commentRepository, ITicketRepository ticketRepository)
    {
        _commentRepository = commentRepository;
        _ticketRepository = ticketRepository;
    }

    public async Task Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _ticketRepository.GetByIdAsync(request.TicketId);
        if (ticket == null) throw new Exception("Ticket not found");

        var comment = ticket.Comments.FirstOrDefault(c => c.CommentId == request.CommentId);
        if (comment == null) throw new Exception("Comment not found in this ticket");

        await _commentRepository.DeleteAsync(comment);
    }
}