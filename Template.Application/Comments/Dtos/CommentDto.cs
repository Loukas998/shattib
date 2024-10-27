namespace Template.Application.Comments.Dtos;

public class CommentDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Message { get; set; } = default!;
    public string? Attachment { get; set; }
}