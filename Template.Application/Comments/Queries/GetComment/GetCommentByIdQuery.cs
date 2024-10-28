using MediatR;
using Template.Application.Comments.Dtos;

namespace Template.Application.Comments.Queries.GetComment;

public class GetCommentByIdQuery(int id) : IRequest<CommentDto>
{
    public int Id { get; set; } = id;
}