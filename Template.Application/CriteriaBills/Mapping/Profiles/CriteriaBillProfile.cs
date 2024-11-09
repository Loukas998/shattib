using AutoMapper;
using Template.Application.CriteriaBills.Commands.CreateCriteriaBillCommand;
using Template.Application.CriteriaBills.Dtos;
using Template.Application.CriteriaBills.Mapping.Resolvers;
using Template.Domain.Entities.Criterias;

namespace Template.Application.CriteriaBills.Mapping.Profiles;

public class CriteriaBillProfile : Profile
{
    public CriteriaBillProfile()
    {
        // TODO:: Create Image Resolver
        CreateMap<CreateCriteriaBillCommand, CriteriaBill>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom<BillImageResolver>());
        CreateMap<CriteriaBill, CriteriaBillDto>();
    }
}