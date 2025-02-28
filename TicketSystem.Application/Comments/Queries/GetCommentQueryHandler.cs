using MediatR;
using global::TicketSystem.Domain.Entities;
using TicketSystem.Application.Comments.Queries;
using TicketSystem.Domain.Repositories;

public class GetCommentQueryHandler : IRequestHandler<GetCommentQuery, CommentDto?>
{
    private readonly ICommentRepository _commentRepository;

    public GetCommentQueryHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<CommentDto?> Handle(GetCommentQuery request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetByTicketAndCommentIdAsync(request.TicketId, request.CommentId);
        return comment is not null ? new CommentDto(comment.Id, comment.CommentId, comment.Content, comment.CreatedDate) : null;
    }
}