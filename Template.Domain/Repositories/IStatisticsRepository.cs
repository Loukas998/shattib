using Template.Domain.Entities.Orders;

namespace Template.Domain.Repositories
{
	public interface IStatisticsRepository
	{
		public Task<int> GetNumberOfProductsAsync();
		public Task<int> GetNumberOfOrdersAsync();
		public Task<int> GetNumberOfClientsAsync();
		public Task<int> GetNumberOfBusinessesAsync();
		public Task<List<MiniProfitsDto>> GetProfitsByDate(int? year, int? month, int? day);
	}
}
