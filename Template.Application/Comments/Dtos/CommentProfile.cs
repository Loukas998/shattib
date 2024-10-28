using AutoMapper;
using Template.Application.Comments.Commands.CreateCommentCommand;
using Template.Domain.Entities.Criteria;

namespace Template.Application.Comments.Dtos;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<Comment, CommentDto>();
        CreateMap<CreateCommentCommand, Comment>()
            .ForMember(dest => dest.Attachment,
                opt => opt.MapFrom<AttachmentResolver>()
            );
    }
}