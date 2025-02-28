namespace TicketSystem.Application.Comments.Queries;

using MediatR;

public record GetCommentsQuery(int TicketId) : IRequest<IEnumerable<CommentDto>>;