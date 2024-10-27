using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Template.Application.Comments.Commands.CreateCommentCommand;
using Template.Domain.Entities.Criteria;

namespace Template.Application.Comments.Dtos;

public class CommentProfile : Profile
{
    private readonly IWebHostEnvironment _environment;
    public CommentProfile(IWebHostEnvironment environment)
    {
        CreateMap<Comment, CommentDto>();
        CreateMap<CreateCommentCommand, Comment>()
            .ForMember(dest => dest.Attachment,
                opt => opt.Ignore()
            );
    }
}