using AutoMapper;
using Template.Domain.Entities.Criteria;
using Template.Domain.Repositories;

namespace Template.Application.Criterias.Dtos;

public class ImageResolver(IFileService fileService) : IValueResolver<CriteriaItemDto, CriteriaItem, string>
{
    public string Resolve(CriteriaItemDto source, CriteriaItem destination, string destMember,
        ResolutionContext context)
    {
        return fileService.SaveFile(source.Image, "Images/Criteria/CriteriaItems", [".jpg", ".png"]);
    }
}