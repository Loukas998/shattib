﻿namespace Template.Application.Users.Dtos
{
    public class UserDto
    {
		public string Id { get; set; } = default!;
		public string UserName { get; set; } = default!;
		public string Email { get; set; } = default!;
		public string Password { get; set; } = default!;
		public string Role { get; set; } = default!;
	}
}
