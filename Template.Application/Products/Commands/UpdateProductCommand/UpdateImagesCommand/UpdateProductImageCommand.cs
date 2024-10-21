using MediatR;
using Microsoft.AspNetCore.Http;

namespace Template.Application.Products.Commands.UpdateProductCommand.UpdateImagesCommand
{
	public class UpdateProductImageCommand : IRequest
	{
		public int ProductId { get; set; }
		public int OldImageId { get; set; }
		public IFormFile? NewImage { get; set; } = default;
	}
}
