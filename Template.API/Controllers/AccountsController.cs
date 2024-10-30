using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Users;
using Template.Application.Users.Commands.Login;
using Template.Application.Users.Commands.RefreshToken;
using Template.Application.Users.Commands.Register;
using Template.Domain.Entities.AuthClasses;

namespace Template.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AccountsController(IMediator mediator, IUserContext userContext) : ControllerBase
	{
		[HttpGet]
		public ActionResult<CurrentUser> GetCurrentUser()
		{
			var user = userContext.GetCurrentUser();
			return Ok(user);
		}

		[HttpPost]
		[Route("Register")]
		public async Task<ActionResult<IEnumerable<IdentityError>>> Register([FromBody] RegisterUserCommand command)
		{
			var result = await mediator.Send(command);
			if (result.Any())
			{
				return BadRequest(result);
			}
			return Ok(result);
		}

		[HttpPost]
		[Route("Login")]
		public async Task<ActionResult<AuthResponseDto>> Login(LoginUserCommand request)
		{
			var result = await mediator.Send(request);
			if (result == null)
			{
				return NotFound("User not found");
			}
			return Ok(result);
		}

		[HttpPost]
		[Route("RefreshToken")]
		public async Task<ActionResult<AuthResponseDto>> RefreshToken([FromBody]RefreshTokenCommand request)
		{
			var response = await mediator.Send(request);
			if (response == null)
			{
				return Unauthorized();
			}
			return Ok(response);
		}
	}
}
