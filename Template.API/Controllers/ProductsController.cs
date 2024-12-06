using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Products.Commands.CreateProductCommand;
using Template.Application.Products.Commands.CreateProductCommand.AppendImage;
using Template.Application.Products.Commands.DeleteImagesCommand.DeleteImagesCommand;
using Template.Application.Products.Commands.DeleteProductCommand;
using Template.Application.Products.Commands.UpdateProductCommand;
using Template.Application.Products.Dtos;
using Template.Application.Products.Queries.GetCatSubCatProducts;
using Template.Application.Products.Queries.GetProduct;
using Template.Application.Products.Queries.GetProductsForHomePage;
using Template.Domain.Constants;


namespace Template.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = $"{UserRoles.Business},{UserRoles.Client},{UserRoles.Administrator}")]
public class ProductsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
	[Authorize(Roles = UserRoles.Administrator)]
	public async Task<IActionResult> CreateProduct([FromForm] CreateProductCommand command)
    {
        var id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetProductById), new { productId = id }, null);
    }

    [HttpGet("{productId:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<ProductDto>> GetProductById([FromRoute] int productId)
    {
        var product = await mediator.Send(new GetProductByIdQuery(productId));
        return Ok(product);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<MiniProductDto>>> GetProductsForHomePage(
        [FromQuery] GetProductsForHomePageQuery query)
    {
        var products = await mediator.Send(query);
        return Ok(products);
    }

    [HttpPatch]
    [Route("{productId:int}")]
	[Authorize(Roles = UserRoles.Administrator)]
	public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductCommand command, [FromRoute] int productId)
    {
        command.ProductId = productId;
        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{productId:int}")]
	[Authorize(Roles = UserRoles.Administrator)]
	public async Task<IActionResult> DeleteProduct([FromRoute] int productId)
    {
        await mediator.Send(new DeleteProductCommand(productId));
        return NoContent();
    }

    [HttpGet("CatsSubCatsProducts")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<CatSubCatProductsDto>>> GetCatSubCatProducts([FromQuery]int categoryId)
    {
        var result = await mediator.Send(new GetCatSubCatProductsQuery(categoryId));
        return Ok(result);
    }

    [HttpDelete]
	[Route("{productId}/Images/{imageId}")]
	[Authorize(Roles = UserRoles.Administrator)]
	public async Task<IActionResult> DeleteProductImage([FromRoute] int imageId, [FromRoute] int productId)
    {
        await mediator.Send(new DeleteProductImageCommand(imageId, productId));
        return NoContent();
    }

    [HttpPatch]
    [Route("{productId}/Images")]
    [Authorize(Roles = UserRoles.Administrator)]
    public async Task<ActionResult<ImageDto>> AppendProductImage([FromForm] AppendImageCommand command,
        [FromRoute] int productId)
    {
        command.ProductId = productId;
        var imageDto = await mediator.Send(command);
        return Ok(imageDto);
    }

    //[HttpGet]
    //public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
    //{
    //    var products = await mediator.Send(new GetAllProductQuery());
    //    return Ok(products);
    //}
}
