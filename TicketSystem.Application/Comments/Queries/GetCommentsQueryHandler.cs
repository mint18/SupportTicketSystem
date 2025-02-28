namespace TicketSystem.Application.Comments.Queries;

using MediatR;
using TicketSystem.Domain.Repositories;

public class GetCommentsQueryHandler : IRequestHandler<GetCommentsQuery, IEnumerable<CommentDto>>
{
    private readonly ICommentRepository _commentRepository;

    public GetCommentsQueryHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<IEnumerable<CommentDto>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
    {
        var comments = await _commentRepository.GetByTicketIdAsync(request.TicketId);
        return comments.Select(c => new CommentDto(c.Id, c.CommentId, c.Content, c.CreatedDate));
    }
}