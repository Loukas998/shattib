using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Domain.Entities.Products;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Specifications.Commands.UpdateCommand
{
	public class UpdateSpecificationCommandHandler(ILogger<UpdateSpecificationCommandHandler> logger,
		ISpecificationRepository specificationRepository) : IRequestHandler<UpdateSpecificationCommand>
	{
		public async Task Handle(UpdateSpecificationCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Updating specification with id: {SpecificationId}", request.SpecificationId);
			var specification = await specificationRepository.GetAttributeById(request.SpecificationId);
			if(specification == null)
			{
				throw new NotFoundException(nameof(Specification), request.SpecificationId.ToString());
			}
			await specificationRepository.UpdateAttribute(request.SpecificationId, request.Name);
		}
	}
}
