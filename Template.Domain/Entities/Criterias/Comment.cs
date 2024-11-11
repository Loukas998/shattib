namespace Template.Domain.Entities.Criterias;

public class Comment
{
    public int Id { get; set; }
    public string UserId { get; set; } = default!;
    public int CriteriaId { get; set; }
    public string Message { get; set; } = default!;
    public string? Attachment { get; set; }

    public Criteria Criteria { get; set; }
    public User user { get; set; }
}