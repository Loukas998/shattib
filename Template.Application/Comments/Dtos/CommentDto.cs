namespace Template.Application.Comments.Dtos;

public class CommentDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = default!;
    public string Message { get; set; } = default!;
    public string? Attachment { get; set; }
}