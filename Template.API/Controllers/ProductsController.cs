using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Products.Commands.CreateProductCommand;
using Template.Application.Products.Commands.UpdateProductCommand;

namespace Template.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IMediator mediator) : ControllerBase
    {
		[HttpPost]
        public Task<ActionResult> CreateProduct(CreateProductCommand command)
        {
			throw new NotImplementedException();
		}

		[HttpGet]
        [Route("{productId}")]
        public Task<ActionResult> GetProductById(int productId)
        {
			throw new NotImplementedException();
		}

		[HttpGet]
        public Task<ActionResult> GetAllProducts()
        {
			throw new NotImplementedException();
		}

		[HttpPatch]
		[Route("{productId}")]
		public Task<ActionResult> UpdateProduct(UpdateProductCommand command, int productId)
        {
			throw new NotImplementedException();
		}

		[HttpDelete]
		[Route("{productId}")]
		public Task<ActionResult> DeleteProduct(int productId)
        {
			throw new NotImplementedException();
		}
    }
}
