using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Template.Domain.Entities.Criterias;
using Template.Domain.Repositories;

namespace Template.Application.Criterias.Commands.CreateCriteriaCommand;

public class CreateCriteriaCommandHandler(
    ILogger<CreateCriteriaCommand> logger,
    IMapper mapper,
    ICriteriaRepository criteriaRepository, IFileService fileService) : IRequestHandler<CreateCriteriaCommand, int>
{
    public async Task<int> Handle(CreateCriteriaCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new Criteria {@Criteria}", request);
        var criteria = mapper.Map<Criteria>(request);
        return await criteriaRepository.CreateCriteriaAsync(criteria);
		
    }
}

//var criteriaItems = new List<CriteriaItem>();

//for (int i = 0; i < request.CriteriaItems.Count; i++)
//{
//	var itemDto = request.CriteriaItems[i];
//	var criteriaItem = mapper.Map<CriteriaItem>(itemDto); 

//	if (i < request.Images.Count)
//	{
//		var image = request.Images[i]; 
//		var imagePath = fileService.SaveFile(image, "Images", [".jpg", ".jpeg", ".png" ]); 
//		criteriaItem.Image = imagePath; 
//	}

//	criteriaItems.Add(criteriaItem);
//}

//criteria.CriteriaItems = criteriaItems;

//return await criteriaRepository.CreateCriteriaAsync(criteria, cancellationToken);