using MediatR;
using Microsoft.AspNetCore.Http;
using Template.Application.Products.Dtos;
namespace Template.Application.Products.Commands.CreateProductCommand.AppendImage
{
	public class AppendImageCommand : IRequest<ImageDto>
	{
		public int ProductId { get; set; }
		public IFormFile NewImage { get; set; } = default!;
	}
}
