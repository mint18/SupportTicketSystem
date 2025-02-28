namespace TicketSystem.Application.Comments.Commands;

using MediatR;

public record DeleteCommentCommand(int TicketId, int CommentId) : IRequest;