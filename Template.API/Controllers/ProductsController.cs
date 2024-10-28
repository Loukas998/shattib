using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Products.Commands.CreateProductCommand;
using Template.Application.Products.Commands.DeleteImagesCommand.DeleteImagesCommand;
using Template.Application.Products.Commands.DeleteProductCommand;
using Template.Application.Products.Commands.UpdateProductCommand;
using Template.Application.Products.Commands.UpdateProductCommand.UpdateImagesCommand;
using Template.Application.Products.Dtos;
using Template.Application.Products.Queries.GetAllProducts;
using Template.Application.Products.Queries.GetProduct;

namespace Template.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromForm] CreateProductCommand command)
    {
        var id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetProductById), new { productId = id }, null);
    }

    [HttpGet("{productId:int}")]
    public async Task<ActionResult<ProductDto>> GetProductById([FromRoute] int productId)
    {
        var product = await mediator.Send(new GetProductByIdQuery(productId));
        return Ok(product);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
    {
        var products = await mediator.Send(new GetAllProductQuery());
        return Ok(products);
    }

    [HttpPatch]
    [Route("{productId:int}")]
    public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductCommand command, [FromRoute] int productId)
    {
        command.ProductId = productId;
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPatch]
    [Route("{productId}/images/{imageId}")]
    public async Task<IActionResult> UpdateProductImage([FromForm] UpdateProductImageCommand command,
        [FromRoute] int imageId,
        [FromRoute] int productId)
    {
        command.OldImageId = imageId;
        command.ProductId = productId;
        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{productId:int}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] int productId)
    {
        await mediator.Send(new DeleteProductCommand(productId));
        return NoContent();
    }

    [HttpDelete("Images/{imageId:int}")]
    public async Task<IActionResult> DeleteProductImage([FromRoute] int imageId)
    {
        await mediator.Send(new DeleteProductImageCommand(imageId));
        return NoContent();
    }
}