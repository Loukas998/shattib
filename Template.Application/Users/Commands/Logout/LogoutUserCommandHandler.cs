using MediatR;
using Microsoft.Extensions.Logging;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Users.Commands.Logout
{
	public class LogoutUserCommandHandler(ILogger<LogoutUserCommandHandler> logger, 
		IAccountRepository accountRepository, IUserContext userContext) : IRequestHandler<LogoutUserCommand>
	{
		public async Task Handle(LogoutUserCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Logging a user out");
			var currentUser = userContext.GetCurrentUser();
			if (currentUser == null) throw new UnauthorizedException("You are unauthorized.. login again (no userId)");
			var userId = currentUser.Id;
			var user = await accountRepository.GetUserById(userId);
			await accountRepository.TokenDelete(user);
		}
	}
}
