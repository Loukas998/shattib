using MediatR;
using Microsoft.Extensions.Logging;
using Template.Domain.Entities.Products;
using Template.Domain.Repositories;

namespace Template.Application.Specifications.Queries.GetAllSpecifications
{
	public class GetAllSpecificationsQueryHandler(ISpecificationRepository specificationRepository,
		ILogger<GetAllSpecificationsQueryHandler> logger)
		: IRequestHandler<GetAllSpecificationsQuery, IEnumerable<Specification>>
	{
		public async Task<IEnumerable<Specification>> Handle(GetAllSpecificationsQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Getting all the specifications");
			return await specificationRepository.GetAllAttributes();
		}
	}
}
