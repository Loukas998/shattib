using MediatR;
using Microsoft.AspNetCore.Http;

namespace Template.Application.Products.Commands.CreateProductCommand.AppendColor
{
	public class AppendColorCommand : IRequest
	{
		public string HexCode { get; set; } = default!;
		public float Price { get; set; }
		public IFormFile ImagePath { get; set; } = default!;
	}
}
