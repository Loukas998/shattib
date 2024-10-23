using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Domain.Entities.Products;
using Template.Domain.Repositories;

namespace Template.Application.Specifications.Commands.CreateCommand
{
    public class CreateSpecificationCommandHandler(ILogger<CreateSpecificationCommandHandler> logger,
        ISpecificationRepository attributeRepository, IMapper mapper) : IRequestHandler<CreateSpecificationCommand>
    {
        public Task Handle(CreateSpecificationCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating attribute with name: {Name}", request.Name);
            var specification = mapper.Map<Specification>(request);
            attributeRepository.AddAttribute(specification);

            return Task.CompletedTask;
        }
    }
}
