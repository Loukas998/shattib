using MediatR;
using Microsoft.Extensions.Logging;
using Template.Domain.Entities.AuthClasses;
using Template.Domain.Repositories;

namespace Template.Application.Users.Commands.Login
{
    public class LoginUserCommandHandler(ILogger<LoginUserCommandHandler> logger,
		IAccountRepository accountRepository)
		: IRequestHandler<LoginUserCommand, AuthResponseDto?>
	{
		public async Task<AuthResponseDto?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Logging a user in");
			var authResponse = await accountRepository.Login(request.Email, request.Password);
			if (authResponse == null)
			{
				return null;
			}
			return authResponse;
		}
	}
}
