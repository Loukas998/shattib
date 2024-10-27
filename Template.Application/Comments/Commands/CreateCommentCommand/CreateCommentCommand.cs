using MediatR;
using Microsoft.AspNetCore.Http;

namespace Template.Application.Comments.Commands.CreateCommentCommand;

public class CreateCommentCommand : IRequest<int>
{
    public int CriteriaId { get; set; } = default!;
    public string Message { get; set; } = default!;
    public IFormFile? AttachmentFile { get; set; }
}