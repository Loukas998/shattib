using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Template.Domain.Entities;
using Template.Domain.Repositories;

namespace Template.Application.Users.Commands.Register
{
	public class RegisterUserCommandHandler(ILogger<RegisterUserCommandHandler> logger, IMapper mapper,
		IAccountRepository accountRepository) : IRequestHandler<RegisterUserCommand, IEnumerable<IdentityError>>
	{
		public async Task<IEnumerable<IdentityError>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Registering User");
			var user = mapper.Map<User>(request);
			return await accountRepository.Register(user, request.Password, request.Role);
		}
	}
}
