using MediatR;

namespace TicketSystem.Application.Comments.Queries;

public record GetCommentQuery(int TicketId, int CommentId) : IRequest<CommentDto?>;