using MediatR;
using Microsoft.Extensions.Logging;
using Template.Domain.Entities.AuthClasses;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Users.Commands.RefreshToken
{
	public class RefreshTokenCommandHandler(ILogger<RefreshTokenCommandHandler> logger, 
		IUserContext userContext, IAccountRepository accountRepository) : IRequestHandler<RefreshTokenCommand, AuthResponseDto?>
	{
		public async Task<AuthResponseDto?> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
		{
			var currentUser = userContext.GetCurrentUser();
			if (currentUser == null) throw new UnauthorizedException("You are unauthorized.. login again (no userId)");
			var user = currentUser.Id;

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
