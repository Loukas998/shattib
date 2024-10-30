using MediatR;
using Microsoft.Extensions.Logging;
using Template.Domain.Entities.AuthClasses;
using Template.Domain.Repositories;

namespace Template.Application.Users.Commands.RefreshToken
{
	public class RefreshTokenCommandHandler(ILogger<RefreshTokenCommandHandler> logger, 
		IUserContext userContext, IAccountRepository accountRepository) : IRequestHandler<RefreshTokenCommand, AuthResponseDto?>
	{
		public async Task<AuthResponseDto?> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
		{
			var user = userContext.GetCurrentUser()!.Id.ToString();

			var refreshTokenRequest = new RefreshTokenRequest
			{
				UserId = user,
				RefreshToken = request.RefreshToken
			};
			var authResponseDto = await accountRepository.VerifyRefreshToken(refreshTokenRequest);
			return authResponseDto;
		}
	}
}
