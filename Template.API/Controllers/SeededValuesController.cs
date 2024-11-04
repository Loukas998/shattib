using Microsoft.AspNetCore.Mvc;
using Template.Domain.Repositories;

namespace Template.API.Controllers
{
	[ApiController]
	[Route("api/SeededValues")]
	public class SeededValuesController(ISeededValuesRepository seededValuesRepository) : ControllerBase
	{
		[HttpGet("subcategories")]
		public async Task<IActionResult> GetSubCategories()
		{
			var subCategories = await seededValuesRepository.GetAllSubCategories();
			return Ok(subCategories);
		}

		[HttpGet("categories")]
		public async Task<IActionResult> GetCategories()
		{
			var categories = await seededValuesRepository.GetAllCategories();
			return Ok(categories);
		}

		[HttpGet("orderstatuses")]
		public IActionResult GetOrderStatuses()
		{
			var orderStatuses = seededValuesRepository.GetOrderStatuses();
			return Ok(orderStatuses);
		}

		[HttpGet("orderkinds")]
		public IActionResult GetOrderKinds()
		{
			var orderKinds = seededValuesRepository.GetOrderKinds();
			return Ok(orderKinds);
		}
	}
}
