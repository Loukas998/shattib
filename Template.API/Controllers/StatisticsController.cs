using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Statistics.Dtos;
using Template.Application.Statistics.Queries.GetNumberOfBusinesses;
using Template.Application.Statistics.Queries.GetNumberOfClients;
using Template.Application.Statistics.Queries.GetNumberOfOrders;
using Template.Application.Statistics.Queries.GetNumberOfProducts;
using Template.Application.Statistics.Queries.GetProfitsByDate;
using Template.Domain.Constants;

namespace Template.API.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize(Roles = UserRoles.Administrator)]
public class StatisticsController(IMediator mediator) : ControllerBase
{
    [HttpGet("GetClients")]
    public async Task<ActionResult<StatisticsDto>> GetNumberOfClients()
    {
        var numberOfClients = await mediator.Send(new GetNumberOfClientsQuery());
        return Ok(numberOfClients);
    }

    [HttpGet("GetBusinesses")]
    public async Task<ActionResult<StatisticsDto>> GetNumberOfBusinesses()
    {
        var numberOfBusiness = await mediator.Send(new GetNumberOfBusinessesQuery());
        return Ok(numberOfBusiness);
    }

    [HttpGet("GetOrders")]
    public async Task<ActionResult<StatisticsDto>> GetNumberOfOrders()
    {
        var numberOfOrders = await mediator.Send(new GetNumberOfOrdersQuery());
        return Ok(numberOfOrders);
    }


    [HttpGet("GetProducts")]
    public async Task<ActionResult<StatisticsDto>> GetNumberOfProducts()
    {
        var numberOfProducts = await mediator.Send(new GetNumberOfProductsQuery());
        return Ok(numberOfProducts);
    }

    [HttpGet("GetProfits")]
    public async Task<ActionResult<ProfitsDto>> GetProfitsByDate([FromQuery] GetProfitsByDateQuery query)
    {
        var profits = await mediator.Send(query);
        return Ok(profits);
    }
}