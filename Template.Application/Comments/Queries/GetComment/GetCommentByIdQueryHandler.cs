using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Comments.Dtos;
using Template.Domain.Repositories;

namespace Template.Application.Comments.Queries.GetComment;

public class GetCommentByIdQueryHandler(
    ILogger<GetCommentByIdQueryHandler> logger,
    IMapper mapper,
    ICommentRepository commentRepository) : IRequestHandler<GetCommentByIdQuery, CommentDto>
{
    public async Task<CommentDto> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting comment by id: {Id}", request.Id);
        var comment = await commentRepository.GetCommentByIdAsync(request.Id);
        if (comment == null) throw new NotImplementedException();
        return mapper.Map<CommentDto>(comment);
    }
}