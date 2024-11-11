using MediatR;

namespace Template.Application.ContactUs.Queries.GetAll
{
	public class GetAllQuery : IRequest<IEnumerable<Domain.Entities.ContactUs>>
	{
	}
}
