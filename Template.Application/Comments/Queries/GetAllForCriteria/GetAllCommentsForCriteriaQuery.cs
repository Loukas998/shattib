using MediatR;
using Template.Application.Comments.Dtos;

namespace Template.Application.Comments.Queries.GetAllForCriteria;

public class GetAllCommentsForCriteriaQuery : IRequest<IEnumerable<CommentDto>>
{
    public int CriteriaId { get; set; } = default!;
    public int? LastCommentId { get; set; }
}