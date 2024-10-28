using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Domain.Entities.Criteria;
using Template.Domain.Repositories;

namespace Template.Application.Comments.Commands.CreateCommentCommand;

public class CreateCommentCommandHandler(
    ILogger<CreateCommentCommandHandler> logger,
    IMapper mapper,
    ICommentRepository commentRepository,
    IFileService fileService) : IRequestHandler<CreateCommentCommand, int>
{
    public async Task<int> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating Comment {@Comment}", request);
        var comment = mapper.Map<Comment>(request);
        var id = await commentRepository.CreateCommentAsync(comment);
        return id;
    }
}