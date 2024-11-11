using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Domain.Entities;
using Template.Domain.Repositories;

namespace Template.Application.ContactUs.Command.SendContactUs
{
	public class SendContactUsCommandHandler(ILogger<SendContactUsCommandHandler> logger,
		IContactUsRepository contactUsRepository, IMapper mapper) : IRequestHandler<SendContactUsCommand, int>
	{
		public async Task<int> Handle(SendContactUsCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Sending contact us form: {@ContactUs}", request);
			var contactForm = mapper.Map<Domain.Entities.ContactUs>(request);
			int id = await contactUsRepository.SendAsync(contactForm);
			return id;
		}
	}
}
