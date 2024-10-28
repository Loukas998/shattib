using AutoMapper;
using Template.Application.Comments.Commands.CreateCommentCommand;
using Template.Domain.Entities.Criteria;
using Template.Domain.Repositories;

namespace Template.Application.Comments.Dtos;

public class AttachmentResolver(IFileService fileService) : IValueResolver<CreateCommentCommand, Comment, string?>
{
    public string Resolve(CreateCommentCommand source, Comment destination, string? destMember,
        ResolutionContext context)
    {
        return source.AttachmentFile == null
            ? null
            : fileService.SaveFile(source.AttachmentFile!, "Comments", [".jpg", ".png"]);
    }
}