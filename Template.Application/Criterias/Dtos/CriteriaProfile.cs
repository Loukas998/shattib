using AutoMapper;
using Template.Application.Criterias.Commands.CreateCriteriaCommand;
using Template.Domain.Entities.Criteria;

namespace Template.Application.Criterias.Dtos;

public class CriteriaProfile : Profile
{
    public CriteriaProfile()
    {
        CreateMap<CreateCriteriaCommand, Criteria>()
            .ForMember(dest => dest.CriteriaItems, opt => opt.MapFrom(src => src.CriteriaItems));
        CreateMap<CriteriaItemDto, CriteriaItem>().ForMember(dest => dest.Image, opt => opt.MapFrom<ImageResolver>());
    }
}