using System.Runtime.InteropServices;
using Template.Domain.Entities.Criteria;

namespace Template.Domain.Repositories;

public interface ICommentRepository
{
    public Task<int> CreateCommentAsync(Comment comment);

    public Task<List<Comment>> GetCommentsForCriteriaAsync(int criteriaId, [Optional] int? lastCommentId);

    public Task<Comment> GetCommentByIdAsync(int commentId);
}