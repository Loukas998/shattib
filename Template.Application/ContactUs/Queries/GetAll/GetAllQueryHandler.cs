using MediatR;
using Microsoft.Extensions.Logging;
using Template.Domain.Repositories;

namespace Template.Application.ContactUs.Queries.GetAll
{
	public class GetAllQueryHandler(ILogger<GetAllQueryHandler> logger, 
		IContactUsRepository contactUsRepository) : IRequestHandler<GetAllQuery, IEnumerable<Domain.Entities.ContactUs>>
	{
		public async Task<IEnumerable<Domain.Entities.ContactUs>> Handle(GetAllQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Getting all forms");
			var contactForms = await contactUsRepository.GetAllAsync();
			return contactForms;
		}
	}
}
