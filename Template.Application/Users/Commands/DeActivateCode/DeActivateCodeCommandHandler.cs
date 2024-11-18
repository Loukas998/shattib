using MediatR;
using Microsoft.Extensions.Logging;
using Template.Domain.Repositories;
namespace Template.Application.Users.Commands.DeActivateCode
{
	public class DeActivateCodeCommandHandler(ILogger<DeActivateCodeCommandHandler> logger,
		IAccountRepository accountRepository) : IRequestHandler<DeActivateCodeCommand>
	{
		public async Task Handle(DeActivateCodeCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Deactivating code: {Code}", request.Code);
			await accountRepository.DeActivateOTPAsync(request.Code);
		}
	}
}
