using MediatR;
using Microsoft.Extensions.Logging;
using Template.Domain.Entities;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.ContactUs.Queries.GetById
{
	internal class GetByIdQueryHandler(ILogger<GetByIdQueryHandler> logger, IContactUsRepository contactUsRepository)
		: IRequestHandler<GetByIdQuery, Domain.Entities.ContactUs>
	{
		public async Task<Domain.Entities.ContactUs> Handle(GetByIdQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Getting contact us from with id: {Id}", request.Id);
			var contactForm = await contactUsRepository.GetByAsync(request.Id);
			if (contactForm == null)
			{
				throw new NotFoundException(nameof(Domain.Entities.ContactUs), request.Id.ToString());
			}
			return contactForm;
		}
	}
}
