using AutoMapper;
using Template.Application.Comments.Dtos;
using Template.Application.Criterias.Commands.CreateCriteriaCommand;
using Template.Application.Criterias.Dtos;
using Template.Application.Criterias.Mapping.Resolvers;
using Template.Application.Shared.Mapping.Resolvers;
using Template.Domain.Entities.Criterias;

namespace Template.Application.Criterias.Mapping.Profiles;

public class CriteriaProfile : Profile
{
    public CriteriaProfile()
    {
        CreateMap<CreateCriteriaCommand, Criteria>()
            .ForMember(dest => dest.CriteriaItems, opt => opt.MapFrom(src => src.CriteriaItems))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom<UserIdResolver<CreateCriteriaCommand, Criteria>>());

        CreateMap<CreateCriteriaItemDto, CriteriaItem>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom<ImageResolver>());

        CreateMap<Criteria, CriteriaDto>()
            .ForMember(dest => dest.CriteriaItems, opt => opt.MapFrom(src => src.CriteriaItems))
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
            .ForMember(dest => dest.Invoices, opt => opt.MapFrom(src => src.CriteriaBills))
			.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
			.ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber));

        CreateMap<CriteriaItem, GetCriteriaItemDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
        CreateMap<Comment, CommentDto>();
        CreateMap<CriteriaBill, CreateCriteriaBillsDto>();
    }
}