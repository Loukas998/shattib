using Microsoft.AspNetCore.Mvc;
using Template.Domain.Repositories;

namespace Template.API.Controllers
{
	[ApiController]
	[Route("api/SeededValues")]
	public class SeededValuesController(ISeededValuesRepository seededValuesRepository) : ControllerBase
	{
		[HttpGet("SubCategories")]
		public async Task<IActionResult> GetSubCategories()
		{
			var subCategories = await seededValuesRepository.GetAllSubCategories();
			return Ok(subCategories);
		}

		[HttpGet("Categories")]
		public async Task<IActionResult> GetCategories()
		{
			var categories = await seededValuesRepository.GetAllCategories();
			return Ok(categories);
		}

		[HttpGet("OrderStatuses")]
		public IActionResult GetOrderStatuses()
		{
			var orderStatuses = seededValuesRepository.GetOrderStatuses();
			return Ok(orderStatuses);
		}

		[HttpGet("OrderKinds")]
		public IActionResult GetOrderKinds()
		{
			var orderKinds = seededValuesRepository.GetOrderKinds();
			return Ok(orderKinds);
		}

		[HttpGet("ConsultationStatuses")]
		public IActionResult GetConsultationStatuses()
		{
			var orderKinds = seededValuesRepository.GetConsultationStatuses();
			return Ok(orderKinds);
		}
	}
}
