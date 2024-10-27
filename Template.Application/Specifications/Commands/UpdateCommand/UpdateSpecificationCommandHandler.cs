using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Domain.Repositories;

namespace Template.Application.Specifications.Commands.UpdateCommand
{
	public class UpdateSpecificationCommandHandler(ILogger<UpdateSpecificationCommandHandler> logger,
		ISpecificationRepository specificationRepository) : IRequestHandler<UpdateSpecificationCommand>
	{
		public async Task Handle(UpdateSpecificationCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Updating specification with id: {SpecificationId}", request.SpecificationId);
			await specificationRepository.UpdateAttribute(request.SpecificationId, request.Name);
		}
	}
}
