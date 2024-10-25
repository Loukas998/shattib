using Template.Domain.Entities.Criteria;

namespace Template.Domain.Repositories;

public interface ICommentRepository
{
    public Task<int> CreateCommentAsync(Comment comment);

    public Task<List<Comment>> GetCommentsAsync();
}