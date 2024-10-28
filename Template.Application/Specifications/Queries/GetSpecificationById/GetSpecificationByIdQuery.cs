using MediatR;
using Template.Domain.Entities.Products;

namespace Template.Application.Specifications.Queries.GetSpecificationById
{
	public class GetSpecificationByIdQuery(int specificationId) : IRequest<Specification>
	{
		public int SpecificationId { get; } = specificationId;
	}
}
