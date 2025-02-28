namespace TicketSystem.Application.Comments.Queries;

public record CommentDto(int Id, int CommentId, string Content, DateTime CreatedDate);