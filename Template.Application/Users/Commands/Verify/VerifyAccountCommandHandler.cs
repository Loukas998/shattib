using MediatR;
using Microsoft.Extensions.Logging;
using Template.Domain.Repositories;

namespace Template.Application.Users.Commands.Verify
{
	public class VerifyAccountCommandHandler(ILogger<VerifyAccountCommandHandler> logger,
		IAccountRepository accountRepository) : IRequestHandler<VerifyAccountCommand, bool>
	{
		public async Task<bool> Handle(VerifyAccountCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Account verification");
			bool result = await accountRepository.VerifyAccountAsync(request.OTPCode);
			return result;
		}
	}
}
