using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Domain.Constants;
using Template.Domain.Repositories;

namespace Template.API.Controllers
{
	[ApiController]
	[Route("api/SeededValues")]
	//[Authorize(Roles = $"{UserRoles.Administrator}, {UserRoles.Business}, {UserRoles.Client}")]
	public class SeededValuesController(ISeededValuesRepository seededValuesRepository) : ControllerBase
	{
		[HttpGet("SubCategories")]
		public async Task<IActionResult> GetSubCategories()
		{
			var subCategories = await seededValuesRepository.GetAllSubCategories();
			return Ok(subCategories);
		}

		[HttpGet("Categories/{categoryId}/SubCategories")]
		public async Task<IActionResult> GetSubCategories([FromRoute]int categoryId)
		{
			var subCategories = await seededValuesRepository.GetSubCategoriesByCategoryId(categoryId);
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
