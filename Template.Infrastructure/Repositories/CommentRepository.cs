using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities.Criteria;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories;

public class CommentRepository(TemplateDbContext dbContext) : ICommentRepository
{
    public async Task<List<Comment>> GetCommentsForCriteriaAsync(int criteriaId, int? lastCommentId)
    {
        // return await dbContext.Comments.Where(comment => comment.CriteriaId == criteriaId).ToListAsync();
        var query = dbContext.Comments.Where(comment => comment.CriteriaId == criteriaId).AsQueryable();
        if (lastCommentId != null) query = query.Where(c => c.Id > lastCommentId);

        return await query.ToListAsync();
    }

    public async Task<int> CreateCommentAsync(Comment comment)
    {
        dbContext.Comments.Add(comment);
        await dbContext.SaveChangesAsync();
        return comment.Id;
    }

    public async Task<Comment> GetCommentByIdAsync(int commentId)
    {
        return await dbContext.Comments.FindAsync(commentId);
    }
}