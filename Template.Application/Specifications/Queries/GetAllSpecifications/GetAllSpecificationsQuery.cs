using MediatR;
using Template.Domain.Entities.Products;

namespace Template.Application.Specifications.Queries.GetAllSpecifications
{
	public class GetAllSpecificationsQuery : IRequest<IEnumerable<Specification>>
	{
	}
}
