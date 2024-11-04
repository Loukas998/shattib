using AutoMapper;
using Template.Application.Criterias.Dtos;
using Template.Domain.Entities.Criteria;
using Template.Domain.Repositories;

namespace Template.Application.Criterias.Mapping.Resolvers;

public class ImageResolver(IFileService fileService) : IValueResolver<CreateCriteriaItemDto, CriteriaItem, string>
{
    public string Resolve(CreateCriteriaItemDto source, CriteriaItem destination, string destMember,
        ResolutionContext context)
    {
        return fileService.SaveFile(source.Image, "Images/Criteria/CriteriaItems", [".jpg", ".png"]);
    }
}