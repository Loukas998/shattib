using AutoMapper;
using Template.Application.CriteriaBills.Commands.CreateCriteriaBillCommand;
using Template.Domain.Entities.Criterias;
using Template.Domain.Repositories;

namespace Template.Application.CriteriaBills.Mapping.Resolvers;

public class BillImageResolver(IFileService fileService)
    : IValueResolver<CreateCriteriaBillCommand, CriteriaBill, string>
{
    public string Resolve(CreateCriteriaBillCommand source, CriteriaBill destination, string destMember,
        ResolutionContext context)
    {
        return fileService.SaveFile(source.Image, "Images/Criteria/Bills", [".jpg", ".png"]);
    }
}