namespace TicketSystem.Application.Comments.Commands;

using MediatR;

public record CreateCommentCommand(int TicketId, string Content) : IRequest<int>;