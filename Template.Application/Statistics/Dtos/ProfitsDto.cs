using Template.Domain.Entities.Orders;

namespace Template.Application.Statistics.Dtos
{
	public class ProfitsDto
	{
		public List<MiniProfitsDto>? MiniProfitsDtos = [];
		public float Total;
	}
}
