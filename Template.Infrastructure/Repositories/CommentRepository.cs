using Template.Domain.Entities.Criteria;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories;

public class CommentRepository(TemplateDbContext dbContext) : ICommentRepository
{
    public async Task<List<Comment>> GetCommentsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<int> CreateCommentAsync(Comment comment)
    {
        dbContext.Comments.Add(comment);
        await dbContext.SaveChangesAsync();
        return comment.Id;
    }
}