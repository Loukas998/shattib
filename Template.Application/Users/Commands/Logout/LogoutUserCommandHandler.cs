using MediatR;
using Microsoft.Extensions.Logging;
using Template.Domain.Repositories;

namespace Template.Application.Users.Commands.Logout
{
	public class LogoutUserCommandHandler(ILogger<LogoutUserCommandHandler> logger, 
		IAccountRepository accountRepository, IUserContext userContext) : IRequestHandler<LogoutUserCommand>
	{
		public async Task Handle(LogoutUserCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Logging a user out");
			string userId = userContext.GetCurrentUser()!.Id;
			var user = await accountRepository.GetUserById(userId);
			await accountRepository.TokenDelete(user);
		}
	}
}
