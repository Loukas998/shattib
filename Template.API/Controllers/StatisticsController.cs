using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using Template.Application.Statistics;
using Template.Application.Statistics.Queries.GetNumberOfBusinesses;
using Template.Application.Statistics.Queries.GetNumberOfClients;
using Template.Application.Statistics.Queries.GetNumberOfOrders;
using Template.Application.Statistics.Queries.GetNumberOfProducts;

namespace Template.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
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


	}
}
