using MediatR;
using Template.Domain.Entities;

namespace Template.Application.ContactUs.Queries.GetById
{
	public class GetByIdQuery(int id) : IRequest<Domain.Entities.ContactUs>
	{
		public int Id { get; set; } = id;
	}
}
