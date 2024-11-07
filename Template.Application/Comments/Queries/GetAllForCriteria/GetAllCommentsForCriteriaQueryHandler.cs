using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Comments.Dtos;
using Template.Domain.Repositories;

namespace Template.Application.Comments.Queries.GetAllForCriteria;

public class GetAllCommentsForCriteriaQueryHandler(
    ILogger<GetAllCommentsForCriteriaQuery> logger,
    IMapper mapper,
    ICommentRepository commentRepository) : IRequestHandler<GetAllCommentsForCriteriaQuery, IEnumerable<CommentDto>>
{
    public async Task<IEnumerable<CommentDto>> Handle(GetAllCommentsForCriteriaQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all Comments for Criteria with Id: {CriteriaId}", request.CriteriaId);
        var comments = await commentRepository.GetCommentsForCriteriaAsync(request.CriteriaId, request.LastCommentId);
        var commentsDto = mapper.Map<IEnumerable<CommentDto>>(comments);
        return commentsDto;
    }
}